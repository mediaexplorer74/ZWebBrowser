// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Commands.CommandQueue
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ZHttpStockLib.Commands
{
  public class CommandQueue
  {
    private List<CommandQueueItem> _queue = new List<CommandQueueItem>();

    public IReadOnlyList<CommandQueueItem> Queue => (IReadOnlyList<CommandQueueItem>) this._queue;

    public void AddRange(IEnumerable<CommandQueueItem> items) => this._queue.AddRange(items);

    public void Schedule(IEnumerable<CommandQueueItem> items)
    {
      foreach (CommandQueueItem commandQueueItem in items)
      {
        if (commandQueueItem.Status == CommandQueueStatus.Available)
          commandQueueItem.Status = CommandQueueStatus.Scheduled;
      }
    }

    public void Schedule() => this.Schedule((IEnumerable<CommandQueueItem>) this._queue);

    public void Reschedule(IEnumerable<CommandQueueItem> items)
    {
      foreach (CommandQueueItem commandQueueItem in items)
      {
        if (CommandQueueStatus.Cancelled == commandQueueItem.Status || CommandQueueStatus.Failure == commandQueueItem.Status || commandQueueItem.Status == CommandQueueStatus.Available)
          commandQueueItem.Status = CommandQueueStatus.Scheduled;
      }
    }

    public void Reschedule() => this.Reschedule((IEnumerable<CommandQueueItem>) this._queue);

    public CommandQueueItem NextNone()
    {
      CommandQueueItem commandQueueItem = (CommandQueueItem) null;
      lock (this)
      {
        try
        {
          commandQueueItem = this._queue.First<CommandQueueItem>((Func<CommandQueueItem, bool>) (x => CommandQueueStatus.Scheduled == x.Status));
          if (commandQueueItem != null)
            commandQueueItem.Status = CommandQueueStatus.Picking;
        }
        catch (InvalidOperationException ex)
        {
            Debug.WriteLine(ex.Message);
        }
      }
      return commandQueueItem;
    }
  }
}
