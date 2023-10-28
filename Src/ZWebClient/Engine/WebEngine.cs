// Type: ZWebClient.Engine.WebEngine
// Assembly: ZWebClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FD963AF-0266-4512-9EC7-B262A77CA6CF

using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace ZWebClient.Engine
{
  public class WebEngine
  {
    private static WebEngine webEngine;

    private static WebEngine WebEngineClient
    {
      get
      {
        if (WebEngine.webEngine == null)
          WebEngine.webEngine = new WebEngine();
        return WebEngine.webEngine;
      }
    }

    public static WebEngine GetInstance() => WebEngine.WebEngineClient;

    public async Task<IBuffer> GetBufferAsync(
      Uri r,
      CancellationToken cancellationToken,
      IProgress<HttpProgress> progressHandler)
    {
      return await new HttpClient()
                .GetBufferAsync(r)
                  .AsTask<IBuffer, HttpProgress>(cancellationToken, progressHandler);
    }
  }
}
