using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirintus.Model
{
    public class LabyrinthEventArgs :EventArgs
    {
        private Int32 _gameTime;
        private Boolean _isWon;

        /// <summary>
        /// Játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }

        /// <summary>
        /// Győzelem lekérdezése.
        /// </summary>
        public Boolean IsWon { get { return _isWon; } }


        /// <summary>
        /// Labirintus eseményargumentum példányosítása.
        /// </summary>
        /// <param name="isWon">Győzelem lekérdezése.</param>
        /// <param name="gameTime">Játékidő.</param>
        public LabyrinthEventArgs(Boolean isWon,  Int32 gameTime)
        {
            _isWon = isWon;           
            _gameTime = gameTime;
        }
    }
}
