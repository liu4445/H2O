﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public class HwpToJson
    {
        private string jsonpath;
        private string jsontextpath;

        JObject json;
        JObject jsonT;

        public HwpToJson()
        {
            this.jsonpath = Data.currentPath + @"\test.json";
            this.jsontextpath = Data.currentPath + @"\onlytext.json";
        }

        public void Run()
        {
            ExecuteCommandSync(Data.filePath);
            CreateJsonFile();
        }

        public JObject getJson()
        {
            return this.json;
        }

        public JObject getJsonT()
        {
            return this.jsonT;
        }

        private void CreateJsonFile()
        {
            StreamReader file = File.OpenText(jsonpath);
            JsonTextReader reader = new JsonTextReader(file);
            json = (JObject)JToken.ReadFrom(reader);


            file.Close();
            reader.Close();


            StreamReader file2 = File.OpenText(jsontextpath);
            JsonTextReader reader2 = new JsonTextReader(file2);
            jsonT = (JObject)JToken.ReadFrom(reader2);

            file2.Close();
            reader2.Close();

        }


        public void ExecuteCommandSync(Object filepath)
        {
            String path = System.IO.Directory.GetCurrentDirectory();

            try

            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("java", @"-jar temp44-0.0.1-jar-with-dependencies.jar " + "\"" + filepath + "\" " + Environment.NewLine);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.WorkingDirectory = path;
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;

                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                /*             proc.StandardInput.WriteLine(filepath);
                             proc.StandardInput.Close();
                */             // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);

            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }
    }
}