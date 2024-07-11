namespace RaspberryPi_IDE
{
    partial class FileMaker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayout_fileButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.label_description = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.button_open = new System.Windows.Forms.Button();
            this.textBox_fileName = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_filePath = new System.Windows.Forms.RichTextBox();
            this.button_explorePath = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_extension = new System.Windows.Forms.RichTextBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_fileName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_extension = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayout_fileButtons);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel2.Controls.Add(this.label_description);
            this.splitContainer1.Panel2.Controls.Add(this.label_name);
            this.splitContainer1.Size = new System.Drawing.Size(725, 403);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.TabIndex = 4;
            // 
            // flowLayout_fileButtons
            // 
            this.flowLayout_fileButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayout_fileButtons.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayout_fileButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayout_fileButtons.Location = new System.Drawing.Point(3, 0);
            this.flowLayout_fileButtons.Name = "flowLayout_fileButtons";
            this.flowLayout_fileButtons.Size = new System.Drawing.Size(395, 400);
            this.flowLayout_fileButtons.TabIndex = 0;
            // 
            // label_description
            // 
            this.label_description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_description.Location = new System.Drawing.Point(4, 37);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(313, 363);
            this.label_description.TabIndex = 1;
            this.label_description.Text = "description";
            // 
            // label_name
            // 
            this.label_name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_name.Location = new System.Drawing.Point(4, 4);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(313, 33);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "name";
            // 
            // button_open
            // 
            this.button_open.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_open.BackColor = System.Drawing.SystemColors.Control;
            this.button_open.Location = new System.Drawing.Point(647, 5);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(75, 29);
            this.button_open.TabIndex = 1;
            this.button_open.Text = "create";
            this.button_open.UseVisualStyleBackColor = false;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // textBox_fileName
            // 
            this.textBox_fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_fileName.Location = new System.Drawing.Point(103, 6);
            this.textBox_fileName.Name = "textBox_fileName";
            this.textBox_fileName.Size = new System.Drawing.Size(441, 28);
            this.textBox_fileName.TabIndex = 2;
            this.textBox_fileName.Text = "";
            this.textBox_fileName.TextChanged += new System.EventHandler(this.textBox_fileName_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label_extension);
            this.panel3.Controls.Add(this.label_fileName);
            this.panel3.Controls.Add(this.textBox_fileName);
            this.panel3.Controls.Add(this.button_open);
            this.panel3.Location = new System.Drawing.Point(0, 500);
            this.panel3.MinimumSize = new System.Drawing.Size(450, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(728, 44);
            this.panel3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_filePath);
            this.panel1.Controls.Add(this.button_explorePath);
            this.panel1.Location = new System.Drawing.Point(0, 456);
            this.panel1.MinimumSize = new System.Drawing.Size(450, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 44);
            this.panel1.TabIndex = 4;
            // 
            // textBox_filePath
            // 
            this.textBox_filePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_filePath.Location = new System.Drawing.Point(103, 6);
            this.textBox_filePath.Name = "textBox_filePath";
            this.textBox_filePath.Size = new System.Drawing.Size(576, 28);
            this.textBox_filePath.TabIndex = 2;
            this.textBox_filePath.Text = "";
            // 
            // button_explorePath
            // 
            this.button_explorePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_explorePath.BackColor = System.Drawing.SystemColors.Control;
            this.button_explorePath.Location = new System.Drawing.Point(685, 5);
            this.button_explorePath.Name = "button_explorePath";
            this.button_explorePath.Size = new System.Drawing.Size(37, 29);
            this.button_explorePath.TabIndex = 1;
            this.button_explorePath.Text = "...";
            this.button_explorePath.UseVisualStyleBackColor = false;
            this.button_explorePath.Click += new System.EventHandler(this.button_explorePath_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBox_extension);
            this.panel2.Controls.Add(this.button_cancel);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.MinimumSize = new System.Drawing.Size(450, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(722, 51);
            this.panel2.TabIndex = 5;
            // 
            // textBox_extension
            // 
            this.textBox_extension.Location = new System.Drawing.Point(100, 11);
            this.textBox_extension.Name = "textBox_extension";
            this.textBox_extension.Size = new System.Drawing.Size(96, 28);
            this.textBox_extension.TabIndex = 2;
            this.textBox_extension.Text = "";
            this.textBox_extension.TextChanged += new System.EventHandler(this.textBox_extension_TextChanged);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.BackColor = System.Drawing.SystemColors.Control;
            this.button_cancel.Location = new System.Drawing.Point(644, 13);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 29);
            this.button_cancel.TabIndex = 0;
            this.button_cancel.Text = "cancel";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label_fileName
            // 
            this.label_fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_fileName.AutoSize = true;
            this.label_fileName.Location = new System.Drawing.Point(13, 9);
            this.label_fileName.Name = "label_fileName";
            this.label_fileName.Size = new System.Drawing.Size(84, 20);
            this.label_fileName.TabIndex = 3;
            this.label_fileName.Text = "File Name:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "File Path:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Ending:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label_extension
            // 
            this.label_extension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_extension.Location = new System.Drawing.Point(550, 9);
            this.label_extension.Name = "label_extension";
            this.label_extension.Size = new System.Drawing.Size(91, 20);
            this.label_extension.TabIndex = 4;
            this.label_extension.Text = ".extension";
            // 
            // FileMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(728, 544);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel3);
            this.MaximumSize = new System.Drawing.Size(750, 600);
            this.MinimumSize = new System.Drawing.Size(475, 600);
            this.Name = "FileMaker";
            this.Text = "Form2";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayout_fileButtons;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.RichTextBox textBox_fileName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox textBox_filePath;
        private System.Windows.Forms.Button button_explorePath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox textBox_extension;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label_fileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_extension;
    }
}