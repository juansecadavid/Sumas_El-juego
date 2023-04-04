using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sumas__TheGame
{
    internal class Character
    {
        public enum type
        {
            main, 
            evil
        }
        private type tipo;
        private int level;

        public Character(int level, type characterType)
        {
            this.level = level;
            this.tipo = characterType;
        }

        public int Level { get => level; set => level = value; }
        private type Tipo { get => tipo; set => tipo = value; }
        
    }
}
