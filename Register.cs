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
using System.Threading;

namespace VoiceAuthomatedEmail
{
    public partial class Register : Form
    {
        string user, Eml, Pwd;
        int pin;
        private accessDB adb = new accessDB();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginepwd = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginerepwd = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineuser = new SpeechRecognitionEngine();
        SpeechRecognitionEngine enginepinr = new SpeechRecognitionEngine();

        private void Register_Load(object sender, EventArgs e)
        {
            ss.Rate = 1;
            ss.Volume = 80;
            onloadfunc();
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            txtEml.Text = "";
            txtPwd.Text = "";
            txtRpwd.Text = "";
            txtUsrnm.Text = "";
            txtPn.Text = "";
        }
         void errorhandler()
        {
            engine.RecognizeAsyncStop();
            enginepinr.RecognizeAsyncStop();
            engineuser.RecognizeAsyncStop();
            enginepwd.RecognizeAsyncStop();
            enginerepwd.RecognizeAsyncStop();
            this.Hide();
            System.Threading.Thread.Sleep(2000);
            Register reload = new Register();
            reload.Show();
        }
        public void onloadfunc()
        {
            ss.Rate = 1;
            ss.Volume = 80;
            ss.SpeakAsync("enter email,say it one letter at a time, when your done say stop");

            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
            { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop email","undo email","confirm email","exit register"}))));
            engine.RecognizeAsync(RecognizeMode.Multiple);

            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_rec);

        }
        void engine_SpeechRecognised_rec(object sender, SpeechRecognizedEventArgs e1)
        {


            if (e1.Result.Text.ToString() == "stop email")
            {

                txtEml.Enabled = false;
                Eml = txtEml.Text.ToString();
                ss.SpeakAsync(txtEml.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e1.Result.Text.ToString() == "undo email")
            {
                txtEml.Text = "";
                txtEml.Enabled = true;
            }
            else if (e1.Result.Text.ToString() == "exit register")
            {
                exitregister();
            }
            else if (e1.Result.Text.ToString() == "confirm email")
            {
                engine.RecognizeAsyncStop();
                ss.SpeakAsync(txtEml.Text);
                ss.SpeakAsync("enter password ,when you are done say stop ");

                enginepwd.SetInputToDefaultAudioDevice();
                enginepwd.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                {"a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop password","undo password","confirm password","exit register"}))));
                enginepwd.RecognizeAsync(RecognizeMode.Multiple);

                enginepwd.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginepwd_SpeechRecognised_pwd);

            }

            else if (e1.Result.Text.ToString() != "stop email" && e1.Result.Text.ToString() != "confirm email")
            {
                txtEml.Text += e1.Result.Text.ToString();
            }
        }
        void enginepwd_SpeechRecognised_pwd(object sender, SpeechRecognizedEventArgs e2)
        {

            if (e2.Result.Text.ToString() == "stop password")
            {

                txtPwd.Enabled = false;
                user = txtPwd.Text.ToString();
                ss.SpeakAsync(txtPwd.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e2.Result.Text.ToString() == "undo password")
            {
                txtPwd.Text = "";
                txtPwd.Enabled = true;
            }
            else if (e2.Result.Text.ToString() == "exit register")
            {
                exitregister();
            }
            else if (e2.Result.Text.ToString() == "exit register")
            {
                exitregister();
            }
            else if (e2.Result.Text.ToString() == "confirm password")
            {
                txtRpwd.Enabled = false;
                txtRpwd.Text = txtPwd.Text;
                enginepwd.RecognizeAsyncStop();
                ss.SpeakAsync("enter username,when your done say stop");
                //engineuser.RecognizeAsyncStop();
                //ss.SpeakAsync(txtPwd.Text);
                engineuser.SetInputToDefaultAudioDevice();
                engineuser.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop username","undo username","confirm username","exit register"}))));
                engineuser.RecognizeAsync(RecognizeMode.Multiple);

                engineuser.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_usr);
    
            }

            else if (e2.Result.Text.ToString() != "stop password" && e2.Result.Text.ToString() != "confirm password")
            {
                txtPwd.Text += e2.Result.Text.ToString();
            }
        }

        void engine_SpeechRecognised_usr(object sender, SpeechRecognizedEventArgs e3)
        {


            if (e3.Result.Text.ToString() == "stop username")
            {

                txtUsrnm.Enabled = false;
                user = txtUsrnm.Text.ToString();
                ss.SpeakAsync(txtUsrnm.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e3.Result.Text.ToString() == "exit register")
            {
                exitregister();
            }
            else if (e3.Result.Text.ToString() == "undo username")
            {
                txtUsrnm.Text = "";
                txtUsrnm.Enabled = true;
            }
            else if (e3.Result.Text.ToString() == "confirm username")
            {
                engineuser.RecognizeAsyncStop();
                ss.SpeakAsync(txtUsrnm.Text);
                ss.SpeakAsync("enter pin ,when you are done say stop ");

                enginepinr.SetInputToDefaultAudioDevice();
                enginepinr.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                {
                    "1","2","3","4","5","6","7","8","9","0","stop pin","undo pin","confirm pin","exit register"}))));
                enginepinr.RecognizeAsync(RecognizeMode.Multiple);

                enginepinr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginepin_SpeechRecognised_pin);

            }

            else if (e3.Result.Text.ToString() != "stop username" && e3.Result.Text.ToString() != "confirm username")
            {
                txtUsrnm.Text += e3.Result.Text.ToString();
            }
        }
        void enginepin_SpeechRecognised_pin(object sender, SpeechRecognizedEventArgs e4)
        {

            if (e4.Result.Text.ToString() == "stop pin")
            {

                txtPn.Enabled = false;
                user = txtPn.Text.ToString();
                ss.SpeakAsync(txtPn.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e4.Result.Text.ToString() == "undo pin")
            {
                txtPn.Text = "";
                txtPn.Enabled = true;
            }
            else if (e4.Result.Text.ToString() == "confirm pin")
            {
                enginepinr.RecognizeAsyncStop();
                finalregister(); 
            }
            else if (e4.Result.Text.ToString() == "exit register")
            {
                exitregister();
            }
            else if (e4.Result.Text.ToString() != "stop pin" && e4.Result.Text.ToString() != "confirm pin")
            {
                txtPn.Text += e4.Result.Text.ToString();
            }
        }
        void finalregister()
        {
            if (txtEml.Text == "" || txtPn.Text == "" || txtPwd.Text == "" || txtRpwd.Text == "" || txtUsrnm.Text == "")
            {
                ss.SpeakAsync("Please fill all fields");
                errorhandler();
            }
            else if (txtRpwd.Text != txtPwd.Text)
            {
                ss.SpeakAsync("the repeated password is incorrect");
                errorhandler();
            }
            else
            {
                user = txtUsrnm.Text;
                Eml = txtEml.Text + "@gmail.com";
                Pwd = txtPwd.Text;
                pin = Convert.ToInt32(txtPn.Text);
                try
                {
                    accessDB adb = new accessDB();
                    adb.insertuser(Eml, Pwd, user, pin);
                }
                catch
                {
                    ss.SpeakAsync("registration failure");
                }
                user = Pwd = Eml = "";
                pin = 0;
                Thread.Sleep(2000);
                engine.RecognizeAsyncStop();
                this.Hide();
                login f1 = new login();
                f1.Show();
            }
        }
        private void btnSgn_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            enginepinr.RecognizeAsyncStop();
            engineuser.RecognizeAsyncStop();
            enginepwd.RecognizeAsyncStop();
            enginerepwd.RecognizeAsyncStop();
            finalregister();
        }

        private void btnExt_Click(object sender, EventArgs e)
        {
            exitregister();
        }
        void exitregister()
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            enginepinr.RecognizeAsyncStop();
            engineuser.RecognizeAsyncStop();
            enginepwd.RecognizeAsyncStop();
            enginerepwd.RecognizeAsyncStop();
            this.Hide();
            login f1 = new login();
            f1.Show();
        }
        SpeechSynthesizer ss = new SpeechSynthesizer();
        public Register()
        {
            InitializeComponent();
            accessDB.InitializeDB();
        }
        
    }
}
