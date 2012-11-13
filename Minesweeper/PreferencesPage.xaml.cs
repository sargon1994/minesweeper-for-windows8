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
using MineSweeperViewProject.View;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Minesweeper
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PreferencesPage : Minesweeper.Common.LayoutAwarePage
    {               
        public PreferencesPage()
        {
            this.InitializeComponent();

            ViewContext viewContext = MinesweeperPage.getViewContext();          

            if (viewContext.IsColored)
            {
                coloredNumbers.SelectedIndex = 1;
            }
            else
            {
                coloredNumbers.SelectedIndex = 0;
            }

            if (18 == viewContext.SquareDeadSpace)
            {
                this.interval.SelectedIndex = 0;
            }
            else if (8 == viewContext.SquareDeadSpace)
            {
                this.interval.SelectedIndex = 1;
            } else {
                this.interval.SelectedIndex = 2;
            }

            if (0 == viewContext.getMapSize())
            {
                this.mapSize.SelectedIndex = 0;
            }
            else if (1 == viewContext.getMapSize())
            {
                this.mapSize.SelectedIndex = 1;
            }
            else
            {
                this.mapSize.SelectedIndex = 2;
            }

            if (0 == viewContext.getMineNumber())
            {
                this.difficulty.SelectedIndex = 0;
            }
            else if (1 == viewContext.getMineNumber())
            {
                this.difficulty.SelectedIndex = 1;
            }
            else
            {
                this.difficulty.SelectedIndex = 2;
            }   
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {        
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }        

        private void changeColoredNumbers(object sender, SelectionChangedEventArgs e)
        {
            String color = (String)e.AddedItems.First();
            if (color.Equals("True"))
            {
                MinesweeperPage.getViewContext().IsColored = true;
            }
            else
            {
                MinesweeperPage.getViewContext().IsColored = false;
            }
        }

        private void changeMapSize(object sender, SelectionChangedEventArgs e)
        {            
            String size = (String)e.AddedItems.First();            
            if (size.Equals("Small")) {
                if (0 != MinesweeperPage.getViewContext().getMapSize())
                {
                    MinesweeperPage.getViewContext().setMapSize(0);
                    MinesweeperPage.newGame();
                }                
            }
            else if (size.Equals("Medium"))
            {
                if (1 != MinesweeperPage.getViewContext().getMapSize())
                {
                    MinesweeperPage.getViewContext().setMapSize(1);
                    MinesweeperPage.newGame();
                }                
            } else {
                if (2 != MinesweeperPage.getViewContext().getMapSize())
                {
                    MinesweeperPage.getViewContext().setMapSize(2);
                    MinesweeperPage.newGame();
                }                
            }            
        }

        private void changeDifficult(object sender, SelectionChangedEventArgs e)
        {
            String difficult = (String)e.AddedItems.First();
            if (difficult.Equals("Easy"))
            {
                if (0 != MinesweeperPage.getViewContext().getMineNumber())
                {
                    MinesweeperPage.getViewContext().setMineNumber(0);
                    MinesweeperPage.newGame();
                }                
            }
            else if (difficult.Equals("Medium"))
            {
                if (1 != MinesweeperPage.getViewContext().getMineNumber())
                {
                    MinesweeperPage.getViewContext().setMineNumber(1);
                    MinesweeperPage.newGame();
                }                
            }
            else
            {
                if (2 != MinesweeperPage.getViewContext().getMineNumber())
                {
                    MinesweeperPage.getViewContext().setMineNumber(2);
                    MinesweeperPage.newGame();
                }                
            }
        }

        private void changeInterval(object sender, SelectionChangedEventArgs e)
        {
            String interv = (String)e.AddedItems.First();
            if (interv.Equals("Low"))
            {
                MinesweeperPage.getViewContext().SquareDeadSpace = 18;
            }
            else if (interv.Equals("Medium"))
            {
                MinesweeperPage.getViewContext().SquareDeadSpace = 8;
            }
            else
            {
                MinesweeperPage.getViewContext().SquareDeadSpace = 4;
            }
        }
    }
}
