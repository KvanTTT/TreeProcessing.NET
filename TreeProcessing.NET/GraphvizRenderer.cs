using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace TreeProcessing.NET
{
    public class GraphvizRenderer
    {
        private static string _graphvizExePath;

        public static string SolutionDirectory { get; private set; }

        static GraphvizRenderer()
        {
            GetSolutionDirectory();
        }

        public void Render(string filePath, string dotString)
        {
            var tempDotFileName = Path.GetTempFileName();
            File.WriteAllText(tempDotFileName, dotString);
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = _graphvizExePath,
                Arguments = "\"" + tempDotFileName + "\" -Tpng -o \"" + filePath + "\" ",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });
            process.WaitForExit();
            File.Delete(tempDotFileName);
            var errors = process.StandardError.ReadToEnd();
            if (process.ExitCode != 0 || errors != "")
            {
                throw new Exception($"Error while graph rendering. Errors: {errors}");
            }
        }

        /// <summary>
        /// Returns path to the current source code file.
        /// </summary>
        /// <param name="thisFilePath"></param>
        private static void GetSolutionDirectory([CallerFilePath] string thisFilePath = null)
        {
            SolutionDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(thisFilePath), ".."));
            _graphvizExePath = Path.Combine(SolutionDirectory, @"packages\Graphviz.2.38.0.2\dot.exe");
        }
    }
}
