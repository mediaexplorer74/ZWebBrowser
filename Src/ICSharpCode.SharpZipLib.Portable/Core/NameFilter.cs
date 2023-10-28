﻿// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.NameFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSharpCode.SharpZipLib.Core
{
  public class NameFilter : IScanFilter
  {
    private string filter_;
    private ArrayList inclusions_;
    private ArrayList exclusions_;

    public NameFilter(string filter)
    {
      this.filter_ = filter;
      this.inclusions_ = new ArrayList();
      this.exclusions_ = new ArrayList();
      this.Compile();
    }

    public static bool IsValidExpression(string expression)
    {
      bool flag = true;
      try
      {
        Regex regex = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
      }
      catch (ArgumentException ex)
      {
        flag = false;
      }
      return flag;
    }

    public static bool IsValidFilterExpression(string toTest)
    {
      bool flag = true;
      try
      {
        if (toTest != null)
        {
          string[] strArray = NameFilter.SplitQuoted(toTest);
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (strArray[index] != null && strArray[index].Length > 0)
            {
              Regex regex = new Regex(strArray[index][0] != '+' ? (strArray[index][0] != '-' ? strArray[index] : strArray[index].Substring(1, strArray[index].Length - 1)) : strArray[index].Substring(1, strArray[index].Length - 1), RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
          }
        }
      }
      catch (ArgumentException ex)
      {
        flag = false;
      }
      return flag;
    }

    public static string[] SplitQuoted(string original)
    {
      char ch = '\\';
      char[] array = new char[1]{ ';' };
      ArrayList arrayList = new ArrayList();
      if (original != null && original.Length > 0)
      {
        int index = -1;
        StringBuilder stringBuilder = new StringBuilder();
        while (index < original.Length)
        {
          ++index;
          if (index >= original.Length)
            arrayList.Add((object) stringBuilder.ToString());
          else if ((int) original[index] == (int) ch)
          {
            ++index;
            if (index >= original.Length)
              throw new ArgumentException("Missing terminating escape character", nameof (original));
            if (Array.IndexOf<char>(array, original[index]) < 0)
              stringBuilder.Append(ch);
            stringBuilder.Append(original[index]);
          }
          else if (Array.IndexOf<char>(array, original[index]) >= 0)
          {
            arrayList.Add((object) stringBuilder.ToString());
            stringBuilder.Length = 0;
          }
          else
            stringBuilder.Append(original[index]);
        }
      }
      return (string[]) arrayList.ToArray(typeof (string));
    }

    public override string ToString() => this.filter_;

    public bool IsIncluded(string name)
    {
      bool flag = false;
      if (this.inclusions_.Count == 0)
      {
        flag = true;
      }
      else
      {
        foreach (Regex inclusion in (List<object>) this.inclusions_)
        {
          if (inclusion.IsMatch(name))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    public bool IsExcluded(string name)
    {
      bool flag = false;
      foreach (Regex exclusion in (List<object>) this.exclusions_)
      {
        if (exclusion.IsMatch(name))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public bool IsMatch(string name) => this.IsIncluded(name) && !this.IsExcluded(name);

    private void Compile()
    {
      if (this.filter_ == null)
        return;
      string[] strArray = NameFilter.SplitQuoted(this.filter_);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] != null && strArray[index].Length > 0)
        {
          bool flag = strArray[index][0] != '-';
          string pattern = strArray[index][0] != '+' ? (strArray[index][0] != '-' ? strArray[index] : strArray[index].Substring(1, strArray[index].Length - 1)) : strArray[index].Substring(1, strArray[index].Length - 1);
          if (flag)
            this.inclusions_.Add((object) new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline));
          else
            this.exclusions_.Add((object) new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline));
        }
      }
    }
  }
}
