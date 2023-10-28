// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Commands.Command
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace ZHttpStockLib.Commands
{
  public class Command : CommandQueueItem
  {
    private static string[] _cmdUrlFileNameSep = new string[1]
    {
      " "
    };

    internal Command(string cmdString)
    {
      this.CommandRawString = cmdString;
      this.SeparateCmdFileName(cmdString);
      this.Name = this.RemoteUrl;
    }

    public string CommandRawString { get; set; }

    public string RemoteUrl { get; set; }

    public string SaveToFileName { get; set; }

    private void SeparateCmdFileName(string cmd)
    {
      string[] source = cmd.Split(Command._cmdUrlFileNameSep, StringSplitOptions.RemoveEmptyEntries);
      this.RemoteUrl = ((IEnumerable<string>) source).Count<string>() <= 0 ? "" : source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.SaveToFileName = source[1];
      else
        this.SaveToFileName = this.RemoteUrl;
    }
  }
}
