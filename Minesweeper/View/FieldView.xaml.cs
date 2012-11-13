using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MineSweeperViewProject.Document;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace MineSweeperViewProject.View
{
    /// <summary>
    /// Interaction logic for FieldView.xaml
    /// </summary>
    public partial class FieldView : UserControl, ContextSensitive
    {
        ViewContext viewContext;

        Field field;
        SquareView[,] squareViews;

        public FieldView()
        {
            InitializeComponent();
        }

        public void setContext(ViewContext context)
        {
            this.viewContext = context;
            foreach(SquareView s in this.squareViews) s.setContext(context);
        }

        public void setField(Field field)
        {
            this.field = field;
            this.squareViews = new SquareView[this.field.Width, this.field.Heigth];

            for (int x = 0; x < field.Width; x++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                this.MainGrid.ColumnDefinitions.Add(columnDefinition);
            }
            for (int y = 0; y < field.Heigth; y++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                this.MainGrid.RowDefinitions.Add(rowDefinition);
            }

            for (int x = 0; x < field.Width; x++)
            {
                
                for (int y = 0; y < field.Heigth; y++)
                {
                    SquareView actualView = new SquareView();
                    actualView.Square = field.getSquare(x, y);
                    actualView.FieldView = this;
                    this.squareViews[x, y] = actualView;
                    actualView.updateImage(false);
                    actualView.VerticalAlignment = VerticalAlignment.Stretch;
                    actualView.HorizontalAlignment = HorizontalAlignment.Stretch;
                    this.MainGrid.Children.Add(actualView);
                    Grid.SetColumn(actualView, x);
                    Grid.SetRow(actualView, y);
                }
                System.Diagnostics.Debug.WriteLine("Progress: " + ((double)x+1) / (field.Width)*100 + "%");
            }
            this.UpdateLayout();
        }

        public Field getField()
        {
            return this.field;
        }

        public void pickAndUpdate(Square picked)
        {
            if (!this.field.GameHasEnded)
            {
                if (picked.isFlagged) flagAndUpdate(picked);
                else
                {
                    field.Pick(picked);
                    foreach (var s in this.squareViews) s.updateImage(this.field.GameHasEnded);
                }
            }
        }

        public void flagAndUpdate(Square picked)
        {
            if (!this.field.GameHasEnded)
            {
                field.Flag(picked);
                foreach (var s in this.squareViews) s.updateImage(this.field.GameHasEnded);
            }
        }
    }
}
