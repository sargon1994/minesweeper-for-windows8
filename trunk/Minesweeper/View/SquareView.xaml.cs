using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MineSweeperViewProject.Document;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MineSweeperViewProject.View
{
    /// <summary>
    /// Interaction logic for SquerView.xaml
    /// </summary>
    public partial class SquareView : UserControl, ContextSensitive
    {
        private static BitmapImage[] nonMineImages;
        private static BitmapImage[] nonMineColoredImages;
        private static BitmapImage activeMineImage, passiveMineImage, unfoldedImage, flagImage, mineFoundImage;

        private static BitmapImage loadImage(String name)
        {
            return new BitmapImage(new Uri(@"ms-appx:///pics/" + name + ".png", UriKind.RelativeOrAbsolute));
        }

        static SquareView()
        {
            nonMineImages = new BitmapImage[9];
            nonMineColoredImages = new BitmapImage[9];
            nonMineImages[0] = loadImage("0");
            nonMineColoredImages[0] = nonMineImages[0];
            for (int i = 1; i <= 8; i++)
            {
                nonMineImages[i] = loadImage("" + i);
                nonMineColoredImages[i] = loadImage(i + "c");
            }
            activeMineImage = loadImage("minedetonated");
            passiveMineImage = loadImage("minerevealed");
            unfoldedImage = loadImage("normal");
            flagImage = loadImage("flag");
            mineFoundImage = loadImage("minefound");
        }

        ViewContext viewContext;

        public SquareView()
        {
            InitializeComponent();
        }

        public Square Square { get; set; }
        public FieldView FieldView { get; set; }

        public void setContext(ViewContext context)
        {
            this.viewContext = context;
            this.DeadSpaceMiddleColumn.Width = new GridLength(context.SquareDeadSpace, GridUnitType.Star);
            this.DeadSpaceMiddleRow.Height = new GridLength(context.SquareDeadSpace, GridUnitType.Star);
        }

        public void updateImage(bool showAll)
        {
            if (this.Square.isUnfolded || showAll)
            {
                if (Square.isEmpty) setImage(nonMineImages[0]);
                else if (Square.isNumber)
                {
                    if(viewContext.IsColored)
                        setImage(nonMineColoredImages[Square.NumberOfMineNeighbours]);
                    else
                        setImage(nonMineImages[Square.NumberOfMineNeighbours]);
                }
                else if (Square.isMine)
                {
                    if (Square.isUnfolded) setImage(activeMineImage);
                    else if (Square.isFlagged) setImage(mineFoundImage);
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

        private void tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.FieldView.pickAndUpdate(Square);
        }

        private void rightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            this.FieldView.flagAndUpdate(Square);
            e.Handled = true;
        }

        /*private void clickSpace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timestamp = e.Timestamp;
            timestampSet = true;
        }

        private void clickSpace_LeftButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (timestampSet && e.Timestamp - timestamp <= viewContext.ClickTime)
            {
                
            }
        }

        private void ClickSpace_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (timestampSet && e.Timestamp - timestamp <= viewContext.ClickTime)
            {
                
            }
        }*/
    }
}
