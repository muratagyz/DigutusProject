namespace DigutusProject.Mail;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}