using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspberryPi_IDE
{
    public partial class FileMaker : Form
    {
        private SftpClient _sftpClient = null;

        private Button _selectedButton = null;

        private string _fileName = null;
        private string _fileEnding = null;

        public string Path { get; private set; }
        public string Contents { get; private set; }
        public string FileName { get; private set; }

        public FileMaker(SftpClient stfpClient, string path)
        {
            InitializeComponent();
            CreateButtons();

            FileName = "NewFile";
            Contents = "";

            _sftpClient = stfpClient;
            UpdateFilePath(path, true);
        }

        private void CreateButtons()
        {
            var buttonDataList = new List<FileTypeData>
            {
                new FileTypeData { Name = "Blank", Description = "A blank file with no file ending, can be opened with a text editor.", FileEnding = "" },
                new FileTypeData { Name = "Text", Description = "A typical file type for creating text documents.", FileEnding = ".txt" },
                new FileTypeData { Name = "C Script", Description = "A file for the C scripting language.", FileEnding = ".c" },
                new FileTypeData { Name = "C# Script", Description = "A file for the C# scripting language.", FileEnding = ".c" },
                new FileTypeData { Name = "C++ Script", Description = "A file for the C++ scripting language.", FileEnding = ".cpp" },
                new FileTypeData { Name = "Python Script", Description = "A file for the Python scripting language.", FileEnding = ".py" },
                new FileTypeData { Name = "Javascript Script", Description = "A file for the C language.", FileEnding = ".js" },
                new FileTypeData { Name = "Shell Script", Description = "A file for shell commands on a linux OS.", FileEnding = ".sh" },

                new FileTypeData { Name = "Dockerfile", Description = "Blank file named for running Docker images.", FileEnding = ".c" }
            };

            foreach (var data in buttonDataList)
            {
                var button = new Button
                {
                    Text = $"{data.Name} - {data.FileEnding}",
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = true,
                    Margin = new Padding(5),
                    Tag = data
                };

                button.Click += Button_Click;

                flowLayout_fileButtons.Controls.Add(button);
                SelectButton(flowLayout_fileButtons.Controls[0] as Button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            SelectButton(button);
        }

        private void SelectButton(Button button)
        {
            if (_selectedButton != null)
            {
                _selectedButton.BackColor = SystemColors.Control;
            }

            _selectedButton = button;
            _selectedButton.BackColor = Color.LightBlue;

            var data = _selectedButton.Tag as FileTypeData;

            UpdateFileName("NewFile", true);

            _fileEnding = data.FileEnding;
            UpdateFileEnding(data.FileEnding, true);
            
            label_description.Text = data.Description;

            FileName = _fileName + _fileEnding;
        }

        private void UpdateFilePath(string path, bool updateTextBox)
        {
            // check if path is valid

            Path = path;
            
            if (updateTextBox)
            {
                textBox_filePath.Text = Path;
            }
        }

        private void UpdateFileName(string name, bool updateTextBox)
        {
            _fileName = name;
            label_name.Text = _fileName;

            if (updateTextBox == true)
            {
                textBox_fileName.Text = name;
            }
        }

        private void UpdateFileEnding(string ending, bool updateTextBox)
        {
            if (!ending.StartsWith("."))
            {
                ending = "." + ending;
            }

            _fileEnding = ending;

            label_extension.Text = _fileEnding;

            if (updateTextBox)
            {
                textBox_extension.Text = _fileEnding;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_extension_TextChanged(object sender, EventArgs e)
        {
            UpdateFileEnding(textBox_extension.Text, false);
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            CompleteForm();
            this.Close();
        }

        private void CompleteForm()
        {
            if (_fileName == null)
            {
                _fileName = "FileName";
            }

            if (!_fileEnding.StartsWith("."))
            {
                _fileEnding = "." + _fileEnding;
            }

            FileName = _fileName + _fileEnding;

            if (Path == null)
            {
                UpdateFilePath("/", false);
            }

            DialogResult = DialogResult.OK;
        }

        private void button_explorePath_Click(object sender, EventArgs e)
        {
            using (var fileBrowser = new DirectoryBrowser(_sftpClient, Path))
            {
                if (fileBrowser.ShowDialog() == DialogResult.OK)
                {
                    Path = fileBrowser.SelectedFilePath;
                }
            }
        }

        private void textBox_fileName_TextChanged(object sender, EventArgs e)
        {
            UpdateFileName(textBox_fileName.Text, false);
        }
    }
}
