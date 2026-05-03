namespace NewDEMO.tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void AddTwoNumbers()
    {
        int result = 2 + 3;
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void CheckName()
    {
        string name = "Alice";
        Assert.AreEqual("Alice", name);
    }

    [TestMethod]
    public void CheckIsTrue()
    {
        bool isAdult = true;
        Assert.IsTrue(isAdult);
        Console.WriteLine("Alice is an adult.");
    }
}