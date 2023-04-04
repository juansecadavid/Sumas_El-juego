﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sumas__TheGame
{
    internal class Tower
    {
        private List<Floor> floorList;
        //private bool isFromEnemy;
        private Character.type type;
        public Tower(List<Floor> floorList, Character.type type)
        {
            this.floorList = floorList;
            this.Type = type;
        }

        //public bool IsFromEnemy { get => isFromEnemy; set => isFromEnemy = value; }
        internal List<Floor> FloorList { get => floorList; set => floorList = value; }
        internal Character.type Type { get => type; set => type = value; }

        public void RemoveFloor(Floor floor)
        {
            for (int i = 0; i < floorList.Count; i++)
            {
                if (floorList[i]==floor)
                {
                    floorList.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddFloor(Floor floor)
        {
            floorList.Add(floor);
        }
    }
}
