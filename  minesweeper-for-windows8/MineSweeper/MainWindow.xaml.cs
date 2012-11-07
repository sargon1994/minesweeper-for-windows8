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
using MineSweeper.View;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            Field field = new Field(20,20,60);
            ViewContext vc = new ViewContext();
            InitializeComponent();

            fieldView3D.setField(field);
            fieldView3D.setContext(vc);
        }
    }
}
