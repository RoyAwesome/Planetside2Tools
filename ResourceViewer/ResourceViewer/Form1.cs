using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ResourceViewer
{
    public partial class Form1 : Form
    {
        PackFile loadedFile = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void openpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadedFile = new PackFile(openFileDialog1.FileName);



                for (int i = 0; i < loadedFile.files; i++)
                {
                    ListViewItem item = new ListViewItem(loadedFile.filenames[i]);
                    item.SubItems.Add(loadedFile.fileData[i].size.ToString());
                    listView1.Items.Add(item);
                }
            }
        }

        private void exportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                loadedFile.WritePackToDirectory(folderBrowserDialog1.SelectedPath);
            }
        }

      
    }
}
