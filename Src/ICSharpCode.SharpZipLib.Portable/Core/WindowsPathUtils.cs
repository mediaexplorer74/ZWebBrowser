// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.WindowsPathUtils
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Core
{
  public abstract class WindowsPathUtils
  {
    internal WindowsPathUtils()
    {
    }

    public static string DropPathRoot(string path)
    {
      string str = path;
      if (path != null && path.Length > 0)
      {
        if (path[0] == '\\' || path[0] == '/')
        {
          if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
          {
            int index = 2;
            int num = 2;
            while (index <= path.Length && (path[index] != '\\' && path[index] != '/' || --num > 0))
              ++index;
            int startIndex = index + 1;
            str = startIndex >= path.Length ? "" : path.Substring(startIndex);
          }
        }
        else if (path.Length > 1 && path[1] == ':')
        {
          int count = 2;
          if (path.Length > 2 && (path[2] == '\\' || path[2] == '/'))
            count = 3;
          str = str.Remove(0, count);
        }
      }
      return str;
    }
  }
}
