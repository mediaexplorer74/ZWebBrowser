// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Common.SuspensionManager
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ZWebBrowser.Common
{
  internal sealed class SuspensionManager
  {
    private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();
    private static List<Type> _knownTypes = new List<Type>();
    private const string sessionStateFilename = "_sessionState.xml";
    private static DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof (string), typeof (SuspensionManager), (PropertyMetadata) null);
    private static DependencyProperty FrameSessionBaseKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionBaseKeyParams", typeof (string), typeof (SuspensionManager), (PropertyMetadata) null);
    private static DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached("_FrameSessionState", typeof (Dictionary<string, object>), typeof (SuspensionManager), (PropertyMetadata) null);
    private static List<WeakReference<Frame>> _registeredFrames = new List<WeakReference<Frame>>();

    public static Dictionary<string, object> SessionState => SuspensionManager._sessionState;

    public static List<Type> KnownTypes => SuspensionManager._knownTypes;

    public static async Task SaveAsync()
    {
      try
      {
        foreach (WeakReference<Frame> registeredFrame in SuspensionManager._registeredFrames)
        {
          Frame target;
          if (registeredFrame.TryGetTarget(out target))
            SuspensionManager.SaveFrameNavigationState(target);
        }
        MemoryStream sessionData = new MemoryStream();
        new DataContractSerializer(typeof (Dictionary<string, object>), (IEnumerable<Type>) SuspensionManager._knownTypes).WriteObject((Stream) sessionData, (object) SuspensionManager._sessionState);
        using (Stream fileStream = await ((IStorageFile) await ApplicationData.Current.LocalFolder.CreateFileAsync("_sessionState.xml", (CreationCollisionOption) 1)).OpenStreamForWriteAsync())
        {
          sessionData.Seek(0L, SeekOrigin.Begin);
          await sessionData.CopyToAsync(fileStream);
        }
        sessionData = (MemoryStream) null;
      }
      catch (Exception ex)
      {
        throw new SuspensionManagerException(ex);
      }
    }

    public static async Task RestoreAsync(string sessionBaseKey = null)
    {
      SuspensionManager._sessionState = new Dictionary<string, object>();
      try
      {
        using (IInputStream windowsRuntimeStream = await (await ApplicationData.Current.LocalFolder.GetFileAsync("_sessionState.xml")).OpenSequentialReadAsync())
          SuspensionManager._sessionState = (Dictionary<string, object>) new DataContractSerializer(typeof (Dictionary<string, object>), (IEnumerable<Type>) SuspensionManager._knownTypes).ReadObject(windowsRuntimeStream.AsStreamForRead());
        foreach (WeakReference<Frame> registeredFrame in SuspensionManager._registeredFrames)
        {
          Frame target;
          if (registeredFrame.TryGetTarget(out target) && (string) ((DependencyObject) target).GetValue(SuspensionManager.FrameSessionBaseKeyProperty) == sessionBaseKey)
          {
            ((DependencyObject) target).ClearValue(SuspensionManager.FrameSessionStateProperty);
            SuspensionManager.RestoreFrameNavigationState(target);
          }
        }
      }
      catch (Exception ex)
      {
        throw new SuspensionManagerException(ex);
      }
    }

    public static void RegisterFrame(Frame frame, string sessionStateKey, string sessionBaseKey = null)
    {
      if (((DependencyObject) frame).GetValue(SuspensionManager.FrameSessionStateKeyProperty) != null)
        throw new InvalidOperationException("Frames can only be registered to one session state key");
      if (((DependencyObject) frame).GetValue(SuspensionManager.FrameSessionStateProperty) != null)
        throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
      if (!string.IsNullOrEmpty(sessionBaseKey))
      {
        ((DependencyObject) frame).SetValue(SuspensionManager.FrameSessionBaseKeyProperty, (object) sessionBaseKey);
        sessionStateKey = sessionBaseKey + "_" + sessionStateKey;
      }
      ((DependencyObject) frame).SetValue(SuspensionManager.FrameSessionStateKeyProperty, (object) sessionStateKey);
      SuspensionManager._registeredFrames.Add(new WeakReference<Frame>(frame));
      SuspensionManager.RestoreFrameNavigationState(frame);
    }

    public static void UnregisterFrame(Frame frame)
    {
      SuspensionManager.SessionState.Remove((string) ((DependencyObject) frame).GetValue(SuspensionManager.FrameSessionStateKeyProperty));
      Frame target;
      SuspensionManager._registeredFrames.RemoveAll((Predicate<WeakReference<Frame>>) (weakFrameReference => !weakFrameReference.TryGetTarget(out target) || target == frame));
    }

    public static Dictionary<string, object> SessionStateForFrame(Frame frame)
    {
      Dictionary<string, object> dictionary = (Dictionary<string, object>) ((DependencyObject) frame).GetValue(SuspensionManager.FrameSessionStateProperty);
      if (dictionary == null)
      {
        string key = (string) ((DependencyObject) frame).GetValue(SuspensionManager.FrameSessionStateKeyProperty);
        if (key != null)
        {
          if (!SuspensionManager._sessionState.ContainsKey(key))
            SuspensionManager._sessionState[key] = (object) new Dictionary<string, object>();
          dictionary = (Dictionary<string, object>) SuspensionManager._sessionState[key];
        }
        else
          dictionary = new Dictionary<string, object>();
        ((DependencyObject) frame).SetValue(SuspensionManager.FrameSessionStateProperty, (object) dictionary);
      }
      return dictionary;
    }

    private static void RestoreFrameNavigationState(Frame frame)
    {
      Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(frame);
      if (!dictionary.ContainsKey("Navigation"))
        return;
      frame.SetNavigationState((string) dictionary["Navigation"]);
    }

    private static void SaveFrameNavigationState(Frame frame) => SuspensionManager.SessionStateForFrame(frame)["Navigation"] = (object) frame.GetNavigationState();
  }
}
