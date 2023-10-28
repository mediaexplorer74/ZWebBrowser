// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.VfsExtensions
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System.Collections.Generic;
using System.Linq;

namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  public static class VfsExtensions
  {
    public static bool DirectoryExists(this IVirtualFileSystem vfs, string directoryName)
    {
      try
      {
        return vfs.GetDirectoryInfo(directoryName).Exists;
      }
      catch
      {
        return false;
      }
    }

    public static bool FileExists(this IVirtualFileSystem vfs, string fileName)
    {
      try
      {
        return vfs.GetFileInfo(fileName).Exists;
      }
      catch
      {
        return false;
      }
    }

    public static IEnumerable<string> GetDirectoriesAndFiles(
      this IVirtualFileSystem vfs,
      string directoryName)
    {
      return vfs.GetDirectories(directoryName).Concat<string>(vfs.GetFiles(directoryName));
    }
  }
}
