// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.FileAttributes
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  [Flags]
  public enum FileAttributes
  {
    ReadOnly = 1,
    Hidden = 2,
    Directory = 16, // 0x00000010
    Archive = 32, // 0x00000020
    Normal = 128, // 0x00000080
  }
}
