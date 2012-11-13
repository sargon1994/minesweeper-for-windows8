using Minesweeper.Document;
using MineSweeperViewProject.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Minesweeper
{
    public sealed partial class InfoUserControl : UserControl, FieldListener
    {
        private static String endText = "";
        public InfoUserControl()
        {
            this.InitializeComponent();
        }

        public void setField(Field field)
        {
            field.addListener(this);
            if (!field.GameHasEnded)
            {
                this.info.Text = "/";
                this.numberOfMines.Text = field.NumberOfMines + "";
                this.needToFind.Text = field.NeedToFind + "";
            }
            else
            {
                this.info.Text = endText;
            }
        }

        public void fieldChanged(MineSweeperViewProject.Document.Field field)
        {
            this.needToFind.Text = field.NeedToFind + "";
        }

        public void gameEnded(MineSweeperViewProject.Document.Field field, bool win)
        {
            this.numberOfMines.Text = "";
            this.needToFind.Text =  "";
            endText = win ? "You have won!" : "You have lost!";
            this.info.Text = endText;
        }
    }


}
