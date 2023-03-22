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

namespace SudokuGame
{
    public partial class Form1 : Form
    {
        class sudokuMatrix
        {
            public int[,,] sumat = new int[10, 10, 10]; //0 - цифра, 1-изменяемость значения, 2 - ошибка
        }
        sudokuMatrix SM = new sudokuMatrix();

        public int settr = 0;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TxtReader("clearLVL");
        }

        private void ErrorCatcher(string bName)
        {
            int buf = Convert.ToInt32(bName.Substring(6));
            int x = buf % 10;
            int y = buf / 10;


            if (SM.sumat[y,x,1] == 0) {SM.sumat[y, x, 0] = settr;}
            else {return;}

            for (int x1 = 1; x1 < 10; x1++)
            {
                for (int y1 = 1; y1 < 10; y1++)
                {
                    SM.sumat[y1, x1, 2] = 0;
                }
            }
            Stolb();
            Stroka();
            Kvadrat1();
            SudokuGenerator();
            labelW.Text = WinCheck() == true ? "Вы победили" : "";

        }

        // БОЖЕ ДАЙ МНЕ СИЛ НАПИСАТЬ ОБРАБОТЧИК ОШИБОК

        private void Stolb()
        {
            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    if (SM.sumat[y, x, 0] == 0) { SM.sumat[y, x, 2] = 0; continue; }

                    for(int y2 = y+1; y2 < 10; y2++)
                    {
                        if (SM.sumat[y,x,0] == SM.sumat[y2, x, 0]) { SM.sumat[y, x, 2] = SM.sumat[y2, x, 2] = 1; }
                    }
                }

            }
        }

        private void Stroka()
        {
            for (int y = 1; y < 10; y++)
            {
                for (int x = 1; x < 9; x++)
                {
                    if (SM.sumat[y, x, 0] == 0) { SM.sumat[y, x, 2] = 0; continue; }
                    for (int x2 = x + 1; x2 < 10; x2++)
                    {
                        if (SM.sumat[y, x, 0] == SM.sumat[y, x2, 0]) { SM.sumat[y, x, 2] = SM.sumat[y, x2, 2] = 1; }
                    }
                }
            }
        }

        private void Kvadrat1()
        {
            for (int Kx = 0; Kx < 3; Kx++)
            {
                for (int Ky = 0; Ky < 3; Ky++)
                {
                    Kvadrat2(Kx * 3, Ky * 3);
                }
            }
        }

        private void Kvadrat2(int bufX, int bufY)
        {

            for (int x = 1 + bufX; x < 4 + bufX; x++)
            {
                for (int y = 1 + bufY; y < 4 + bufY; y++)
                {
                    if (SM.sumat[y,x,0] == 0) { SM.sumat[y, x, 2] = 0; continue;}
                    if (x % 3 == 0 && y % 3 == 0) { continue; }

                    int y2 = y, x2 = x;

                    if (y2 % 3 == 0) { x2++; y2 -= 2; }
                    else { y2++; }
                    int x3 = x2, y3 = y2;

                    for (x2 = x3; x2 < 4 + bufX; x2++)
                    {
                        for (y2 = y3; y2 < 4 + bufY; y2++)
                        {
                            if (SM.sumat[y, x, 0] == SM.sumat[y2, x2, 0]) { SM.sumat[y, x, 2] = SM.sumat[y2, x2, 2] = 1; }
                        }
                        y3 = 1 + bufY;
                    }
                }
            }
        }

        private void ColorChanger()
        {
            button11.BackColor = SM.sumat[1, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button12.BackColor = SM.sumat[1, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button13.BackColor = SM.sumat[1, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button14.BackColor = SM.sumat[1, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button15.BackColor = SM.sumat[1, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button16.BackColor = SM.sumat[1, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button17.BackColor = SM.sumat[1, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button18.BackColor = SM.sumat[1, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button19.BackColor = SM.sumat[1, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button21.BackColor = SM.sumat[2, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button22.BackColor = SM.sumat[2, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button23.BackColor = SM.sumat[2, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button24.BackColor = SM.sumat[2, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button25.BackColor = SM.sumat[2, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button26.BackColor = SM.sumat[2, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button27.BackColor = SM.sumat[2, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button28.BackColor = SM.sumat[2, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button29.BackColor = SM.sumat[2, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button31.BackColor = SM.sumat[3, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button32.BackColor = SM.sumat[3, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button33.BackColor = SM.sumat[3, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button34.BackColor = SM.sumat[3, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button35.BackColor = SM.sumat[3, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button36.BackColor = SM.sumat[3, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button37.BackColor = SM.sumat[3, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button38.BackColor = SM.sumat[3, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button39.BackColor = SM.sumat[3, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button41.BackColor = SM.sumat[4, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button42.BackColor = SM.sumat[4, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button43.BackColor = SM.sumat[4, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button44.BackColor = SM.sumat[4, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button45.BackColor = SM.sumat[4, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button46.BackColor = SM.sumat[4, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button47.BackColor = SM.sumat[4, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button48.BackColor = SM.sumat[4, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button49.BackColor = SM.sumat[4, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button51.BackColor = SM.sumat[5, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button52.BackColor = SM.sumat[5, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button53.BackColor = SM.sumat[5, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button54.BackColor = SM.sumat[5, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button55.BackColor = SM.sumat[5, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button56.BackColor = SM.sumat[5, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button57.BackColor = SM.sumat[5, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button58.BackColor = SM.sumat[5, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button59.BackColor = SM.sumat[5, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button61.BackColor = SM.sumat[6, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button62.BackColor = SM.sumat[6, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button63.BackColor = SM.sumat[6, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button64.BackColor = SM.sumat[6, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button65.BackColor = SM.sumat[6, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button66.BackColor = SM.sumat[6, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button67.BackColor = SM.sumat[6, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button68.BackColor = SM.sumat[6, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button69.BackColor = SM.sumat[6, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button71.BackColor = SM.sumat[7, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button72.BackColor = SM.sumat[7, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button73.BackColor = SM.sumat[7, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button74.BackColor = SM.sumat[7, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button75.BackColor = SM.sumat[7, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button76.BackColor = SM.sumat[7, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button77.BackColor = SM.sumat[7, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button78.BackColor = SM.sumat[7, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button79.BackColor = SM.sumat[7, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button81.BackColor = SM.sumat[8, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button82.BackColor = SM.sumat[8, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button83.BackColor = SM.sumat[8, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button84.BackColor = SM.sumat[8, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button85.BackColor = SM.sumat[8, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button86.BackColor = SM.sumat[8, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button87.BackColor = SM.sumat[8, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button88.BackColor = SM.sumat[8, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button89.BackColor = SM.sumat[8, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

            button91.BackColor = SM.sumat[9, 1, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button92.BackColor = SM.sumat[9, 2, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button93.BackColor = SM.sumat[9, 3, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button94.BackColor = SM.sumat[9, 4, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button95.BackColor = SM.sumat[9, 5, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button96.BackColor = SM.sumat[9, 6, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button97.BackColor = SM.sumat[9, 7, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button98.BackColor = SM.sumat[9, 8, 2] == 1 ? Color.Red : Color.WhiteSmoke;
            button99.BackColor = SM.sumat[9, 9, 2] == 1 ? Color.Red : Color.WhiteSmoke;

        }

        private void SudokuGenerator()
        {
            button11.Text = SudokuFunct(1, 1);
            button12.Text = SudokuFunct(1, 2);
            button13.Text = SudokuFunct(1, 3);
            button14.Text = SudokuFunct(1, 4);
            button15.Text = SudokuFunct(1, 5);
            button16.Text = SudokuFunct(1, 6);
            button17.Text = SudokuFunct(1, 7);
            button18.Text = SudokuFunct(1, 8);
            button19.Text = SudokuFunct(1, 9);

            button21.Text = SudokuFunct(2, 1);
            button22.Text = SudokuFunct(2, 2);
            button23.Text = SudokuFunct(2, 3);
            button24.Text = SudokuFunct(2, 4);
            button25.Text = SudokuFunct(2, 5);
            button26.Text = SudokuFunct(2, 6);
            button27.Text = SudokuFunct(2, 7);
            button28.Text = SudokuFunct(2, 8);
            button29.Text = SudokuFunct(2, 9);

            button31.Text = SudokuFunct(3, 1);
            button32.Text = SudokuFunct(3, 2);
            button33.Text = SudokuFunct(3, 3);
            button34.Text = SudokuFunct(3, 4);
            button35.Text = SudokuFunct(3, 5);
            button36.Text = SudokuFunct(3, 6);
            button37.Text = SudokuFunct(3, 7);
            button38.Text = SudokuFunct(3, 8);
            button39.Text = SudokuFunct(3, 9);

            button41.Text = SudokuFunct(4, 1);
            button42.Text = SudokuFunct(4, 2);
            button43.Text = SudokuFunct(4, 3);
            button44.Text = SudokuFunct(4, 4);
            button45.Text = SudokuFunct(4, 5);
            button46.Text = SudokuFunct(4, 6);
            button47.Text = SudokuFunct(4, 7);
            button48.Text = SudokuFunct(4, 8);
            button49.Text = SudokuFunct(4, 9);

            button51.Text = SudokuFunct(5, 1);
            button52.Text = SudokuFunct(5, 2);
            button53.Text = SudokuFunct(5, 3);
            button54.Text = SudokuFunct(5, 4);
            button55.Text = SudokuFunct(5, 5);
            button56.Text = SudokuFunct(5, 6);
            button57.Text = SudokuFunct(5, 7);
            button58.Text = SudokuFunct(5, 8);
            button59.Text = SudokuFunct(5, 9);

            button61.Text = SudokuFunct(6, 1);
            button62.Text = SudokuFunct(6, 2);
            button63.Text = SudokuFunct(6, 3);
            button64.Text = SudokuFunct(6, 4);
            button65.Text = SudokuFunct(6, 5);
            button66.Text = SudokuFunct(6, 6);
            button67.Text = SudokuFunct(6, 7);
            button68.Text = SudokuFunct(6, 8);
            button69.Text = SudokuFunct(6, 9);

            button71.Text = SudokuFunct(7, 1);
            button72.Text = SudokuFunct(7, 2);
            button73.Text = SudokuFunct(7, 3);
            button74.Text = SudokuFunct(7, 4);
            button75.Text = SudokuFunct(7, 5);
            button76.Text = SudokuFunct(7, 6);
            button77.Text = SudokuFunct(7, 7);
            button78.Text = SudokuFunct(7, 8);
            button79.Text = SudokuFunct(7, 9);

            button81.Text = SudokuFunct(8, 1);
            button82.Text = SudokuFunct(8, 2);
            button83.Text = SudokuFunct(8, 3);
            button84.Text = SudokuFunct(8, 4);
            button85.Text = SudokuFunct(8, 5);
            button86.Text = SudokuFunct(8, 6);
            button87.Text = SudokuFunct(8, 7);
            button88.Text = SudokuFunct(8, 8);
            button89.Text = SudokuFunct(8, 9);

            button91.Text = SudokuFunct(9, 1);
            button92.Text = SudokuFunct(9, 2);
            button93.Text = SudokuFunct(9, 3);
            button94.Text = SudokuFunct(9, 4);
            button95.Text = SudokuFunct(9, 5);
            button96.Text = SudokuFunct(9, 6);
            button97.Text = SudokuFunct(9, 7);
            button98.Text = SudokuFunct(9, 8);
            button99.Text = SudokuFunct(9, 9);

            ColorChanger();
        }

        private string SudokuFunct(int y, int x)
        {
            return SM.sumat[y, x, 0] == 0 ? "" : SM.sumat[y, x, 0].ToString();
        }

        private bool WinCheck()
        {
            bool ch = true;
            for (int y = 1; y < 10; y++)
            {
                for (int x = 1; x < 10; x++)
                {
                    if ((SM.sumat[y,x,0] == 0) || (SM.sumat[y,x,2] == 1) || (ch == false)) { ch = false;}
                }
            }
            return ch;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            TxtReader(textBox1.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TxtReader("clearLVL");
        }

        private void TxtReader(string path)
        {
            path = $@"{path}.txt";
            string buf;
            int y = 1;
            labelW.Text = "";

            try
            {

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    while ((buf = sr.ReadLine()) != null)
                    {
                        string s = buf;
                        char[] a = s.ToArray();
                        for (int x = 1; x < 10; x++)
                        {
                            if (Convert.ToString(a[x - 1]) == "0")
                            {
                                SM.sumat[y, x, 1] = 0;
                                SM.sumat[y, x, 2] = 0;
                                SM.sumat[y, x, 0] = 0;
                            }
                            else
                            {
                                string buf1 = Convert.ToString(a[x - 1]); ;
                                SM.sumat[y, x, 1] = 1;
                                SM.sumat[y, x, 0] = Convert.ToInt32(buf1);
                                SM.sumat[y, x, 2] = 0;
                            }
                        }
                        y++;
                    }
                }
            }
            catch
            {
                labelW.Text = "Такого уровня не существует";
            }

            SudokuGenerator();
        }


        //        НЕ ТРОГАТЬ ЭТО ГОВНО        //
        //  КОСТЫЛИ И ГОВНО КОДЫ ДОЛЖНЫ ЖИТЬ  //
        private void button11_Click(object sender, EventArgs e) => ErrorCatcher(button11.Name);

        private void button12_Click(object sender, EventArgs e) => ErrorCatcher(button12.Name);

        private void button13_Click(object sender, EventArgs e) => ErrorCatcher(button13.Name);

        private void button14_Click(object sender, EventArgs e) => ErrorCatcher(button14.Name);

        private void button15_Click(object sender, EventArgs e) => ErrorCatcher(button15.Name);

        private void button16_Click(object sender, EventArgs e) => ErrorCatcher(button16.Name);

        private void button17_Click(object sender, EventArgs e) => ErrorCatcher(button17.Name);

        private void button18_Click(object sender, EventArgs e) => ErrorCatcher(button18.Name);

        private void button19_Click(object sender, EventArgs e) => ErrorCatcher(button19.Name);

        private void button21_Click(object sender, EventArgs e) => ErrorCatcher(button21.Name);

        private void button22_Click(object sender, EventArgs e) => ErrorCatcher(button22.Name);

        private void button23_Click(object sender, EventArgs e) => ErrorCatcher(button23.Name);

        private void button24_Click(object sender, EventArgs e) => ErrorCatcher(button24.Name);

        private void button25_Click(object sender, EventArgs e) => ErrorCatcher(button25.Name);

        private void button26_Click(object sender, EventArgs e) => ErrorCatcher(button26.Name);

        private void button27_Click(object sender, EventArgs e) => ErrorCatcher(button27.Name);

        private void button28_Click(object sender, EventArgs e) => ErrorCatcher(button28.Name);

        private void button29_Click(object sender, EventArgs e) => ErrorCatcher(button29.Name);

        private void button31_Click(object sender, EventArgs e) => ErrorCatcher(button31.Name);

        private void button32_Click(object sender, EventArgs e) => ErrorCatcher(button32.Name);

        private void button33_Click(object sender, EventArgs e) => ErrorCatcher(button33.Name);

        private void button34_Click(object sender, EventArgs e) => ErrorCatcher(button34.Name);

        private void button35_Click(object sender, EventArgs e) => ErrorCatcher(button35.Name);

        private void button36_Click(object sender, EventArgs e) => ErrorCatcher(button36.Name);

        private void button37_Click(object sender, EventArgs e) => ErrorCatcher(button37.Name);

        private void button38_Click(object sender, EventArgs e) => ErrorCatcher(button38.Name);

        private void button39_Click(object sender, EventArgs e) => ErrorCatcher(button39.Name);

        private void button41_Click(object sender, EventArgs e) => ErrorCatcher(button41.Name);

        private void button42_Click(object sender, EventArgs e) => ErrorCatcher(button42.Name);

        private void button43_Click(object sender, EventArgs e) => ErrorCatcher(button43.Name);

        private void button44_Click(object sender, EventArgs e) => ErrorCatcher(button44.Name);

        private void button45_Click(object sender, EventArgs e) => ErrorCatcher(button45.Name);

        private void button46_Click(object sender, EventArgs e) => ErrorCatcher(button46.Name);

        private void button47_Click(object sender, EventArgs e) => ErrorCatcher(button47.Name);

        private void button48_Click(object sender, EventArgs e) => ErrorCatcher(button48.Name);

        private void button49_Click(object sender, EventArgs e) => ErrorCatcher(button49.Name);

        private void button51_Click(object sender, EventArgs e) => ErrorCatcher(button51.Name);

        private void button52_Click(object sender, EventArgs e) => ErrorCatcher(button52.Name);

        private void button53_Click(object sender, EventArgs e) => ErrorCatcher(button53.Name);

        private void button54_Click(object sender, EventArgs e) => ErrorCatcher(button54.Name);

        private void button55_Click(object sender, EventArgs e) => ErrorCatcher(button55.Name);

        private void button56_Click(object sender, EventArgs e) => ErrorCatcher(button56.Name);

        private void button57_Click(object sender, EventArgs e) => ErrorCatcher(button57.Name);

        private void button58_Click(object sender, EventArgs e) => ErrorCatcher(button58.Name);

        private void button59_Click(object sender, EventArgs e) => ErrorCatcher(button59.Name);

        private void button61_Click(object sender, EventArgs e) => ErrorCatcher(button61.Name);

        private void button62_Click(object sender, EventArgs e) => ErrorCatcher(button62.Name);

        private void button63_Click(object sender, EventArgs e) => ErrorCatcher(button63.Name);

        private void button64_Click(object sender, EventArgs e) => ErrorCatcher(button64.Name);

        private void button65_Click(object sender, EventArgs e) => ErrorCatcher(button65.Name);

        private void button66_Click(object sender, EventArgs e) => ErrorCatcher(button66.Name);

        private void button67_Click(object sender, EventArgs e) => ErrorCatcher(button67.Name);

        private void button68_Click(object sender, EventArgs e) => ErrorCatcher(button68.Name);

        private void button69_Click(object sender, EventArgs e) => ErrorCatcher(button69.Name);

        private void button71_Click(object sender, EventArgs e) => ErrorCatcher(button71.Name);

        private void button72_Click(object sender, EventArgs e) => ErrorCatcher(button72.Name);

        private void button73_Click(object sender, EventArgs e) => ErrorCatcher(button73.Name);

        private void button74_Click(object sender, EventArgs e) => ErrorCatcher(button74.Name);

        private void button75_Click(object sender, EventArgs e) => ErrorCatcher(button75.Name);

        private void button76_Click(object sender, EventArgs e) => ErrorCatcher(button76.Name);

        private void button77_Click(object sender, EventArgs e) => ErrorCatcher(button77.Name);

        private void button78_Click(object sender, EventArgs e) => ErrorCatcher(button78.Name);

        private void button79_Click(object sender, EventArgs e) => ErrorCatcher(button79.Name);

        private void button81_Click(object sender, EventArgs e) => ErrorCatcher(button81.Name);

        private void button82_Click(object sender, EventArgs e) => ErrorCatcher(button82.Name);

        private void button83_Click(object sender, EventArgs e) => ErrorCatcher(button83.Name);

        private void button84_Click(object sender, EventArgs e) => ErrorCatcher(button84.Name);

        private void button85_Click(object sender, EventArgs e) => ErrorCatcher(button85.Name);

        private void button86_Click(object sender, EventArgs e) => ErrorCatcher(button86.Name);

        private void button87_Click(object sender, EventArgs e) => ErrorCatcher(button87.Name);

        private void button88_Click(object sender, EventArgs e) => ErrorCatcher(button88.Name);

        private void button89_Click(object sender, EventArgs e) => ErrorCatcher(button89.Name);

        private void button91_Click(object sender, EventArgs e) => ErrorCatcher(button91.Name);

        private void button92_Click(object sender, EventArgs e) => ErrorCatcher(button92.Name);

        private void button93_Click(object sender, EventArgs e) => ErrorCatcher(button93.Name);

        private void button94_Click(object sender, EventArgs e) => ErrorCatcher(button94.Name);

        private void button95_Click(object sender, EventArgs e) => ErrorCatcher(button95.Name);

        private void button96_Click(object sender, EventArgs e) => ErrorCatcher(button96.Name);

        private void button97_Click(object sender, EventArgs e) => ErrorCatcher(button97.Name);

        private void button98_Click(object sender, EventArgs e) => ErrorCatcher(button98.Name);

        private void button99_Click(object sender, EventArgs e) => ErrorCatcher(button99.Name);

        private void button1_Click(object sender, EventArgs e)
        {
            settr = 1;
            label1.Text = settr.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            settr = 2;
            label1.Text = settr.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            settr = 3;
            label1.Text = settr.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            settr = 4;
            label1.Text = settr.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            settr = 5;
            label1.Text = settr.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            settr = 6;
            label1.Text = settr.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            settr = 7;
            label1.Text = settr.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            settr = 8;
            label1.Text = settr.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            settr = 9;
            label1.Text = settr.ToString();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            settr = 0;
            label1.Text = "";
        }

        //   ГОВНОКОД ЗАКОНЧИЛСЯ    //
    }

}

