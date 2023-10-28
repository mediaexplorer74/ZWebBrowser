// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Common.ObservableDictionary
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;

namespace ZWebBrowser.Common
{
  public class ObservableDictionary : 
    IObservableMap<string, object>,
    IDictionary<string, object>,
    ICollection<KeyValuePair<string, object>>,
    IEnumerable<KeyValuePair<string, object>>,
    IEnumerable
  {
    private Dictionary<string, object> _dictionary = new Dictionary<string, object>();

    public event MapChangedEventHandler<string, object> MapChanged
    {
      add => EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).AddEventHandler(value);
      remove => EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).RemoveEventHandler(value);
    }

    private void InvokeMapChanged(CollectionChange change, string key) => EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).InvocationList?.Invoke((IObservableMap<string, object>) this, (IMapChangedEventArgs<string>) new ObservableDictionary.ObservableDictionaryChangedEventArgs(change, key));

    public void Add(string key, object value)
    {
      this._dictionary.Add(key, value);
      this.InvokeMapChanged((CollectionChange) 1, key);
    }

    public void Add(KeyValuePair<string, object> item) => this.Add(item.Key, item.Value);

    public bool Remove(string key)
    {
      if (!this._dictionary.Remove(key))
        return false;
      this.InvokeMapChanged((CollectionChange) 2, key);
      return true;
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      object objB;
      if (!this._dictionary.TryGetValue(item.Key, out objB) || !object.Equals(item.Value, objB) || !this._dictionary.Remove(item.Key))
        return false;
      this.InvokeMapChanged((CollectionChange) 2, item.Key);
      return true;
    }

    public object this[string key]
    {
      get => this._dictionary[key];
      set
      {
        this._dictionary[key] = value;
        this.InvokeMapChanged((CollectionChange) 3, key);
      }
    }

    public void Clear()
    {
      string[] array = this._dictionary.Keys.ToArray<string>();
      this._dictionary.Clear();
      foreach (string key in array)
        this.InvokeMapChanged((CollectionChange) 2, key);
    }

    public ICollection<string> Keys => (ICollection<string>) this._dictionary.Keys;

    public bool ContainsKey(string key) => this._dictionary.ContainsKey(key);

    public bool TryGetValue(string key, out object value) => this._dictionary.TryGetValue(key, out value);

    public ICollection<object> Values => (ICollection<object>) this._dictionary.Values;

    public bool Contains(KeyValuePair<string, object> item) => this._dictionary.Contains<KeyValuePair<string, object>>(item);

    public int Count => this._dictionary.Count;

    public bool IsReadOnly => false;

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => (IEnumerator<KeyValuePair<string, object>>) this._dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._dictionary.GetEnumerator();

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      int length = array.Length;
      foreach (KeyValuePair<string, object> keyValuePair in this._dictionary)
      {
        if (arrayIndex >= length)
          break;
        array[arrayIndex++] = keyValuePair;
      }
    }

    private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<string>
    {
      public ObservableDictionaryChangedEventArgs(CollectionChange change, string key)
      {
        this.CollectionChange = change;
        this.Key = key;
      }

      public CollectionChange CollectionChange { get; private set; }

      public string Key { get; private set; }
    }
  }
}
