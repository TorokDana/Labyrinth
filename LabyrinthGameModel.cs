using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labirintus.Persistence;


namespace Labirintus.Model
{
    /// <summary>
    /// Játéktábla méretének felsorolási típusa.
    /// </summary>
    public enum GameDifficulty { Easy, Medium, Hard }

    /// <summary>
    /// Labirintus játék típusa.
    /// </summary>
    public class LabyrinthGameModel
    {
        #region Difficulty constants

       
        private const string EasyPath = "10.txt";
        private const string MediumPath = "15.txt";
        private const string HardPath = "20.txt";

        #endregion


        #region Fields
        private LabyrinthFileDataAccess _dataAccess; // adatelérés
        private LabyrinthTable _table; // játéktábla
        private GameDifficulty _gameDifficulty; // nehézség
        private Int32 _gameTime; // játékidő
       
        #endregion

        #region Properties

        /// <summary>
        /// Eltelt játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }

        /// <summary>
        /// Játéktábla lekérdezése.
        /// </summary>
        public LabyrinthTable Table { get { return _table; } }

        /// <summary>
        /// Játék végének lekérdezése.
        /// </summary>
        public Boolean IsGameOver { get { return (_table.Coord.Item1 == 0 && _table.Coord.Item2 == _table.Size - 1); } }

        /// <summary>
        /// Játéknehézség lekérdezése, vagy beállítása.
        /// </summary>
        public GameDifficulty GameDifficulty { get { return _gameDifficulty; } set { _gameDifficulty = value; } }

        #endregion

        #region Events

        /// <summary>
        /// Játék előrehaladásának eseménye.
        /// </summary>
        public event EventHandler<LabyrinthEventArgs> GameAdvanced;

        /// <summary>
        /// Játék végének eseménye.
        /// </summary>
        public event EventHandler<LabyrinthEventArgs> GameOver;

        #endregion

        #region Constructor

        /// <summary>
        /// Játék példányosítása.
        /// </summary>
        /// <param name="dataAccess">Az adatelérés.</param>
        public LabyrinthGameModel(LabyrinthFileDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _table = new LabyrinthTable(10);
            //_gameDifficulty = GameDifficulty.Easy;
        }

        #endregion

        #region Public game methods

        /// <summary>
        /// Új játék kezdése.
        /// </summary>
        public void NewGame()
        {
            LabyrinthTable cucc;
            
            switch (_gameDifficulty) 
            {
                
                case GameDifficulty.Easy:
                    cucc = new LabyrinthTable(10);
                    _gameTime = 0;
                    cucc = _dataAccess.LoadTable(EasyPath,10);
                    cucc.Coord = Tuple.Create(9, 0);
                    _table = cucc;
                    break;

                case GameDifficulty.Medium:
                    cucc = new LabyrinthTable(15);
                    _gameTime = 0;
                    cucc = _dataAccess.LoadTable(MediumPath, 15);
                    cucc.Coord = Tuple.Create(14, 0);
                    _table = cucc;
                    break;

                case GameDifficulty.Hard:
                    cucc = new LabyrinthTable(20);
                    _gameTime = 0;
                    cucc = _dataAccess.LoadTable(HardPath, 20);
                    cucc.Coord = Tuple.Create(19, 0);
                    _table = cucc;
                    break;
            }
        }

        /// <summary>
        /// Játékidő léptetése.
        /// </summary>
        public void AdvanceTime()
        {
            if (IsGameOver) // ha már vége, nem folytathatjuk
                return;

            _gameTime++;
            OnGameAdvanced();

           
        }

        /// <summary>
        /// Táblabeli lépés végrehajtása.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        public void Step(Int32 x)
        {  //1:fel 2:le 3:jobbra 4:balra

            Tuple<Int32, Int32> cor = Tuple.Create(_table.Coord.Item1, _table.Coord.Item2);

            switch (x)
            {
                case 1:
                    if(cor.Item1-1 >= 0 )
                    {
                        cor = Tuple.Create(cor.Item1-1, cor.Item2 );
                    }
                    break;
                case 2:
                    if (cor.Item1 + 1 < _table.Size)
                    {
                        cor = Tuple.Create(cor.Item1 +1, cor.Item2);
                    }
                    break;
                case 3:
                    if (cor.Item2 + 1 < _table.Size)
                    {
                        cor = Tuple.Create(cor.Item1  , cor.Item2+1);
                    }
                    break;
                case 4:
                    if (cor.Item2 -1 >= 0)
                    {
                        cor = Tuple.Create(cor.Item1, cor.Item2-1);
                    }
                    break;
            }

            
            if (!_table.isRoad(cor)) // ha a mező fal, nem léphetünk
                return;

            _table.setCoord(cor);

            
            if (_table.isExit()) // ha vége a játéknak, jelezzük, hogy győztünk
            {
                OnGameOver(true);
            }

            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;
            

            

            

            OnGameAdvanced();
            
        }

        public Boolean isCurCoord (Tuple<Int32, Int32> kerd)
        {
            return kerd == _table.Coord;
        }

        public Tuple<Int32, Int32> CurCoord()
        {
            return _table.Coord;
        }

        public Boolean isRoad(int x, int y )
        {
            return _table.isRoad(Tuple.Create(x,y));
        }





        #endregion



        #region Private game methods

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new LabyrinthEventArgs(false, _gameTime));
        }

         private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new LabyrinthEventArgs(isWon, _gameTime));
        }

        #endregion

    }
}
