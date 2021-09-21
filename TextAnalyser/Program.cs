using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace StringAnalyserProgram
{
    // CLASS WHICH WRITES STRING IN FILE
    class TextWriter
    {
        private string textPath { get; set; }
        private string text { get; set; }
        public TextWriter(string textPath, string text)
        {
            this.textPath = textPath;
            this.text = text;
        }
        public async Task WhriteText()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(this.textPath, false, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    // CLASS WHICH CONDUCTS STRING ANALYSIS
    class TextAnalyser
    {
        private string text { get; set; }
        private string textPath { get; set; }
        private List<string> tripletList = new List<string>();
        public TextAnalyser(string textPath)
        {
            this.textPath = textPath;
        }
        // THE SPECIAL LIST INCLUDES CHARACTERS THAT SHOULD NOT BE COUNTED FROM THE STRING
        private List<string> stopSymbolsTable = new List<string>
        {"!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "<", ">", ",", ".", "?", "/", "|", "\n", "+", "\t", "=", "_", "-",
         "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", " "};


        public async Task TextAnalyserMeth()
        {
            try
            {
                // READING STRING FROM FILE
                using (StreamReader sr = new StreamReader(textPath))
                {
                    text = await sr.ReadToEndAsync();

                    for (int i = 0; i < text.Length - 2;)
                    {
                        if (!stopSymbolsTable.Contains(text[i].ToString()) && text[i] == text[i + 1] && text[i] == text[i + 2])
                        {
                            string value = $"{text[i]}{text[i + 1]}{text[i + 2]}";
                            tripletList.Add(value);
                            i += 3;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    foreach (var val in tripletList.Distinct())
                    {
                        Console.WriteLine(val + " - " + tripletList.Where(x => x == val).Count() + " times");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    class TextTripletAnalysis
    {

        static async Task Main(string[] args)
        {
            // NAME OF THE DIRECTORY
            string textPath = @"C:\TestForText\test.txt";

            // EXAMPLE STRING
            string text = "1111112341241241251sdfsdfsdfsdfffffq1234121qqqqq";

            // WHRITING STRING IN FILE
            TextWriter tw = new TextWriter(textPath, text);
            await tw.WhriteText();

            // PERFOMING STRING ANALYSIS
            TextAnalyser ta = new TextAnalyser(textPath);
            await ta.TextAnalyserMeth();
        }
    }
}

