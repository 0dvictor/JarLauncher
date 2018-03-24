using System;
using System.Diagnostics;
using System.IO;

namespace JavaLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Executable information
            var ExecutableName = Process.GetCurrentProcess().MainModule.FileName;
            var ExecutableFolder = Path.GetDirectoryName(ExecutableName);
            var CommandName = Environment.GetCommandLineArgs()[0];
            var CommandArguments = Environment.CommandLine;
            CommandArguments = CommandArguments.Substring(CommandName.Length + (CommandArguments[0] == '"' ? 2 : 0));

            // Java information
            var Java = Path.Combine(ExecutableFolder, "jre", "bin", "java.exe");
            if (!File.Exists(Java))
            {
                var JavaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
                if (JavaHome != null)
                {
                    Java = Path.Combine(JavaHome, "jre", "bin", "java.exe");
                }
                else
                {
                    Java = "java.exe";
                }
            }

            Process.Start(new ProcessStartInfo(Java, string.Format("-jar \"{0}\"{1}", Path.ChangeExtension(ExecutableName, ".jar"), CommandArguments)) { UseShellExecute = false }).WaitForExit();
        }
    }
}
