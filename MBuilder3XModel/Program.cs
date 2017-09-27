using MEDMCoreLibrary;
using System;
using System.IO;
using System.Xml;

namespace MBuilder3XModel
{
    class Program
    {
        static MEDMGen model = new MEDMGen();
        static void Main(string[] args)
        {
            try
            {
                string path = "";
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                if (args.Length > 0)
                {
                    path = args[0];
                    EDMTrace.IsEnabled = true;
                    Console.WriteLine($"----------------------------------------------------------------------------------------");
                    Console.WriteLine("Генератор моделей (MBuilder3X)");
                    Console.WriteLine($"path: {path}");
                    Console.WriteLine($"----------------------------------------------------------------------------------------");
                    if (Console.ReadKey().Key != ConsoleKey.Escape) {

                        model = new MEDMGen();

                        foreach (string fn in Directory.GetFiles(Path.Combine(path, "model"), "*.xml", SearchOption.AllDirectories)) {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"----------------------------------------------------------------------------------------");
                            Console.WriteLine($"{fn}");
                            Console.WriteLine($"----------------------------------------------------------------------------------------");
                            XmlDocument xdoc = new XmlDocument();
                            FileStream xstream = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.Read);
                            xdoc.Load(xstream);
                            model.Load(xdoc);
                            if (model.Errors>0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Кол-во ошибок - {model.Errors}");
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"----------------------------------------------------------------------------------------");
                        Console.WriteLine("Генератор классов");
                        Console.WriteLine($"----------------------------------------------------------------------------------------");
                        if (Console.ReadKey().Key != ConsoleKey.Escape)
                        {
                            foreach (MEDMProject mr in model.MainDic[typeof(MEDMProject)].Values)
                            {
                                File.WriteAllText(Path.Combine(path, $"{mr.Name}.cs"), model.GenClasses(mr));
                            }
                            if (model.Errors > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Кол-во ошибок - {model.Errors}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Путь до папки с моделями не задан...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine($"{e}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"----------------------------------------------------------------------------------------");
            Console.WriteLine($"Готово...");
            Console.ReadKey();
        }
    }

}