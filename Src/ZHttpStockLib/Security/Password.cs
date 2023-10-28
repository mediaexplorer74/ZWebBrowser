// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Security.Password
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System.Runtime.Serialization;

namespace ZHttpStockLib.Security
{
  [DataContract]
  public class Password
  {
    [DataMember]
    public string PasswordText { get; set; }

    [DataMember]
    public string Hint { get; set; }

    [DataMember]
    public bool Enabled { get; set; }
  }
}
