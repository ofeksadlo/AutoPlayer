using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoPlayer
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        bool changingEposide = false;
        private bool seriesCase()
        {
            string eposideString = "";
            for (int i = 0; i < 9; i++)// We are looping until 9 because this the decimal number of the eposide can't be larger than 90. In most series at least.
            {
                foreach (string sub in axWindowsMediaPlayer.URL.Split('.'))
                {
                    if (sub.Contains("E" + i))
                    {
                        eposideString = sub;
                    }
                }
            }
            if(eposideString != "")
            {                       //                                  Converting the string eposide number to integer in order to raise it by 1.
                int eposideNum = int.Parse(eposideString[eposideString.Count() - 2].ToString() + eposideString[eposideString.Count() - 1].ToString());//We take the last 2 charecters because they contain the eposide number.
                int newEposideNum = eposideNum + 1;// Raising the eposide integer by 1 and into a new variable.
                string stingEposideNum = eposideNum.ToString();//We put both the integers eposideNum and newEposideNum into string variables because the eposide might be less than 10. 
                string stringNewEposideNum = newEposideNum.ToString();//And in that case we would want to add a - 0 before the real number.
                if(eposideNum / 10 == 0)// We need to check
                {
                    stingEposideNum = "0" + eposideNum.ToString();
                }
                if (newEposideNum / 10 == 0)
                {
                    stringNewEposideNum = "0" + newEposideNum.ToString();
                }
                //Replacing in the original eposideString wich also include the season number, The eposide number.
                string newEposideString = eposideString.Replace("E"+ stingEposideNum, "E" + stringNewEposideNum);
                string nextEposidePath = axWindowsMediaPlayer.URL.Replace(eposideString, newEposideString);
                if (File.Exists(nextEposidePath))//Checking if the next eposide path exists.
                {//If the path exists we are saving the path of the next eposide. And relaunching the program so the next eposide will start immediately.
                    AutoPlayer.Properties.Settings.Default.LastPlayed = 0;
                    AutoPlayer.Properties.Settings.Default.Path = nextEposidePath;
                    AutoPlayer.Properties.Settings.Default.Saved = true;
                    AutoPlayer.Properties.Settings.Default.Save();
                    changingEposide = true;
                    Application.Restart();
                    return true;
                }
                
            }
            return false;
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            //uiMode means - User Interface Mode. The program will create a new file called uiMode in wich you can change the value 1 which means full User Interface.
            if(File.Exists(Environment.CurrentDirectory + "\\uiMode.ini"))//Into 0 wich mean no User Interface great for bing watching something.
            {
                if(File.ReadAllText(Environment.CurrentDirectory + "\\uimode.ini") == "0")
                {
                    axWindowsMediaPlayer.uiMode = "none";
                }
                else
                {
                    axWindowsMediaPlayer.uiMode = "full";
                }
            }
            else
            {
                File.WriteAllText(Environment.CurrentDirectory + "\\uiMode.ini", "1");//The starting value is 1 but anything except 0 will do the same.
            }
            if (AutoPlayer.Properties.Settings.Default.Saved == true && AutoPlayer.Properties.Settings.Default.LastPlayed == 0)
            {//In this case the client is watching a series. And we want the new eposide will start immediately.
                axWindowsMediaPlayer.Ctlcontrols.currentPosition = AutoPlayer.Properties.Settings.Default.LastPlayed;
                axWindowsMediaPlayer.URL = AutoPlayer.Properties.Settings.Default.Path;
                this.Text = axWindowsMediaPlayer.URL;
            }
            else if(AutoPlayer.Properties.Settings.Default.Saved == true)
            {//In this case the client is not watching a series. But closed the video while saving the exit point.
                if (MessageBox.Show("?אתה רוצה להמשיך מאותה נקודה", AutoPlayer.Properties.Settings.Default.Path, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    axWindowsMediaPlayer.Ctlcontrols.currentPosition = AutoPlayer.Properties.Settings.Default.LastPlayed;
                    axWindowsMediaPlayer.URL = AutoPlayer.Properties.Settings.Default.Path;
                    this.Text = axWindowsMediaPlayer.URL;
                }
                else
                {
                    if(openFileDialog.ShowDialog()==DialogResult.OK)
                    {
                        axWindowsMediaPlayer.URL = openFileDialog.FileName;
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }//This is the case the client either never opened the program before. Or didn't saved the time position.
            else
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    axWindowsMediaPlayer.URL = openFileDialog.FileName;
                }
                else
                {
                    Application.Exit();
                }
            }

            

        }
        bool clipFinished = false;// Determines if the video is finished.
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)// In a case of closing the player we will ask the client weather he want to save
        {//                                                                        the time position.
            if (axWindowsMediaPlayer.URL != "" && clipFinished == false && changingEposide == false)
            {
                if (MessageBox.Show("לשמור לך תזמן צפייה אחי?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    AutoPlayer.Properties.Settings.Default.LastPlayed = (int)axWindowsMediaPlayer.Ctlcontrols.currentPosition;
                    AutoPlayer.Properties.Settings.Default.Path = axWindowsMediaPlayer.URL;
                    AutoPlayer.Properties.Settings.Default.Saved = true;
                    AutoPlayer.Properties.Settings.Default.Save();
                }
                else
                {
                    AutoPlayer.Properties.Settings.Default.Saved = false;
                    AutoPlayer.Properties.Settings.Default.Save();
                }
            }
            
        }

        private void axWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            
            if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded && seriesCase()== false)
            {
                clipFinished = true;
                axWindowsMediaPlayer.fullScreen = false;

                seriesCase();
                AutoPlayer.Properties.Settings.Default.Saved = false;
                AutoPlayer.Properties.Settings.Default.Save();
                Application.Restart();
            }
        }

    }
}
