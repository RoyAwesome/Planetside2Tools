using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResourceViewer
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
        public int files = 0;
        private string filename;

        public PackFile(string filename)
        {
            this.filename = filename;
            int files = 5;
            int subpacks = 2;

            using (FileStream s = File.Open(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BEBinaryReader(s))
            {
                while (true)
                {
                    header = reader.ReadInt32();
                    if (header == 0) break;
                    filesInPack = reader.ReadInt32();
                    //packData.header.i3 = reader.ReadInt32();


                   files = filesInPack;

                    for (int i = 0; i < files; i++)
                    {
                        string readName = reader.ReadString();
                        filenames.Add(readName);

                       

                        FileData fileDat = new FileData();
                        fileDat.offset = reader.ReadInt32();
                        fileDat.size = reader.ReadInt32();
                        fileDat.i3 = reader.ReadInt32();
                        fileData.Add(fileDat);

                        this.files++;
                    }

                    s.Seek(header, SeekOrigin.Begin);
                }

            }
        }

        public void WritePackToDirectory(string directory)
        {
            for (int i = 0; i < files; i++)
            {
                
                using (FileStream s = File.Open(filename, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BEBinaryReader(s))
                using (FileStream outfile = File.Open(directory + Path.DirectorySeparatorChar + filenames[i], FileMode.Create, FileAccess.Write))
                {
                    s.Seek(fileData[i].offset, SeekOrigin.Begin);
                    byte[] file = reader.ReadBytes(fileData[i].size);
                    outfile.Write(file, 0, file.Length);
                }

            }
        }

    }
}
