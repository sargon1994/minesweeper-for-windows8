using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper.Document
{
    public class Field
    {
        Square[,] squares;
        int width, heigth;
        int mines;
        bool gameHasEnded;
        int pickedSquares;
        int flaggedSquares;

        public Field(int width, int heigth, int mines)
        {
            this.width = width;
            this.heigth = heigth;
            this.mines = mines;
            this.gameHasEnded = false;
            this.pickedSquares = 0;
            this.flaggedSquares = 0;

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

        public int PickedSquares
        {
            get { return pickedSquares; }
        }

        public int FlaggedSuares
        {
            get { return flaggedSquares; }
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
            if (this.pickedSquares == 0)
            {
                this.Init(picked);
            }
            bool result = picked.Pick();
            pickedSquares++;
            if (result == true)
            {
                gameHasEnded = true;
                return false;
            }
            else if (pickedSquares >= this.Width * this.Heigth - mines)
            {
                gameHasEnded=true;
                return true;
            }
            else return null;
        }

        public void Flag(Square picked)
        {
            if (picked.isFlagged)
            {
                picked.isFlagged = false;
                flaggedSquares--;
            }
            else
            {
                picked.isFlagged = true;
                flaggedSquares++;
            }
        }

        public bool GameHasEnded
        {
            get { return this.gameHasEnded; }
        }
    }
}

