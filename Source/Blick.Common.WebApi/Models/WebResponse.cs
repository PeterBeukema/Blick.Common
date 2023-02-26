using System.Collections.Generic;
using System.Net;

namespace Blick.Common.WebApi.Models;

public class WebResponse
{
    public HttpStatusCode StatusCode { get; set; }

    public string? RedirectUrl { get; set; }

    public string? ErrorCode { get; set; }

    public Dictionary<string, string[]>? Errors { get; set; }
}