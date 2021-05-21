using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Threading;

namespace VoiceAuthomatedEmail
{
    public partial class Recipient : Form
    {
        private const string V = "\",";
        OpenFileDialog ofdAttachment;
        string fileName = "", ftype = "";
        public string SdrE, SdrP,contact;
        string to,bdy,sbjt;
        string x;
        string[] wrd= new string[100];
        string[] sntc = new string[100];
        SpeechSynthesizer ss = new SpeechSynthesizer();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginesbj = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginebdy = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineattach = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginewr = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineftyp = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginesnd = new SpeechRecognitionEngine();
        public Recipient()
        {
            InitializeComponent();
            accessDB.InitializeDB();
        }
        private void Compose_Load(object sender, EventArgs e)
        {
            onloadfunc();
        }
        void fetchtemp()
        {
            List<accessDB> templ = accessDB.SelecTemplate(SdrE);
            int i = 0;
            foreach (accessDB t in templ)
            {
                wrd[i] = t.bdykey;
                sntc[i] = t.bdybody;
                i++;
            }
        }
        public void onloadfunc()
        {
            ss.Rate = 1;
            ss.Volume = 80;

            if (contact==""|| contact == null)
            {
                ss.SpeakAsync("To enter the Recipient's Email address, say it one letter at a time, when your done say stop");
            
            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
            { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop address","undo address","confirm address","exit composer"}))));
            engine.RecognizeAsync(RecognizeMode.Multiple);

            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_rec);
            
            }
            else
            {
                txtRec.Text = contact;
                to = txtRec.Text.ToString();
                ss.SpeakAsync("compose subject,when you are done say stop ");
                enginesbj.SetInputToDefaultAudioDevice();
                enginesbj.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "meeting","reply","request","application","social","payment",
                    "stop subject","undo subject","confirm subject"}))));
                enginesbj.RecognizeAsync(RecognizeMode.Multiple);
                enginesbj.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginesbj_SpeechRecognised_sbj);
            }
           
        }
        void engine_SpeechRecognised_rec(object sender, SpeechRecognizedEventArgs e1)
        {


            if (e1.Result.Text.ToString() == "stop address")
            {
                txtRec.Text += "@gmail.com";
                txtRec.Enabled = false;
                to = txtRec.Text.ToString();
                ss.SpeakAsync(txtRec.Text);
                ss.SpeakAsync("confirm or undo?");
                
            }
            else if (e1.Result.Text.ToString() == "undo address")
            {
                txtRec.Text = "";
                txtRec.Enabled = true;
            }
            else if (e1.Result.Text.ToString() == "exit composer")
            {
                enginesnd.RecognizeAsyncStop();
                engine.RecognizeAsyncStop();
                engineattach.RecognizeAsyncStop();
                enginebdy.RecognizeAsyncStop();
                engineftyp.RecognizeAsyncStop();
                enginewr.RecognizeAsyncStop();
                enginesbj.RecognizeAsyncStop();
                engine.RecognizeAsyncStop();
                this.Hide();
                menu ibx = new menu();
                ibx.SdrEi = SdrE;
                ibx.SdrPi = SdrP;
                ibx.Show();
            }
            else if(e1.Result.Text.ToString() == "confirm address") {
                engine.RecognizeAsyncStop();
                txtRec.Text = to;
               // ss.SpeakAsync(to);
                ss.SpeakAsync("compose subject,when you are done say stop ");

                enginesbj.SetInputToDefaultAudioDevice();
                enginesbj.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "meeting","reply","request","application","social","payment",
                    "stop subject","undo subject","confirm subject"}))));
                enginesbj.RecognizeAsync(RecognizeMode.Multiple);
                enginesbj.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginesbj_SpeechRecognised_sbj);
            }
            else if (e1.Result.Text.ToString() != "stop address"&& e1.Result.Text.ToString() != "confirm address")
            {
                txtRec.Text += e1.Result.Text.ToString();
            }

        }
            void enginesbj_SpeechRecognised_sbj(object sender, SpeechRecognizedEventArgs e2)
        {
            if (e2.Result.Text.ToString() == "stop subject")
            {
                txtSbj.Enabled = false;
                sbjt = txtSbj.Text.ToString();
                ss.SpeakAsync(txtSbj.Text);
                ss.SpeakAsync("confirm or undo?");
            }
            else if (e2.Result.Text.ToString() == "confirm subject")
            {
                txtSbj.Text = sbjt;
               // ss.SpeakAsync(sbjt);
                enginesbj.RecognizeAsyncStop();
                ss.SpeakAsync("compose body, when you are done say stop");
                enginebdy.SetInputToDefaultAudioDevice();
                enginebdy.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "meeting","start","greet","end","received","contact","application","information","social start",
                    "confirm payment","notify payment","attachment","catalogue",
                    "stop body","undo body","confirm body"}))));
                try
                {
                    foreach (string y in wrd)
                    {
                        x = y;
                        enginebdy.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { x }))));
                    }
                }
                catch
                {

                }
                enginebdy.RecognizeAsync(RecognizeMode.Multiple);
                enginebdy.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginebdy_SpeechRecognised_bdy);    
            }
            else if (e2.Result.Text.ToString() == "undo subject")
            {
                txtSbj.Text = "";
                txtSbj.Enabled = true;
            }
            else if (e2.Result.Text.ToString() != "stop subject")
            {
                txtSbj.Text += " " + e2.Result.Text.ToString();
            }

        }
        void enginebdy_SpeechRecognised_bdy(object sender, SpeechRecognizedEventArgs e3)
        {
            if (e3.Result.Text.ToString() == "stop body")
            {

                richtxtBdy.Enabled = false;
                bdy = richtxtBdy.Text.ToString();
                ss.SpeakAsync(richtxtBdy.Text);
                ss.SpeakAsync("confirm or undo?");
            }
            else if (e3.Result.Text.ToString() == "confirm body")
            {
                enginebdy.RecognizeAsyncStop();
                richtxtBdy.Text = bdy;
                //ss.SpeakAsync(bdy);
                ss.SpeakAsync("Browse or state name of file in default path");
                Thread.Sleep(1000);
                engineattach.SetInputToDefaultAudioDevice();
                engineattach.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "open dialog", "write file", "skip attachment" }))));
                engineattach.RecognizeAsync(RecognizeMode.Multiple);
                engineattach.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginebdy_SpeechRecognised_att);

            }
            else if (e3.Result.Text.ToString() == "undo body")
            {
                richtxtBdy.Text = "";
                richtxtBdy.Enabled = true;
            }
            else if (e3.Result.Text.ToString() == x)
            { 
                for(int j = 0; j < 100; j++)
                {
                    if (x ==wrd[j])
                    {
                        richtxtBdy.Text += " "+sntc[j]+" ";
                    }
                }
            }
            else if (e3.Result.Text.ToString() == "start")
            {
                richtxtBdy.Text += "Dear " + to.Substring(0, to.Length - 10) + "\n\n";
            }
            else if (e3.Result.Text.ToString() == "meeting")
            {
                richtxtBdy.Text += " I would like to set up a meeting. ";
            }
            else if (e3.Result.Text.ToString() == "greet")
            {
                richtxtBdy.Text += " Greetings\n\n ";
            }
            else if (e3.Result.Text.ToString() == "end")
            {
                richtxtBdy.Text += " \n\t\tRegards,\n" + SdrE.Substring(0, SdrE.Length - 10) + "\n\n";
            }
            else if (e3.Result.Text.ToString() == "attachment")
            {
                richtxtBdy.Text += " I have attached a file with this email. ";
            }
            else if (e3.Result.Text.ToString() == "received")
            {
                richtxtBdy.Text += " I have received and read your email, Thank you. ";
            }
            else if (e3.Result.Text.ToString() == "contact")
            {
                richtxtBdy.Text += " Please contact me as soon as possible. ";
            }
            else if (e3.Result.Text.ToString() == "application")
            {
                richtxtBdy.Text += " I have attached a filledout application along with this email. ";
            }
            else if (e3.Result.Text.ToString() == "social start")
            {
                richtxtBdy.Text += " hey, how are you? how is everything? ";
            }
            else if (e3.Result.Text.ToString() == "information")
            {
                richtxtBdy.Text += " I would like information regarding your organization. ";
            }
            else if (e3.Result.Text.ToString() == "catalogue")
            {
                richtxtBdy.Text += " I would like a catalogue; can you please send it to this email address. ";
            }
            else if (e3.Result.Text.ToString() == "notify payment")
            {
                richtxtBdy.Text += " I have made the required  payment to your account. please check it. ";
            }
            else if (e3.Result.Text.ToString() == "confirm payment")
            {
                richtxtBdy.Text += " I have received the payment you made. Nice doing business with you.";
            }
        }
        void enginebdy_SpeechRecognised_att(object sender, SpeechRecognizedEventArgs e4)
        {
            if (e4.Result.Text.ToString() == "open dialog")
            {
                try
                {
                    ofdAttachment = new OpenFileDialog();
                    ofdAttachment.Filter = "Images(.jpg,.png)|*.png;*.jpg;|Pdf files|*.pdf";
                    // ofdAttachment.CheckFileExists
                    ofdAttachment.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    if (ofdAttachment.ShowDialog() == DialogResult.OK)
                    {
                        fileName = ofdAttachment.FileName;
                    }
                    send();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txtAttach.Text = fileName;
                engineattach.RecognizeAsyncStop();
                send();
            }
            if (e4.Result.Text.ToString() == "skip attachment")
            {
                engineattach.RecognizeAsyncStop();
                send();
            }
            if (e4.Result.Text.ToString() == "write file")
            {
                ss.SpeakAsync("state file name");
                engineattach.RecognizeAsyncStop();
                
                enginewr.SetInputToDefaultAudioDevice();
                enginewr.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
            { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop attachment","confirm attachment","undo attachment"}))));
                enginewr.RecognizeAsync(RecognizeMode.Multiple);
                enginewr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_file);
                
            }
        }
        void engine_SpeechRecognised_file(object sender, SpeechRecognizedEventArgs e5)
        {

            if (e5.Result.Text.ToString() == "stop attachment")
            {
                ss.SpeakAsync(txtAttach.Text);
                fileName = txtAttach.Text.ToString();
                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                txtAttach.Text = fileName.ToString();
                ss.SpeakAsync("confirm or undo?");
            }
            else if (e5.Result.Text.ToString() == "confirm attachment")
            {
                txtAttach.Text = fileName.ToString();
                ss.SpeakAsync("state and confirm file type");
                enginewr.RecognizeAsyncStop();
                engineftyp.SetInputToDefaultAudioDevice();
                engineftyp.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "confirm type","png", "jpg", "gif", "pdf","txt","zip", "rar","ppt","xls", "doc","docx","pptx", "xlsx", "mp3","flv","avi", "mpeg4", "webm","wmv" }))));
                engineftyp.RecognizeAsync(RecognizeMode.Multiple);
                engineftyp.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engineftyp_SpeechRecognised_ftyp);
            }
            else if (e5.Result.Text.ToString() == "undo attachment")
            {
                txtAttach.Text = "";
                txtAttach.Enabled = true;
            }
            else if (e5.Result.Text.ToString() != "stop attachment" && e5.Result.Text.ToString() != "confirm attachment")
            {
                txtAttach.Text += e5.Result.Text.ToString();
            }
        }
            void engineftyp_SpeechRecognised_ftyp(object sender, SpeechRecognizedEventArgs e8)
            {
             if (e8.Result.Text.ToString() == "confirm type")
            {
                engineftyp.RecognizeAsyncStop();
                ss.SpeakAsync(txtAttach.Text);
                txtAttach.Text += "."+ftype;
                fileName = txtAttach.Text.ToString();
                send();
            }
            else if (e8.Result.Text.ToString() != "confirm type")
            {
                ss.SpeakAsync(e8.Result.Text.ToString());
                ftype = e8.Result.Text.ToString();
            }
        }

        
        void send()
        {
            ss.SpeakAsync("are you sure you want to send this email?");
            enginesnd.SetInputToDefaultAudioDevice();
            enginesnd.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "yes", "no" }))));
            enginesnd.RecognizeAsync(RecognizeMode.Multiple);
            enginesnd.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginesnd_SpeechRecognised_snd);
        }
        void enginesnd_SpeechRecognised_snd(object sender, SpeechRecognizedEventArgs e7)
        {
            if (e7.Result.Text.ToString() == "yes")
            {
                finalsend();
                
            }
        }

        void finalsend()
        {
            enginesnd.RecognizeAsyncStop();
            engine.RecognizeAsyncStop();
            engineattach.RecognizeAsyncStop();
            enginebdy.RecognizeAsyncStop();
            engineftyp.RecognizeAsyncStop();
            enginewr.RecognizeAsyncStop();
            enginesbj.RecognizeAsyncStop();
            try
            {
                /*
                 gmail>> smtp serever:smtp.gmail.com port: ssl:reqiured
                 yahoo>> smtp serever:smtp.mail.yahoo.com port:587 ssl:reqiured
                */
                
                to = txtRec.Text.ToString();
                sbjt = txtSbj.Text.ToString();
                bdy = richtxtBdy.Text.ToString();
                int len = fileName.Length - 1;
                string fn;
                fn = fileName.Replace("\\","\\\\");
                //fn= fileName.Substring(fileName.LastIndexOf('\\'),len);
                //to = txtRec.Text.ToString();

                SmtpClient clientdetail = new SmtpClient();
                clientdetail.Host = "smtp.gmail.com";
                clientdetail.Port = 587;
                clientdetail.EnableSsl = true;
                clientdetail.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientdetail.UseDefaultCredentials = false;
                clientdetail.Credentials = new NetworkCredential(SdrE, SdrP);

                //message details
                MailMessage maildetails = new MailMessage();
                maildetails.From = new MailAddress(SdrE);
                maildetails.To.Add(to/*.Trim()*/);

                maildetails.Subject = sbjt;
                maildetails.Body = bdy;

                //attachment
                if (fileName.Length > 0)
                {
                    Attachment attachment = new Attachment(fileName);
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
                
                adb.insertsent(to, sbjt,bdy,fn,SdrE,date);
     

                txtRec.Text = txtAttach.Text = richtxtBdy.Text = txtSbj.Text = null;
                fileName = to = bdy = sbjt = "";
                engine.RecognizeAsyncStop();
                this.Hide();
                Thread.Sleep(2000);
                menu ibx = new menu();
                ibx.SdrEi = SdrE;
                ibx.SdrPi = SdrP;
                ibx.Show();
            }
            catch (Exception )
            {
                ss.SpeakAsyncCancelAll();
                
                ss.SpeakAsync("mail not sent");
                engine.RecognizeAsyncStop();
                this.Hide();
                Thread.Sleep(2000);
                menu ibx = new menu();
                System.Threading.Thread.Sleep(2000);
                ibx.SdrEi = SdrE;
                ibx.SdrPi = SdrP;
                ibx.Show();
                
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            finalsend();
        }
        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                ofdAttachment = new OpenFileDialog();
                ofdAttachment.Filter = "Images(.jpg,.png)|*.png;*.jpg;|Pdf files(.pdf)|*.pdf";  
                if (ofdAttachment.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofdAttachment.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtAttach.Text = fileName;
        }
       
    }
}
