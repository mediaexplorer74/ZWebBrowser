// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Contents.ContentList
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System.Collections.Generic;

namespace ZHttpStockLib.Contents
{
  public class ContentList
  {
    private List<ContentFile> list = new List<ContentFile>();

    internal ContentList()
    {
    }

    internal void Add(ContentFile file) => this.list.Add(file);

    internal void AddRange(ContentFile[] files) => this.list.AddRange((IEnumerable<ContentFile>) files);

    internal void Clear() => this.list.Clear();

    public IReadOnlyList<ContentFile> AllItems() => (IReadOnlyList<ContentFile>) this.list;
  }
}
