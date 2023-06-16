namespace Proget.Messaging.SMSApi.Publishers
{
    internal class SmsPublisher : ISmsPublisher
    {
        private readonly IClient _client;
        private readonly SMSFactory? _smsFactory;
        private readonly SmsApiOptions? _smsOptions;

        public SmsPublisher(SmsApiOptions smsOptions)
        {
            _smsOptions = smsOptions;

            _client  = new ClientOAuth(_smsOptions.ApiKey);
            _smsFactory = new SMSFactory(_client);
        }

        Task ISmsPublisher.PublishAsync<TMessage>(TMessage message, string? messageId)
        {
            var smsMessage = message as SmsApiMessage ??
                throw new SmsMessageIsNullException();

            if(smsMessage.RecipientNumber!.IsNullOrEmpty())
                throw new RecipientNumberIsNullOrEmptyException();

            if (smsMessage.Message!.IsNullOrEmpty())
                throw new SmsMessageContentIsNullOrEmptyException();

            _smsFactory?.ActionSend()
                .SetText(smsMessage.Message)
                .SetTo(smsMessage.RecipientNumber)
                .SetSender(_smsOptions?.SenderName)
                .Execute();

            return Task.CompletedTask;
        }
    }
}
