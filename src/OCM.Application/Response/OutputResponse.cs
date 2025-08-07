﻿namespace OCM.Application.Response;

public record OutputResponse
{
    public OutputResponse()
    {
    }

    public OutputResponse(int identifier)
    {
        Identifier = identifier;
    }

    public OutputResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    public string ErrorMessage { get; }

    public int Identifier { get; init; }
}