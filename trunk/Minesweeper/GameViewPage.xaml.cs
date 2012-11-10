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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameViewPage : Page
    {
        public GameViewPage()
        {
            this.InitializeComponent();
            Field field = new Field(40, 20, 100);
            this.fieldViewPseudo3D.setField(field);
            this.fieldViewPseudo3D.setContext(new MineSweeperViewProject.View.ViewContext());
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void openMainPage(object sender, RoutedEventArgs e)
        {
            if (null != this.Frame)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void openPreferencesPage(object sender, RoutedEventArgs e)
        {
            if (null != this.Frame)
            {
                this.Frame.Navigate(typeof(PreferencesPage));
            }
        }

        private void openHelpPage(object sender, RoutedEventArgs e)
        {
            if (null != this.Frame)
            {
                this.Frame.Navigate(typeof(HelpPage));
            }
        }
    }
}
