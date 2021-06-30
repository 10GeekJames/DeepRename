using System.Timers;
using System.Data;
using System.Globalization;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace DeepRename
{
    class Program
    {
        static void Main(string[] args)
        {
            var executeIn = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!System.IO.File.Exists(System.IO.Path.Combine(executeIn, "deeprename.zip")))
            {
                executeIn = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\..\\..\\..\\";
                if (!System.IO.File.Exists(System.IO.Path.Combine(executeIn, "deeprename.zip")))
                {
                    Console.WriteLine($"Please ensure your deeprename.zip is next to this .exe {System.IO.Path.Combine(executeIn, "deeprename.zip")}");
                    Console.WriteLine("I will exit early now so you can work on that, thank you, cheers, -robot");
                    return;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("If you would please,");
            Console.WriteLine("What word should I search for?");
            var fromKeyword = Console.ReadLine();
            var fromUpper = fromKeyword.Substring(0, 1).ToUpper() + fromKeyword.Substring(1);
            var fromLower = fromKeyword.Substring(0, 1).ToLower() + fromKeyword.Substring(1);
            var fromAllUpper = fromKeyword.ToUpper();

            Console.WriteLine("What word should I replace it with?");
            var toKeyword = Console.ReadLine();
            var toUpper = toKeyword.Substring(0, 1).ToUpper() + toKeyword.Substring(1);
            var toLower = toKeyword.Substring(0, 1).ToLower() + toKeyword.Substring(1);
            var toAllUpper = toKeyword.ToUpper();

            Console.WriteLine($"Starting process to replace {fromUpper} with {toUpper} and {fromLower} with {toLower} and {fromAllUpper} with {toAllUpper}.");

            var executeInSub = Path.Combine(executeIn, "deeprename");
            if (System.IO.Directory.Exists(executeInSub))
            {
                System.IO.Directory.Delete(executeInSub, true);
            }

            Timer timer = new Timer(300);
            var spinnerState = 0;
            var timerPreText = "";

            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;

            var zipFile = System.IO.Path.Combine(executeIn, "deeprename.zip");
            timerPreText = "Zip Discovered, unpacking pls hold this can take a bit";

            ZipFile.ExtractToDirectory(zipFile, executeInSub);

            if (!System.IO.Directory.Exists(executeInSub))
            {
                throw new Exception($"Need to setup the initial {executeInSub} /deeprename folder beneath this exe and place your work there");
            }
            timer.Enabled = false;

            var properCaseConverter = new CultureInfo("en-US", false).TextInfo;

            timer.Enabled = true;
            string[] searchFiles;

            // pass one, clean up file contents
            timerPreText = "Reworking File Contents";

            searchFiles = System.IO.Directory.GetFiles(executeInSub, "*.*", SearchOption.AllDirectories);
            foreach (var file in searchFiles.OrderByDescending(rs => rs.Length))
            {
                var directoryName = System.IO.Path.GetDirectoryName(file);

                var scanContents = System.IO.File.ReadAllText(file);
                if (scanContents.ToUpper().Contains(fromAllUpper))
                {
                    Console.WriteLine($"File contents rework ! {System.IO.Path.GetFileName(file)}");
                    try
                    {
                        scanContents = scanContents.Replace(fromUpper, toUpper);
                        scanContents = scanContents.Replace(fromLower, toLower);
                        scanContents = scanContents.Replace(fromAllUpper, toAllUpper);
                        System.IO.File.WriteAllText(file, scanContents);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"<Failed to> File contents rework ! {System.IO.Path.GetFileName(file)}");
                    }
                }
            }

            // pass two, clean up file names
            timerPreText = "Reworking File Names";

            searchFiles = System.IO.Directory.GetFiles(executeInSub, "*.*", SearchOption.AllDirectories);
            foreach (var file in searchFiles.OrderByDescending(rs => rs.Length))
            {
                var directoryName = System.IO.Path.GetDirectoryName(file);
                if (System.IO.Path.GetFileName(file).ToUpper().Contains(fromAllUpper))
                {
                    var newFileName = ReplaceLastInstanceOfString(file, fromUpper, toUpper);
                    newFileName = ReplaceLastInstanceOfString(newFileName, fromLower, toLower);
                    newFileName = ReplaceLastInstanceOfString(newFileName, fromAllUpper, toAllUpper);
                    Console.WriteLine($"New filename {newFileName}");
                    try
                    {
                        File.Move(file, newFileName);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"<Failed to> File move to ! {newFileName}");
                    }

                }
            }

            // pass three, clean up folder names
            timerPreText = "Reworking Folder Names ....";

            searchFiles = System.IO.Directory.GetDirectories(executeInSub, "*.*", SearchOption.AllDirectories);
            foreach (var directory in searchFiles.OrderByDescending(rs => rs.Length))
            {
                var dirName = new DirectoryInfo(directory).Name;
                if (dirName.ToUpper().Contains(fromAllUpper))
                {
                    var newDirName = ReplaceLastInstanceOfString(directory, fromUpper, toUpper);
                    newDirName = ReplaceLastInstanceOfString(newDirName, fromLower, toLower);
                    newDirName = ReplaceLastInstanceOfString(newDirName, fromAllUpper, toAllUpper);

                    Console.WriteLine($"Folder move to ! {newDirName}");
                    try
                    {
                        Directory.Move(directory, newDirName);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"<Failed to> Folder move to ! {newDirName}");
                    }
                }
            }

            // pass four, zip back up and clean out work area
            timerPreText = "Creating New Zip File ....";

            timer.Enabled = true;
            var zipResults = System.IO.Path.Combine(executeIn, "deeprename_renamed.zip");

            if (System.IO.File.Exists(zipResults))
            {
                System.IO.File.Delete(zipResults);
            }

            ZipFile.CreateFromDirectory(executeInSub, zipResults);

            timerPreText = "Cleaning up work area ....";

            if (System.IO.Directory.Exists(executeInSub))
            {
                System.IO.Directory.Delete(executeInSub, true);
            }

            timer.Enabled = false;
            Console.WriteLine($"The process has completed, press anykey to close");
            Console.ReadKey();

            string ReplaceLastInstanceOfString(string stringToWorkWith, string replaceFrom, string replaceTo)
            {
                var lastIndex = stringToWorkWith.LastIndexOf("\\");
                if (lastIndex < 0)
                {
                    return stringToWorkWith;
                }
                var lastString = stringToWorkWith.Substring(lastIndex);
                var replaced = lastString.Replace(replaceFrom, replaceTo);
                var result = stringToWorkWith.Substring(0, lastIndex) + replaced;
                return result;
            }
            void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                string[] spinner = { "\\", "/" };
                spinnerState++;
                if (spinnerState > 1)
                {
                    spinnerState = 0;
                }
                Console.Write($"{timerPreText} {spinner[spinnerState]}");
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }
    }
}
