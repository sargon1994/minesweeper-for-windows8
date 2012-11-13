using Minesweeper.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperViewProject.Document
{
    public class Field
    {
        HashSet<FieldListener> listeners;

        Square[,] squares;
        int width, heigth;
        int mines;
        bool gameHasStarted;
        bool gameHasEnded;

        public Field(int width, int heigth, int mines)
        {
            this.listeners = new HashSet<FieldListener>();

            this.width = width;
            this.heigth = heigth;
            this.mines = mines;
            this.gameHasEnded = false;
            this.gameHasStarted = false;

            this.squares = new Square[this.width, this.heigth];
            for (int x = 0; x < this.width; x++)
                for (int y = 0; y < this.heigth; y++)
                {
                    this.squares[x, y] = new Square();
                }

            //Setting the neighbours
            for (int x = 0; x < this.width; x++)
                for (int y = 0; y < this.heigth; y++)
                {
                    Square[] neigbours = new Square[8];
                    if (y - 1 >= 0)
                        neigbours[(int)Adjacent.North] = this.squares[x, y - 1];
                    if (y - 1 >= 0 && x + 1 < width)
                        neigbours[(int)Adjacent.NorthEast] = this.squares[x + 1, y - 1];
                    if (x + 1 < width)
                        neigbours[(int)Adjacent.East] = this.squares[x + 1, y];
                    if (x + 1 < width && y + 1 < heigth)
                        neigbours[(int)Adjacent.SouthEast] = this.squares[x + 1, y + 1];
                    if (y + 1 < heigth)
                        neigbours[(int)Adjacent.South] = this.squares[x, y + 1];
                    if (x - 1 >= 0 && y + 1 < heigth)
                        neigbours[(int)Adjacent.SouthWest] = this.squares[x - 1, y + 1];
                    if (x - 1 >= 0)
                        neigbours[(int)Adjacent.West] = this.squares[x - 1, y];
                    if (y - 1 >= 0 && x - 1 >= 0)
                        neigbours[(int)Adjacent.NorthWest] = this.squares[x - 1, y - 1];
                    this.squares[x, y].setNeighbours(neigbours);
                }
        }

        private void Init(Square firstPicked)
        {
            //The first chosen square is an exceptional case, 
            int fieldNeedToPlace = this.width * this.heigth-1;
            int mineNeedToPlace = this.mines;
            Random random = new Random();
            for(int x=0; x<this.width; x++)
                for (int y = 0; y < this.heigth; y++)
                {
                    if (this.squares[x, y] == firstPicked)
                        this.squares[x, y].isMine = false;
                    else
                    {
                        bool isNeighbourOfTheFirstPicked = false;
                        foreach(var n in this.squares[x,y].getNeighbours())
                            if (n == firstPicked)
                            {
                                isNeighbourOfTheFirstPicked = true;
                                break;
                            }

                        if (isNeighbourOfTheFirstPicked)
                            this.squares[x, y].isMine = false;
                        else
                        {
                            bool isMine = random.Next(fieldNeedToPlace) < mineNeedToPlace;
                            this.squares[x, y].isMine = isMine;
                            if (isMine) mineNeedToPlace--;
                            fieldNeedToPlace--;
                        }
                    }
                }
        }

        public void addListener(FieldListener listener)
        {
            this.listeners.Add(listener);
        }

        public int Width
        {
            get { return width; }
        }

        public int Heigth
        {
            get { return heigth; }
        }

        public int NumberOfMines
        {
            get { return mines; }
        }

        public int FlaggedSuares
        {
            get
            {
                int i = 0;
                foreach (var s in this.squares)
                    if (!s.isUnfolded && s.isFlagged)
                        i++;
                return i;
            }
        }

        public int NeedToFind
        {
            get { return NumberOfMines - FlaggedSuares; }
        }

        public Square getSquare(int x, int y)
        {
            return this.squares[x, y];
        }

        public Boolean? Pick(Square picked)
        {
            if(gameHasEnded) return null;
            if (!this.gameHasStarted)
            {
                this.Init(picked);
            }
            gameHasStarted = true;
            bool result = picked.Pick();
            foreach (var l in this.listeners) l.fieldChanged(this);
            if (result == true)
            {
                gameHasEnded = true;
                foreach (var l in this.listeners) l.gameEnded(this, false);
                return false;
            }
            else 
            {
                if (!this.hasUnfoldedNonMine())
                {
                    gameHasEnded = true;
                    foreach (var l in this.listeners) l.gameEnded(this, true);
                    return true;
                }
                else return null;
            }
        }

        private bool hasUnfoldedNonMine()
        {
            for (int x = 0; x < this.width; x++)
                for (int y = 0; y < this.heigth; y++)
                    if (!this.getSquare(x, y).isUnfolded && !this.getSquare(x, y).isMine)
                        return true;
            return false;
        }

        public void Flag(Square picked)
        {
            if (picked.isFlagged)
            {
                picked.isFlagged = false;
            }
            else
            {
                picked.isFlagged = true;
            }
            foreach (var l in this.listeners) l.fieldChanged(this);
        }

        public bool GameHasEnded
        {
            get { return this.gameHasEnded; }
        }
    }
}

