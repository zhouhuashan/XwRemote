﻿using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace XwUpdater
{
    class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                string runprocess = args[0];
                string zipfile = args[1];
                string destination = args[2];
                
                //wait a bit
                Thread.Sleep(500);

                //kill running process if its still there
                foreach (var process in Process.GetProcessesByName(runprocess.Replace(".exe", "")))
                    process.Kill();

                //wait another bit
                Thread.Sleep(500);

                if (!File.Exists(zipfile))
                    return;

                //unzip new
                using (ZipArchive archive = ZipFile.Open(zipfile, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry file in archive.Entries)
                    {
                        string completeFileName = Path.Combine(destination, file.FullName);
                        if (file.Name == "")
                        {// Assuming Empty for Directory
                            Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                            continue;
                        }
                        // create dirs
                        var dirToCreate = destination;
                        for (var i = 0; i < file.FullName.Split('/').Length - 1; i++)
                        {
                            var s = file.FullName.Split('/')[i];
                            dirToCreate = Path.Combine(dirToCreate, s);
                            if (!Directory.Exists(dirToCreate))
                                Directory.CreateDirectory(dirToCreate);
                        }
                        file.ExtractToFile(completeFileName, true);
                    }
                }

                //run process again
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = runprocess;
                    process.StartInfo.Arguments = "";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.Start();
                }

                //delete zip
                File.Delete(zipfile);
            }
        }
    }
}
