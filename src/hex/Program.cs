using System;
using System.IO;
using System.Text;

namespace ZTC.Hexaminer
{
    class Program
    {
        static void Main(string[] args)
        {
            var argstr = string.Join(" ",args);
            Console.WriteLine($"hexaminer® 2020 ZTC");

            var file = new FileInfo(args[0]);
            if (file.Exists)
            {
                Console.WriteLine($"reading {file.Name}");
                using (var str = file.OpenRead())
                {
                    byte[] row = new byte[16];
                    int br = 0;
                    
                    int pos = 0;
                    while ((br = str.Read(row,0,row.Length))>0)
                    {
                        var x = BitConverter.ToString(row);
                        var line = x.Replace("-"," ").ToLower();

                        var sb = new StringBuilder();
                        for (int a=0; a<br; a++){
                            if (row[a]>30 && row[a]<128)
                            {
                                sb.Append((char)row[a]);
                            } else {
                                sb.Append(".");
                            }
                        }

                        var asc = sb.ToString();

                        Console.WriteLine($"{pos:x8}  {line}  {asc}");
                        pos+=br;

                        if (pos>=512)
                            break;
                    }
                }
            }
        }
    }
}
