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
            if (null != pageState)
            {
                smallMap.IsChecked = (bool)pageState["smallMapIsChecked"];
                mediumMap.IsChecked = (bool)pageState["mediumMapIsChecked"];
                largeMap.IsChecked = (bool)pageState["largeMapIsChecked"];
                easyGame.IsChecked = (bool)pageState["easyGame"];
                mediumGame.IsChecked = (bool)pageState["mediumGame"];
                hardGame.IsChecked = (bool)pageState["hardGame"];
                intervalSize.Value = (double)pageState["intervalSize"];
            }
            else
            {
                smallMap.IsChecked = true;
                mediumMap.IsChecked = false;
                largeMap.IsChecked = false;
                easyGame.IsChecked = true;
                mediumGame.IsChecked = false;
                hardGame.IsChecked = false;
                intervalSize.Value = (double)50.0;
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            pageState["smallMapIsChecked"] = smallMap.IsChecked;
            pageState["mediumMapIsChecked"] = mediumMap.IsChecked;
            pageState["largeMapIsChecked"] = largeMap.IsChecked;
            pageState["easyGame"] = easyGame.IsChecked;
            pageState["mediumGame"] = mediumGame.IsChecked;
            pageState["hardGame"] = hardGame.IsChecked;
            pageState["intervalSize"] = intervalSize.Value;
        }

        private void setDifficultyToEasy(object sender, RoutedEventArgs e)
        {
            //TODO add call to model
        }

        private void setDifficultyToMedium(object sender, RoutedEventArgs e)
        {
            //TODO add call to model
        }

        private void setDifficultyToHard(object sender, RoutedEventArgs e)
        {
            //TODO add call to model
        }

        private void setMapSizeToSmall(object sender, RoutedEventArgs e)
        {
            //TODO add call to model
        }

        private void setMapSizeToMedium(object sender, RoutedEventArgs e)
        {
            //TODO add call to model
        }

        private void setMapSizeToLarge(object sender, RoutedEventArgs e)
        {
            //TODO add call to model
        }

        private void setInterval(object sender, KeyRoutedEventArgs e)
        {
            //TODO add call to model
        }
    }
}
