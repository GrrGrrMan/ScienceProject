using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomUIV1._0
{
    public partial class Form1 : Form
    {
        // Settings
        int lengthMax = 1;
        int delseqtill = 100;

        int MinsTillNextSave = 10;



        bool canLogWordCount = true;

        bool canUseChangeLength = false;

        bool autoSave = true;
        bool clearOnSave = true;

        bool useAbcOnly = false;
        bool useBothAbcAndNumbers = false;
        bool useUniqueOnly = false;
        bool useNumbers = true;
        bool useNumbersAndUnique = false;
        bool useAbcAndUnique = false;
        bool useBothAbcAndNumbersAndUnique = false;
        // Leave These Stuff
        Random random1 = new Random();
        Random random2 = new Random();
        string targetChar = ""; // Ignore this
        int targetCharCount = 0;
        int timesPrinted = 0;
        int msM = 60000;
        bool bToggle = false;
        bool delDummyFolder = true;
        bool autoSaveDebounce = true;
        string ranLetter = "";
        string autoSaveLocation = "";
        string combine = "";
        string[] abc = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        string[] nums = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] numAbc = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9","a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        string[] unique = {"!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+",
"[", "]", "{", "}", ";", ":", "\"", "\"", "\\", "|", ",", ".", "<", ">", "/", "?", "`", "~",
" "};
        string[] numUnique = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
"!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+",
"[", "]", "{", "}", ";", ":", "\"", "\"", "\\", "|", ",", ".", "<", ">", "/", "?", "`", "~",
" "};
        string[] abcUnique = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z","!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+",
"[", "]", "{", "}", ";", ":", "\"", "\"", "\\", "|", ",", ".", "<", ">", "/", "?", "`", "~",
" "};
        string[] abcUniqueNum = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+",
"[", "]", "{", "}", ";", ":", "\"", "\"", "\\", "|", ",", ".", "<", ">", "/", "?", "`", "~",
" "};
        public Form1()
        {
            InitializeComponent();
            saveTimer.Interval = MinsTillNextSave * msM; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (autoSaveDebounce == true && autoSave == true)
            {
                autoSaveDebounce = false;
                folderBrowserDialog1.ShowDialog();
                if (folderBrowserDialog1.SelectedPath == "")
                {
                    MessageBox.Show("Stupid boy", "dum boy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    autoSaveLocation = folderBrowserDialog1.SelectedPath;

                }
            }
            if (bToggle == false)
            {
                bToggle = true;
                button1.Text = "Stop";
                whileTimer.Start();
                if (autoSaveDebounce == false)
                {
                    saveTimer.Start();
                }
            }
            else if (bToggle == true)
            {
                bToggle = false;
                button1.Text = "Start";
                whileTimer.Stop();
                if (autoSaveDebounce == false)
                {
                    saveTimer.Stop();
                }
            }
        }

        private void whileTimer_Tick(object sender, EventArgs e)
        {
            timesPrinted += 1;
            if (timesPrinted > delseqtill && false)
            {
                timesPrinted = 0;
                combine = "";
            }
            if (useAbcOnly == true)
            {
                Random random = new Random();
                var randomNum = random.Next(0, abc.Length);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {
                        var randomNum1 = random1.Next(1, abc.Length);
                        string charV = abc[randomNum1];
                        ranLetter = charV;
                        combine += charV;
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = abc[randomNum];
                    combine += abc[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            else if (useBothAbcAndNumbers == true)
            {
                Random random = new Random();
                var randomNum = random.Next(0, numAbc.Length);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {
                        var randomNum1 = random1.Next(1, numAbc.Length);
                        string charV = numAbc[randomNum1];
                        ranLetter = charV;
                        combine += charV;
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = numAbc[randomNum];
                    combine += numAbc[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            else if (useNumbers == true)
            {
                Random random = new Random();
                var randomNum = random.Next(0, 9);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {

                        ranLetter = randomNum.ToString();
                        combine += randomNum.ToString();
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = nums[randomNum];
                    combine += nums[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            else if (useBothAbcAndNumbersAndUnique == true)
            {

                Random random = new Random();
                var randomNum = random.Next(0, abcUniqueNum.Length);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {
                        var randomNum1 = random1.Next(1, abcUniqueNum.Length);
                        string charV = abcUniqueNum[randomNum1];
                        ranLetter = charV;
                        combine += charV;
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = abcUniqueNum[randomNum];
                    combine += abcUniqueNum[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            else if (useNumbersAndUnique == true)
            {
                Random random = new Random();
                var randomNum = random.Next(0, numUnique.Length);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {
                        var randomNum1 = random1.Next(1, numUnique.Length);
                        string charV = numUnique[randomNum1];
                        ranLetter = charV;
                        combine += charV;
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = numUnique[randomNum];
                    combine += numUnique[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            else if (useAbcAndUnique == true)
            {
                Random random = new Random();
                var randomNum = random.Next(0, abcUnique.Length);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {
                        var randomNum1 = random1.Next(1, abcUnique.Length);
                        string charV = abcUnique[randomNum1];
                        ranLetter = charV;
                        combine += charV;
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = abcUnique[randomNum];
                    combine += abcUnique[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            else if (useUniqueOnly == true)
            {
                Random random = new Random();
                var randomNum = random.Next(0, unique.Length);
                if (canUseChangeLength == true)
                {
                    var randomNum2 = random2.Next(1, lengthMax + 1);
                    for (int num = 0; num < randomNum2; num++)
                    {
                        var randomNum1 = random1.Next(1, unique.Length);
                        string charV = unique[randomNum1];
                        ranLetter = charV;
                        combine += charV;
                        if (canLogWordCount == true)
                        {

                            if (ranLetter == targetChar)
                            {
                                targetCharCount += 1;
                            }

                        }
                    }
                    combine += "\n";
                }
                else if (canUseChangeLength == false)
                {
                    ranLetter = unique[randomNum];
                    combine += unique[randomNum] + "\n";
                    if (canLogWordCount == true)
                    {

                        if (ranLetter == targetChar)
                        {
                            targetCharCount += 1;
                        }

                    }
                }
            }
            richTextBox1.Text = combine;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            var file = File.Create(saveFileDialog1.FileName);
            file.Close();
            using (StreamWriter fileF = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8))
            {
                fileF.WriteLine(DateTime.Now + "\n" + richTextBox1.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            combine = "";

        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            int dirName = 0;
            string saveLocation = "";
            if (!Directory.EnumerateFileSystemEntries(autoSaveLocation).Any())
            {
                Directory.CreateDirectory($"{autoSaveLocation}\\DummyFolder");
            }
            HashSet<string> existingDirectories = Directory.GetDirectories(autoSaveLocation).Select(d => new DirectoryInfo(d).Name).ToHashSet();
            while (existingDirectories.Contains($"{dirName}"))
            {
                dirName += 1;
            }
            string newDirPath = $"{autoSaveLocation}\\{dirName}";
            Directory.CreateDirectory(newDirPath);
            saveLocation = $"{newDirPath}\\Result.txt";
            using (StreamWriter fileF = new StreamWriter(saveLocation, false, Encoding.UTF8))
            {
                fileF.WriteLine(DateTime.Now + "\n" + richTextBox1.Text);
            }
            if (clearOnSave)
            {
                combine = "";
                richTextBox1.Text = "";
            }

        }
    }

}
