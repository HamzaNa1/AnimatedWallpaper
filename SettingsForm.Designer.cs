
namespace AnimatedWallpaper
{
    partial class SettingsForm
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
            this.saveBtn = new System.Windows.Forms.Button();
            this.urlLabel = new System.Windows.Forms.Label();
            this.videoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.urlText = new System.Windows.Forms.TextBox();
            this.selectBtn = new System.Windows.Forms.Button();
            this.startup_chk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(12, 160);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(500, 29);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(12, 28);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(109, 20);
            this.urlLabel.TabIndex = 1;
            this.urlLabel.Text = "Video Location";
            // 
            // videoFileDialog
            // 
            this.videoFileDialog.Filter = "Video files (*.mp4)|*.mp4";
            this.videoFileDialog.RestoreDirectory = true;
            // 
            // urlText
            // 
            this.urlText.Location = new System.Drawing.Point(128, 28);
            this.urlText.Name = "urlText";
            this.urlText.ReadOnly = true;
            this.urlText.Size = new System.Drawing.Size(284, 27);
            this.urlText.TabIndex = 2;
            // 
            // selectBtn
            // 
            this.selectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBtn.Location = new System.Drawing.Point(418, 28);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(94, 29);
            this.selectBtn.TabIndex = 3;
            this.selectBtn.Text = "Select";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // startup_chk
            // 
            this.startup_chk.AutoSize = true;
            this.startup_chk.Location = new System.Drawing.Point(12, 79);
            this.startup_chk.Name = "startup_chk";
            this.startup_chk.Size = new System.Drawing.Size(123, 24);
            this.startup_chk.TabIndex = 4;
            this.startup_chk.Text = "Run at startup";
            this.startup_chk.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 201);
            this.Controls.Add(this.startup_chk);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.urlText);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.saveBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.OpenFileDialog videoFileDialog;
        private System.Windows.Forms.TextBox urlText;
        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.CheckBox startup_chk;
    }
}