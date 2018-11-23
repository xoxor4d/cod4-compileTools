namespace CoD4CompileTools
{
    partial class msgbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(msgbox));
            this.msgTextbox = new System.Windows.Forms.RichTextBox();
            this.btn_OK = new ns1.BunifuFlatButton();
            this.btn_CANCEL = new ns1.BunifuFlatButton();
            this.SuspendLayout();
            // 
            // msgTextbox
            // 
            this.msgTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.msgTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgTextbox.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.msgTextbox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.msgTextbox.Location = new System.Drawing.Point(23, 73);
            this.msgTextbox.Name = "msgTextbox";
            this.msgTextbox.ReadOnly = true;
            this.msgTextbox.Size = new System.Drawing.Size(379, 80);
            this.msgTextbox.TabIndex = 146;
            this.msgTextbox.Text = "msg here";
            // 
            // btn_OK
            // 
            this.btn_OK.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_OK.BorderRadius = 0;
            this.btn_OK.ButtonText = "OK";
            this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OK.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_OK.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_OK.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_OK.Iconimage")));
            this.btn_OK.Iconimage_right = null;
            this.btn_OK.Iconimage_right_Selected = null;
            this.btn_OK.Iconimage_Selected = null;
            this.btn_OK.IconMarginLeft = 0;
            this.btn_OK.IconMarginRight = 0;
            this.btn_OK.IconRightVisible = false;
            this.btn_OK.IconRightZoom = 0D;
            this.btn_OK.IconVisible = false;
            this.btn_OK.IconZoom = 60D;
            this.btn_OK.IsTab = false;
            this.btn_OK.Location = new System.Drawing.Point(23, 178);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_OK.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_OK.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_OK.selected = false;
            this.btn_OK.Size = new System.Drawing.Size(184, 30);
            this.btn_OK.TabIndex = 154;
            this.btn_OK.Text = "OK";
            this.btn_OK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_OK.Textcolor = System.Drawing.Color.White;
            this.btn_OK.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_CANCEL
            // 
            this.btn_CANCEL.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_CANCEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_CANCEL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_CANCEL.BorderRadius = 0;
            this.btn_CANCEL.ButtonText = "CANCEL";
            this.btn_CANCEL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_CANCEL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_CANCEL.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_CANCEL.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_CANCEL.Iconimage")));
            this.btn_CANCEL.Iconimage_right = null;
            this.btn_CANCEL.Iconimage_right_Selected = null;
            this.btn_CANCEL.Iconimage_Selected = null;
            this.btn_CANCEL.IconMarginLeft = 0;
            this.btn_CANCEL.IconMarginRight = 0;
            this.btn_CANCEL.IconRightVisible = false;
            this.btn_CANCEL.IconRightZoom = 0D;
            this.btn_CANCEL.IconVisible = false;
            this.btn_CANCEL.IconZoom = 60D;
            this.btn_CANCEL.IsTab = false;
            this.btn_CANCEL.Location = new System.Drawing.Point(218, 178);
            this.btn_CANCEL.Name = "btn_CANCEL";
            this.btn_CANCEL.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_CANCEL.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_CANCEL.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_CANCEL.selected = false;
            this.btn_CANCEL.Size = new System.Drawing.Size(184, 30);
            this.btn_CANCEL.TabIndex = 155;
            this.btn_CANCEL.Text = "CANCEL";
            this.btn_CANCEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_CANCEL.Textcolor = System.Drawing.Color.White;
            this.btn_CANCEL.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CANCEL.Click += new System.EventHandler(this.btn_CANCEL_Click);
            // 
            // msgbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 231);
            this.ControlBox = false;
            this.Controls.Add(this.btn_CANCEL);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.msgTextbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "msgbox";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Information:";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox msgTextbox;
        private ns1.BunifuFlatButton btn_OK;
        private ns1.BunifuFlatButton btn_CANCEL;
    }
}