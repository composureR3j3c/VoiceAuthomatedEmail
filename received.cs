using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using MailKit;
using MimeKit;
using MailKit.Search;
using MailKit.Security;
using MailKit.Net.Imap;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Threading;

namespace VoiceAuthomatedEmail
{
    public partial class received : Form
    {
        
        public string recEML, recPwd;
        ////public string sndr, sbjct, body, attachement, date, recipient;
        DataTable dt;
        SpeechSynthesizer ss = new SpeechSynthesizer();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineFwd = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineAll = new SpeechRecognitionEngine();

        int unseen = 0, i;
        public string msgFrom { get; private set; }
        public string to { get; private set; }
        public string subject { get; private set; }
        public string datenow { get; private set; }
        public string bdytext { get; private set; }

        public received()
        {
            InitializeComponent();
            accessDB.InitializeDB();
            
        }
        
        private void received_Load(object sender, EventArgs e)
        {
            ss.Rate = 1;
            ss.Volume = 80;
            loadall();
            
        }
        //unseen

        private void loadall()
        {

            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                    client.Authenticate(recEML, recPwd);
                    client.Inbox.Open(FolderAccess.ReadWrite);
                    var uids = client.Inbox.Search(SearchQuery.All);
                    var items = client.Inbox.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);
                    dt = new DataTable("inbox");
                    dt.Columns.Add("Sender", typeof(string));
                    dt.Columns.Add("Subject", typeof(string));
                    dt.Columns.Add("Date", typeof(string));
                    dt.Columns.Add("Body", typeof(string));
                    dt.Columns.Add("Attachments", typeof(string));
                    dataGridViewRec.DataSource = dt;
                    foreach (var uid in uids)
                    {
                        unseen++;
                        var message = client.Inbox.GetMessage(uid);
                        msgFrom = message.From.ToString();
                        to = message.To.ToString();
                        subject = message.Subject.ToString();
                        datenow = message.Date.ToString();
                        bdytext = message.TextBody;
                        int x = 0;
                        String[] att = new String[100];
                        String y="", z = "";
                        int j = 0;
                        foreach (var attachment in message.Attachments)
                        {
                            x++;
                            var part = (MimePart)attachment;

                            var fileName = part.FileName;


                            var path = (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName));
                            //var path = Path.Combine(dir.ToString(), fileName);
                            if (x != 0)
                                z = path.ToString();
                            att[j] = fileName.ToString();
                            // decode and save the content to a file
                            using (var stream = File.Create(path))
                                part.Content.DecodeTo(stream);
                            j++;
                        }
                        if (x > 0)
                        {
                            y = "" + x + " named";
                        }
                        else
                        {
                            y = "0";
                        }
                           
                        foreach (string str in att)
                        {
                            y += " " + str;
                        }
                        
                        dt.Rows.Add(new object[] { msgFrom, subject, datenow, bdytext, z });
                        try
                        {
                            client.Inbox.AddFlags(uid, MessageFlags.Seen, true);
                        }
                        catch(Exception)
                        {

                        }                        
                    }
                    client.Disconnect(true);
                    ss.SpeakAsync("you have" + unseen + " emails in Your inbox");
                    unseen = 0;
                    i = dt.Rows.Count - 1;

                    displayEml();
                }
            }
            catch (Exception )
            {

                ss.SpeakAsyncCancelAll();
                ss.SpeakAsync("no network connection");
                engine.RecognizeAsyncStop();
                engineAll.RecognizeAsyncStop();
                engineFwd.RecognizeAsyncStop();
                this.Hide();
                System.Threading.Thread.Sleep(2000);
                menu ibx = new menu();
                ibx.SdrEi = recEML;
                ibx.SdrPi = recPwd;
                ibx.Show();
            }
        }
        void displayEml()
        {
            
            if (i >= 0)
            {
                ss.SpeakAsync("from\n" + dt.Rows[i]["Sender"].ToString()
                            + "\nSubject\n" + dt.Rows[i]["Subject"].ToString() + "\n");
                //
                if (dt.Rows[i]["Attachments"].ToString().Substring(0,0)!="0")
                ss.SpeakAsync("Attachments" + dt.Rows[i]["Attachments"].ToString());
                ss.SpeakAsync("read, forward or skip?");
               }
            if (i == dt.Rows.Count - 1)
            {
                engine.SetInputToDefaultAudioDevice();
                engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "read email","skip email","exit inbox","forward email","stop reading"}))));
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
            else if (e1.Result.Text.ToString() == "stop reading" && i >= 0)
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

                //fwd();
            }

            else if (e1.Result.Text.ToString() == "exit inbox" && i > 0)
            {
                exitInbx();
            }

            if (i < 0)
            {
                ss.SpeakAsync("those are all your emails");
                exitInbx();
            }
        }
        void exitInbx()
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            engineAll.RecognizeAsyncStop();
            engineFwd.RecognizeAsyncStop();
            this.Hide();
            menu ibx = new menu();
            ibx.SdrEi = recEML;
            ibx.SdrPi = recPwd;
            ibx.Show();

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
                engineAll.RecognizeAsyncStop();
                engineFwd.RecognizeAsyncStop();
                txtFwdEml.Text = to;
                fwd();
            }
            else if (e1.Result.Text.ToString() != "stop address" && e1.Result.Text.ToString() != "confirm address")
            {
                txtFwdEml.Text += e1.Result.Text.ToString();
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadall();
        }

        private void btnReadout_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            ss.SpeakAsync(richTextBoxBody.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            engineAll.RecognizeAsyncStop();
            engineFwd.RecognizeAsyncStop();
         
            this.Hide();
            
            menu ibx = new menu();
            ibx.SdrEi = recEML;
            ibx.SdrPi = recPwd;
            ibx.Show();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            txtFwdEml.Visible = true;
            btnSend.Visible = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
            fwd();          
        }

        private void dataGridViewRec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dt.Rows.Count)
            {
                richTextBoxBody.Text = dt.Rows[e.RowIndex]["Body"].ToString();
                // where index is set for button;
                i = e.RowIndex;
            }
        }

        void fwd()
        {
            if (txtFwdEml.Text != null&& richTextBoxBody.Text!=null)
            {

                try
                {
                    string fname = "";
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
                    clientdetail.Credentials = new NetworkCredential(recEML, recPwd);

                    //message details
                    MailMessage maildetails = new MailMessage();
                    maildetails.From = new MailAddress(recEML);
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

                    adb.insertsent(dt.Rows[i]["Sender"].ToString(), dt.Rows[i]["Subject"].ToString(), dt.Rows[i]["Body"].ToString(), fn, recEML, date);


                    txtFwdEml.Text = null;
                    fname = fn = "";
                    engine.RecognizeAsyncStop();
                    engineAll.RecognizeAsyncStop();
                    engineFwd.RecognizeAsyncStop();
                    
                    this.Hide();
                    menu ibx = new menu();
                    ibx.SdrEi = recEML;
                    ibx.SdrPi = recPwd;
                    ibx.Show();
                }
                catch (Exception ex)
                {
                    ss.SpeakAsyncCancelAll();
                    ss.SpeakAsync(ex.Message);

                    engine.RecognizeAsyncStop();
                    engineAll.RecognizeAsyncStop();
                    engineFwd.RecognizeAsyncStop();
                    this.Hide();
                    System.Threading.Thread.Sleep(2000);
                    menu ibx = new menu();
                    ibx.SdrEi = recEML;
                    ibx.SdrPi = recPwd;
                    ibx.Show();
                }
            }
        }


        private void btnRead_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            engineAll.RecognizeAsyncStop();
            engineFwd.RecognizeAsyncStop();
            this.Hide();
            received rec = new received();
            rec.recEML = recEML;
            rec.recPwd= recPwd;
            rec.Show();
        }
    }
}
