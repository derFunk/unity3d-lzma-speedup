using System.Diagnostics;
using System.Reflection;
using System;
using System.IO;

namespace lzma_speedup
{
    class Program
    {
        static void Main(string[] args)
        {
            string lzmaRealExeName = "lzma_real.exe";
			string logFileName = "lzma_call_cli_arguments_log.txt";
			string lzmaPath = "";
			
			// Take "encode file" lzma parameter from unity
			if (args.Length < 3)
			{
				Console.WriteLine("Error: No filename given to encode!");
				Environment.Exit(1);
			}
			
			// args[1] = source, args[2] = destination
			// Unity3D 3.4 used to use parameter -fb372
            string lzmaArgs = "e " + args[1] + " " + args[2] + " -a0 -d0 -mt4 -fb5 -mc0 -lc0 -pb0 -mfbt2";
			
			Stopwatch sw = new Stopwatch();
            sw.Start();
			
            StreamWriter logFile = null;
            try
            {
                lzmaPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				
				// check if lzma_real.exe exists. This is the one which was shipped with Unity3D and which you renamed 
				// from lzma.exe to lzma_real.exe
				if (!File.Exists(Path.Combine(lzmaPath, lzmaRealExeName)))
				{
					Console.WriteLine("Error: " + lzmaRealExeName + " does not exist!");
					Environment.Exit(1);
				}
				
                logFile = new System.IO.StreamWriter(Path.Combine(@lzmaPath, logFileName));
                logFile.AutoFlush = true;
				
				logFile.WriteLine("Unity3D lzma parameters were: " + String.Join (" ", args));

                logFile.WriteLine("Now calling: " + lzmaPath + "\\" + lzmaRealExeName + " " + lzmaArgs);

                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @lzmaPath;
                start.FileName = lzmaRealExeName;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.Arguments = lzmaArgs;
                start.CreateNoWindow = true;

                using (Process process = Process.Start(start))
                {
                    process.WaitForExit();
                    //
                    // Read in all the text from the process with the StreamReader.
                    //
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                        logFile.WriteLine(result);
                    }
                }

                string elapsed = "Elapsed Time: " + (sw.ElapsedMilliseconds / 1000.0) + "(s)";
                Console.WriteLine(elapsed);
                logFile.WriteLine(elapsed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e + lzmaRealExeName);
                logFile.WriteLine(e);
				Environment.Exit(1);
            }
            finally
            {
                if (logFile != null)
                    logFile.Close();
            }


        }
    }
}
