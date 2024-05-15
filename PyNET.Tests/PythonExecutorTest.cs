namespace PyNET.Tests;

public class PythonExecutorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CheckVersion()
    {
        var installed = PythonExecutor.IsPythonInstalled();
        Assert.That(installed, Is.EqualTo(true));
    }

    [Test]
    public void RunCodeString()
    {
        // Python code to be executed
        string pythonCode = @"
print('Hello, world!')
for i in range(5):
    print('Number:', i)
exit()
";

        // Call the method to run the Python code
        var output = PythonExecutor.RunPythonCode(pythonCode);
        Console.WriteLine(output);
        Assert.That(output, Is.EqualTo("Hello, world!\nNumber: 0\nNumber: 1\nNumber: 2\nNumber: 3\nNumber: 4\n"));
    }
}