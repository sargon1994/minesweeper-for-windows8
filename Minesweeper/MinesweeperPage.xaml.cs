﻿using Minesweeper.View;
using MineSweeperViewProject.Document;
using MineSweeperViewProject.View;
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
    public sealed partial class MinesweeperPage : Minesweeper.Common.LayoutAwarePage
    {
        static private Field field;
        static private ViewContext viewContext;        
        static private CameraPosition cameraPosition;


        public MinesweeperPage()
        {
            this.InitializeComponent();

            if (null == field) {
                field = new Field(getViewContext().getMapWidth(), getViewContext().getMapHeight(), getViewContext().getMines());
                //this.infoUserControl.setField(field);
                //cameraPosition = this.fieldViewPseudo3D.setCameraPosition();                                                                       
            }

            cameraPosition = this.fieldViewPseudo3D.setCameraPosition();    
            this.infoUserControl.setField(field);
            this.fieldViewPseudo3D.setField(field);
            this.fieldViewPseudo3D.setContext(getViewContext());
            this.fieldViewPseudo3D.setCameraPosition(cameraPosition);
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
            pageState["field"] = field;
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

        private void restartGame(object sender, RoutedEventArgs e)
        {

        }

        public static void setMapSize(int size)
        {
            field = new Field(size,size, 10);
        }

        public static ViewContext getViewContext()
        {
            if (null == viewContext) {
                viewContext = new ViewContext();
            }
            return viewContext;
        }

        public static void newGame()
        {
            field = null;            
        }

        private void restart(object sender, RoutedEventArgs e)
        {
            field = new Field(getViewContext().getMapWidth(), getViewContext().getMapHeight(), getViewContext().getMines());
            this.infoUserControl.setField(field);
            cameraPosition = this.fieldViewPseudo3D.setCameraPosition();                                                                                  
            this.infoUserControl.setField(field);
            this.fieldViewPseudo3D.setField(field);
            this.fieldViewPseudo3D.setContext(getViewContext());
            this.fieldViewPseudo3D.setCameraPosition(cameraPosition);
        }  
    }
}
