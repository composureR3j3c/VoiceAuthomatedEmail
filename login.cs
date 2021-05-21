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
    public partial class login : Form
    {
        string user ="",SdrEml="", SdrPwd="";
        int pin=0;
        private accessDB adb = new accessDB();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechSynthesizer ss = new SpeechSynthesizer();
        SpeechRecognitionEngine enginepin = new SpeechRecognitionEngine();
        public login()
        {
            InitializeComponent(); 
            accessDB.InitializeDB();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            enginepin.RecognizeAsyncStop();
            this.Hide();
            Register rg = new Register();
            rg.Show();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            ss.Rate = 1;
            ss.Volume = 80;
            onloadfunc();
        }
        public void onloadfunc()
        {
            ss.Rate = 1;
            ss.Volume = 80;
            ss.SpeakAsync("enter username,say it one letter at a time, when your done say stop");
            Thread.Sleep(3000);
            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
            { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop username","undo username","confirm username","register","open manual"}))));
            engine.RecognizeAsync(RecognizeMode.Multiple);

            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_rec);

        }
        void engine_SpeechRecognised_rec(object sender, SpeechRecognizedEventArgs e1)
        {


            if (e1.Result.Text.ToString() == "stop username")
            {
                
                txtUsr.Enabled = false;
                user = txtUsr.Text.ToString();
                ss.SpeakAsync(txtUsr.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e1.Result.Text.ToString() == "undo username")
            {
                txtUsr.Text = "";
                txtUsr.Enabled = true;
            }
            else if (e1.Result.Text.ToString() == "register")
            {
                engine.RecognizeAsyncStop();
                this.Hide();
                Register rg = new Register();
                rg.Show();
            }
            else if(e1.Result.Text.ToString() == "open manual")
            {
                engine.RecognizeAsyncStop();
                this.Hide();
                usermanual um = new usermanual();
                um.from = "login";
                um.Show();
            }
            else if (e1.Result.Text.ToString() == "confirm username")
            {
                engine.RecognizeAsyncStop();
                txtUsr.Text = user;
                //ss.SpeakAsync(txtUsr.Text);
                ss.SpeakAsync("enter pin ,when you are done say stop ");
                
                enginepin.SetInputToDefaultAudioDevice();
                enginepin.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { 
                    "1","2","3","4","5","6","7","8","9","0","stop pin","undo pin","confirm pin"}))));
                enginepin.RecognizeAsync(RecognizeMode.Multiple);

                enginepin.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(enginepin_SpeechRecognised_pin);

            }
            
            else if (e1.Result.Text.ToString() != "stop username" && e1.Result.Text.ToString() != "confirm username")
            {
                txtUsr.Text += e1.Result.Text.ToString();
            }
        }
        void enginepin_SpeechRecognised_pin(object sender, SpeechRecognizedEventArgs e2)
            {

            if (e2.Result.Text.ToString() == "stop pin")
            {

                txtPin.Enabled = false;
                try
                {
                    pin = Convert.ToInt32(txtPin.Text);
                }
                catch (Exception)
                {
                    ss.SpeakAsync("Pin must be numeric");
                    errorhandler();
                }
                ss.SpeakAsync(txtPin.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e2.Result.Text.ToString() == "undo pin")
            {
                txtPin.Text = "";
                txtPin.Enabled = true;
            }
            else if (e2.Result.Text.ToString() == "confirm pin")
            {
                enginepin.RecognizeAsyncStop();
                txtPin.Text = pin.ToString();
                finallogin();
                //ss.SpeakAsync("You are logged in");
            }
            else
            {
                txtPin.Text += e2.Result.Text.ToString();
            }
        }

        private void btnUsrMnl_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            enginepin.RecognizeAsyncStop();
            ss.SpeakAsyncCancelAll();
            this.Hide();
            usermanual user = new usermanual();
            user.from = "login";
            user.Show();
        }

        private void btnLgIn_Click(object sender, EventArgs e)
        {
            engine.RecognizeAsyncStop();
            enginepin.RecognizeAsyncStop();
            finallogin();
        }
        

        void errorhandler()
        {
            engine.RecognizeAsyncStop();
            enginepin.RecognizeAsyncStop();
            Thread.Sleep(2000);
            this.Hide();
            login reload = new login();
            reload.Show();
        }
        public void finallogin()
        {
            if (txtUsr.Text == "" || txtPin.Text == "")
            {
                //MessageBox.Show("please enter user name and pin");
                ss.SpeakAsync("please enter user name and pin");
                errorhandler();
            }
            else if (int.TryParse(txtPin.Text, out pin) == false)
            {
                ss.SpeakAsync("Pin must be numeric");
                errorhandler();
            }
            else
            {
                if (user == "")
                {
                    user = txtUsr.Text;
                    pin = Convert.ToInt32(txtPin.Text);
                }
                try
                {
                    adb.selectuser(user, pin);
                    SdrEml = adb.GetEmail();
                    SdrPwd = adb.GetPassword();
                    engine.RecognizeAsyncStop();
                    if (SdrEml != "" && SdrPwd != null)
                    {
                        ss.SpeakAsyncCancelAll();
                        this.Hide();
                        menu ibx = new menu();
                        ibx.SdrEi = SdrEml;
                        ibx.SdrPi = SdrPwd;
                        ibx.Show();
                    }
                    else
                    {
                        ss.SpeakAsync("invalid user name and pin");
                        
                        errorhandler();
                    }
                }
                catch (Exception ex)
                {
                    ss.SpeakAsync(ex.Message);
                    errorhandler();
                }
            }
        }
        
    }
}
