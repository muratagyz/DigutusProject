namespace DigutusProject.Mail.Utilities;

public class VerificationCodeHelper
{
    public static string verificationCode { get; set; }
    public static string CreateVerificationCode()
    {
        Random rnd = new Random();
        for (int i = 1; i <= 6; i++)
        {
            int result = rnd.Next(1, 10);
            verificationCode += result.ToString();
        }

        return verificationCode;
    }
}