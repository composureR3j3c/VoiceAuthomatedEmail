using System;
using System.Collections.Generic;
using System.Data;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using MailKit;
using MimeKit;
using MailKit.Search;
using MailKit.Security;
using MailKit.Net.Imap;
using System.IO;
namespace VoiceAuthomatedEmail
{
    public partial class Sent : Form
    {
        public string sntEML, sntPwd,status;
        ////public string sndr, sbjct, body, attachement, date, recipient;
        string to;
        DataTable dt;
        int i;
        SpeechSynthesizer ss = new SpeechSynthesizer();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineFwd = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineAll = new SpeechRecognitionEngine();
        public Sent()
        {
            InitializeComponent();
            accessDB.InitializeDB();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            engineAll.RecognizeAsyncStop();
            engineFwd.RecognizeAsyncStop();
            this.Hide();
            menu ibx = new menu();
            ibx.SdrEi = sntEML;
            ibx.SdrPi = sntPwd;
            ibx.Show();
        }

        private void Sent_Load(object sender, EventArgs e)
        {
           
            doonload();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            txtFwdEml.Visible = true;
            btnSend.Visible = true;
        }
        void doonload()
        {
            dt = new DataTable("Contact");
            dt.Columns.Add("Recipient", typeof(string));
            dt.Columns.Add("Subject", typeof(string));
            dt.Columns.Add("Body", typeof(string));
            dt.Columns.Add("Attachments", typeof(string));

            dataGridViewRec.DataSource = dt;
            List<accessDB> sents = accessDB.SelectSent(sntEML,status);
            foreach (accessDB c in sents)
            {
                dt.Rows.Add(new object[] { c.srecipient, c.ssubject,c.sbody, c.sattachement });
            }
            i = dt.Rows.Count - 1;

            displayEml();
        }
        void delMail()
        {
            if (status == "sent")
            {
                accessDB accessdb = new accessDB();
                accessdb.UpdateTrash(sntEML, dt.Rows[i]["Recipient"].ToString(), dt.Rows[i]["Subject"].ToString());
                dt.Rows[i]["Recipient"] = "";
                dt.Rows[i]["Subject"] = "";
                dt.Rows[i]["Body"] = "";
                dt.Rows[i]["Attachments"] = "";
            }
            if (status == "trash")
            {
                accessDB accessdb = new accessDB();
                accessdb.UpdateTrashP(sntEML, dt.Rows[i]["Recipient"].ToString(), dt.Rows[i]["Subject"].ToString());
                dt.Rows[i]["Recipient"] = "";
                dt.Rows[i]["Subject"] = "";
                dt.Rows[i]["Body"] = "";
                dt.Rows[i]["Attachments"] = "";
            }
            ss.SpeakAsync("message removed from " +status);
            i--;
            displayEml();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            delMail();
        }

        private void dataGridViewRec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dt.Rows.Count)
            {
                richTextBoxBody.Text = dt.Rows[e.RowIndex]["Body"].ToString();
                i = e.RowIndex;
            }
        }

        private void dataGridViewRec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dt.Rows.Count)
            {
                richTextBoxBody.Text = dt.Rows[e.RowIndex]["Body"].ToString();
                i = e.RowIndex;
            }
        }

        private void btnReadout_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            ss.SpeakAsync(richTextBoxBody.Text);
        }

        
        void displayEml()
        {

            if (i >= 0)
            {
                ss.SpeakAsync("from\n" + dt.Rows[i]["Recipient"].ToString()
                            + "\nSubject\n" + dt.Rows[i]["Subject"].ToString() + "\n");
                if (dt.Rows[i]["Attachments"]!=null)
                {
                    ss.SpeakAsync("Attachments" + dt.Rows[i]["Attachments"]);
                }
                ss.SpeakAsync("read, forward or skip?");
            }
            if (i == dt.Rows.Count - 1)
            {
                engine.SetInputToDefaultAudioDevice();
                engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "read email","skip email","stop reading","forward email","delete email","exit "+status}))));
                engine.RecognizeAsync(RecognizeMode.Multiple);

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_rec);

            }
        }


        void engine_SpeechRecognised_rec(object sender, SpeechRecognizedEventArgs e1)
        {


            if (e1.Result.Text.ToString() == "read email" && i >= 0)
            {
                //engine.RecognizeAsyncStop();
                richTextBoxBody.Text = dt.Rows[i]["Body"].ToString();
                ss.SpeakAsync(richTextBoxBody.Text);
                i--;
                displayEml();

            }
            else if (e1.Result.Text.ToString() == "skip email" && i >= 0)
            {
                //engine.RecognizeAsyncStop();
                i--;
                displayEml();
            }
            else if (e1.Result.Text.ToString() == "stop reading")
            {
                //engine.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                i--;
                displayEml();
            }
            else if (e1.Result.Text.ToString() == "forward email")
            {
                label1.Visible = true;
                txtFwdEml.Visible = true;
                btnSend.Visible = true;
                engine.RecognizeAsyncStop();
                ss.SpeakAsync("enter the Recipient's Email address");
                engineFwd.SetInputToDefaultAudioDevice();
                engineFwd.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop address","undo address","confirm address"}))));
                engineFwd.RecognizeAsync(RecognizeMode.Multiple);

                engineFwd.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_fwd);

            }
            else if (e1.Result.Text.ToString() == "delete email")
            {
                delMail();
            }
            else if ((e1.Result.Text.ToString() == "exit " + status) && i>0)
            {
                ss.SpeakAsyncCancelAll();
                engine.RecognizeAsyncStop();
                engineAll.RecognizeAsyncStop();
                engineFwd.RecognizeAsyncStop();
                this.Hide();
                menu ibx = new menu();
                ibx.SdrEi = sntEML;
                ibx.SdrPi = sntPwd;
                ibx.Show();
            }
            if (i < 0)
            {
                btnRead.Enabled = true;
                //btnLoad.Enabled = true;
                ss.SpeakAsync("those are all your emails.");
               engine.RecognizeAsyncStop();
                this.Hide();
                menu ibx = new menu();
                ibx.SdrEi = sntEML;
                ibx.SdrPi = sntPwd;
                ibx.Show();
            }
        }
        void engine_SpeechRecognised_fwd(object sender, SpeechRecognizedEventArgs e1)
        {


            if (e1.Result.Text.ToString() == "stop address")
            {
                txtFwdEml.Text += "@gmail.com";
                txtFwdEml.Enabled = false;
                to = txtFwdEml.Text.ToString();
                ss.SpeakAsync(txtFwdEml.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e1.Result.Text.ToString() == "undo address")
            {
                txtFwdEml.Text = "";
                txtFwdEml.Enabled = true;
            }
            else if (e1.Result.Text.ToString() == "confirm address")
            {
                engine.RecognizeAsyncStop();
                txtFwdEml.Text = to;
                fwd();
            }
            else if (e1.Result.Text.ToString() != "stop address" && e1.Result.Text.ToString() != "confirm address")
            {
                txtFwdEml.Text += e1.Result.Text.ToString();
            }

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            fwd();
        }
        void fwd()
        {
            engine.RecognizeAsyncStop();
            engineAll.RecognizeAsyncStop();
            engineFwd.RecognizeAsyncStop();
            if (txtFwdEml.Text != null)
            {

                try
                {
                    string fname="";
                    if (dt.Rows[i]["Attachments"].ToString() != "")
                    {
                         fname = dt.Rows[i]["Attachments"].ToString();
                    }
                    
                    /*

                                + "\nSubject\n" + dt.Rows[i]["Subject"].ToString()+"\n"
                     */
                    string fn;
                    fn = fname.Replace("\\", "\\\\");
                    //fn= fileName.Substring(fileName.LastIndexOf('\'),len);
                    to = txtFwdEml.Text.ToString();

                    SmtpClient clientdetail = new SmtpClient();
                    clientdetail.Host = "smtp.gmail.com";
                    clientdetail.Port = 587;
                    clientdetail.EnableSsl = true;
                    clientdetail.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clientdetail.UseDefaultCredentials = false;
                    clientdetail.Credentials = new NetworkCredential(sntEML, sntPwd);

                    //message details
                    MailMessage maildetails = new MailMessage();
                    maildetails.From = new MailAddress(sntEML);
                    maildetails.To.Add(to/*.Trim()*/);

                    maildetails.Subject = dt.Rows[i]["Subject"].ToString();
                    maildetails.Body = dt.Rows[i]["Body"].ToString();

                    //attachment
                    if (fname.Length > 0)
                    {
                        Attachment attachment = new Attachment(fname);
                        maildetails.Attachments.Add(attachment);
                    }
                    clientdetail.Send(maildetails);

                    // MessageBox.Show("your mail has been sent");
                    SpeechSynthesizer ss = new SpeechSynthesizer();
                    ss.SpeakAsync("your mail has been sent");
                    DateTime now = DateTime.Now;
                    string date = now.ToLongDateString();

                    // save to sent
                    accessDB adb = new accessDB();

                    adb.insertsent(dt.Rows[i]["Sender"].ToString(), dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["Body"].ToString(), fn, sntEML, date);


                    txtFwdEml.Text = null;
                    fname = fn = "";

                    engine.RecognizeAsyncStop();
                    this.Hide();
                    menu ibx = new menu();
                    ibx.SdrEi = sntEML;
                    ibx.SdrPi = sntPwd;
                    ibx.Show();
                }
                catch (Exception ex)
                {
                    ss.SpeakAsyncCancelAll();
                    ss.SpeakAsync(ex.Message);

                    engine.RecognizeAsyncStop();
                    this.Hide();
                    System.Threading.Thread.Sleep(2000);
                    menu ibx = new menu();
                    ibx.SdrEi = sntEML;
                    ibx.SdrPi = sntPwd;
                    ibx.Show();
                }
            }
        }
    }
}
