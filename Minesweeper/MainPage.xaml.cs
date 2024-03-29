﻿using MineSweeperViewProject.Document;
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
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Minesweeper
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Minesweeper.Common.LayoutAwarePage
    {
        public MainPage()
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

        async Task showProgress()
        {
            this.progressRing.IsActive = true;
            this.loadingText.Text = "Creating game...";
            await Task.Delay(TimeSpan.FromSeconds(0.1));            
        }


        private async void openGameVIewPage(object sender, TappedRoutedEventArgs e)
        {
            await showProgress();

            if (null != this.Frame)
            {
                MinesweeperPage.newGame();
                this.Frame.Navigate(typeof(MinesweeperPage));
            }
        }

        private void openPreferencesPage(object sender, TappedRoutedEventArgs e)
        {
            this.progressRing.IsActive = true;
            if (null != this.Frame)
            {
                this.Frame.Navigate(typeof(PreferencesPage));
            }
        }

        private void openHelpPage(object sender, TappedRoutedEventArgs e)
        {
            this.progressRing.IsActive = true;
            if (null != this.Frame)
            {
                this.Frame.Navigate(typeof(HelpPage));
            }
        }
    }
}
