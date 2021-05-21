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


namespace VoiceAuthomatedEmail
{
    public partial class menu : Form
    {
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineext = new SpeechRecognitionEngine();
        SpeechSynthesizer ss = new SpeechSynthesizer();
        public string SdrEi,SdrPi;
        public menu()
        {
            InitializeComponent();
            
        }

        private void compseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login f = new login();
            f.Show();
        }

        private void MnuCmps_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            this.Hide();
            Recipient cse = new Recipient();
            
            cse.SdrE = SdrEi;
            cse.SdrP = SdrPi;
            cse.Show();

        }

        private void Inbox_Load(object sender, EventArgs e)
        {
            
            ss.SpeakAsync("menu");
            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "Compose","Inbox","sent","contact","open manual","trash","leave app" }))));
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised);
            
        }

        private void inboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            engineext.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            this.Hide();
            received inbx = new received();
            inbx.recEML = SdrEi;
            inbx.recPwd = SdrPi;
            inbx.Show();
        }

       
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {
           // ss.SpeakAsync("choose any folder");
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            engineext.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            this.Hide();
            usermanual user = new usermanual();
            user.manEml = SdrEi;
            user.manPwd = SdrPi;
            user.from = "menu";
            user.Show();
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            engineext.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            this.Hide();
            contact cntct = new contact();
            cntct.cntctEml = SdrEi;
            cntct.cntctPwd = SdrPi;
            cntct.Show();
        }

        private void MeuSnt_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            engineext.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            this.Hide();
            Sent snt = new Sent();

            snt.sntEML = SdrEi;
            snt.sntPwd = SdrPi;
            snt.status = "sent";

            snt.Show();
        }

        private void trashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            engineext.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            this.Hide();
            Sent snt = new Sent();

            snt.sntEML = SdrEi;
            snt.sntPwd = SdrPi;
            snt.status = "trash";

            snt.Show();
        }

        void engine_SpeechRecognised(object ob, SpeechRecognizedEventArgs e)
        {
            
            if (e.Result.Text == "Compose")
            {
                engine.RecognizeAsyncStop();
                engineext.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                this.Hide();
                Recipient cse = new Recipient();
                
                cse.SdrE = SdrEi;
                cse.SdrP = SdrPi;
                cse.Show();
            }
            if (e.Result.Text == "Inbox")
            {
                engine.RecognizeAsyncStop();
                engineext.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                this.Hide();
                received inbx = new received();
                inbx.recEML  = SdrEi;
                inbx.recPwd = SdrPi;
                inbx.Show();
            }
            if (e.Result.Text == "contact")
            {
                engine.RecognizeAsyncStop();
                engineext.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                this.Hide();
                contact cntct = new contact();
                cntct.cntctEml = SdrEi;
                cntct.cntctPwd = SdrPi;
                cntct.Show();
            }
            if (e.Result.Text == "trash")
            {
                engine.RecognizeAsyncStop();
                engineext.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                this.Hide();
                Sent snt = new Sent();

                snt.sntEML = SdrEi;
                snt.sntPwd = SdrPi;
                snt.status = "trash";

                snt.Show();
            }
            if (e.Result.Text == "open manual")
            {
                engine.RecognizeAsyncStop();
                engineext.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                this.Hide();
                usermanual user = new usermanual();
                user.manEml = SdrEi;
                user.manPwd = SdrPi;
                user.from = "menu";
                user.Show();
            }
            if (e.Result.Text == "leave app")
            {
                engine.RecognizeAsyncStop();
                ss.SpeakAsync("are you, sure you want to exit?");
                engineext.SetInputToDefaultAudioDevice();
                engineext.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "yes", "no" }))));
                engineext.RecognizeAsync(RecognizeMode.Multiple);
                engineext.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engineext_SpeechRecognised_snd);
            }
            if (e.Result.Text == "sent")
            {
                engine.RecognizeAsyncStop();
                engineext.RecognizeAsyncStop();
                ss.SpeakAsyncCancelAll();
                this.Hide();
                Sent snt = new Sent();

                snt.sntEML = SdrEi;
                snt.sntPwd = SdrPi;
                snt.status = "sent";

                snt.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitapp();
        }

        void engineext_SpeechRecognised_snd(object sender, SpeechRecognizedEventArgs e7)
        {
            if (e7.Result.Text.ToString() == "yes")
            {
                exitapp(); 
            }
            else
            {
                engineext.RecognizeAsyncStop();
                engine.RecognizeAsync();
            }
        }
        void exitapp()
        {
            engine.RecognizeAsyncStop();
            engineext.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            Application.Exit();

        }
    }
}
