using Eval1;

namespace TestProject1;

[TestClass]
public sealed class Test1
{
    
    [TestMethod]
    public void Test_Register_Username()
    {
        User user = new User();
        user.Register("Alice", "pass123");
        Assert.AreEqual("Alice", user.Username);
    }

    
    [TestMethod]
    public void Test_Register_Password()
    {
        User user = new User();
        user.Register("Alice", "pass123");
        Assert.AreEqual("pass123", user.Password);
    }

    
    [TestMethod]
    public void Test_Login_Correct()
    {
        User user = new User();
        user.Register("Alice", "pass123");
        bool result = user.Authenticate("Alice", "pass123");
        Assert.IsTrue(result);
    }

    
    [TestMethod]
    public void Test_Login_WrongPassword()
    {
        User user = new User();
        user.Register("Alice", "pass123");
        bool result = user.Authenticate("Alice", "wrongpass");
        Assert.IsFalse(result);
    }

    
    [TestMethod]
    public void Test_Encrypt()
    {
        string encrypted = EncryptionHandlers.Encrypt("HelloWorld");
        Assert.AreNotEqual("HelloWorld", encrypted);
    }

    
    [TestMethod]
    public void Test_Decrypt()
    {
        string encrypted = EncryptionHandlers.Encrypt("HelloWorld");
        string decrypted = EncryptionHandlers.Decrypt(encrypted);
        Assert.AreEqual("HelloWorld", decrypted);
    }

    
    [TestMethod]
    public void Test_EmptyUsername()
    {
        User user = new User();
        user.Register("", "pass123");
        Assert.IsNull(user.Username);
    }

    
    [TestMethod]
    public void Test_EmptyPassword()
    {
        User user = new User();
        user.Register("Alice", "");
        Assert.IsNull(user.Password);
    }

    
    [TestMethod]
    public void Test_Log()
    {
        Loggers.Log("Test log message");
        Assert.IsTrue(true);
    }

    
    [TestMethod]
    public void Test_LogError()
    {
        Loggers.LogError("Test error message");
        Assert.IsTrue(true);
    }
}