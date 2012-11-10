
namespace MineSweeperViewProject.View
{
    public class ViewContext
    {
        private double squareDeadSpace = 8;

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

    }
}
