
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
            this.videoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.addBtn = new System.Windows.Forms.Button();
            this.startup_chk = new System.Windows.Forms.CheckBox();
            this.activateLst = new System.Windows.Forms.ListBox();
            this.deactivateLst = new System.Windows.Forms.ListBox();
            this.upBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.activateBtn = new System.Windows.Forms.Button();
            this.deactivateBtn = new System.Windows.Forms.Button();
            this.activeLbl = new System.Windows.Forms.Label();
            this.inactiveLbl = new System.Windows.Forms.Label();
            this.removeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(12, 412);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(472, 29);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // videoFileDialog
            // 
            this.videoFileDialog.RestoreDirectory = true;
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addBtn.Location = new System.Drawing.Point(157, 319);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(87, 28);
            this.addBtn.TabIndex = 3;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // startup_chk
            // 
            this.startup_chk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startup_chk.AutoSize = true;
            this.startup_chk.Location = new System.Drawing.Point(12, 363);
            this.startup_chk.Name = "startup_chk";
            this.startup_chk.Size = new System.Drawing.Size(123, 24);
            this.startup_chk.TabIndex = 4;
            this.startup_chk.Text = "Run at startup";
            this.startup_chk.UseVisualStyleBackColor = true;
            // 
            // activateLst
            // 
            this.activateLst.FormattingEnabled = true;
            this.activateLst.ItemHeight = 20;
            this.activateLst.Location = new System.Drawing.Point(48, 49);
            this.activateLst.Name = "activateLst";
            this.activateLst.Size = new System.Drawing.Size(150, 264);
            this.activateLst.TabIndex = 5;
            this.activateLst.SelectedIndexChanged += new System.EventHandler(this.activateLst_SelectedIndexChanged);
            // 
            // deactivateLst
            // 
            this.deactivateLst.FormattingEnabled = true;
            this.deactivateLst.ItemHeight = 20;
            this.deactivateLst.Location = new System.Drawing.Point(298, 49);
            this.deactivateLst.Name = "deactivateLst";
            this.deactivateLst.Size = new System.Drawing.Size(150, 264);
            this.deactivateLst.TabIndex = 6;
            this.deactivateLst.SelectedIndexChanged += new System.EventHandler(this.deactivateLst_SelectedIndexChanged);
            // 
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(12, 149);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(30, 30);
            this.upBtn.TabIndex = 7;
            this.upBtn.Text = "▲";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(12, 185);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(30, 30);
            this.downBtn.TabIndex = 8;
            this.downBtn.Text = "▼";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // activateBtn
            // 
            this.activateBtn.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.activateBtn.Location = new System.Drawing.Point(204, 249);
            this.activateBtn.Name = "activateBtn";
            this.activateBtn.Size = new System.Drawing.Size(87, 29);
            this.activateBtn.TabIndex = 9;
            this.activateBtn.Text = "Activate";
            this.activateBtn.UseVisualStyleBackColor = true;
            this.activateBtn.Click += new System.EventHandler(this.activateBtn_Click);
            // 
            // deactivateBtn
            // 
            this.deactivateBtn.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.deactivateBtn.Location = new System.Drawing.Point(204, 284);
            this.deactivateBtn.Name = "deactivateBtn";
            this.deactivateBtn.Size = new System.Drawing.Size(87, 29);
            this.deactivateBtn.TabIndex = 10;
            this.deactivateBtn.Text = "Deactivate";
            this.deactivateBtn.UseVisualStyleBackColor = true;
            this.deactivateBtn.Click += new System.EventHandler(this.deactivateBtn_Click);
            // 
            // activeLbl
            // 
            this.activeLbl.AutoSize = true;
            this.activeLbl.Location = new System.Drawing.Point(48, 26);
            this.activeLbl.Name = "activeLbl";
            this.activeLbl.Size = new System.Drawing.Size(50, 20);
            this.activeLbl.TabIndex = 11;
            this.activeLbl.Text = "Active";
            // 
            // inactiveLbl
            // 
            this.inactiveLbl.AutoSize = true;
            this.inactiveLbl.Location = new System.Drawing.Point(298, 26);
            this.inactiveLbl.Name = "inactiveLbl";
            this.inactiveLbl.Size = new System.Drawing.Size(60, 20);
            this.inactiveLbl.TabIndex = 12;
            this.inactiveLbl.Text = "Inactive";
            // 
            // removeBtn
            // 
            this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeBtn.Location = new System.Drawing.Point(250, 319);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(87, 28);
            this.removeBtn.TabIndex = 13;
            this.removeBtn.Text = "Remove";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 453);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.inactiveLbl);
            this.Controls.Add(this.activeLbl);
            this.Controls.Add(this.deactivateBtn);
            this.Controls.Add(this.activateBtn);
            this.Controls.Add(this.downBtn);
            this.Controls.Add(this.upBtn);
            this.Controls.Add(this.deactivateLst);
            this.Controls.Add(this.activateLst);
            this.Controls.Add(this.startup_chk);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.saveBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.OpenFileDialog videoFileDialog;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.CheckBox startup_chk;
        private System.Windows.Forms.ListBox activateLst;
        private System.Windows.Forms.ListBox deactivateLst;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.Button activateBtn;
        private System.Windows.Forms.Button deactivateBtn;
        private System.Windows.Forms.Label activeLbl;
        private System.Windows.Forms.Label inactiveLbl;
        private System.Windows.Forms.Button removeBtn;
    }
}