using Eval1;

try
{
    // STEP 1: REGISTER
    Console.WriteLine("========== REGISTER ==========");
    Console.WriteLine("Enter username:");
    string regUsername = Console.ReadLine();

    Console.WriteLine("Enter password:");
    string regPassword = Console.ReadLine();

    User user = new User();
    user.Register(regUsername, regPassword);

    // STEP 2: LOGIN
    Console.WriteLine("\n========== LOGIN ==========");
    Console.WriteLine("Enter username:");
    string loginUsername = Console.ReadLine();

    Console.WriteLine("Enter password:");
    string loginPassword = Console.ReadLine();

    user.Authenticate(loginUsername, loginPassword);

    // STEP 3: ENCRYPT AND DECRYPT
    
    
    string textToEncrypt = loginPassword;

    string encrypted = EncryptionHandlers.Encrypt(textToEncrypt);
    

    string decrypted = EncryptionHandlers.Decrypt(encrypted);
    

    
    Loggers.Log("App finished running successfully!");
}
catch (Exception ex)
{
    Loggers.LogError("App crashed: " + ex.Message);
    Console.WriteLine("Something went wrong. Please try again.");
}