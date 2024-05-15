# PythonExecutor

## Clone this project

```sh
git clone https://github.com/thanhtunguet/PyNET.git
```

## Usage

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
