// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.IVirtualFileSystem
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.Collections.Generic;

namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  public interface IVirtualFileSystem
  {
    IEnumerable<string> GetFiles(string directory);

    IEnumerable<string> GetDirectories(string directory);

    string GetFullPath(string path);

    IDirectoryInfo GetDirectoryInfo(string directoryName);

    IFileInfo GetFileInfo(string filename);

    void SetLastWriteTime(string name, DateTime dateTime);

    void SetAttributes(string name, FileAttributes attributes);

    void CreateDirectory(string directory);

    string GetTempFileName();

    void CopyFile(string fromFileName, string toFileName, bool overwrite);

    void MoveFile(string fromFileName, string toFileName);

    void DeleteFile(string fileName);

    VfsStream CreateFile(string filename);

    VfsStream OpenReadFile(string filename);

    VfsStream OpenWriteFile(string filename);

    string CurrentDirectory { get; }

    char DirectorySeparatorChar { get; }
  }
}
