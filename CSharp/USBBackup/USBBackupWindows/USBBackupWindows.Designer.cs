namespace USBBackupWindows
{
    partial class USBBackupWindows
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
            this.button1 = new System.Windows.Forms.Button();
            this.DriversList = new System.Windows.Forms.ListBox();
            this.DirectorysList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Lista";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DriversList
            // 
            this.DriversList.FormattingEnabled = true;
            this.DriversList.Location = new System.Drawing.Point(93, 12);
            this.DriversList.Name = "DriversList";
            this.DriversList.Size = new System.Drawing.Size(207, 238);
            this.DriversList.TabIndex = 1;
            this.DriversList.DoubleClick += new System.EventHandler(this.DriversList_DoubleClick);
            // 
            // DirectorysList
            // 
            this.DirectorysList.FormattingEnabled = true;
            this.DirectorysList.Location = new System.Drawing.Point(306, 12);
            this.DirectorysList.Name = "DirectorysList";
            this.DirectorysList.Size = new System.Drawing.Size(207, 238);
            this.DirectorysList.TabIndex = 2;
            // 
            // USBBackupWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 262);
            this.Controls.Add(this.DirectorysList);
            this.Controls.Add(this.DriversList);
            this.Controls.Add(this.button1);
            this.Name = "USBBackupWindows";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox DriversList;
        private System.Windows.Forms.ListBox DirectorysList;
    }
}

