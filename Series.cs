using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayer
{
    class Series
    {
        List<Eposide> eposides = new List<Eposide>();

        public List<Eposide> GetAllNextEposides(string path)
        {
            fillEposidesList(path, false);
            eposides.Add(Eposide.getEposide(path));
            fillEposidesList(path, true);
            
            return eposides;
        }
        public void fillEposidesList(string path, bool next)
        {
            Eposide eposide = new Eposide(getEposidePath(path, next));
            if(eposide.Path != "")
            {
                eposides.Add(eposide);
                fillEposidesList(eposide.Path,next);
            }
        }
        public static string getEposidePath(string path, bool next)
        {
            string eposideString = "";
            for (int i = 0; i < 9; i++)// We are looping until 9 because this the decimal number of the eposide can't be larger than 90. In most series at least.
            {
                foreach (string sub in path.Split('.'))
                {
                    if (sub.Contains("E" + i))
                    {
                        eposideString = sub;
                    }
                }
            }
            if (eposideString != "")
            {                       //                                  Converting the string eposide number to integer in order to raise it by 1.
                int eposideNum = int.Parse(eposideString[eposideString.Count() - 2].ToString() + eposideString[eposideString.Count() - 1].ToString());//We take the last 2 charecters because they contain the eposide number.
                int newEposideNum = 0;
                if (next == true)
                {
                    newEposideNum = eposideNum + 1;// Raising the eposide integer by 1 and into a new variable.
                }
                else
                {
                    newEposideNum = eposideNum - 1;// Decreasing the eposide integer by 1 and into a new variable.
                }
                string stingEposideNum = eposideNum.ToString();//We put both the integers eposideNum and newEposideNum into string variables because the eposide might be less than 10. 
                string stringNewEposideNum = newEposideNum.ToString();//And in that case we would want to add a - 0 before the real number.
                if (eposideNum / 10 == 0)// We need to check
                {
                    stingEposideNum = "0" + eposideNum.ToString();
                }
                if (newEposideNum / 10 == 0)
                {
                    stringNewEposideNum = "0" + newEposideNum.ToString();
                }
                //Replacing in the original eposideString wich also include the season number, The eposide number.
                string newEposideString = eposideString.Replace("E" + stingEposideNum, "E" + stringNewEposideNum);
                string nextEposidePath = path.Replace(eposideString, newEposideString);
                if (File.Exists(nextEposidePath))//Checking if the next eposide path exists.
                {//If the path exists the function will return the path of the next eposide.
                    return nextEposidePath;
                }

            }
            return "";
        }

        public static void startNextEposide(string nextEposidePath)
        {
                        Settings.UpdateSettings(0, nextEposidePath);
                        Application.Restart();
        }
    }
}
