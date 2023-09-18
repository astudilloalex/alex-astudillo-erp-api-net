namespace AlexAstudilloERP.API.Handlers;

public class FirebaseHttpHandler : DelegatingHandler
{
    private readonly string apiKey;

    public FirebaseHttpHandler(string apiKey)
    {
        this.apiKey = apiKey;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        UriBuilder uriBuilder = new(request.RequestUri!);
        string query = uriBuilder.Query;
        if (string.IsNullOrEmpty(query))
        {
            query = $"key={apiKey}";
        }
        else
        {
            query += $"&key={apiKey}";
        }
        uriBuilder.Query = query;
        request.RequestUri = uriBuilder.Uri;
        return base.SendAsync(request, cancellationToken);
    }
}
