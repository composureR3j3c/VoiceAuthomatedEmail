using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Threading;

namespace VoiceAuthomatedEmail
{
    public partial class usermanual : Form
    {
        public String manEml, manPwd, from;
        
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginestp = new SpeechRecognitionEngine();

        SpeechSynthesizer ss = new SpeechSynthesizer();
        void doonload()
        {
            ss.SpeakAsync("choose a functionality");
            Thread.Sleep(2000);
            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "login", "register", "inbox",
                "sent", "trash","add contact","use contact","menu","user manual","exit manual" }))));
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised);
        }

        private void engine_SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {
            if(e.Result.Text == "login")
            {
                readmanual("login.txt");
            }
            if (e.Result.Text == "register")
            {
                readmanual("register.txt");
            }
            if (e.Result.Text == "inbox")
            {
                readmanual("inbox.txt");
            }
            if (e.Result.Text == "sent")
            {
                readmanual("sent.txt");
            }
            
            if (e.Result.Text == "trash")
            {
                readmanual("trash.txt");
            }
            if (e.Result.Text == "add contact")
            {
                readmanual("add.txt");
            }
            if (e.Result.Text == "use contact")
            {
                readmanual("use.txt");
            }
            if (e.Result.Text == "menu")
            {
                readmanual("menu.txt");
            }
            if (e.Result.Text == "user manual")
            {
                readmanual("manual.txt");
                
            }
            if (e.Result.Text == "exit manual")
            {
                backtomenu();
            }
        }

        void displaymanual(string filenm)
        {
            ss.SpeakAsyncCancelAll();
            string text = File.ReadAllText(filenm, Encoding.UTF8);
            richTextBox1.Text = text;
            //ss.SpeakAsync(richTextBox1.Text);
        }
        void readmanual(string filenm)
        {
            engine.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            string text = File.ReadAllText(filenm, Encoding.UTF8);
            richTextBox1.Text = text;
            ss.SpeakAsync(richTextBox1.Text);

            enginestp.SetInputToDefaultAudioDevice();
            enginestp.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "stop reading" }))));
            enginestp.RecognizeAsync(RecognizeMode.Multiple);
            enginestp.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginestp_SpeechRecognised);
        }
        private void enginestp_SpeechRecognised(object sender, SpeechRecognizedEventArgs e1)
        {
            if (e1.Result.Text == "stop reading")
                if (e1.Result.Text == "stop reading")
                {
                richTextBox1.Text = "";
                ss.SpeakAsyncCancelAll();
                backtomenu();
            }
        }
        void errorhandler()
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncCancel();
            enginestp.RecognizeAsyncCancel();
            Thread.Sleep(2000);
            this.Hide();
            usermanual reload = new usermanual();
            if (from == "menu")
            {
                reload.manEml = manEml;
                reload.manPwd = manPwd;
                reload.from = "menu";
                reload.Show();
            }
            else
            {
                reload.manEml = manEml;
                reload.manPwd = manPwd;
                reload.from = "login";
                reload.Show();
            }

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            displaymanual("login.txt");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
           displaymanual("register.txt");
        }

        private void buttonCompose_Click(object sender, EventArgs e)
        {
            displaymanual("compose.txt");
        }

        private void btnInbox_Click(object sender, EventArgs e)
        {
            displaymanual("inbox.txt");
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            displaymanual("sent.txt");
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            displaymanual("add.txt");
        }

        private void btnUseCnct_Click(object sender, EventArgs e)
        {
            displaymanual("use.txt");
        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            displaymanual("trash.txt");
        }

        private void btnUserManual_Click(object sender, EventArgs e)
        {
            displaymanual("manual.txt");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            backtomenu();
        }

        private void usermanual_Load(object sender, EventArgs e)
        {
            doonload();
        }

        void backtomenu()
        {
            if (from == "menu")
            {
                ss.SpeakAsyncCancelAll();
                engine.RecognizeAsyncStop();
                enginestp.RecognizeAsync();
                this.Hide();
                menu ibx = new menu();
                ibx.SdrEi = manEml;
                ibx.SdrPi = manPwd;
                ibx.Show();
            }
            else if (from == "login")
            {
                ss.SpeakAsyncCancelAll();
                engine.RecognizeAsyncStop();
                enginestp.RecognizeAsyncStop();
                this.Hide();
                login ibx = new login();
                ibx.Show();
            }
        }

        public usermanual()
        {
            InitializeComponent();
        }
        
    }
}
