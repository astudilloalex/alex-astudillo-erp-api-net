using AlexAstudilloERP.Domain.Exceptions.Firebase;
using AlexAstudilloERP.Domain.Interfaces.APIs;
using AlexAstudilloERP.Domain.Models.FirebaseAuth;
using FirebaseAdmin.Auth;
using Google.Api.Gax;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AlexAstudilloERP.Infrastructure.APIs;

public class FirebaseAuthAPI : IFirebaseAuthAPI
{
    private readonly FirebaseAuth _auth = FirebaseAuth.DefaultInstance;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _serializerOptions;

    public FirebaseAuthAPI(HttpClient client, JsonSerializerOptions serializerOptions)
    {
        _client = client;
        _serializerOptions = serializerOptions;
    }

    public async Task<string> ConfirmEmailVerification(string oobCode)
    {
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/accounts:update")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    { "oobCode", oobCode }
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        JsonNode? uidNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("localId")).Value) ?? throw new FirebaseException("firebase-exception");
        return uidNode.Deserialize<string>()!;
    }

    public async Task<string> ConfirmPasswordReset(string oobCode, string password)
    {
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/accounts:resetPassword")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    { "oobCode", oobCode },
                    { "newPassword", password }
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        JsonNode? emailNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("email")).Value) ?? throw new FirebaseException("firebase-exception");
        return emailNode.Deserialize<string>()!;
    }

    public Task<UserRecord> CreateAsync(UserRecordArgs args)
    {
        return _auth.CreateUserAsync(args);
    }

    public Task<string> CreateCustomTokenAsync(string uid)
    {
        return _auth.CreateCustomTokenAsync(uid);
    }

    public Task<DeleteUsersResult> DeleteAllAsync(List<string> uids)
    {
        return _auth.DeleteUsersAsync(uids);
    }

    public Task DeleteAsync(string uid)
    {
        return _auth.DeleteUserAsync(uid);
    }

    public async Task<FirebaseSignInResponse> ExchangeRefreshTokenForIdToken(string refreshToken)
    {
        _client.BaseAddress = new Uri("https://securetoken.googleapis.com");
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/token")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    {"grant_type", "refresh_token" },
                    {"refresh_token" , refreshToken},
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        return new()
        {
            ExpiresIn = jsonObject.FirstOrDefault(j => j.Key.Equals("expires_in")).Value?.ToString() ?? "",
            LocalId = jsonObject.FirstOrDefault(j => j.Key.Equals("user_id")).Value?.ToString() ?? "",
            RefreshToken = jsonObject.FirstOrDefault(j => j.Key.Equals("refresh_token")).Value?.ToString() ?? "",
            Token = jsonObject.FirstOrDefault(j => j.Key.Equals("id_token")).Value?.ToString() ?? "",
        };
    }

    public async Task<List<UserRecord>> GetAllAsync()
    {
        List<UserRecord> records = new();
        Page<ExportedUserRecord> data = await _auth.ListUsersAsync(new ListUsersOptions()
        {
            PageSize = 1000,
        }).ReadPageAsync(1000);
        foreach (ExportedUserRecord item in data) records.Add(item);
        return records;
    }

    public Task<UserRecord> GetByUidAsync(string uid)
    {
        return _auth.GetUserAsync(uid);
    }

    public async Task<string> SendEmailVerification(string idToken)
    {
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/accounts:sendOobCode")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    { "requestType", "VERIFY_EMAIL" },
                    { "idToken", idToken }
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        JsonNode? emailNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("email")).Value) ?? throw new FirebaseException("firebase-exception");
        return emailNode.Deserialize<string>()!;
    }

    public async Task<string> SendPasswordResetEmail(string email)
    {
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/accounts:sendOobCode")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    { "requestType", "PASSWORD_RESET" },
                    { "email", email }
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        JsonNode? emailNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("email")).Value) ?? throw new FirebaseException("firebase-exception");
        return emailNode.Deserialize<string>()!;
    }

    public async Task<FirebaseSignInResponse> SignInWithEmail(string email, string password)
    {
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/accounts:signInWithPassword")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    {"email", email },
                    {"password" , password},
                    {"returnSecureToken", true},
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        return JsonSerializer.Deserialize<FirebaseSignInResponse?>(await responseMessage.Content.ReadAsStringAsync(), _serializerOptions)!;
    }

    public async Task<FirebaseSignInResponse> SignUpWithEmailAsync(string email, string password)
    {
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, "/v1/accounts:signUp")
        {
            Content = new StringContent(JsonSerializer.Serialize(new Dictionary<string, object>()
                {
                    {"email", email },
                    {"password" , password},
                    {"returnSecureToken", true},
                }, _serializerOptions)
            ),
        };
        using HttpResponseMessage responseMessage = await _client.SendAsync(requestMessage);
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(await responseMessage.Content.ReadAsStringAsync())!;
            JsonNode? messageNode = (jsonObject.FirstOrDefault(j => j.Key.Equals("error")).Value?
                .AsObject()?
                .FirstOrDefault(j => j.Key.Equals("message")).Value) ?? throw new FirebaseException("firebase-exception");
            throw new FirebaseException(messageNode.Deserialize<string>(_serializerOptions)!);
        }
        return JsonSerializer.Deserialize<FirebaseSignInResponse?>(await responseMessage.Content.ReadAsStringAsync(), _serializerOptions)!;
    }

    public Task<FirebaseToken> VerifyTokenAsync(string token)
    {
        return _auth.VerifyIdTokenAsync(token);
    }
}
