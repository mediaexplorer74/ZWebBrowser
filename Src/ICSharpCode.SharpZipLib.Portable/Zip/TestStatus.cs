// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.TestStatus
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Zip
{
  public class TestStatus
  {
    private ZipFile file_;
    private ZipEntry entry_;
    private bool entryValid_;
    private int errorCount_;
    private long bytesTested_;
    private TestOperation operation_;

    public TestStatus(ZipFile file) => this.file_ = file;

    public TestOperation Operation => this.operation_;

    public ZipFile File => this.file_;

    public ZipEntry Entry => this.entry_;

    public int ErrorCount => this.errorCount_;

    public long BytesTested => this.bytesTested_;

    public bool EntryValid => this.entryValid_;

    internal void AddError()
    {
      ++this.errorCount_;
      this.entryValid_ = false;
    }

    internal void SetOperation(TestOperation operation) => this.operation_ = operation;

    internal void SetEntry(ZipEntry entry)
    {
      this.entry_ = entry;
      this.entryValid_ = true;
      this.bytesTested_ = 0L;
    }

    internal void SetBytesTested(long value) => this.bytesTested_ = value;
  }
}
