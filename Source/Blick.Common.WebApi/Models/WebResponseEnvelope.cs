namespace Blick.Common.WebApi.Models;

public class WebResponseEnvelope<TPayload> : WebResponse
{
    public TPayload? Payload { get; set; }
}