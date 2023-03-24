using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace FileHosting.Shared.Api.IntegrationTests;

public class JsonBody : StringContent
{
    public JsonBody(string jsonBody) : base(jsonBody, Encoding.UTF8, MediaTypeNames.Application.Json)
    {
    }

    public JsonBody(object obj) : this(JsonConvert.SerializeObject(obj))
    {
    }
}