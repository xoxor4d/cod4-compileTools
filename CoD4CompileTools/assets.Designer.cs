namespace CoD4CompileTools
{
    partial class assets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(assets));
            this.btnSaveLevelCSV = new ns1.BunifuFlatButton();
            this.lblLevelCSV = new ns1.BunifuCustomLabel();
            this.btn_close = new ns1.BunifuImageButton();
            this.txtLevelCSV = new System.Windows.Forms.RichTextBox();
            this.txtMissingAssets = new System.Windows.Forms.RichTextBox();
            this.bunifuSeparator5 = new ns1.BunifuSeparator();
            this.bunifuSeparator1 = new ns1.BunifuSeparator();
            this.bunifuCustomLabel9 = new ns1.BunifuCustomLabel();
            this.bunifuCustomLabel1 = new ns1.BunifuCustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveLevelCSV
            // 
            this.btnSaveLevelCSV.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(129)))), ((int)(((byte)(100)))));
            this.btnSaveLevelCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.btnSaveLevelCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveLevelCSV.BorderRadius = 0;
            this.btnSaveLevelCSV.ButtonText = "Save Zone";
            this.btnSaveLevelCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveLevelCSV.DisabledColor = System.Drawing.Color.Gray;
            this.btnSaveLevelCSV.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSaveLevelCSV.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnSaveLevelCSV.Iconimage")));
            this.btnSaveLevelCSV.Iconimage_right = null;
            this.btnSaveLevelCSV.Iconimage_right_Selected = null;
            this.btnSaveLevelCSV.Iconimage_Selected = null;
            this.btnSaveLevelCSV.IconMarginLeft = 0;
            this.btnSaveLevelCSV.IconMarginRight = 0;
            this.btnSaveLevelCSV.IconRightVisible = false;
            this.btnSaveLevelCSV.IconRightZoom = 0D;
            this.btnSaveLevelCSV.IconVisible = false;
            this.btnSaveLevelCSV.IconZoom = 60D;
            this.btnSaveLevelCSV.IsTab = false;
            this.btnSaveLevelCSV.Location = new System.Drawing.Point(23, 695);
            this.btnSaveLevelCSV.Name = "btnSaveLevelCSV";
            this.btnSaveLevelCSV.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.btnSaveLevelCSV.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(129)))), ((int)(((byte)(100)))));
            this.btnSaveLevelCSV.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSaveLevelCSV.selected = false;
            this.btnSaveLevelCSV.Size = new System.Drawing.Size(602, 30);
            this.btnSaveLevelCSV.TabIndex = 169;
            this.btnSaveLevelCSV.Text = "Save Zone";
            this.btnSaveLevelCSV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSaveLevelCSV.Textcolor = System.Drawing.Color.White;
            this.btnSaveLevelCSV.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveLevelCSV.Click += new System.EventHandler(this.btnSaveLevelCSV_Click);
            // 
            // lblLevelCSV
            // 
            this.lblLevelCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLevelCSV.AutoSize = true;
            this.lblLevelCSV.BackColor = System.Drawing.Color.Transparent;
            this.lblLevelCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelCSV.ForeColor = System.Drawing.Color.Silver;
            this.lblLevelCSV.Location = new System.Drawing.Point(19, 55);
            this.lblLevelCSV.Name = "lblLevelCSV";
            this.lblLevelCSV.Size = new System.Drawing.Size(138, 20);
            this.lblLevelCSV.TabIndex = 181;
            this.lblLevelCSV.Text = " +Zone File Path +";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Image = global::CoD4CompileTools.Properties.Resources.close_new;
            this.btn_close.ImageActive = null;
            this.btn_close.Location = new System.Drawing.Point(600, 23);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(25, 25);
            this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_close.TabIndex = 185;
            this.btn_close.TabStop = false;
            this.btn_close.Zoom = 10;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // txtLevelCSV
            // 
            this.txtLevelCSV.AcceptsTab = true;
            this.txtLevelCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.txtLevelCSV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLevelCSV.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLevelCSV.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtLevelCSV.Location = new System.Drawing.Point(332, 121);
            this.txtLevelCSV.Name = "txtLevelCSV";
            this.txtLevelCSV.Size = new System.Drawing.Size(293, 561);
            this.txtLevelCSV.TabIndex = 188;
            this.txtLevelCSV.Text = "";
            this.txtLevelCSV.WordWrap = false;
            // 
            // txtMissingAssets
            // 
            this.txtMissingAssets.AcceptsTab = true;
            this.txtMissingAssets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.txtMissingAssets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMissingAssets.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMissingAssets.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtMissingAssets.Location = new System.Drawing.Point(23, 121);
            this.txtMissingAssets.Name = "txtMissingAssets";
            this.txtMissingAssets.Size = new System.Drawing.Size(293, 561);
            this.txtMissingAssets.TabIndex = 189;
            this.txtMissingAssets.Text = "";
            this.txtMissingAssets.WordWrap = false;
            // 
            // bunifuSeparator5
            // 
            this.bunifuSeparator5.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.bunifuSeparator5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.bunifuSeparator5.LineThickness = 30;
            this.bunifuSeparator5.Location = new System.Drawing.Point(332, 90);
            this.bunifuSeparator5.Name = "bunifuSeparator5";
            this.bunifuSeparator5.Size = new System.Drawing.Size(293, 30);
            this.bunifuSeparator5.TabIndex = 190;
            this.bunifuSeparator5.Transparency = 255;
            this.bunifuSeparator5.Vertical = false;
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.bunifuSeparator1.LineThickness = 30;
            this.bunifuSeparator1.Location = new System.Drawing.Point(23, 90);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(293, 30);
            this.bunifuSeparator1.TabIndex = 191;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // bunifuCustomLabel9
            // 
            this.bunifuCustomLabel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.bunifuCustomLabel9.Font = new System.Drawing.Font("Segoe UI Semilight", 14F);
            this.bunifuCustomLabel9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.bunifuCustomLabel9.Location = new System.Drawing.Point(23, 90);
            this.bunifuCustomLabel9.Name = "bunifuCustomLabel9";
            this.bunifuCustomLabel9.Size = new System.Drawing.Size(293, 30);
            this.bunifuCustomLabel9.TabIndex = 192;
            this.bunifuCustomLabel9.Text = "++ Missing Assets ++";
            this.bunifuCustomLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Segoe UI Semilight", 14F);
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(332, 90);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(293, 30);
            this.bunifuCustomLabel1.TabIndex = 193;
            this.bunifuCustomLabel1.Text = "++ Map Zone File ++";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // assets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 748);
            this.ControlBox = false;
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.bunifuCustomLabel9);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.bunifuSeparator5);
            this.Controls.Add(this.txtMissingAssets);
            this.Controls.Add(this.txtLevelCSV);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.lblLevelCSV);
            this.Controls.Add(this.btnSaveLevelCSV);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "assets";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Zone File Editor";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.assets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ns1.BunifuFlatButton btnSaveLevelCSV;
        private ns1.BunifuCustomLabel lblLevelCSV;
        private ns1.BunifuImageButton btn_close;
        private System.Windows.Forms.RichTextBox txtLevelCSV;
        private System.Windows.Forms.RichTextBox txtMissingAssets;
        private ns1.BunifuSeparator bunifuSeparator5;
        private ns1.BunifuSeparator bunifuSeparator1;
        private ns1.BunifuCustomLabel bunifuCustomLabel9;
        private ns1.BunifuCustomLabel bunifuCustomLabel1;
    }
}