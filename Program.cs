using System.Diagnostics;

namespace cowsayApp
{
    class Cowsay
    {
        public static string Say(string? message) //? check if null
        {
            string output = string.Empty; 
            string error = string.Empty;

            using (Process myProcess = new())
                {
                    myProcess.StartInfo.FileName = "/usr/games/cowsay";
                    // myProcess.StartInfo.Arguments = "-f vader";
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.RedirectStandardInput = true;
                    myProcess.StartInfo.RedirectStandardOutput = true;
                    myProcess.StartInfo.RedirectStandardError = true;

                    myProcess.Start();

                    //input
                    StreamWriter myStreamWriter = myProcess.StandardInput;
                    myStreamWriter.WriteLine(message);
                    myStreamWriter.Close();

                    output = myProcess.StandardOutput.ReadToEnd();
                    error = myProcess.StandardError.ReadToEnd();
                    myProcess.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception(error);
                    }
                }
            
            return output;
        }
    }
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("What is the cow thinking?");
                string? message = Console.ReadLine();
                
                Cowsay Cow = new Cowsay();
                string output = Cow.Say(message);

                Console.WriteLine(output);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}