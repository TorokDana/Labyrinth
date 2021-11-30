using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirintus.Persistence
{
    /// <summary>
    /// Labirintus játéktábla típusa.
    /// </summary>
    public class LabyrinthTable
    {
        #region Fields

        private Int32 _Size; // méret
        private Int32[,] _fieldValues; // mezőértékek 0:út 1:fal
        private Tuple<Int32,Int32> _curCoord; // aktuális coordináták

        #endregion

        #region Properties
        /// <summary>
        /// Játéktábla méretének lekérdezése.
        /// </summary>
        public Int32 Size { get { return _Size; } }

        /// <summary>
        /// Aktuális koordináta lekérdezése.
        /// </summary>
        public Tuple<Int32, Int32> Coord { get { return _curCoord; } set { _curCoord = value; } }


        /// <summary>
        /// Mező értékének lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>0:út 1:fal</returns>
        ///public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }
        #endregion

        #region Constructors

        /// <summary>
        /// Játéktábla példányosítása.
        /// </summary>
        

        /// <summary>
        /// Sudoku játéktábla példányosítása.
        /// </summary>
        /// <param name="tableSize">Játéktábla mérete.</param>
        /// <param name="regionSize">Ház mérete.</param>
        public LabyrinthTable(Int32 Size)
        {
            if (Size != 10 && Size != 15 && Size != 20)
                throw new ArgumentOutOfRangeException("There is no table,that fulfills this size", "tableSize");
            


            _Size = Size;
            _curCoord = Tuple.Create(0,Size-1);
            _fieldValues = new Int32[Size,Size];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// fal  vagy út lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>Igaz, ha a mező ki van töltve, egyébként hamis.</returns>
        public Boolean isRoad(Tuple<Int32,Int32> coord)
        {
            if (coord.Item1 < 0 || coord.Item1 >= _Size)
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (coord.Item2 < 0 || coord.Item2 >= _Size)
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[coord.Item1, coord.Item2] == 0;
        }

        /// <summary>
        /// Aktuális helyzet beállítása
        /// </summary>
            
        public void setCoord(Tuple<Int32, Int32> coord)
        {
            if (coord.Item1 < 0 || coord.Item1 >= _Size)
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (coord.Item2 < 0 || coord.Item2 >= _Size)
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            
            _curCoord = coord;

        }


        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= _Size)
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _Size)
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (value != 1 && value != 0)
                throw new ArgumentOutOfRangeException("value", "The value is out of range.");


            _fieldValues[x, y] = value;
        }

        public Boolean isExit()
        {
            return (_curCoord.Item1 == 0 && _curCoord.Item2 == _Size - 1);
        }

        #endregion





    }




}
