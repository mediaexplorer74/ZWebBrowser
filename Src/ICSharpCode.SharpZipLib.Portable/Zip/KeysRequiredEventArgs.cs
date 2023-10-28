// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class KeysRequiredEventArgs : EventArgs
  {
    private string fileName;
    private byte[] key;

    public KeysRequiredEventArgs(string name) => this.fileName = name;

    public KeysRequiredEventArgs(string name, byte[] keyValue)
    {
      this.fileName = name;
      this.key = keyValue;
    }

    public string FileName => this.fileName;

    public byte[] Key
    {
      get => this.key;
      set => this.key = value;
    }
  }
}
