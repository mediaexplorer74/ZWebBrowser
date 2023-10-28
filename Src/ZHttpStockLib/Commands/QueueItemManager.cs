// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Commands.QueueItemManager
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;
using ZHttpStockLib.Commands.Exception;
using ZHttpStockLib.Contents;
using ZWebClient.Engine;

namespace ZHttpStockLib.Commands
{
  public class QueueItemManager
  {
    private CommandQueueItem _cmd;
    private CancellationTokenSource cts = new CancellationTokenSource();

    private QueueItemManager(CommandQueueItem cmd) => this._cmd = cmd;

    public CommandQueueItem Cmd => this._cmd;

    public static QueueItemManager LoadFor(CommandQueueItem cmd)
    {
      cmd.Status = CommandQueueStatus.Loaded;
      return new QueueItemManager(cmd);
    }

    public async Task Start()
    {
      try
      {
        this.Cmd.Status = CommandQueueStatus.Processing;
        this.Cmd.ProgressValue = 10.0;
        IProgress<HttpProgress> progressHandler = (IProgress<HttpProgress>) new Progress<HttpProgress>(new Action<HttpProgress>(this.ProgressHandler));
        IBuffer buf = await WebEngine.GetInstance().GetBufferAsync(new Uri(((Command) this.Cmd).RemoteUrl), this.cts.Token, progressHandler);
        this.cts.Token.ThrowIfCancellationRequested();
        if (CommandQueueStatus.Cancelling == this.Cmd.Status)
          throw new CancellationException();
        this.Cmd.ProgressValue = 90.0;
        ContentFile async = await ContentFile.CreateAsync(ContentFile.FileNameEscape(((Command) this.Cmd).SaveToFileName));
        this.Cmd.ProgressValue = 95.0;
        int num = (int) await ContentManager.GetInstance().SaveContentAsync(async, buf);
        this.Cmd.Status = CommandQueueStatus.Completed;
      }
      catch (CancellationException ex)
      {
        Debug.WriteLine(ex.Message);
        this.Cmd.Status = CommandQueueStatus.Cancelled;
      }
      catch (TaskCanceledException ex)
      {
        Debug.WriteLine(ex.Message);
        this.Cmd.Status = CommandQueueStatus.Cancelled;
      }
      catch (System.Exception ex)
      {
        Debug.WriteLine(ex.Message);
        this.Cmd.Status = CommandQueueStatus.Failure;
        throw;
      }
      finally
      {
        this.Cmd.ProgressValue = 0.0;
      }
    }

    public void Cancel()
    {
      if (CommandQueueStatus.Processing == this.Cmd.Status)
        this.Cmd.Status = CommandQueueStatus.Cancelling;
      else if (this.Cmd.Status != CommandQueueStatus.Available && CommandQueueStatus.Cancelled != this.Cmd.Status && CommandQueueStatus.Completed != this.Cmd.Status && CommandQueueStatus.Failure != this.Cmd.Status)
        this.Cmd.Status = CommandQueueStatus.Cancelled;
      if (!this.cts.Token.CanBeCanceled)
        return;
      this.cts.Cancel();
    }

    public void ReleaseCommand()
    {
      if (this._cmd == null)
        return;
      this._cmd = (CommandQueueItem) null;
    }

    private void ProgressHandler(HttpProgress progressInfo)
    {
      if (this.Cmd.ProgressValue < 10.0 || this.Cmd.ProgressValue > 90.0)
        return;
      ulong num1;
      if (progressInfo.TotalBytesToReceive.HasValue)
      {
        ulong? nullable1 = progressInfo.TotalBytesToReceive;
        if ((1UL > nullable1.GetValueOrDefault() ? (nullable1.HasValue ? 1 : 0) : 0) == 0)
        {
          ulong num2 = 80;
          ulong bytesReceived = progressInfo.BytesReceived;
          ulong? nullable2 = progressInfo.TotalBytesToReceive;
          nullable1 = nullable2.HasValue ? new ulong?(bytesReceived / nullable2.GetValueOrDefault()) : new ulong?();
          ulong? nullable3;
          if (!nullable1.HasValue)
          {
            nullable2 = new ulong?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new ulong?(num2 * nullable1.GetValueOrDefault());
          nullable2 = nullable3;
          num1 = nullable2.Value;
          goto label_8;
        }
      }
      num1 = progressInfo.BytesReceived / (progressInfo.BytesReceived + 1000UL) * 100UL;
label_8:
      this.Cmd.ProgressValue = (double) (int) (num1 + 10UL);
    }
  }
}
