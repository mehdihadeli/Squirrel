# Squirrel.Validation

Assembly scanning and registration for FluentValidation validators.

## Install

```bash
dotnet add package Squirrel.Validation
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomValidators(typeof(CreateOrderValidator).Assembly);
```

Validators are registered as their implemented `IValidator<T>` interfaces with transient lifetime. Pass the assembly that contains the validators you want to scan.

## Example

```csharp
public sealed class CreateOrderValidator : AbstractValidator<CreateOrder>
{
    public CreateOrderValidator()
    {
        RuleFor(command => command.CustomerId).NotEmpty();
    }
}
```
