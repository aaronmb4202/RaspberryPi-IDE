using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace RaspberryPi_IDE
{
    public partial class Form1 : Form
    {
        const string DISCONNECTED = "DISCONNECTED";

        private SftpClient _sftpClient;
        private SshClient _client;
        private ShellStream _shellStream;

        private string _hostName = null;
        private string _userName = null;
        private int _port = 0;

        private string _selectedPath = null;
        private string _explorerPath = null;

        private string _defaultPath = "/";

        private Dictionary<string, TabPage> _openTabs = new Dictionary<string, TabPage>();
        private TabPage _currentTab;

        private IContainer _components = null;

        public SftpClient SftpClient => _sftpClient;
        public string ExplorerPath => _explorerPath;

        public Form1()
        {
            InitializeComponent();

            terminalTextBoxOutput.ReadOnly = true;
            textBox1.ReadOnly = true;

            SetDisconnected();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectClient();
        }

        private void SetConnected(string host)
        {
            Color cColor = Color.FromArgb(0, 255, 127);

            textBox1.Text = $"Connected to {host}!";
            textBox1.BackColor = cColor;

            panel3.BackColor = cColor;

            terminalTextBoxOutput.Clear();
            terminalTextBoxInput.Clear();

            terminalTextBoxInput.ReadOnly = false;

            SetToolbarConnected(true);
            UpdateExplorerToolbar(true);
        }

        private void SetDisconnected()
        {
            DisconnectClient();

            Color dColor = Color.FromArgb(153, 180, 209);

            textBox1.Text = DISCONNECTED;
            textBox1.BackColor = dColor;

            panel3.BackColor = dColor;

            terminalTextBoxOutput.Clear();
            terminalTextBoxOutput.Text = "No device is connected.";
            terminalTextBoxInput.ReadOnly = true;

            _explorerPath = null;

            SetToolbarConnected(false);
            UpdateExplorerToolbar(false);
        }

        private void UpdateExplorerToolbar(bool connected)
        {
            if (connected)
            {
                SetExplorerPath(_defaultPath);
                //enable buttons
            }
            else
            {
                _explorerPath = null;

                textBox_explorerPath.Text = string.Empty;
                // disable buttons
            }
        }

        private void SetExplorerPath(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                _explorerPath = directoryPath;
            }
            else
            {
                MessageBox.Show($"'{directoryPath}' does not exist.");
                _explorerPath = "/";
            }

            textBox_explorerPath.Text = _explorerPath;
        }

        private bool IsPathInFolder(string filePath, string folderPath)
        {
            try
            {
                var fileFullPath = Path.GetFullPath(filePath);
                var folderFullPath = Path.GetFullPath(folderPath);

                return fileFullPath.StartsWith(folderFullPath, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error verifying path: {ex.Message}");
                return false;
            }
        }

        private void SetToolbarConnected(bool connected)
        {
            bool connectedTrue = connected ? true : false;
            bool connectedFalse = connected ? false : true;

            saveToolStripMenuItem.Enabled = connectedTrue;
            saveAsToolStripMenuItem.Enabled = connectedTrue;

            openFileToolStripMenuItem.Enabled = connectedTrue;

            newDirectoryToolStripMenuItem.Enabled = connectedTrue;
            openDirectoryToolStripMenuItem.Enabled = connectedTrue;
            closeToolStripMenuItem.Enabled = connectedTrue;

            undoToolStripMenuItem.Enabled = connectedTrue;
            redoToolStripMenuItem.Enabled = connectedTrue;
            copyToolStripMenuItem.Enabled = connectedTrue;
            pasteToolStripMenuItem.Enabled = connectedTrue;
            deleteToolStripMenuItem.Enabled = connectedTrue;

            cloneRepositoryToolStripMenuItem.Enabled = connectedTrue;
            clonetToHomeToolStripMenuItem.Enabled = connectedTrue;
            cloneToToolStripMenuItem.Enabled = connectedTrue;

            runScriptInTerminalToolStripMenuItem.Enabled = connectedTrue;

            newSSHToolStripMenuItem.Enabled = connectedFalse;
            existingSSHToolStripMenuItem.Enabled = connectedFalse;
        }

        private void DisconnectClient()
        {
            if (IsConnected)
            {
                _client.Disconnect();
            }
        }

        private bool IsConnected => _client != null && _client.IsConnected;

        private void UpdateOutput(string text)
        {
            text = StripAnsiEscapeCodes(text); // Strip ANSI codes

            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateOutput), text);
                return;
            }

            terminalTextBoxOutput.AppendText(text);
            terminalTextBoxOutput.ScrollToCaret();  // Ensure the latest text is visible
        }

        // Strip ANSI escape sequences
        private string StripAnsiEscapeCodes(string text)
        {
            return Regex.Replace(text, @"\x1B\[([0-9;]*[mG])", string.Empty);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl_files.SelectedIndexChanged += TabControlFiles_SelectedIndexChanged;
        }

        private void TabControlFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_files.SelectedTab != null)
            {
                _currentTab = tabControl_files.SelectedTab;
                foreach (var file in _openTabs)
                {
                    if (file.Value == tabControl_files.SelectedTab)
                    {
                        _selectedPath = file.Key;
                        
                    }
                }
            }

            UpdateFileDisplay();
        }

        private void UpdateFileDisplay()
        {
            if (_currentTab != null)
            {
                textBox_filePath.Text = _currentTab.Tag.ToString();
                textBox_fileName.Text = Path.GetFileName(_currentTab.Tag.ToString());
            }
            else
            {
                textBox_filePath.Text = string.Empty;
                textBox_fileName.Text = string.Empty;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void newSSHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sshForm = new NewSSHForm())
            {
                if (sshForm.ShowDialog() == DialogResult.OK)
                {
                    ConnectToSSH(sshForm.Hostname, sshForm.Username, sshForm.Password, sshForm.Port);
                }
            }
        }

        private void OpenFileMaker(string path)
        {
            using (var fileMaker = new FileMaker(_sftpClient, path))
            {
                if (fileMaker.ShowDialog() == DialogResult.OK)
                {
                    CreateFile(fileMaker.Path, fileMaker.Contents);
                }
            }
        }

        private void ConnectToSSH(string hostname, string username, string password, int port)
        {
            try
            {
                _client = new SshClient(hostname, port, username, password);
                _client.Connect();

                _sftpClient = new SftpClient(hostname, port, username, password);
                _sftpClient.Connect();

                if (_client.IsConnected)
                {
                    _hostName = hostname;
                    _userName = username;
                    _port = port;

                    SetConnected(_hostName);

                    _shellStream = _client.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
                    Task.Run(() => ReadShellStream());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                SetDisconnected();
                terminalTextBoxOutput.Text = ex.Message;
            }
        }

        private async Task ReadShellStream()
        {
            try
            {
                var buffer = new byte[1024];
                while (_client.IsConnected && _shellStream.CanRead)
                {
                    var bytesRead = await _shellStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        var output = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        UpdateOutput(output);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateOutput($"Error: {ex.Message}");
            }
        }

        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string command = terminalTextBoxInput.Text.Trim();
                if (!IsConnected)
                {
                    terminalTextBoxInput.Clear();
                }
                else
                {
                    if (command == "clear")
                    {
                        terminalTextBoxOutput.Clear();
                    }
                    else if (!string.IsNullOrEmpty(command) && _shellStream != null && _shellStream.CanWrite)
                    {
                        _shellStream.WriteLine(command);
                        _shellStream.Flush();  // Ensure the command is sent immediately
                        
                    }
                    e.SuppressKeyPress = true; // Prevents the ding sound
                }

                terminalTextBoxInput.Clear();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisconnectFromDevice();
        }

        private void DisconnectFromDevice()
        {
            if (IsConnected && _hostName != null)
            {
                DialogResult dialogResult = MessageBox.Show($"Are you sure you close connection with {_hostName}?", "Close Connection", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    SetDisconnected();
                }
            }
        }

        // Save button on left side toolbar
        private void button_save_Click(object sender, EventArgs e)
        {
            var remoteFilePath = _selectedPath;
            if (!string.IsNullOrEmpty(remoteFilePath))
            {
                SaveFile(remoteFilePath);
            }
            else
            {
                MessageBox.Show("Please enter a remote file path.");
            }
        }

        // Method to open an existing file
        private void OpenFile(string filePath)
        {
            if (_openTabs.ContainsKey(filePath))
            {
                tabControl_files.SelectedTab = _openTabs[filePath];
                return;
            }

            try
            {
                var fileContent = string.Empty;
                using (var fileStream = SftpClient.OpenRead(filePath))
                using (var reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }

                CreateFileTab(filePath, fileContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}");
            }
        }

        private void CreateFileTab(string filePath, string fileContent)
        {
            var tabPage = new TabPage(Path.GetFileName(filePath))
            {
                Tag = filePath
            };
            var richTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Text = fileContent,
                Font = new System.Drawing.Font("Consolas", 10)
            };
            richTextBox.TextChanged += (s, ev) => MarkTabAsUnsaved(tabPage);

            tabPage.Controls.Add(richTextBox);
            tabControl_files.TabPages.Add(tabPage);
            tabControl_files.SelectedTab = tabPage;
            _openTabs[filePath] = tabPage;
            _currentTab = tabPage;

            UpdateFileDisplay();
        }

        // Method to create a new file and save it to a designated path
        private void CreateFile(string filePath, string fileContent = "")
        {
            try
            {
                using (var fileStream = SftpClient.Create(filePath))
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(fileContent);
                }

                CreateFileTab(filePath, fileContent);
                MessageBox.Show("File created and saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating file: {ex.Message}");
            }
        }

        // Save file referencing given file path
        private void SaveFile(string filePath)
        {
            try
            {
                if (_openTabs.ContainsKey(filePath))
                {
                    var tabPage = _openTabs[filePath];
                    var richTextBox = tabPage.Controls[0] as RichTextBox;
                    var fileContent = richTextBox.Text;

                    using (var fileStream = SftpClient.OpenWrite(filePath))
                    using (var writer = new StreamWriter(fileStream))
                    {
                        writer.Write(fileContent);
                    }

                    MarkTabAsSaved(tabPage);
                    MessageBox.Show("File saved successfully.");
                }
                else
                {
                    // Handle new file save
                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            var tabPage = _currentTab;
                            var richTextBox = tabPage.Controls[0] as RichTextBox;
                            var fileContent = richTextBox.Text;
                            var savePath = saveFileDialog.FileName;

                            using (var fileStream = SftpClient.Create(savePath))
                            using (var writer = new StreamWriter(fileStream))
                            {
                                writer.Write(fileContent);
                            }

                            tabPage.Tag = savePath;
                            tabPage.Text = Path.GetFileName(savePath);
                            _openTabs[savePath] = tabPage;

                            MarkTabAsSaved(tabPage);
                            MessageBox.Show("File saved successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }
        }

        // Close tab referencing given file path
        private void CloseTab(string filePath)
        {
            if (_openTabs.ContainsKey(filePath))
            {
                var tabPage = _openTabs[filePath];
                if (tabPage.Text.EndsWith("(unsaved)"))
                {
                    var result = MessageBox.Show("You have unsaved changes. Do you want to save them before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel) return;
                    if (result == DialogResult.Yes) SaveFile(filePath);
                }

                tabControl_files.TabPages.Remove(tabPage);
                _openTabs.Remove(filePath);
                _currentTab = tabControl_files.TabPages.Count > 0 ? tabControl_files.SelectedTab : null;
            }

            UpdateFileDisplay();
        }

        // Close tab button on left hand side
        private void button4_Click(object sender, EventArgs e)
        {
            if (_currentTab != null)
            {
                CloseTab(_currentTab.Tag.ToString());
            }
            else
            {
                MessageBox.Show("No file is currently open.");
            }
        }

        // Save current file on toolstrip; File > Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(_selectedPath);
        }

        // New file on toolstrip; File > New.. > File
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileMaker(_explorerPath);
        }

        // Mark open tab as unsaved
        private void MarkTabAsUnsaved(TabPage tabPage)
        {
            if (!tabPage.Text.EndsWith("(unsaved)"))
            {
                tabPage.Text += " (unsaved)";
            }
        }

        // Mark open tab as saved
        private void MarkTabAsSaved(TabPage tabPage)
        {
            if (tabPage.Text.EndsWith("(unsaved)"))
            {
                tabPage.Text = tabPage.Text.Replace(" (unsaved)", "");
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SftpClient != null && SftpClient.IsConnected)
            {
                using (var fileBrowser = new DirectoryBrowser(_sftpClient, _selectedPath))
                {
                    if (fileBrowser.ShowDialog() == DialogResult.OK)
                    {
                        _selectedPath = fileBrowser.SelectedFilePath;
                        OpenFile(_selectedPath);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please connect to the server first.");
            }
        }

        private void button_connectedDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectFromDevice();
        }

        private void button_fileGoToPath_Click(object sender, EventArgs e)
        {
            SetExplorerPath(_selectedPath);
        }

        private void button_explorerCreateFile_Click(object sender, EventArgs e)
        {
            OpenFileMaker(_explorerPath);
        }
    }
}
