using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class pacman : Form
    {
        const int NUM_ROWS = 31;
        const int NUM_COLS = 28;
        const int CELL_SIZE = 30;

        Player pc = new Player();
        Ghost Blinky = new Ghost();

        bool right, down, left, up;

        PictureBox[,] pbArr = new PictureBox[NUM_ROWS, NUM_COLS];
        PictureBox pb;
    
        int[,] mapArr = new int[NUM_ROWS,NUM_COLS] 
        {  
            {9, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 10, 9, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 10},
            {11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11},
            {11, 3, 9, 6, 6, 10, 3, 9, 6, 6, 6, 10, 3, 11, 11, 3, 9, 6, 6, 6, 10, 3, 9, 6, 6, 10, 3, 11},
            {11, 3, 11, 4, 4, 11, 3, 11, 4, 4, 4, 11, 3, 11, 11, 3, 11, 4, 4, 4, 11, 3, 11, 4, 4, 11, 3, 11},
            {11, 3, 1, 6, 6, 2, 3, 1, 6, 6, 6, 2, 3, 1, 2, 3, 1, 6, 6, 6, 2, 3, 1, 6, 6, 2, 3, 11},
            {11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 11},
            {11, 3, 9, 6, 6, 10, 3, 9, 10, 3, 9, 6, 6, 6, 6, 6, 6, 10, 3, 9, 10, 3, 9, 6, 6, 10, 3, 11},
            {11, 3, 1, 6, 6, 2, 3, 11, 11, 3, 1, 6, 6, 10, 9, 6, 6, 2, 3, 11, 11, 3, 1, 6, 6, 2, 3, 11},
            {11, 3, 3, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 3, 3, 11},
            {1, 6, 6, 6, 6, 10, 3, 11, 1, 6, 6, 10, 3, 11, 11, 3, 9, 6, 6, 2, 11, 3, 9, 6, 6, 6, 6, 2},
            {4, 4, 4, 4, 4, 11, 3, 11, 9, 6, 6, 2, 3, 1, 2, 3, 1, 6, 6, 10, 11, 3, 11, 4, 4, 4, 4, 4},
            {4, 4, 4, 4, 4, 11, 3, 11, 11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11, 11, 3, 11, 4, 4, 4, 4, 4},
            {4, 4, 4, 4, 4, 11, 3, 11, 11, 3, 9, 6, 6, 8, 8, 6, 6, 10, 3, 11, 11, 3, 11, 4, 4, 4, 4, 4},
            {9, 6, 6, 6, 6, 2, 3, 1, 2, 3, 11, 4, 4, 4, 4, 4, 4, 11, 3, 1, 2, 3, 1, 6, 6, 6, 6, 10},
            {11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11, 4, 16, 17, 19, 4, 4, 11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11},
            {1, 6, 6, 6, 6, 10, 3, 9, 10, 3, 11, 4, 4, 4, 4, 4, 4, 11, 3, 9, 10, 3, 9, 6, 6, 6, 6, 2},
            {4, 4, 4, 4, 4, 11, 3, 11, 11, 3, 1, 6, 6, 6, 6, 6, 6, 2, 3, 11, 11, 3, 11, 4, 4, 4, 4, 4},
            {4, 4, 4, 4, 4, 11, 3, 11, 11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11, 11, 3, 11, 4, 4, 4, 4, 4},
            {4, 4, 4, 4, 4, 11, 3, 11, 11, 3, 9, 6, 6, 6, 6, 6, 6, 10, 3, 11, 11, 3, 11, 4, 4, 4, 4, 4},
            {9, 6, 6, 6, 6, 2, 3, 1, 2, 3, 1, 6, 6, 10, 9, 6, 6, 2, 3, 1, 2, 3, 1, 6, 6, 6, 6, 10},
            {11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11},
            {11, 3, 9, 6, 6, 10, 3, 9, 6, 6, 6, 10, 3, 11, 11, 3, 9, 6, 6, 6, 10, 3, 9, 6, 6, 10, 3, 11},
            {11, 3, 1, 6, 10, 11, 3, 1, 6, 6, 6, 2, 3, 1, 2, 3, 1, 6, 6, 6, 2, 3, 11, 9, 6, 2, 3, 11},
            {11, 3, 3, 3, 11, 11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11, 11, 3, 3, 3, 11},
            {1, 6, 10, 3, 11, 11, 3, 9, 10, 3, 9, 6, 6, 6, 6, 6, 6, 10, 3, 9, 10, 3, 11, 11, 3, 9, 6, 2},
            {9, 6, 2, 3, 1, 2, 3, 11, 11, 3, 1, 6, 6, 10, 9, 6, 6, 2, 3, 11, 11, 3, 1, 2, 3, 1, 6, 10},
            {11, 3, 3, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 11, 11, 3, 3, 3, 3, 3, 3, 11},
            {11, 3, 9, 6, 6, 6, 6, 2, 1, 6, 6, 10, 3, 11, 11, 3, 9, 6, 6, 2, 1, 6, 6, 6, 6, 10, 3, 11},
            {11, 3, 1, 6, 6, 6, 6, 6, 6, 6, 6, 2, 3, 1, 2, 3, 1, 6, 6, 6, 6, 6, 6, 6, 6, 2, 3, 11},
            {11, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 11},
            {1, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 2}
        };

        public pacman()
        {
            InitializeComponent();
        }

        private void pacman_Load(object sender, EventArgs e)
        {
            tmrDown.Enabled = true;
            this.Width = CELL_SIZE * NUM_COLS;
            this.Height = CELL_SIZE * NUM_ROWS;

            makePics();
            loadPics();

            pc.col = 1;
            pc.row = 1;

            pbArr[pc.row, pc.col].Image = ilsPics.Images[12];

            Blinky.col = 13;
            Blinky.row = 17;

        }

        private void makePics()
        {
            for (int r = 0; r < NUM_ROWS; r++)
            {
                for (int c = 0; c < NUM_COLS; c++)
                {
                    pb = new PictureBox();
                    pb.Width = CELL_SIZE;
                    pb.Height = CELL_SIZE;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BackColor = Color.Black;

                    tlpPacman.Controls.Add(pb);
                    pbArr[r, c] = pb;
                    pb.Visible = true;
                    pb.Margin = new Padding(0);
                }
            }
        }

        private void loadPics()
        {
            for (int r = 0; r < NUM_ROWS; r++)
            {
                for (int c = 0; c < NUM_COLS; c++)
                {
                    switch (mapArr[r, c])
                    {
                        case 1:
                            pbArr[r, c].Image = ilsPics.Images[0];
                            break;
                        case 2:
                            pbArr[r, c].Image = ilsPics.Images[1];
                            break;
                        case 3:
                            pbArr[r, c].Image = ilsPics.Images[2];
                            break;
                        case 4:
                            pbArr[r, c].Image = ilsPics.Images[3];
                            break;
                        case 5:
                            pbArr[r, c].Image = ilsPics.Images[4];
                            break;
                        case 6:
                            pbArr[r, c].Image = ilsPics.Images[5];
                            break;
                        case 7:
                            pbArr[r, c].Image = ilsPics.Images[6];
                            break;
                        case 8:
                            pbArr[r, c].Image = ilsPics.Images[7];
                            break;
                        case 9:
                            pbArr[r, c].Image = ilsPics.Images[8];
                            break;
                        case 10:
                            pbArr[r, c].Image = ilsPics.Images[9];
                            break;
                        case 11:
                            pbArr[r, c].Image = ilsPics.Images[10];
                            break;
                        case 12:
                            pbArr[r, c].Image = ilsPics.Images[11];
                            break;
                        case 13:
                            pbArr[r, c].Image = ilsPics.Images[12];
                            break;
                        case 14:
                            pbArr[r, c].Image = ilsPics.Images[13];
                            break;
                        case 15:
                            pbArr[r, c].Image = ilsPics.Images[14];
                            break;
                        case 16:
                            pbArr[r, c].Image = ilsPics.Images[15];
                            break;
                        case 17:
                            pbArr[r, c].Image = ilsPics.Images[16];
                            break;
                        case 18:
                            pbArr[r, c].Image = ilsPics.Images[17];
                            break;
                        case 19:
                            pbArr[r, c].Image = ilsPics.Images[18];
                            break;

                    }

                }
            }
        }

        public enum DIR
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum MAPPARTS
        {
            Coin,
            Empty,
            TL, //top left
            H, //horizontal
            TR, //top right
            V, //vertical
            BL, //bottom left
            BR, //bottom right
            Inky, //blue ghost
            Clyde, //orange ghost
            Blinky, //red ghost
            Pinky, //pink ghost
            PMup, //pacman up
            PMdown, 
            PMleft, 
            PMright, 
            Doorway, //ghost safe zone
            Energizer
        }

        public enum MODE
        {
            Chase,
            Scatter,
            Frightened
        }

        struct Player
        {
            //const int VALUE = 
            public DIR dir; 
            public int row;
            public int col;
            public Image pic;
            //const int VALUE
        }

        struct Ghost
        {
            public int value;
            public DIR dir;
            public int row;
            public int col;
            public MODE mode;
            public int prevContents; //what was in the tile before ghost arrives 
        }

        private void pacman_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case (Keys.Down):
                    {
                        pc.dir = DIR.Down;
                        down = true;
                        up = false;
                        right = false;
                        left = false;
                        tmrDown.Enabled = true;
                    }
                    break;
                case (Keys.Up):
                    {
                        pc.dir = DIR.Up;

                        up = true;
                        down = false;
                        right = false;
                        left = false;

                        tmrUp.Enabled = true;
                    }
                    break;
                case (Keys.Left):
                    {
                        pc.dir = DIR.Left;

                        left = true;
                        up = false;
                        right = false;
                        down = false;

                        tmrLeft.Enabled = true;
                        
                    }
                    break;
                case (Keys.Right):
                    {
                        pc.dir = DIR.Right;

                        right = true;
                        up = false;
                        down = false;
                        left = false;

                        tmrRight.Enabled = true;
                    }
                    break;
            }
        }

        private void tmrDown_Tick(object sender, EventArgs e)
        {
            if (down)
            {
                if (mapArr[pc.row + 1, pc.col] == 3 || mapArr[pc.row + 1, pc.col] == 4)
                {
                    pc.row++;
                    pbArr[pc.row, pc.col].Image = ilsPics.Images[12];
                    pbArr[pc.row - 1, pc.col].Image = ilsPics.Images[3];

                }
                else if (mapArr[pc.row + 1, pc.col] != 3 || mapArr[pc.row + 1, pc.col] != 4)
                    tmrDown.Enabled = false;
            }
        }

        private void tmrBlinky_Tick(object sender, EventArgs e)
        {
            if (pbArr[Blinky.row, Blinky.col] == pbArr[pc.row, pc.col])
            {
                gameOver();
            }

            if (mapArr[Blinky.row - 1, Blinky.col] == 3 || mapArr[Blinky.row - 1, Blinky.col] == 4)//|| mapArr[Blinky.row - 1, Blinky.col] == 8)
            {
                if (pc.row < Blinky.row)
                {
                    Blinky.dir = DIR.Up;

                    int up = mapArr[Blinky.row, Blinky.col];
                   // pbArr[Blinky.row, Blinky.col].Image.Tag = up;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[up];

                    Blinky.row--;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[17];
                }
            }

            if (mapArr[Blinky.row, Blinky.col - 1] == 3 || mapArr[Blinky.row, Blinky.col - 1] == 4) //|| mapArr[Blinky.row, Blinky.col - 1] == 8)
            {
                if (pc.col < Blinky.col)
                {
                    Blinky.dir = DIR.Left;

                    int left = mapArr[Blinky.row, Blinky.col];
                    //pbArr[Blinky.row, Blinky.col].Image.Tag = left;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[left];

                    Blinky.col--;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[17];
                }
            }

            if (mapArr[Blinky.row + 1, Blinky.col] == 3 || mapArr[Blinky.row + 1, Blinky.col] == 4)//|| mapArr[Blinky.row + 1, Blinky.col] == 8)
            {
                if (pc.row > Blinky.row)
                {
                    Blinky.dir = DIR.Down;

                    int down = mapArr[Blinky.row, Blinky.col];
                    //pbArr[Blinky.row, Blinky.col].Image.Tag = down;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[down];

                    Blinky.row++;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[17];
                }
            }

            if (mapArr[Blinky.row, Blinky.col + 1] == 3 || mapArr[Blinky.row, Blinky.col + 1] == 4 )//|| mapArr[Blinky.row, Blinky.col + 1] == 8)
            {
                if (pc.col > Blinky.col)
                {
                    Blinky.dir = DIR.Right;

                    int right = mapArr[Blinky.row, Blinky.col];
                    //pbArr[Blinky.row, Blinky.col].Image.Tag = right;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[right];

                    Blinky.col++;

                    pbArr[Blinky.row, Blinky.col].Image = ilsPics.Images[17];
                }
            }
        }

        private void gameOver()
        {
            tmrBlinky.Enabled = false;
            tmrUp.Enabled = false;
            tmrDown.Enabled = false;
            tmrRight.Enabled = false;
            tmrLeft.Enabled = false;

            MessageBox.Show("Game Over!!!");

            Application.Exit();
        }

        private void tmrUp_Tick(object sender, EventArgs e)
        {
            if (up)
            {
                if (mapArr[pc.row - 1, pc.col] == 3 || mapArr[pc.row + 1, pc.col] == 4 || mapArr[pc.row - 1, pc.col] == 5)
                {
                    pc.row--;
                    pbArr[pc.row, pc.col].Image = ilsPics.Images[13];
                    pbArr[pc.row + 1, pc.col].Image = ilsPics.Images[3];
                }
                else if (mapArr[pc.row - 1, pc.col] != 3 || mapArr[pc.row + 1, pc.col] != 4 || mapArr[pc.row + 1, pc.col] != 5)
                    tmrUp.Enabled = false;
            }
        }

        private void tmrRight_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                if (mapArr[pc.row, pc.col + 1] == 3 || mapArr[pc.row + 1, pc.col] == 4 || mapArr[pc.row, pc.col + 1] == 5)
                {
                    pc.col++;
                    pbArr[pc.row, pc.col].Image = ilsPics.Images[11];
                    pbArr[pc.row, pc.col - 1].Image = ilsPics.Images[3];
                }
                else if (mapArr[pc.row, pc.col+1] != 3 || mapArr[pc.row, pc.col+1] != 4 || mapArr[pc.row, pc.col+1] != 5)
                    tmrRight.Enabled = false;
            }
        }

        private void tmrLeft_Tick(object sender, EventArgs e)
        {
            if (left)
            {
                if (mapArr[pc.row, pc.col - 1] == 3 || mapArr[pc.row + 1, pc.col] == 4 || mapArr[pc.row, pc.col - 1] == 5)
                {
                    pc.col--;
                    pbArr[pc.row, pc.col].Image = ilsPics.Images[14];
                    pbArr[pc.row, pc.col + 1].Image = ilsPics.Images[3];
                }
                else if (mapArr[pc.row, pc.col-1] != 3 || mapArr[pc.row, pc.col-1] != 4 || mapArr[pc.row, pc.col-1] != 5)
                    tmrLeft.Enabled = false;
            }
        }
    }
}
