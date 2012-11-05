using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MineSweeper.Document;

namespace MineSweeper.View
{
    /// <summary>
    /// Interaction logic for SquerView.xaml
    /// </summary>
    public partial class SquareView : UserControl, ContextSensitive
    {
        private static BitmapImage[] nonMineImages;
        private static BitmapImage activeMineImage, passiveMineImage, unfoldedImage, flagImage;

        private static BitmapImage loadImage(String name)
        {
            return new BitmapImage(new Uri(@"\pics\"+name+".png", UriKind.RelativeOrAbsolute));
        }

        static SquareView()
        {
            nonMineImages = new BitmapImage[9];
            nonMineImages[0] = loadImage("empty");
            for (int i = 1; i <= 8; i++)
                nonMineImages[i] = loadImage("" + i);
            activeMineImage = loadImage("active_mine");
            passiveMineImage = loadImage("passive_mine");
            unfoldedImage = loadImage("unfolded");
            flagImage = loadImage("flag");
        }

        ViewContext viewContext;

        int timestamp;
        bool timestampSet = false;

        public SquareView()
        {
            InitializeComponent();
        }

        public Square Square { get; set; }
        public FieldView FieldView { get; set; }

        public void setContext(ViewContext context)
        {
            this.viewContext = context;
            this.MiddleColumn.Width = new GridLength(context.SquareDeadSpace, GridUnitType.Star);
            this.MiddleRow.Height = new GridLength(context.SquareDeadSpace, GridUnitType.Star);
        }

        public void updateImage(bool showAll)
        {
            if (this.Square.isUnfolded || showAll)
            {
                if (Square.isEmpty) setImage(nonMineImages[0]);
                else if (Square.isNumber) setImage(nonMineImages[Square.NumberOfMineNeighbours]);
                else if (Square.isMine)
                {
                    if (Square.isUnfolded) setImage(activeMineImage);
                    else if (Square.isFlagged) setImage(flagImage);
                    else setImage(passiveMineImage);
                }
            }
            else if (this.Square.isFlagged) setImage(flagImage);
            else this.setImage(unfoldedImage);
        }

        private void setImage(ImageSource source)
        {
            if (this.image.Source != source)
                this.image.Source = source;
        }

        private void clickSpace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timestamp = e.Timestamp;
            timestampSet = true;
        }

        private void clickSpace_LeftButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (timestampSet && e.Timestamp - timestamp <= viewContext.ClickTime)
            {
                this.FieldView.pickAndUpdate(Square);
            }
        }

        private void ClickSpace_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (timestampSet && e.Timestamp - timestamp <= viewContext.ClickTime)
            {
                this.FieldView.flagAndUpdate(Square);
            }
        }
    }
}
