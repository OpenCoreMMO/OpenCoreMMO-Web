﻿using FluentValidation;
using OCM.Application.Requests.Commands;

namespace OCM.Application.Requests.Validators;

public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(10, 29).WithMessage("Password must be between 10 and 29 characters.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter (a-z).")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter (A-Z).")
            .Matches("[0-9]").WithMessage("Password must contain at least one number (0-9).")
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Password contains invalid characters. Only letters and numbers are allowed.");

        RuleFor(x => x.AccountName)
            .NotEmpty()
            .Length(4, 29).WithMessage("Account name must be between 4 and 29 characters.")
            .WithMessage("Account name is required.");
    }
}