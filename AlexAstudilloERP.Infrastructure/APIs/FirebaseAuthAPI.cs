﻿using AlexAstudilloERP.Domain.Exceptions.Firebase;
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

    public Task<FirebaseToken> VerifyTokenAsync(string token)
    {
        return _auth.VerifyIdTokenAsync(token);
    }
}
