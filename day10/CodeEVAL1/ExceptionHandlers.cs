namespace Eval1;

public class EncryptionHandlers
{
    // Encrypt: reverse the string
    public static string Encrypt(string text)
    {
        try
        {
            if (text == "")
            {
                Console.WriteLine("Cannot encrypt empty text!");
                Loggers.LogError("Encryption failed - Text was empty.");
                return null;
            }

            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            string encryptedText = new string(chars);

            Console.WriteLine("Data encrypted successfully!");
            Loggers.Log("Data encrypted successfully.");
            return encryptedText;
        }
        catch (Exception ex)
        {
            Loggers.LogError("Encryption error: " + ex.Message);
            Console.WriteLine("Something went wrong during encryption.");
            return null;
        }
    }


    public static string Decrypt(string text)
    {
        try
        {
            if (text == "")
            {
                Console.WriteLine("Cannot decrypt empty text!");
                Loggers.LogError("Decryption failed - Text was empty.");
                return null;
            }

            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            string decryptedText = new string(chars);

            Console.WriteLine("Data decrypted successfully!");
            Loggers.Log("Data decrypted successfully.");
            return decryptedText;
        }
        catch (Exception ex)
        {
            Loggers.LogError("Decryption error: " + ex.Message);
            Console.WriteLine("Something went wrong during decryption.");
            return null;
        }
    }
}