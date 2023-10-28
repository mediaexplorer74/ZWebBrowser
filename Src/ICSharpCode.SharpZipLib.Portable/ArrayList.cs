// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.ArrayList
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace ICSharpCode.SharpZipLib
{
  internal class ArrayList : List<object>
  {
    public ArrayList()
    {
    }

    public ArrayList(int capacity)
      : base(capacity)
    {
    }

    public int Add(object item)
    {
      base.Add(item);
      return this.Count - 1;
    }

    public void Sort(IComparer comparer) => this.Sort((IComparer<object>) new ArrayList.PComparer(comparer));

    public virtual Array ToArray(Type type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      object[] array = this.ToArray();
      Array instance = Array.CreateInstance(type, array.Length);
      Array.Copy((Array) array, 0, instance, 0, array.Length);
      return instance;
    }

    private class PComparer : IComparer<object>
    {
      private IComparer _Cmp;

      public PComparer(IComparer cmp) => this._Cmp = cmp;

      public int Compare(object x, object y) => this._Cmp.Compare(x, y);
    }
  }
}
