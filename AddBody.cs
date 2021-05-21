using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceAuthomatedEmail
{
    public partial class AddBody : Form
    {
        public string addeml;
        String key, bdy;
        int x;
        string [] list = {"meeting","start","greet","end","received","contact","application","information","social start",
                    "confirm payment","notify payment","attachment","catalogue",
                    "stop body","undo body","confirm body"};
        public AddBody()
        {
            InitializeComponent();
            accessDB.InitializeDB();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtKey.Text == null || richTextBoxBody.Text == null)
            {
                MessageBox.Show("Enter all fields");
            }
            else
            {
                key = txtKey.Text;
                bdy = richTextBoxBody.Text;
                foreach (string ls in list)
                {
                    if (key == ls)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = 1;
                    }
                }
                if (x == 0)
                {
                    key = "";
                    txtKey.Text = "";
                }
                else if (x == 1)
                {
                    accessDB adb = new accessDB();
                    adb.inserTemplate(key, bdy, addeml);
                }
            }
        }
    }
}
