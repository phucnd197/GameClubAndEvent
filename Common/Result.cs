using Microsoft.AspNetCore.Mvc;

namespace GameClubAndEvent.Api.Common;

public class Result<TRes>
{
    public bool IsSuccess { get; init; }
    public int StatusCode { get; init; }
    public TRes? Data { get; init; }
    public IList<string>? Errors { get; init; }
}

public static class Result
{
    public static Result<T1> Success<T1>(T1 data, int statusCode = 200)
    {
        return new Result<T1>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true,
        };
    }
    public static Result<T1> Fail<T1>(IList<string> errors, int statusCode = 500)
    {
        return new Result<T1>
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccess = false,
        };
    }
    public static Result<T1> Fail<T1>(string error, int statusCode = 500)
    {
        return new Result<T1>
        {
            Errors = [error],
            StatusCode = statusCode,
            IsSuccess = false,
        };
    }
    public static ObjectResult ToActionResult<T>(this Result<T> result)
    {
        return new ObjectResult(result.IsSuccess ? result.Data : result.Errors)
        {
            StatusCode = result.StatusCode,
        };
    }
}

