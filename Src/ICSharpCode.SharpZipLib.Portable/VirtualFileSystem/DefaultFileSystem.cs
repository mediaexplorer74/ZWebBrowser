// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.DefaultFileSystem
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.Collections.Generic;

namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  public class DefaultFileSystem : IVirtualFileSystem
  {
    private const string InvalidOperationMessage = "The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().";

    public virtual IEnumerable<string> GetFiles(string directory) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual IEnumerable<string> GetDirectories(string directory) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual string GetTempFileName() => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual void CopyFile(string fromFileName, string toFileName, bool overwrite) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual void MoveFile(string fromFileName, string toFileName) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual void DeleteFile(string fileName) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual string GetFullPath(string path) => path;

    public virtual IFileInfo GetFileInfo(string filename) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual IDirectoryInfo GetDirectoryInfo(string directoryName) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual void SetLastWriteTime(string name, DateTime dateTime) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual void SetAttributes(string name, FileAttributes attributes) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual void CreateDirectory(string directory) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual VfsStream CreateFile(string filename) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual VfsStream OpenReadFile(string filename) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual VfsStream OpenWriteFile(string filename) => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual string CurrentDirectory => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");

    public virtual char DirectorySeparatorChar => throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
  }
}
