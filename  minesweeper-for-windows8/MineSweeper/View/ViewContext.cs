
namespace MineSweeper.View
{
    public class ViewContext
    {
        //in ms
        private int clickTime = 150;

        public int ClickTime
        {
            get { return clickTime; }
            set { clickTime = value; }
        }

        private int moveTime=250;

        public int MoveTime
        {
            get { return moveTime; }
            set { moveTime = value; }
        }

        private double squareDeadSpace = 8;

        public double SquareDeadSpace
        {
            get { return squareDeadSpace; }
            set { squareDeadSpace = value; }
        }

        private double mousewheelSpeed = 0.01;

        public double MouseWheelSpeed
        {
            get { return mousewheelSpeed; }
            set { mousewheelSpeed = value; }
        }

        private double mouseWheelSideSpeed = 1;

        public double MouseWheelSideSpeed
        {
            get { return mouseWheelSideSpeed; }
            set { mouseWheelSideSpeed = value; }
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

        private bool isFocusEnabled = false;

        public bool IsFocusEnabled
        {
            get { return isFocusEnabled; }
            set { isFocusEnabled = value; }
        }
    }
}
