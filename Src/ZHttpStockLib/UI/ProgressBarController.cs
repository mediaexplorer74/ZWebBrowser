// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.UI.ProgressBarController
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Threading.Tasks;

namespace ZHttpStockLib.UI
{
  public class ProgressBarController
  {
    private int maxValueInPhase;
    private int maxValue;
    private int currentValue;
    private string _status;
    private readonly string running = "Running";
    private readonly string stopped = nameof (stopped);
    private readonly string stopping = nameof (stopping);

    public event ProgressBarController.ProgressChangedHandler ProgressChanged;

    public ProgressBarController(int maxValue)
    {
      this.Status = this.stopped;
      this.maxValue = maxValue;
    }

    private string Status
    {
      get => this._status;
      set => this._status = value;
    }

    public void NextValueInPhase(int maxValueInPhase)
    {
      this.maxValueInPhase = maxValueInPhase;
      if (!(this.stopped == this.Status))
        return;
      this.Start();
    }

    private void Start()
    {
      this.ResetValues();
      Task.Run((Func<Task>) (async () =>
      {
        this.Status = this.running;
        while (this.currentValue < this.maxValue && this.running == this.Status)
        {
          if (this.currentValue < this.maxValueInPhase - 2)
          {
            int num = (this.maxValueInPhase - this.currentValue) / 4;
            this.currentValue += num > 10 ? 10 : num;
            if (this.ProgressChanged != null)
              this.ProgressChanged(this.currentValue);
          }
          await Task.Delay(100);
        }
        this.Status = this.stopped;
      }));
    }

    private void ResetValues()
    {
      this.currentValue = 0;
      this.maxValueInPhase = 0;
      this.Status = this.stopped;
    }

    public void Stop()
    {
      if (!(this.stopped != this.Status))
        return;
      this.Status = this.stopping;
    }

    public void ForceValue(int v)
    {
      this.currentValue = v;
      if (this.ProgressChanged == null)
        return;
      this.ProgressChanged(this.currentValue);
    }

    public delegate void ProgressChangedHandler(int value);
  }
}
