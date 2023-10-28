// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IEntryFactory
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
  public interface IEntryFactory
  {
    ZipEntry MakeFileEntry(string fileName);

    ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

    ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

    ZipEntry MakeDirectoryEntry(string directoryName);

    ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

    INameTransform NameTransform { get; set; }
  }
}
