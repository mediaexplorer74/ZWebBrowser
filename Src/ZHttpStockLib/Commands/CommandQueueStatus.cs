// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Commands.CommandQueueStatus
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

namespace ZHttpStockLib.Commands
{
  public enum CommandQueueStatus
  {
    Available = 0,
    Scheduled = 1,
    Picking = 2,
    Loaded = 3,
    Processing = 4,
    Cancelling = 5,
    Completed = 10, // 0x0000000A
    Failure = 11, // 0x0000000B
    Cancelled = 12, // 0x0000000C
  }
}
