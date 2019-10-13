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
        string nextEposidePath;
        private void mainForm_Load(object sender, EventArgs e)
        {
            //uiMode means - User Interface Mode. The program will create a new file called uiMode in wich you can change the value 1 which means full User Interface.
            if (File.Exists(Environment.CurrentDirectory + "\\uiMode.ini"))//Into 0 wich mean no User Interface great for bing watching something.
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
            }// The only situation the Saved setting will be true and LastPlayed will be 0. Is when an eposide has been changed automatically.
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
            nextEposidePath = Series.getNextEposidePath(openFileDialog.FileName);
        }
        bool clipFinished = false;// Determines if the video is finished.
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)// In a case of the client closing the player we will ask the client weather he want to save
        {//                                                                        the time position.
            if (axWindowsMediaPlayer.URL != "" && clipFinished == false && nextEposidePath == "")
            {
                if (MessageBox.Show("לשמור לך תזמן צפייה אחי?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Settings.UpdateSettings((int)axWindowsMediaPlayer.Ctlcontrols.currentPosition, axWindowsMediaPlayer.URL);
                    
                }
                else
                {
                    Settings.dontSave();
                }
            }
            else if(nextEposidePath != "")
            {
                Series.nextEposide(nextEposidePath);
            }
            
        }

        private void axWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            
            if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded && nextEposidePath != "")
            {
                clipFinished = true;
                axWindowsMediaPlayer.fullScreen = false;
                Application.Exit();
            }
            else if(axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded && nextEposidePath == "")
            {
                Settings.dontSave();
                Application.Restart();
            }
        }

    }
}
