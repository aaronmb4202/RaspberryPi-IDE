using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace RaspberryPi_IDE
{
    public partial class DirectoryBrowser : Form
    {
        private SftpClient _sftpClient;

        public string SelectedFilePath { get; private set; }

        public DirectoryBrowser(SftpClient sftpClient, string path)
        {
            InitializeComponent();

            _sftpClient = sftpClient;

            SelectedFilePath = path;

            LoadDirectory("/", treeView1.Nodes);
        }

        private void LoadDirectory(string dir, TreeNodeCollection nodes)
        {
            try
            {
                var files = _sftpClient.ListDirectory(dir);
                foreach (var file in files)
                {
                    if (file.Name != "." && file.Name != "..")
                    {
                        var node = new TreeNode(file.Name)
                        {
                            Tag = file,
                            ImageKey = file.IsDirectory ? "folder" : "file",
                            SelectedImageKey = file.IsDirectory ? "folder" : "file"
                        };
                        nodes.Add(node);
                        if (file.IsDirectory)
                        {
                            node.Nodes.Add(new TreeNode()); // Placeholder for expanding
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading directory: {ex.Message}");
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            var file = (SftpFile)node.Tag;
            if (file.IsDirectory)
            {
                // Directory double-clicked: Load the directory content
                node.Nodes.Clear();
                LoadDirectory(file.FullName, node.Nodes);
            }
            else
            {
                // File double-clicked: Set the selected file path and close the dialog
                SelectedFilePath = file.FullName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
            {
                node.Nodes.Clear();
                var file = (SftpFile)node.Tag;
                LoadDirectory(file.FullName, node.Nodes);
            }
        }
    }
}
