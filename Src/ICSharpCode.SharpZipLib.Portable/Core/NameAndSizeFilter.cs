// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.NameAndSizeFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.Core
{
  [Obsolete("Use ExtendedPathFilter instead")]
  public class NameAndSizeFilter : PathFilter
  {
    private long minSize_;
    private long maxSize_ = long.MaxValue;

    public NameAndSizeFilter(string filter, long minSize, long maxSize)
      : base(filter)
    {
      this.MinSize = minSize;
      this.MaxSize = maxSize;
    }

    public override bool IsMatch(string name)
    {
      bool flag = base.IsMatch(name);
      if (flag)
      {
        long length = VFS.Current.GetFileInfo(name).Length;
        flag = this.MinSize <= length && this.MaxSize >= length;
      }
      return flag;
    }

    public long MinSize
    {
      get => this.minSize_;
      set => this.minSize_ = value >= 0L && this.maxSize_ >= value ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }

    public long MaxSize
    {
      get => this.maxSize_;
      set => this.maxSize_ = value >= 0L && this.minSize_ <= value ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }
  }
}
