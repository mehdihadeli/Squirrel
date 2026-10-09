using Squirrel.Abstractions.Domain;
using Squirrel.Core.Domain.Exceptions;
using Squirrel.Core.Exception;

namespace Squirrel.Core.Domain;

public static class RuleChecker
{
    public static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }

    public static void CheckRule<TException>(IBusinessRuleWithExceptionType<TException> rule)
        where TException : System.Exception
    {
        if (rule.IsBroken())
            throw rule.Exception;
    }
}
