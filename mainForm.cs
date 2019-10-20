using System;
using System.IO;
using System.Windows.Forms;

namespace AutoPlayer
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        Clip currentClip;// Represents the clip the user is currently viewing.
        private void mainForm_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.enableContextMenu = false;
            axWindowsMediaPlayer.CustomContextMenu = contextMenuStrip;
            //uiMode means - User Interface Mode. The program will create a new file called uiMode in wich you can change the value 1 which means full User Interface.
            if (File.Exists(Environment.CurrentDirectory + "\\uiMode.ini"))//Into 0 wich mean no User Interface great for bing watching something.
            {
                if (File.ReadAllText(Environment.CurrentDirectory + "\\uimode.ini") == "0")
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
            if(AutoPlayer.Properties.Settings.Default.LastPlayedPath !="") // Checks if the player has ever been used for playing a clip
            {//If it did it will play the last position of the clip.
                currentClip = Clip.getClipByPath(AutoPlayer.Properties.Settings.Default.LastPlayedPath);
                axWindowsMediaPlayer.Ctlcontrols.currentPosition = currentClip.Time;
                axWindowsMediaPlayer.URL = currentClip.Path;
                this.Text = currentClip.Path;
            }
            else
            {//If the player never been used it will show the open media dialog.
                chooseNewClip();
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {// In a case of the client closing the player. We want to save the last position of the clip so we use this event function.
                if (axWindowsMediaPlayer.URL != "")
            {// We check the url is not null because in a case of first running the program. And closing the open media dialog it will cause a null exception.
                if (Clip.getClipByPath(axWindowsMediaPlayer.URL)== null)
                    {//If the clip doesn't exists in our database at this line of code. It means the client changed eposides
                    //until he got to a clip which doesn't exists in our database. The reason is that changing eposides doesn't add clips to the database.
                        Clip.addClipToDatabase(axWindowsMediaPlayer.URL);
                    }
                    Clip.updateClipTimeFromDatabase(axWindowsMediaPlayer.URL, (int)axWindowsMediaPlayer.Ctlcontrols.currentPosition);
                    AutoPlayer.Properties.Settings.Default.LastPlayedPath = currentClip.Path;
                    AutoPlayer.Properties.Settings.Default.Save();
                }
        }

        private void axWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                currentClip = Clip.getNextClip(currentClip);
                if (currentClip != null)
                {
                    this.Text = currentClip.Path;
                    this.BeginInvoke(new Action(() => {
                        this.axWindowsMediaPlayer.URL = currentClip.Path;
                    }));
                }
                else
                {
                    chooseNewClip();
                }
            }
            if(axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                this.axWindowsMediaPlayer.fullScreen = true;
            }
        }

        private void יציאהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //forcedCloseing = true;
            Application.Exit();
        }
        private void chooseNewClip()
        {
            if(currentClip != null)
            {
                Clip.updateClipTimeFromDatabase(axWindowsMediaPlayer.URL, (int)axWindowsMediaPlayer.Ctlcontrols.currentPosition);
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentClip = Clip.getClipByPath(openFileDialog.FileName);
                if (currentClip == null)
                {
                    Clip.addClipToDatabase(openFileDialog.FileName);
                    currentClip = new Clip(openFileDialog.FileName, 0);
                }
                axWindowsMediaPlayer.URL = openFileDialog.FileName;
                axWindowsMediaPlayer.Ctlcontrols.currentPosition = currentClip.Time;
                this.Text = axWindowsMediaPlayer.URL;
            }
            else
            {
                currentClip = new Clip(axWindowsMediaPlayer.URL, (int)axWindowsMediaPlayer.Ctlcontrols.currentPosition);
            }
        }

        private void חדשToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chooseNewClip();
        }

        private void פרק_הבאToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clip.updateClipTimeFromDatabase(currentClip.Path, (int)axWindowsMediaPlayer.Ctlcontrols.currentPosition);
            currentClip = Clip.getNextClip(currentClip);
            if(currentClip != null)
            {
                this.Text = currentClip.Path;
                axWindowsMediaPlayer.URL = currentClip.Path;
                axWindowsMediaPlayer.Ctlcontrols.currentPosition = currentClip.Time;
            }
            else
            {
                chooseNewClip();
            }
        }

        private void פרקקודםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clip.updateClipTimeFromDatabase(currentClip.Path, (int)axWindowsMediaPlayer.Ctlcontrols.currentPosition);
            currentClip = Clip.getPreviousClip(currentClip);
            if (currentClip != null)
            {
                this.Text = currentClip.Path;
                axWindowsMediaPlayer.URL = currentClip.Path;
                axWindowsMediaPlayer.Ctlcontrols.currentPosition = currentClip.Time;
            }
            else
            {
                chooseNewClip();
            }
           
        }
    }
}
