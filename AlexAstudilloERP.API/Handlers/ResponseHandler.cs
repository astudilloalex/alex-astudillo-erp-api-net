using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.API.Handlers;

public static class ResponseHandler
{
    /// <summary>
    /// Returns a bad request information.
    /// </summary>
    /// <param name="message">The message to send the client</param>
    /// <returns>A dictionary with information.</returns>
    public static Dictionary<string, object> BadRequest(string message)
    {
        return new()
            {
                {"statusCode", 400 },
                {"message", message },
            };
    }

    /// <summary>
    /// Returns a conflict information.
    /// </summary>
    /// <param name="message">The message to send to the client</param>
    /// <returns>A dictionary with information.</returns>
    public static Dictionary<string, object> Conflict(string message)
    {
        return new()
            {
                {"statusCode",409 },
                {"message", message },
            };
    }

    /// <summary>
    /// A custom error with custom message.
    /// </summary>
    /// <param name="code">The HTTP code.</param>
    /// <param name="message">The message to show.</param>
    /// <returns>A dictionary with data.</returns>
    public static Dictionary<string, object> Error(int code, string message)
    {
        return new()
            {
                {"statusCode", code},
                {"message", message},
            };
    }

    /// <summary>
    /// A custom forbidden with custom message.
    /// </summary>
    /// <returns>A dictionary with data.</returns>
    public static Dictionary<string, object> Forbidden()
    {
        return new()
            {
                {"statusCode", 403},
                {"message", "forbidden"},
            };
    }

    /// <summary>
    /// The forbidden formatted data.
    /// </summary>
    /// <param name="message">The message to show.</param>
    /// <returns>A <see cref="Dictionary{TKey, TValue}"/> with data.</returns>
    public static Dictionary<string, object> Forbidden(string message)
    {
        return new()
            {
                {"statusCode", 403},
                {"message", message},
            };
    }

    /// <summary>
    /// The not found formatted data with default message.
    /// </summary>
    /// <returns>A <see cref="Dictionary{TKey, TValue}"/> with data.</returns>
    public static Dictionary<string, object> NotFound()
    {
        return new()
            {
                {"statusCode", 404},
                {"message", "not-found"},
            };
    }

    /// <summary>
    /// The not found formatted data.
    /// </summary>
    /// <param name="message">The message to show.</param>
    /// <returns>A <see cref="Dictionary{TKey, TValue}"/> with data.</returns>
    public static Dictionary<string, object> NotFound(string message)
    {
        return new()
            {
                {"statusCode", 404},
                {"message", message},
            };
    }

    /// <summary>
    /// Generate a response with status code 200.
    /// Use to send a success notice to the client.
    /// </summary>
    /// <returns>A Dictionary object with successful message.</returns>
    public static Dictionary<string, object> Ok()
    {
        return new()
            {
                {"statusCode", 200 },
                {"message", "successful" },
            };
    }

    /// <summary>
    /// Generate a response with status code 200.
    /// </summary>
    /// <typeparam name="T">The class type</typeparam>
    /// <param name="page">The page to send data</param>
    /// <returns>A dictionary.</returns>
    public static Dictionary<string, object> Ok<T>(IPage<T> page) where T : class
    {
        return new()
            {
                {"statusCode", 200 },
                {"message", "successful" },
                {"totalElements", page.GetTotalElements() },
                {"totalPages", page.GetTotalPages() },
                {"numberOfElements", page.GetNumberOfElements() },
                {"pageNumber", page.GetNumber() + 1 },
                {"last", page.IsLast()},
                {"first",page.IsFirst()},
                {"offset", page.GetPageable().GetOffset()},
                {"data", page.GetContent()},
            };
    }

    /// <summary>
    /// Generate a response with status code 200.
    /// </summary>
    /// <typeparam name="T">The type of the object to return inside data field.</typeparam>
    /// <param name="data">The data, List or model to add inside data field.</param>
    /// <returns>A Dictionary object with successful message and data.</returns>
    public static Dictionary<string, object?> Ok<T>(T data)
    {
        return new()
            {
                { "statusCode", 200 },
                { "message", "successful" },
                { "data", data }
            };
    }

    /// <summary>
    /// Map formatted data.
    /// </summary>
    /// <returns>A <see cref="Dictionary{TKey, TValue}"/> with a default message 'unauthorized'</returns>
    public static Dictionary<string, object> Unauthorized()
    {
        return new()
            {
                { "statusCode", 401 },
                { "message", "unauthorized" },
            };
    }

    /// <summary>
    /// Use for unauthorized data.
    /// </summary>
    /// <param name="message">The message for the response.</param>
    /// <returns>A dictionary with data</returns>
    public static Dictionary<string, object> Unauthorized(string message)
    {
        return new()
            {
                { "statusCode", 401 },
                { "message", message },
            };
    }
}
