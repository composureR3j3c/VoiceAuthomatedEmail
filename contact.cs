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
    public partial class contact : Form
    {
        public string cntctEml,cntctPwd;
        string name, email;
        string firstletter;
        DataTable dt;
        int i = 0;
        SpeechSynthesizer ss = new SpeechSynthesizer();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineadd = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineEml = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineGet = new SpeechRecognitionEngine();
        SpeechRecognitionEngine engineALPHA = new SpeechRecognitionEngine();

        private void contact_Load(object sender, EventArgs e)
        {
            doOnLoad();
        }
        void errorhandler()
        {
            
            this.Hide();
            Thread.Sleep(2000);
            contact reload = new contact();
            reload.cntctEml = cntctEml;
            reload.cntctPwd = cntctPwd;
            reload.Show();

        }
        void doOnLoad()
        {
            fetchcontact();
            ss.SpeakAsync("Would you like to add new or get existing contact? or exit?");
            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "add contact","exit contact", "get contact"}))));
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised);

        }
        void fetchcontact()
        {
            dt = new DataTable("Contact");
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));

            dataGridView1.DataSource = dt;
            List<accessDB> contact = accessDB.SelectContact(cntctEml, firstletter);
            foreach (accessDB c in contact)
            {
                dt.Rows.Add(new object[] { c.cntctName, c.cntctEmail });
            }
        }
        void engine_SpeechRecognised(object ob, SpeechRecognizedEventArgs e)
        {

            if (e.Result.Text == "add contact")
            {
                engine.RecognizeAsyncStop();
                ss.SpeakAsync("Enter name of contact");
                engineadd.SetInputToDefaultAudioDevice();
                engineadd.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop name","undo name","confirm name","exit contact"}))));
                engineadd.RecognizeAsync(RecognizeMode.Multiple);

                engineadd.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engineadd_SpeechRecognised);

            }
            else if (e.Result.Text == "get contact")
            {
                engine.RecognizeAsyncStop();
                ss.SpeakAsync("state the first letter of contact");
                engineALPHA.SetInputToDefaultAudioDevice();
                engineALPHA.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","confirm letter","exit contact"}))));
                engineALPHA.RecognizeAsync(RecognizeMode.Multiple);

                engineALPHA.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engineALPHA_SpeechRecognised);
                
            }
            else if (e.Result.Text == "exit contact")
            {
                exitcontact();
                
            }
        }
        void exitcontact()
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            engineadd.RecognizeAsyncStop();
            engineALPHA.RecognizeAsyncStop();
            engineEml.RecognizeAsyncStop();
            engineGet.RecognizeAsyncStop();
            this.Hide();
            menu ibx = new menu();
            ibx.SdrEi = cntctEml;
            ibx.SdrPi = cntctPwd;
            ibx.Show();

        }
        void engineALPHA_SpeechRecognised(object ob, SpeechRecognizedEventArgs e0)
        {
            if (e0.Result.Text == "confirm letter")
            {
                engineALPHA.RecognizeAsyncStop();
                firstletter = txtSrch.Text;
                fetchcontact();
                displayCon();
            }
            else if (e0.Result.Text == "exit contact")
            {
                exitcontact();
            }
            else
            {
                txtSrch.Text = e0.Result.Text.ToString();
                ss.SpeakAsync(txtSrch.Text);
            }

        }
        void engineadd_SpeechRecognised(object ob, SpeechRecognizedEventArgs e1)
            {
            if (e1.Result.Text.ToString() == "stop name")
            {

                txtNm.Enabled = false;
                name = txtNm.Text.ToString();
                ss.SpeakAsync(txtEml.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e1.Result.Text.ToString() == "undo name")
            {
                txtNm.Text = "";
                txtNm.Enabled = true;
            }
            else if (e1.Result.Text == "exit contact")
            {
                ss.SpeakAsyncCancelAll();
                engine.RecognizeAsyncStop();
                engineadd.RecognizeAsyncStop();
                engineALPHA.RecognizeAsyncStop();
                engineEml.RecognizeAsyncStop();
                engineGet.RecognizeAsyncStop();
                this.Hide();
                menu ibx = new menu();
                ibx.SdrEi = cntctEml;
                ibx.SdrPi = cntctPwd;
                ibx.Show();
            }
            else if (e1.Result.Text.ToString() == "confirm name")
            {
                engineadd.RecognizeAsyncStop();
                //ss.SpeakAsync(txtNm.Text);
                ss.SpeakAsync("enter email ,when you are done say stop ");

                engineEml.SetInputToDefaultAudioDevice();
                engineEml.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                {"a","b","c","d","e","f","g","h","i","j",
                    "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                    "1","2","3","4","5","6","7","8","9","0","stop email","undo email","confirm email","exit contact"}))));
                engineEml.RecognizeAsync(RecognizeMode.Multiple);

                engineEml.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engineEml_SpeechRecognised);
            }
            else
            {
                txtNm.Text += e1.Result.Text.ToString();
            }
        }
        void engineEml_SpeechRecognised(object ob, SpeechRecognizedEventArgs e11)
        {
            if (e11.Result.Text.ToString() == "stop email")
            {

                txtEml.Enabled = false;
                email = txtEml.Text.ToString();
                ss.SpeakAsync(txtEml.Text);
                ss.SpeakAsync("confirm or undo?");

            }
            else if (e11.Result.Text == "exit contact")
            {
                exitcontact();
            }
            else if (e11.Result.Text.ToString() == "undo email")
            {
                txtEml.Text = "";
                txtEml.Enabled = true;
            }
            else if (e11.Result.Text.ToString() == "confirm email")
            {
                engineEml.RecognizeAsyncStop();
                //ss.SpeakAsync(txtEml.Text);
                email += "@gmail.com";
                try
                {
                    accessDB adb = new accessDB();
                    adb.insertContact(name,email,cntctEml);
                    ss.SpeakAsync("contact saved");
                    Thread.Sleep(2000);
                    exitcontact();

                }
                catch (Exception e)
                {
                    ss.SpeakAsync(e.Message);
                    errorhandler();
                }
            }
            else 
            {
                txtEml.Text += e11.Result.Text.ToString();
            }
        }

        void displayCon()
        {

            if (i >= 0)
            {
                try
                {
                    ss.SpeakAsync("Name\n" + dt.Rows[i]["Name"].ToString()
                                + "\nEmail\n" + dt.Rows[i]["Email"].ToString() + "\n");
                    ss.SpeakAsync("use or skip");
                }
                catch (Exception)
                {
                    ss.SpeakAsync("there is no more starting with " + firstletter);
                    exitcontact();
                }
            }
            if (i == 0)
            {
                engineGet.SetInputToDefaultAudioDevice();
                engineGet.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[]
                { "use contact","skip contact","exit contact"}))));
                engineGet.RecognizeAsync(RecognizeMode.Multiple);
                engineGet.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognised_rec);
            }
        }
       

        void engine_SpeechRecognised_rec(object sender, SpeechRecognizedEventArgs e1)
        {


            if (e1.Result.Text.ToString() == "use contact" && i <= dt.Rows.Count - 1)
            {

                engine.RecognizeAsyncStop();
                engineadd.RecognizeAsyncStop();
                engineALPHA.RecognizeAsyncStop();
                engineEml.RecognizeAsyncStop();
                engineGet.RecognizeAsyncStop();
                txtSendEml.Text = dt.Rows[i]["Email"].ToString();
                this.Hide();
                Recipient cse = new Recipient();
                cse.contact= txtSendEml.Text;
                /*.All(Char.IsLetter);*/
                cse.SdrE = cntctEml;
                cse.SdrP = cntctPwd;
                cse.Show();

            }
            else if (e1.Result.Text.ToString() == "skip contact" && i <= dt.Rows.Count - 1)
            {
                engineGet.RecognizeAsyncStop();
                i++;
                displayCon();
            }
            

            if (i== dt.Rows.Count)
            {
                ss.SpeakAsync("those are all your new contacts.");
                errorhandler();
            }
        }

        private void txtSrch_TextChanged(object sender, EventArgs e)
        {
            firstletter = txtSrch.Text;
            fetchcontact();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dt.Rows.Count)
            {
                txtSendEml.Text = dt.Rows[e.RowIndex]["Email"].ToString();
            }
        }

        private void btnsndEml_Click(object sender, EventArgs e)
        {

            engine.RecognizeAsyncStop();
            engineadd.RecognizeAsyncStop();
            engineALPHA.RecognizeAsyncStop();
            engineEml.RecognizeAsyncStop();
            engineGet.RecognizeAsyncStop();
            this.Hide();
            Recipient cse = new Recipient();
            cse.contact = txtSendEml.Text;
            /*.All(Char.IsLetter);*/
            cse.SdrE = cntctEml;
            cse.SdrP = cntctPwd;
            cse.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dt.Rows.Count)
            {
                txtSendEml.Text = dt.Rows[e.RowIndex]["Email"].ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ss.SpeakAsyncCancelAll();
            engine.RecognizeAsyncStop();
            engineadd.RecognizeAsyncStop();
            engineALPHA.RecognizeAsyncStop();
            engineEml.RecognizeAsyncStop();
            engineGet.RecognizeAsyncStop();
            this.Hide();
            Thread.Sleep(2000);
            menu ibx = new menu();
            ibx.SdrEi = cntctEml;
            ibx.SdrPi = cntctPwd;
            ibx.Show();
        }

        

        public contact()
        {
            InitializeComponent();
            accessDB.InitializeDB();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            name = txtNm.Text;
            email = txtEml.Text + "@gmail.com";
            
            try
            {
                accessDB adb = new accessDB();
                adb.insertContact(name, email, cntctEml);
                ss.SpeakAsyncCancelAll();
                ss.SpeakAsync("contact saved");
                engine.RecognizeAsyncStop();
                engineadd.RecognizeAsyncStop();
                engineALPHA.RecognizeAsyncStop();
                engineEml.RecognizeAsyncStop();
                engineGet.RecognizeAsyncStop();
                this.Hide();
                Thread.Sleep(1000);
                menu ibx = new menu();
                ibx.SdrEi = cntctEml;
                ibx.SdrPi = cntctPwd;
                ibx.Show();
                
            }
            catch (Exception ee)
            {
                ss.SpeakAsync(ee.Message);
                errorhandler();
            }
        }
    }
}
