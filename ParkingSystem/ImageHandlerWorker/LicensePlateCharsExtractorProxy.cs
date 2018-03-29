using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.ImageHandlerWorker
{
    class LicensePlateCharsExtractorProxy
    {
        private const string _inputFileName = "input.jpg";
        private const string _inputFilePath = @".\x64\input.jpg";
        private const string _licensePlateCharsWorkingDir = @".\x64\";
        private const string _licensePlateCharsExtractor = @".\x64\LicensePlateCharsExtractor.exe";

        public string Extract(Image image)
        {
            SaveInputFile(image);
            return ExecuteProgram();
        }

        private string ExecuteProgram()
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = _licensePlateCharsExtractor;
                p.StartInfo.Arguments = _inputFileName;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), _licensePlateCharsWorkingDir);
                p.Start();

                string output = p.StandardOutput.ReadToEnd()?.Split(new string []{ "\r\n" }, StringSplitOptions.None)[0];

                if (string.IsNullOrEmpty(output))
                    throw new Exception("The number plate extracor didn't detect a number");

                p.WaitForExit();

                return output;
            }
        }

        private void SaveInputFile(Image image)
        {
            if (File.Exists(_inputFilePath))
                File.Delete(_inputFilePath);
            image.Save(_inputFilePath);
        }
    }
}
