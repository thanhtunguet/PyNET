# PythonExecutor

## Clone this project

```sh
git clone https://github.com/thanhtunguet/PyNET.git
```

## Usage

1. Check if Python is installed

```cs
using PyNET;

public class Program {
    static void Main(string[] args)
    {
        var isInstalled = PythonExecutor.IsPythonInstalled();
        Console.WriteLine(isInstalled ? "Python is installed" : "Python is not installed");
    }   
}
```

2. Run Python code

```csharp
using PyNET;

public class Program {
    static void Main(string[] args)
    {
        string pythonCode = "print('Hello from Python!')";
        string output = PythonExecutor.RunPythonCode(pythonCode);
        Console.WriteLine("Output from Python:");
        Console.WriteLine(output);
    }   
}
```
