using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperViewProject.Document
{
    public class Square
    {
        private Square[] neighbours;
        bool mine;
        bool unfolded = false;

        public Square() {
        }

        public void setNeighbours(Square[] neighbours) {
            this.neighbours = neighbours;
        }

        public Square getNeighbour(Adjacent a) {
            return neighbours[(int)a];
        }

        public Square[] getNeighbours()
        {
            return this.neighbours;
        }

        public int NumberOfMineNeighbours
        {
            get
            {
                int mineNeighbours = 0;
                foreach (var neighbour in neighbours)
                    if (neighbour != null && neighbour.isMine) mineNeighbours++;
                return mineNeighbours;
            }
        }

        public bool isMine { get { return this.mine; } set { this.mine = value; } }

        public bool isNumber { get {return (!isMine) && NumberOfMineNeighbours > 0; } }

        public bool isEmpty { get {return !isMine && !isNumber; } }

        public bool isUnfolded { get { return unfolded; } }

        public bool isFlagged { get; set; }

        public bool Pick()
        {
            if (!isUnfolded)
            {
                this.unfolded = true;
                if (isMine) return true;
                else
                {
                    if (isEmpty)
                        foreach (var neighbour in neighbours)
                            if (neighbour != null)
                                neighbour.Pick();
                    return false;
                }
            }
            else return false;
        }
    }
}
