
namespace MineSweeperViewProject.View
{
    public class ViewContext
    {
        private double squareDeadSpace = 8;
        private int mapSize = 1;
        private int mineNumber = 1;

        public double SquareDeadSpace
        {
            get { return squareDeadSpace; }
            set { squareDeadSpace = value; }
        }

        private double maxZoomMultiplier = 1.2;

        public double MaxZoomMultiplier
        {
            get { return maxZoomMultiplier; }
            set { maxZoomMultiplier = value; }
        }

        private double minZoom = 2;

        public double MinZoom
        {
            get { return minZoom; }
            set { minZoom = value; }
        }

        private bool colored = true;

        public bool IsColored
        {
            get { return colored; }
            set { colored = value; }
        }

        public int getMineNumber()
        {
            return mineNumber;
        }

        public void setMineNumber(int mN)
        {
            mineNumber = mN;
        }

        public int getMines()
        {
            return getMapWidth() * getMapHeight() / (8-2*getMineNumber());

        }

        public int getMapSize()
        {
            return mapSize;
        }

        public void setMapSize(int mS)
        {
            mapSize = mS;
        }

        public int getMapHeight()
        {
            if (0 == mapSize) {
                return 10;
            }
            else if (1 == mapSize)
            {
                return 15;
            } else {
                return 20;
            }
        }

        public int getMapWidth()
        {
            if (0 == mapSize)
            {
                return 10;
            }
            else if (1 == mapSize)
            {
                return 17;
            }
            else
            {
                return 35;
            }
        }

    }
}
