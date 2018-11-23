using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace CoD4CompileTools
{
    public partial class msgbox : MetroForm
    {
        public string msgbox_msg { get; set; }

        public msgbox(string msgbox_msg, bool okOnly = false)
        {
            InitializeComponent();
            
            this.msgTextbox.Text = msgbox_msg;

            if(okOnly)
            {
                btn_CANCEL.Enabled = false;
                btn_CANCEL.Visible = false;
                btn_OK.Size = new System.Drawing.Size(379, 30);
            }

            else
            {
                btn_OK.Size = new System.Drawing.Size(184, 30);
                btn_CANCEL.Enabled = true;
                btn_CANCEL.Visible = true;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
