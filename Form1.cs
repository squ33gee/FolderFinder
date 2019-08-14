using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string filename;

        public Form1()
        {
            InitializeComponent();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                //
                //Открывать пустые папки
                //
                //Process Proc = new Process();
                //Proc.StartInfo.FileName = "explorer";
                //Proc.StartInfo.Arguments = listBox1.SelectedItem.ToString();
                //Proc.Start();
                //Proc.Close();
                //
                //=================================
                //

                //
                //Удалять пустые папки
                //
                string path = listBox1.SelectedItem.ToString();
                int index = listBox1.SelectedIndex;

                if (MessageBox.Show("Удалить выбранную папку?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Directory.Delete(path);
                    listBox1.Items.RemoveAt(index);
                }
                //
                //=================================
                //
            }
        }

        private void Folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                filename = FBD.SelectedPath;
                label1.Text = filename;
                start.Enabled = true;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            progressBar1.Value = 0;
            int count = 0;

            if (label1.Text != "")
            {
                try
                {
                    string[] dir = Directory.GetDirectories(filename, "*", SearchOption.AllDirectories);

                    int progress = dir.Count();
                    progressBar1.Maximum = progress;

                    foreach (var files in dir)
                    {
                        try
                        {
                            if (Directory.GetFileSystemEntries(files).Length == 0)
                            {
                                count++;

                                listBox1.Items.Add(files);
                            }

                            progressBar1.Value++;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    label2.Text = "Пустые папки: " + count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
