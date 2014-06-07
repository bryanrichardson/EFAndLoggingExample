namespace EFAndLoggingExample.Mail
{
    public interface IMailer
    {
        void SendEmail(string fromSomeplaceCom, string to, string message);
    }
}