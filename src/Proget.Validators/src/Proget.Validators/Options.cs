using System.Text.RegularExpressions;

namespace Proget.Validators;

public class Options
{
    public string Input { get; }
    public Options(string input) => Input = input;
    public bool HasDigit() => new Regex(@"[0-9]+").IsMatch(Input);
    public bool HasUpperChar() => new Regex(@"[A-Z]+").IsMatch(Input);
    public bool HasLowerChar() => new Regex(@"[a-z]+").IsMatch(Input);
    public bool HasMinLength(int length) => Input.Length >= length;
    public bool HasMaxLength(int length) => Input.Length <= length;
    public bool HasSpecialChar(int count = 1)
        => Input.Where(ch => (!Char.IsLetterOrDigit(ch) && !Char.IsWhiteSpace(ch))).Count().Equals(count);
    public bool IsNullOrEmpty() => string.IsNullOrEmpty(Input);
    public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(Input);
    public bool HasNotSpecialChar() => !Input.Any(ch => (!Char.IsLetterOrDigit(ch) && !Char.IsWhiteSpace(ch)));
    public bool HasEmailSyntax()
        => new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant).IsMatch(Input);
}
