using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PS2Unpacker
{
    struct FileData
    {
       public int offset;
       public int size;
       public int i3;
    }

    class PackFile
    {
        public int header;
        public int filesInPack;
        public List<string> filenames = new List<string>();
        public List<FileData> fileData = new List<FileData>(); 


    }


    class Program
    {
        static void Main(string[] args)
        {

            string filename = args[0];
            int files = 5;
            int subpacks = 2;

            PackFile packData = new PackFile();

            using(FileStream s = File.Open(filename, FileMode.Open, FileAccess.Read))
            using(BinaryReader reader = new BEBinaryReader(s))
            {
                while(true)
                {
                    packData.header = reader.ReadInt32();
                    if (packData.header == 0) break;
                    packData.filesInPack = reader.ReadInt32();
                    //packData.header.i3 = reader.ReadInt32();

              
                    Console.WriteLine("Pack Header");
                    Console.WriteLine("First Int: " + (uint)packData.header);
                    Console.WriteLine("Second Int: " + (uint)packData.filesInPack);
                    files = packData.filesInPack;

                    for (int i = 0; i < files; i++)
                    {
                        Console.WriteLine("File " + i);
                        string readName = reader.ReadString();
                        packData.filenames.Add(readName);

                        Console.WriteLine("Name: " + packData.filenames[i]);


                        FileData fileDat = new FileData();
                        fileDat.offset = reader.ReadInt32();
                        fileDat.size = reader.ReadInt32();
                        fileDat.i3 = reader.ReadInt32();
                        packData.fileData.Add(fileDat);

                        Console.WriteLine("Data: ( " + packData.fileData[i].offset + " , " + packData.fileData[i].size + ", " + (uint)packData.fileData[i].i3 + " )");
                    }

                    s.Seek(packData.header, SeekOrigin.Begin);
                }


                for (int i = 0; i < packData.fileData.Count; i++)
                {
                    using (FileStream outfile = File.Open(packData.filenames[i], FileMode.Create, FileAccess.Write))
                    {
                        s.Seek(packData.fileData[i].offset, SeekOrigin.Begin);
                        byte[] file = reader.ReadBytes(packData.fileData[i].size);
                        outfile.Write(file, 0, file.Length);
                    }

                }
               
            }
           

        }
    }
}
