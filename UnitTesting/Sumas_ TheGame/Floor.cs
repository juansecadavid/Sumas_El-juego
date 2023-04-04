using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sumas__TheGame
{
    internal class Floor
    {
        private List<Character> charactersList;

        public Floor(List<Character> charactersList)
        {
            this.charactersList = charactersList;
        }

        internal List<Character> CharactersList { get => charactersList; set => charactersList = value; }
        public void RemoveCharacter(Character character)
        {
            for (int i = 0; i < charactersList.Count; i++)
            {
                if (charactersList[i]==character)
                {
                    charactersList.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddCharacter(Character character)
        {
            charactersList.Add(character);
        }
    }
}
