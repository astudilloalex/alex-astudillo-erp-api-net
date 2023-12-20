namespace AlexAstudilloERP.API.Handlers;

public class FirebaseHttpHandler(string apiKey) : DelegatingHandler
{
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
