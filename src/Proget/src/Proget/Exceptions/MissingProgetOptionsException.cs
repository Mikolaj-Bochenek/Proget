namespace Proget.Exceptions;

internal sealed class MissingProgetOptionsException : Exception
{
    public HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

    public MissingProgetOptionsException(string section)
        : base($"The Proget options section: '{section}' is missing."
            + $" Add section: '{section}' to the appsettings.json file and fullfill the settings according to the Proget documentation"
            + " or set flag 'override: true' to 'override: false'."
            + "If the 'override' flag is set to 'false', the settings have to be fullfiled via OptionsBuilder with lambda expression in code.")
    {
    }
}
