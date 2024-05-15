using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace PyNET;

public class PythonExecutor
{
    public static string RunPythonCode(string code)
    {
        if (!IsPythonInstalled())
        {
            throw new InvalidOperationException("Python is not installed.");
        }

        string pythonPath = GetPythonExecutablePath();
        if (string.IsNullOrEmpty(pythonPath))
        {
            throw new InvalidOperationException("Could not find Python executable path.");
        }

        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"-c \"{code.Replace("\"", "\\\"")}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                return result;
            }

            using (StreamReader reader = process.StandardError)
            {
                string error = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    throw new InvalidOperationException($"Error: {error}");
                }
            }
        }
    }

    public static bool IsPythonInstalled()
    {
        string command = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "python --version" : "python3 --version";
        try
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = GetShell(),
                Arguments = GetShellArguments(command),
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result.StartsWith("Python");
                }
            }
        }
        catch
        {
            return false;
        }
    }

    public static string GetPythonExecutablePath()
    {
        string command = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "where python" : "which python3";
        try
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = GetShell(),
                Arguments = GetShellArguments(command),
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd().Trim();
                    if (!string.IsNullOrEmpty(result) && File.Exists(result))
                    {
                        return result;
                    }
                }
            }
        }
        catch
        {
            // Fall back to checking some common paths or PATH environment variable
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "python";
            }
            else
            {
                return "python3";
            }
        }

        return string.Empty;
    }

    static string GetShell()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "cmd.exe";
        }
        else
        {
            return "/bin/bash";
        }
    }

    static string GetShellArguments(string command)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return $"/c {command}";
        }
        else
        {
            return $"-c \"{command}\"";
        }
    }
}