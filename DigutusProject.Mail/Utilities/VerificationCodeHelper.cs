namespace DigutusProject.Mail.Utilities;

public class VerificationCodeHelper
{
    private static List<int> VerificationCode = new List<int>();

    public List<int> CreateVerificationCode()
    {
        Random rastgele = new Random();
        for (int i = 1; i <= 6; i++)
        {
            int result = rastgele.Next(1, 100);
            VerificationCode.Add(result);
        }

        return VerificationCode;
    }
}