// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VFS
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.VirtualFileSystem;

namespace ICSharpCode.SharpZipLib
{
  public static class VFS
  {
    private static IVirtualFileSystem _Current;

    public static void SetCurrent(IVirtualFileSystem vfs) => VFS._Current = vfs;

    public static IVirtualFileSystem Current => VFS._Current ?? (VFS._Current = (IVirtualFileSystem) new DefaultFileSystem());
  }
}
