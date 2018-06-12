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

namespace Assignment1Final
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            int tries = 0;       
             
                try
                {
                    x = Convert.ToInt32(numericUpDown1.Value);
                    y = Convert.ToInt32(numericUpDown2.Value);
                    tries = int.Parse(textBox1.Text);
                    if (tries <= 0)
                    {
                        MessageBox.Show("Please enter positive value for number of tries.", "Negative Value");
                    }
                }
                catch (FormatException )
                {                    
                    MessageBox.Show("Please enter valid values!", "Invalid Values");
                }
            
            int c = 1;
            //clearing the .txt file and the richtextbox upon a new request
            using (StreamWriter Writer = new StreamWriter("answers.txt"))
            {
                Writer.Write("");
                richTextBox1.Clear();
            }
            //radio checks
            if (radioButton2.Checked)
            {
                using (StreamWriter Writer = new StreamWriter("answers.txt",append:true))
                {
                    Writer.Write("Intelligent Method \r\n");
                    richTextBox1.AppendText("Intelligent Method \n");
                }
            }
            else if (radioButton1.Checked)
            {
                using (StreamWriter Writer = new StreamWriter("answers.txt",append:true))
                {
                    Writer.Write("Random Method \r\n");
                    richTextBox1.AppendText("Random Method \n");
                }
            }
            //how many times to run the program depending on the tries
            for (int i = 0; i < tries; i++)
            {
                //writing to the .txt file whether intelligent method or random
                if (radioButton1.Checked)
                {
                    using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
                    {

                        Writer.Write("Trial" + c + ": ");
                        richTextBox1.AppendText("Trial" + c + ": ");

                    }
                    RandomMethod(x, y);//calling the random method
                    c++;
                }
                if (radioButton2.Checked)
                {
                    using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
                    {
                        
                        Writer.Write("Trial" + c + ": ");
                        richTextBox1.AppendText("Trial" + c + ": ");

                    }
                    IntelligentMethod(x, y);//calling the intelligent method
                    c++;
                }

                richTextBox1.AppendText("\n");
            }
            /*using (StreamReader Reader = new StreamReader("answers.txt"))
            {
                richTextBox1.Clear();
                while (!Reader.EndOfStream)
                {
                    richTextBox1.AppendText(Reader.ReadLine());
                }
            }*/
        }
        /*
         * intelligent method
         */
        public void IntelligentMethod(int x, int y)
        {
            Cell[,] chess = new Cell[8, 8];

            chessBoard(chess);

            Knight k = new Knight(x, y);
            
            
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        k.move(chess);
                    }
                }
                //number of moves
            using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
            {
                Writer.Write(k.count + " moves ");
               richTextBox1.AppendText(k.count + " moves ");
                Writer.Write("\r\n");
                richTextBox1.AppendText("\n");
            }

            /*
             * Writing the array to the richtextbox and the file
             */
            for (int i = 0; i < 8; i++)
              {
                  for (int j = 0; j < 8; j++)
                  {

                    using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
                    {
                        if ((k.checker[j, i] >= 0) && (k.checker[j, i] < 10))
                        {
                            Writer.Write("0" + k.checker[j, i] + "\t");
                            richTextBox1.AppendText("0" + k.checker[j, i].ToString() + "    ");
                        }
                        else
                        {
                            Writer.Write(k.checker[j, i] + "\t");
                            richTextBox1.AppendText(k.checker[j, i].ToString() + "    ");
                        }
                    }
                }
                using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
                {
                    richTextBox1.AppendText("\n");
                    Writer.Write("\r\n");
                }
                }

        }
        /*
         * Random Method
         */
        public void RandomMethod(int x, int y)
        {
            Cell[,] chess = new Cell[8, 8];

            chessBoard(chess);
            
            KnightRandom k = new KnightRandom(x, y);
            k.count = 0;
            
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        k.move(chess);
                    }
                }
                //writing the moves 
            using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
            {
                Writer.Write(k.count + " moves ");
                richTextBox1.AppendText(k.count + " moves ");
                Writer.Write("\r\n");
                richTextBox1.AppendText("\n");
            }

            /*for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(k.checker[j, i] + "\t");
                }
                Console.WriteLine("");
            }*/

            //writing the array to the richtextbox and the file
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
                    {
                        if ((k.checker[j, i] >= 0) && (k.checker[j, i] < 10))
                        {
                            Writer.Write("0" + k.checker[j, i] + "\t");
                            richTextBox1.AppendText("0" + k.checker[j, i].ToString() + "    ");
                        }
                        else { Writer.Write(k.checker[j, i] + "\t");
                            richTextBox1.AppendText(k.checker[j, i].ToString() + "    ");
                        }
                    }
                }
                using (StreamWriter Writer = new StreamWriter("answers.txt", append: true))
                {
                    richTextBox1.AppendText("\n");
                    Writer.Write("\r\n");
                }
            }


        }
        public static void chessBoard(Cell[,] chess)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    chess[i, j] = new Cell(i, j);
                }
            }
        }    
    }
    public struct Cell
    {
        public int x;
        public int y;
        public bool check;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            check = false;
        }
        //assigning heuristics for intelligent method
        public int GetAccess()
        {
            int access = 8;
            switch (x)
            {
                case 0:
                case 7:
                    {
                        switch (y)
                        {
                            case 0:
                            case 7:
                                access = 2;
                                break;

                            case 1:
                            case 6:
                                access = 3;
                                break;

                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                access = 4;
                                break;

                        }
                    }
                    break;

                case 1:
                case 6:
                    {
                        switch (y)
                        {
                            case 0:
                            case 7:
                                access = 3; break;

                            case 1:
                            case 6:
                                access = 4; break;

                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                access = 6; break;
                        }
                    }
                    break;

                case 2:
                case 3:
                case 4:
                case 5:
                    {
                        switch (y)
                        {
                            case 0:
                            case 7:
                                access = 4;
                                break;

                            case 1:
                            case 6:
                                access = 6;
                                break;

                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                access = 8;
                                break;
                        }
                    }
                    break;
            }

            return access;
        }





    }
    //intelligent method logic
    public struct Knight
    {
        public int x;
        public int y;
        public int count;
        public int[,] checker;

        public Knight(int x, int y)
        {
            this.x = x;
            this.y = y;
            count = 1;
            checker = new int[8, 8];

        }
        public int move(Cell[,] chess)
        {

            checker[x, y] = count;
            chess[x, y].check = true;
            Console.WriteLine(x + " " + y);



            Cell[] possMove = new Cell[8];



            if (x > 5 || y > 6)
                possMove[0] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];
            else
                possMove[0] = chess[x + 2, y + 1];

            if (x > 5)
                possMove[1] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];
            else
                possMove[1] = chess[x + 2, ((y - 1) >= 0) ? (y - 1) : (y + 1)];

            if (y > 6)
                possMove[2] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];
            else
                possMove[2] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), y + 1];
            possMove[3] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];



            if (x > 6 || y > 5)
                possMove[4] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];
            else
                possMove[4] = chess[x + 1, y + 2];

            if (x > 6)
                possMove[5] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];
            else
                possMove[5] = chess[x + 1, ((y - 2) >= 0) ? (y - 2) : (y + 2)];

            if (y > 5)
                possMove[6] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];
            else
                possMove[6] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), y + 2];
            possMove[7] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];



            int sAccess = 8;
            for (int i = 0; i < 8; i++)
            {
                if (possMove[i].GetAccess() <= sAccess)
                {
                    if (possMove[i].check == false)
                    {
                        sAccess = possMove[i].GetAccess();
                    }
                }
            }
            List<Cell> al = new List<Cell>();
            for (int i = 0; i < 8; i++)
            {
                if (possMove[i].GetAccess() == sAccess)
                {
                    if (possMove[i].check == false)
                    {
                        al.Add(possMove[i]);
                    }
                    else
                    {
                        // Console.WriteLine("No more moves left!");
                    }
                }
                if (i > 6)
                {
                   
                    Random r = new Random();
                    int rand = r.Next(0, al.Count);
                    if (al.Count != 0)
                    {
                        this.x = al[rand].x;
                        this.y = al[rand].y;
                        chess[x, y].check = true;
                        count++;
                    }

                }
                
            }
            return count; // number of moves
        }



    }
    //Random method logic
    public struct KnightRandom
    {
        public int x;
        public int y;
        public int count;
        public int[,] checker;

        public KnightRandom(int x, int y)
        {
            this.x = x;
            this.y = y;
            count = 1;
            checker = new int[8, 8];

        }
        public int move(Cell[,] chess)
        {

            checker[x, y] = count;
            chess[x, y].check = true;
            Console.WriteLine(x + " " + y);



            Cell[] possMove = new Cell[8];



            if (x > 5 || y > 6)
                possMove[0] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];
            else
                possMove[0] = chess[x + 2, y + 1];

            if (x > 5)
                possMove[1] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];
            else
                possMove[1] = chess[x + 2, ((y - 1) >= 0) ? (y - 1) : (y + 1)];

            if (y > 6)
                possMove[2] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];
            else
                possMove[2] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), y + 1];
            possMove[3] = chess[((x - 2) >= 0) ? (x - 2) : (x + 2), ((y - 1) >= 0) ? (y - 1) : (y + 1)];



            if (x > 6 || y > 5)
                possMove[4] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];
            else
                possMove[4] = chess[x + 1, y + 2];

            if (x > 6)
                possMove[5] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];
            else
                possMove[5] = chess[x + 1, ((y - 2) >= 0) ? (y - 2) : (y + 2)];

            if (y > 5)
                possMove[6] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];
            else
                possMove[6] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), y + 2];
            possMove[7] = chess[((x - 1) >= 0) ? (x - 1) : (x + 1), ((y - 2) >= 0) ? (y - 2) : (y + 2)];


            List<Cell> al = new List<Cell>();
            for (int i = 0; i < 8; i++)
            {

                if (possMove[i].check == false)
                {
                    al.Add(possMove[i]);
                }


                if (i > 6)
                {
                    Random r = new Random();
                    int rand = r.Next(0, al.Count);
                    if (al.Count != 0)
                    {
                        this.x = al[rand].x;
                        this.y = al[rand].y;
                        chess[x, y].check = true;
                        count++;
                    }

                }
            }

            return count;//Returning moves
        }



    }
}
