using System.Transactions;

namespace Printing_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }


        static void Menu()
        {
            string header = "===============\n  Printer App\n===============\n";
            while (true)
            {
                Console.WriteLine(header);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter to start printer [ (q | quit) to quit ]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                string choice = Console.ReadLine();
                if (String.IsNullOrEmpty(choice)) choice = " ";
                choice = choice.ToLower();
                if (choice == "q" || choice == "quit") break;

                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine("Enter The Data\n-=-=-=-=-=-=-=-=-=-");
                //get the text
                string text = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(header);
                Console.Write("Enter Number Of Copies : ");
                int numOfCopies;

                //get number of copies
                while (true)
                {
                    try
                    {
                        numOfCopies = Convert.ToInt32(Console.ReadLine());
                    }
                    catch(Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Enter Number!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    if (numOfCopies <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Copies Can NOT be less than or equal { 0 }");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        break;
                    }
                }


                Console.WriteLine(header);
                Console.Write("Enter The Name of File : ");
                string? fileName;
                while (true)
                {
                    fileName = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(fileName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Enter Name Of File");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    Console.Clear();
                    break;
                }
                fileName = fileName.Trim();

                Console.WriteLine(header);
                Console.Write("Enter The Path to Save Files in : ");
                string? filePath;
                while (true)
                {
                    filePath = Console.ReadLine();
                    if (!IsValidPath(filePath))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Enter a Valid Path!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    Console.Clear();
                    break;
                }


                Printer printer = new Printer(text, numOfCopies, fileName);


                Console.WriteLine(header);
                Console.WriteLine("1) All Copies in one Folder\n\n2) Each Copy in a Folder\n\n3) Just put the files in the path directely\n\n");
                int? select = null;

                //get user selection
                while (true)
                {
                    try
                    {
                        select = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Enter Number!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    if (select != 1 && select != 2 && select != 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Input!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        break;
                    }
                }

                if(select == 1)
                {
                    printer.CreateInOneFolder(filePath);
                }
                else if(select == 2)
                {
                    printer.CreateEachFileInFolder(filePath);
                }
                else
                {
                    printer.CreateInPath(filePath);
                }

                Console.WriteLine(header);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Done");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n< Click Enter To Continue >");
                Console.ForegroundColor= ConsoleColor.Gray;
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static bool IsValidPath(string path) => Uri.TryCreate(path, UriKind.Absolute, out Uri result) && result != null && Directory.Exists(path);
        
    }
}
