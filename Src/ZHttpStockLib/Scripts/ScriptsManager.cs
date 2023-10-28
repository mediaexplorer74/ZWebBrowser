// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Scripts.ScriptsManager
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System.Collections.Generic;
using System.Text;

namespace ZHttpStockLib.Scripts
{
  public class ScriptsManager
  {
    private static ScriptsManager managerInstance;
    private List<string> outputList = new List<string>();

    private ScriptsManager()
    {
    }

    private static ScriptsManager ManagerInstance
    {
      get
      {
        if (ScriptsManager.managerInstance == null)
          ScriptsManager.managerInstance = new ScriptsManager();
        return ScriptsManager.managerInstance;
      }
    }

    public static ScriptsManager GetInstance() => ScriptsManager.ManagerInstance;

    public void AddOutputText(string output) => this.outputList.Add(output);

    public string GetOutputTextAll()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = this.outputList.Count - 1; index >= 0; --index)
        stringBuilder.AppendLine(this.outputList[index]);
      return stringBuilder.ToString();
    }

    public int ClearOutputs()
    {
      int count = this.outputList.Count;
      this.outputList.Clear();
      return count;
    }
  }
}
