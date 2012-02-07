using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace word_selecter
{
    public partial class Form1 : Form
    {
        private MatchCollection matchCol2;
        private Random r;
        //private string text;
        private string original_text;
        private string word_sample= "@大学\r\nゼミ\r\n授業\r\n@バイト\r\nレジ\r\n品出し\r\n@ゲーム\r\nギルティ\r\nドラクエ\r\n";
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '1' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            textBox1.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "1";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "2";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "3";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(@"./word.txt"))
            {
                MessageBox.Show("単語の登録情報がなかったので新規に作成します");
                StreamWriter sw = new StreamWriter(
                    @"./word.txt",
                    false,
                    Encoding.GetEncoding("shift_jis")
                    );
                sw.Write(word_sample);
                sw.Close();
            }
            applyCategoryList();
            applyWordList();
            /*
            StreamReader sr = new StreamReader(@"./word.txt", Encoding.GetEncoding("shift_jis"));
            text = sr.ReadToEnd();
            original_text = (string)text.Clone();
            Regex regex = new Regex("(?<=^@).*|(?<=\n@).*");
            MatchCollection matchCol = regex.Matches(text);;
            foreach (Match match in matchCol)
            {
                listBox2.Items.Add(match.ToString());
                listBox3.Items.Add(match.ToString());
            }
            Regex regex2 = new Regex("^\\w.*|(?<=\n)\\w.*");
            matchCol2 = regex2.Matches(text);
            foreach (Match match in matchCol2)
            {
                listBox1.Items.Add(match.ToString());
            }
            sr.Close();
             * */
            r = new Random();
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
            textBox2.Font = fontDialog1.Font;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nw = int.Parse(textBox1.Text);
            int selectIndex = listBox2.SelectedIndex;
            if (checkBox2.Checked == true)
            {
                textBox2.Text = "";
            }
            if (selectIndex != 0)
            {
                string name = listBox2.Items[listBox2.SelectedIndex].ToString();
                name = name.Replace("\r", "");
                //text = (string)original_text.Clone();
                //text = text.Replace("\r\n", "");
                Regex regex = new Regex("(?<=@" + name + "\r\n)[^@]*(?=\r\n)");
                Match match = regex.Matches(original_text)[0];
                Regex regex2 = new Regex("^.*|\n.*");
                matchCol2 = regex2.Matches(match.ToString());
            }
            else
            {
                Regex regex2 = new Regex("^\\w.*|(?<=\n)\\w.*");
                matchCol2 = regex2.Matches(original_text);

            }
            if (nw == 1)
            {
                textBox2.Text += matchCol2[r.Next(matchCol2.Count)].ToString() + "\r\n";

            }
            else
            {
                List<string> text_list = new List<string>();
                foreach (Match match in matchCol2)
                {
                    text_list.Add(match.ToString());
                }

                for (int i = 0; i < nw && text_list.Count != 0; i++)
                {
                    int index = r.Next(text_list.Count);
                    textBox2.Text += text_list[index].ToString() + "\r\n";
                    text_list.RemoveAt(index);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                listBox1.Visible = false;
            else
                listBox1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Ver0.02 β　ランダムで単語を選んで表示するソフトウェア 2010/4/17\nひでまさ");
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Form2 form2;
            using (form2 = new Form2())
            {
                form2.ShowDialog();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 form2;
            using (form2 = new Form2())
            {
                form2.ShowDialog();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ver0.03 β　ランダムで単語を選んで表示するソフトウェア 2010/4/20\nCopyright (C) ひでまさ All Rights Reserved.");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            applyWordList();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Font = fontDialog1.Font;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            setOutputFont(10);
        }
        private void setOutputFont(int size)
        {
            string name = fontDialog1.Font.OriginalFontName;
            fontDialog1.Font = new Font(name, size);
            textBox2.Font = fontDialog1.Font;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            setOutputFont(12);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setOutputFont(15);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(@"./word.txt", saveFileDialog1.FileName);
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox3.SelectedIndex;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("追加する単語を入力してください", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string category = listBox3.Items[listBox3.SelectedIndex].ToString();
                Regex regex = new Regex("^@" + category + "\r\n|\r\n@" + category + "\r\n");
                Match match = regex.Match(original_text);
                string a = match.ToString();
                int insert_point = match.Index + match.Length;
                StreamWriter sw = new StreamWriter(
                    @"./word.txt",
                    false,
                    Encoding.GetEncoding("shift_jis")
                );
                string word = textBox3.Text.Replace("@", "");
                
                original_text = original_text.Insert(insert_point, word + "\r\n");
                sw.Write(original_text);
                sw.Close();
                applyWordList();
                textBox3.Text = string.Empty;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("現在登録されている単語・カテゴリが消去されてしまいますがよろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(@"./word.txt", false, Encoding.GetEncoding("shift-jis"));
                if (checkBox5.Checked)
                    sw.Write(string.Empty);
                else
                {
                    sw.Write(word_sample);
                }
                sw.Close();
                applyCategoryList();
                applyWordList();
            }
        }
        private void applyCategoryList()
        {
            clearCategoryItem(listBox2);
            clearCategoryItem(listBox3);
            StreamReader sr = new StreamReader(@"./word.txt", Encoding.GetEncoding("shift_jis"));
            original_text = sr.ReadToEnd();
            //original_text = (string)text.Clone();
            Regex regex = new Regex("(?<=^@).*(?=\r\n)|(?<=\n@).*(?=\r\n)");
            MatchCollection matchCol = regex.Matches(original_text); ;
            foreach (Match match in matchCol)
            {
                listBox2.Items.Add(match.ToString());
                listBox3.Items.Add(match.ToString());
            }
            Regex regex2 = new Regex("^\\w.*|(?<=\n)\\w.*");
            matchCol2 = regex2.Matches(original_text);
            foreach (Match match in matchCol2)
            {
                listBox1.Items.Add(match.ToString());
            }
            sr.Close();
        }
        private void applyWordList()
        {
            if (original_text == null)
                return;
            string name;
            if (listBox2.SelectedIndex == -1)
                name = string.Empty;
            else
            {
                name = listBox2.Items[listBox2.SelectedIndex].ToString();
            }
            label3.Text = name;
            if (name != "全て")
            {
                //name = name.Replace("\r", "");
                //text = (string)original_text.Clone();
                //text = text.Replace("\r\n", "");
                Regex regex = new Regex("(?<=@" + name + "\r\n)[^@]*\r\n");

                Match match = regex.Match(original_text);
                Regex regex2 = new Regex("^.*(?=\r\n)|(?<=\r\n).*(?=\r\n)");
                matchCol2 = regex2.Matches(match.ToString());
            }
            else
            {
                Regex regex2 = new Regex("^\\w.*(?<=\r\n)|(?<=\r\n)\\w.*(?=\r\n)");
                matchCol2 = regex2.Matches(original_text);
            }
            listBox1.Items.Clear();
            listBox4.Items.Clear();
            foreach (Match match2 in matchCol2)
            {
                listBox1.Items.Add(match2);
                listBox4.Items.Add(match2);
            }
            listBox3.SelectedIndex = listBox2.SelectedIndex;
        }
        private void clearCategoryItem(ListBox listBox)
        {
            listBox.Items.Clear();
            listBox.Items.Add("全て");
            listBox.SelectedIndex = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("追加するカテゴリを入力してください", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (ExistsCategory(textBox4.Text))
            {
                MessageBox.Show("既に存在するカテゴリは追加できません", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string word = textBox4.Text.Replace("@", "");
                original_text += "@" + word + "\r\n";

                StreamWriter sw = new StreamWriter(
                    @"./word.txt",
                    false,
                    Encoding.GetEncoding("shift_jis")
                    );
                sw.Write(original_text);

                sw.Close();
                applyCategoryList();
                textBox4.Text = string.Empty;
            }
        }
        private bool ExistsCategory(string category)
        {
            Regex regex = new Regex("(?<=^@)"+category+"\r\n|(?<=\n@)"+category+"\r\n");
            Match match = regex.Match(original_text);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Form3 form3;
            using (form3 = new Form3())
            {
                form3.ShowDialog();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex > 0)
            {
                string name = listBox3.Items[listBox3.SelectedIndex].ToString();
                Regex regex = new Regex("^@" + name + "[^@]*(?=\r\n@)|\n@" + name + "[^@]*(?=\r\n@)|\n@" + name + "[^@]*(?=\r\n$)", RegexOptions.Singleline);
                Match match = regex.Match(original_text);
                string st = match.ToString();
                int offset = match.Index;
                int deleteLength = match.Length;
                StreamWriter sw = new StreamWriter(
                    @"./word.txt",
                    false,
                    Encoding.GetEncoding("shift_jis")
                    );
                original_text = original_text.Remove(offset, deleteLength);
                sw.Write(original_text);
                sw.Close();

                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                applyCategoryList();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            addWord();
        }
        private void addWord()
        {
            if (listBox4.SelectedIndex != -1)
            {
                string name = listBox4.Items[listBox4.SelectedIndex].ToString();
                Regex regex = new Regex(name + "\r\n", RegexOptions.Singleline);
                Match match = regex.Match(original_text);
                string st = match.ToString();
                int offset = match.Index;
                int deleteLength = match.Length;
                StreamWriter sw = new StreamWriter(
                    @"./word.txt",
                    false,
                    Encoding.GetEncoding("shift_jis")
                );
                original_text = original_text.Remove(offset, deleteLength);
                sw.Write(original_text);
                sw.Close();

                listBox4.Items.RemoveAt(listBox4.SelectedIndex);
                if (listBox4.Items.Count != 0)
                    listBox4.SelectedIndex = 0;
            }
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addWord();
                
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.WordWrap = checkBox4.Checked;
        }
    }
}
