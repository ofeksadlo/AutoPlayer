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
        public static string getNextEposidePath(string path)
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
                int newEposideNum = eposideNum + 1;// Raising the eposide integer by 1 and into a new variable.
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
                {//If the path exists we are saving the path of the next eposide. And relaunching the program so the next eposide will start immediately.
                    return nextEposidePath;
                }

            }
            return "";
        }
        public static void nextEposide(string nextEposidePath)
        {
                        Settings.UpdateSettings(0, nextEposidePath);
                        Application.Restart();
        }
    }
}
