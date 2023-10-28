// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Commands.CommandManager
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHttpStockLib.Commands
{
  public class CommandManager
  {
    private static string[] _cmdSeparator = new string[1]
    {
      Environment.NewLine
    };
    private Dictionary<int, CommandManager.RunningStatus> runningStatusDic = new Dictionary<int, CommandManager.RunningStatus>();
    private int _maxThread = 2;
    private CommandQueue _queue = new CommandQueue();
    private Dictionary<CommandQueueItem, QueueItemManager> _itemManager = new Dictionary<CommandQueueItem, QueueItemManager>();

    private CommandManager()
    {
    }

    public CommandQueue CommandList => this._queue;

    public int MaxThread
    {
      get => this._maxThread;
      set => this._maxThread = value;
    }

    public static CommandManager CreateInstance() => new CommandManager();

    public int ParseCommands(string commands)
    {
      string[] source = commands.Split(CommandManager._cmdSeparator, StringSplitOptions.RemoveEmptyEntries);
      this._queue.AddRange((IEnumerable<CommandQueueItem>) ((IEnumerable<string>) source).Select<string, Command>((Func<string, Command>) (v => new Command(v))));
      return ((IEnumerable<string>) source).Count<string>();
    }

    private void StartNext(int id)
    {
      CommandQueueItem n = this._queue.NextNone();
      if (n != null)
      {
        if (!this._itemManager.ContainsKey(n))
        {
          this.SetRunningStatus(id, CommandManager.RunningStatus.Running);
          QueueItemManager mana = QueueItemManager.LoadFor(n);
          this._itemManager.Add(n, mana);
          mana.Start().ContinueWith((Action<Task>) (t =>
          {
            this._itemManager.Remove(n);
            mana.ReleaseCommand();
            mana = (QueueItemManager) null;
            this.StartNext(id);
          }));
        }
        else
          this.SetRunningStatus(id, CommandManager.RunningStatus.Idle);
      }
      else
        this.SetRunningStatus(id, CommandManager.RunningStatus.Idle);
    }

    public void Start()
    {
      int num = this.RunningCount();
      if (num > this.MaxThread)
        return;
      for (int index = 0; index < this.MaxThread - num; ++index)
        this.StartNext(this.NextRunningId());
    }

    public void Stop()
    {
      foreach (QueueItemManager queueItemManager in this._itemManager.Values)
        queueItemManager.Cancel();
    }

    public void Schedule(IEnumerable<CommandQueueItem> items) => this._queue.Schedule(items);

    public void Schedule() => this._queue.Schedule();

    public void Reschedule(IEnumerable<CommandQueueItem> items) => this._queue.Reschedule(items);

    public void Reschedule() => this._queue.Reschedule();

    public int RunningCount() => this.runningStatusDic.Count<KeyValuePair<int, CommandManager.RunningStatus>>((Func<KeyValuePair<int, CommandManager.RunningStatus>, bool>) (v => v.Value == CommandManager.RunningStatus.Running));

    private void SetRunningStatus(int id, CommandManager.RunningStatus s)
    {
      if (this.runningStatusDic.ContainsKey(id))
      {
        this.runningStatusDic[id] = s;
      }
      else
      {
        lock (this)
          this.runningStatusDic.Add(id, s);
      }
    }

    private int NextRunningId()
    {
      lock (this)
        return this.runningStatusDic.Count < 1 ? 1 : this.runningStatusDic.Keys.Max() + 1;
    }

    private enum RunningStatus
    {
      Idle,
      Running,
    }
  }
}
