using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPlayer
{
    class Clip
    {
        private string path;
        private int time;

        public string Path { get => path; set => path = value; }
        public int Time { get => time; set => time = value; }

        public Clip()
        {
            this.path = "";
            this.time = 0;
        }
        public Clip(string path, int time)
        {
            this.path = path;
            this.time = time;
        }


        public static Clip[] getOldClips()
        {
            List<Clip> clips = new List<Clip>();
            foreach (string clip in AutoPlayer.Properties.Settings.Default.Clips.Split(';'))
            {
                string clipPath = clip.Split(',')[0];
                int clipTime = int.Parse(clip.Split(',')[1]);
                clips.Add(new Clip(clipPath,clipTime));
            }
            return clips.ToArray();
        }
        public static Clip getClipByPath(string path)
        {
            foreach (Clip clip in getOldClips())
            {
                if(clip.path == path)
                {
                    return clip;
                }
            }
            return null;
        }
        public static void addClipToDatabase(string path)
        {
            if (AutoPlayer.Properties.Settings.Default.Clips.Contains(';'))
            {
                AutoPlayer.Properties.Settings.Default.Clips += ";" + path + ",0";
            }
            else
            {
                AutoPlayer.Properties.Settings.Default.Clips += path + ",0";
            }
        }
        public static void updateClipTimeFromDatabase(string path,int newTime)
        {
            string oldClipString = "";
            string newClipString = "";
            foreach (string clip in AutoPlayer.Properties.Settings.Default.Clips.Split(';'))
            {
                if(path == clip.Split(',')[0])
                {
                    oldClipString = clip;
                }
            }
            newClipString = oldClipString.Split(',')[0] + "," + newTime.ToString();
            AutoPlayer.Properties.Settings.Default.Clips = AutoPlayer.Properties.Settings.Default.Clips.Replace(oldClipString, newClipString);
            AutoPlayer.Properties.Settings.Default.Save();
        }

        public static Clip getNextClip(Clip clip)
        {
            string clipString = "";
            for (int i = 0; i < 9; i++)// We are looping until 9 because this the decimal number of the eposide can't be larger than 90. In most series at least.
            {
                foreach (string sub in clip.Path.Split('.'))
                {
                    if (sub.ToUpper().Contains("E" + i))
                    {
                        clipString = sub;
                    }
                }
            }
            if (clipString != "")
            {                       //                                  Converting the string eposide number to integer in order to raise it by 1.
                int clipNum = int.Parse(clipString[clipString.Count() - 2].ToString() + clipString[clipString.Count() - 1].ToString());//We take the last 2 charecters because they contain the eposide number.
                int newClipNum = 0;
                newClipNum = clipNum + 1;// Raising the clip integer by 1 and into a new variable.


                string stingClipNum = clipNum.ToString();//We put both the integers eposideNum and newEposideNum into string variables because the eposide might be less than 10. 
                string stringNewClipNum = newClipNum.ToString();//And in that case we would want to add a - 0 before the real number.
                if (clipNum / 10 == 0)// We need to check
                {
                    stingClipNum = "0" + clipNum.ToString();
                }
                if (newClipNum / 10 == 0)
                {
                    stringNewClipNum = "0" + newClipNum.ToString();
                }
                //Replacing in the original eposideString wich also include the season number, The eposide number.
                string newClipString = clipString.Replace("E" + stingClipNum, "E" + stringNewClipNum);
                string nextClipPath = clip.Path.Replace(clipString, newClipString);
                if (File.Exists(nextClipPath))//Checking if the next eposide path exists.
                {//If the path exists the function will return the clip of the next eposide.
                    return getClipByPath(nextClipPath);
                }

            }
            return null;
        }
        public static Clip getPreviousClip(Clip clip)
        {
            string clipString = "";
            for (int i = 0; i < 9; i++)// We are looping until 9 because this the decimal number of the eposide can't be larger than 90. In most series at least.
            {
                foreach (string sub in clip.Path.Split('.'))
                {
                    if (sub.Contains("E" + i))
                    {
                        clipString = sub;
                    }
                }
            }
            if (clipString != "")
            {                       //                                  Converting the string eposide number to integer in order to raise it by 1.
                int clipNum = int.Parse(clipString[clipString.Count() - 2].ToString() + clipString[clipString.Count() - 1].ToString());//We take the last 2 charecters because they contain the eposide number.
                int newClipNum = 0;
                newClipNum = clipNum - 1;// Decreasing the clip integer by 1 and into a new variable.


                string stingClipNum = clipNum.ToString();//We put both the integers eposideNum and newEposideNum into string variables because the eposide might be less than 10. 
                string stringNewClipNum = newClipNum.ToString();//And in that case we would want to add a - 0 before the real number.
                if (clipNum / 10 == 0)// We need to check
                {
                    stingClipNum = "0" + clipNum.ToString();
                }
                if (newClipNum / 10 == 0)
                {
                    stringNewClipNum = "0" + newClipNum.ToString();
                }
                //Replacing in the original eposideString wich also include the season number, The eposide number.
                string newClipString = clipString.Replace("E" + stingClipNum, "E" + stringNewClipNum);
                string nextClipPath = clip.Path.Replace(clipString, newClipString);
                if (File.Exists(nextClipPath))//Checking if the next eposide path exists.
                {//If the path exists the function will return the clip of the next eposide.
                    return getClipByPath(nextClipPath);
                }

            }
            return null;
        }
    }
}
