using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labirintus.Model;
using Labirintus.Persistence;

namespace Labyrinth
{
    public partial class GameForm : Form
    {
        #region Fields

        private LabyrinthFileDataAccess _dataAccess; // adatelérés
        private LabyrinthGameModel _model; // játékmodell
        private Button[,] _buttonGrid; // gombrács
        private Timer _timer; // időzítő
       


        #endregion

        #region Constructors

        /// <summary>
        /// Játékablak példányosítása.
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form event handlers

        /// <summary>
        /// Játékablak betöltésének eseménykezelője.
        /// </summary>
        /// 
        private void GameForm_Load_1(object sender, EventArgs e)
        {
            _dataAccess = new LabyrinthFileDataAccess();

            // modell létrehozása és az eseménykezelők társítása
            _model = new LabyrinthGameModel(_dataAccess);
            _model.GameAdvanced += new EventHandler<LabyrinthEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<LabyrinthEventArgs>(Game_GameOver);

            // időzítő létrehozása
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(Timer_Tick);

            ClearTable();
            _model.GameDifficulty = GameDifficulty.Easy;
            _model.NewGame();
            GenerateTable();
            SetupTable();
            _timer.Start();
        }

       

        #endregion

        private void kicsiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTable();
            _model.GameDifficulty=GameDifficulty.Easy;
            _model.NewGame();
            GenerateTable();
            SetupTable();         
             _timer.Start();
            szünetToolStripMenuItem.Text = "Szünet";


        }

        private void közepesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTable();
            _model.GameDifficulty = GameDifficulty.Medium;
            _model.NewGame();
            GenerateTable();        
            SetupTable();
            _timer.Start();
            szünetToolStripMenuItem.Text = "Szünet";
        }

        private void nagyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTable();
            _model.GameDifficulty = GameDifficulty.Hard;
            _model.NewGame();
            GenerateTable();
            SetupTable();
            _timer.Start();
            szünetToolStripMenuItem.Text = "Szünet";
        }

        private void Game_GameAdvanced(Object sender, LabyrinthEventArgs e)
        {
            _toolLabelGameTime.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");

            // játékidő frissítése
        }

        private void Game_GameOver(Object sender, LabyrinthEventArgs e)
        {
            _timer.Stop();

            foreach (Button button in _buttonGrid) // kikapcsoljuk a gombokat
                button.Enabled = false;

            SetupTable();



            if (e.IsWon) // győzelemtől függő üzenet megjelenítése
            {
                MessageBox.Show("Gratulálok, győztél!" + Environment.NewLine +
                                TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                                "Labirintus",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }


            _model.NewGame();
            SetupTable();
            toolStripStatusLabel2.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
            szünetToolStripMenuItem.Text = "Kezdés";

        }


        private void Timer_Tick(Object sender, EventArgs e)
        {
            _model.AdvanceTime(); // játék léptetése
            toolStripStatusLabel2.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
           // _toolLabelGameTime.Refresh();

        }

        private void ClearTable()
        {

            if (_buttonGrid!=null)
                    {
            for (Int32 i = 0; i < _model.Table.Size; i++)
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    
                         Controls.Remove(_buttonGrid[i, j]);
                    }

                   
                }

        }
        

        private void GenerateTable()
        {
            _buttonGrid = new Button[_model.Table.Size, _model.Table.Size];
            int buttonsize = 850 / _model.Table.Size;
            for (Int32 i = 0; i < _model.Table.Size; i++)
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Location = new Point(buttonsize * j+6, buttonsize * i+28); // elhelyezkedés
                    _buttonGrid[i, j].Size = new Size(buttonsize, buttonsize); // méret
                    _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;
                    _buttonGrid[i, j].Enabled = false; // kikapcsolt állapot
                    _buttonGrid[i, j].TabIndex = 100 + i * _model.Table.Size + j; // a gomb számát a TabIndex-ben tároljuk
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat; // lapított stípus
                    _buttonGrid[i, j].FlatAppearance.BorderSize = 0;
                    // közös eseménykezelő hozzárendelése minden gombhoz

                    Controls.Add(_buttonGrid[i, j]);
                    // felvesszük az ablakra a gombot
                }
        }

        private void SetupTable()
        {
            Image wall = Image.FromFile("wall.jpg");
            Image road = Image.FromFile("road.jpg");
            Image torch = Image.FromFile("torch.png");

            Tuple<Int32, Int32> curCoord = _model.CurCoord();
            for (Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < _buttonGrid.GetLength(1); j++)
                {
                    if (curCoord.Item1 == i && curCoord.Item2 == j) // ha itt állunk éppen
                    {
                        _buttonGrid[i, j].BackgroundImage = torch;
                        _buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if ((curCoord.Item1 + 1 == i || curCoord.Item1 - 1 == i  || curCoord.Item1  == i) && (curCoord.Item2 + 1 == j || curCoord.Item2 - 1 == j || curCoord.Item2  == j))
                    {
                        if (_model.isRoad(i, j))
                        {
                            _buttonGrid[i, j].BackgroundImage = road;
                            _buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else
                        {
                            _buttonGrid[i, j].BackgroundImage = wall;
                            _buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                    else if (curCoord.Item1 - 2 == i && curCoord.Item2 + 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 + 1))  // ha előtte út
                        {
                            if (_model.isRoad(curCoord.Item1 - 2, curCoord.Item2 + 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 - 1 == i && curCoord.Item2 + 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 + 1) && _model.isRoad(curCoord.Item1, curCoord.Item2 + 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 + 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 == i && curCoord.Item2 + 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1, curCoord.Item2 + 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1, curCoord.Item2 + 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 + 1 == i && curCoord.Item2 + 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 + 1) && _model.isRoad(curCoord.Item1, curCoord.Item2 + 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 + 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 + 2 == i && curCoord.Item2 + 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 + 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 2, curCoord.Item2 + 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 - 2 == i && curCoord.Item2 + 1 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 + 1) && _model.isRoad(curCoord.Item1 - 1, curCoord.Item2))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 - 2, curCoord.Item2 + 1))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 - 2 == i && curCoord.Item2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 - 2, curCoord.Item2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 - 2 == i && curCoord.Item2 - 1 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2) && _model.isRoad(curCoord.Item1 - 1, curCoord.Item2 - 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 - 2, curCoord.Item2 - 1))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 - 2 == i && curCoord.Item2 - 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 - 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 - 2, curCoord.Item2 - 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }


                    else if (curCoord.Item1 - 1 == i && curCoord.Item2 - 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 - 1) && _model.isRoad(curCoord.Item1, curCoord.Item2 - 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 - 1, curCoord.Item2 - 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }


                    else if (curCoord.Item1 == i && curCoord.Item2 - 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1, curCoord.Item2 - 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1, curCoord.Item2 - 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }


                    else if (curCoord.Item1 + 1 == i && curCoord.Item2 - 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1, curCoord.Item2 - 1) && _model.isRoad(curCoord.Item1 + 1, curCoord.Item2 - 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 - 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }


                    else if (curCoord.Item1 + 2 == i && curCoord.Item2 - 2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 - 1))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 2, curCoord.Item2 - 2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 + 2 == i && curCoord.Item2 - 1 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 - 1) && _model.isRoad(curCoord.Item1 + 1, curCoord.Item2))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 2, curCoord.Item2 - 1))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }



                    else if (curCoord.Item1 + 2 == i && curCoord.Item2 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 2, curCoord.Item2))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }

                    else if (curCoord.Item1 + 2 == i && curCoord.Item2 + 1 == j)  //ez a mező
                    {
                        if (_model.isRoad(curCoord.Item1 + 1, curCoord.Item2 + 1) && _model.isRoad(curCoord.Item1 + 1, curCoord.Item2))  // ha előtte látjuk
                        {
                            if (_model.isRoad(curCoord.Item1 + 2, curCoord.Item2 + 1))  // ha ez út
                            {
                                _buttonGrid[i, j].BackgroundImage = road;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                _buttonGrid[i, j].BackgroundImage = wall;_buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        else
                        {
                            _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                        }
                    }
                    else
                    {
                        _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].BackgroundImage = null;   // ha nem látjuk
                    }




                }
            }


            _toolLabelGameTime.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {

            //1:fel 2:le 3:jobbra 4:balra
            if (_timer.Enabled)
            {
                switch (e.KeyCode)
            {
                case Keys.Left:
                    _model.Step(4);
                    break;
                case Keys.Right:
                    _model.Step(3);
                    break;
                case Keys.Up:
                    _model.Step(1);
                    break;
                case Keys.Down:
                    _model.Step(2);
                    break;
            }

                SetupTable();
            }
            


            
        }

        private void szünetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (_timer.Enabled)
            {
                _timer.Stop();
                szünetToolStripMenuItem.Text = "Folytatás";
            }
            else
            {
                _timer.Start();
                szünetToolStripMenuItem.Text = "Szünet";
            }
          
        }

        
    }
}
