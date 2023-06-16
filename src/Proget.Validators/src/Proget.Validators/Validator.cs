namespace Proget.Validators;

public static class Validator
{
    public static bool Validate(string input, Func<Options, bool> validators)
        => validators(new Options(input));

}

