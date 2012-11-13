using Minesweeper.View;
using MineSweeperViewProject.Document;
using MineSweeperViewProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace MineSweeperViewProject.View
{
    /// <summary>
    /// Interaction logic for FieldViewPseudo3D.xaml
    /// </summary>
    public partial class FieldViewPseudo3D : UserControl
    {
        ViewContext viewContext;

        //double cameraPosition.X, cameraPosition.Y, cameraPosition.Z;
        CameraPosition cameraPosition;

        FieldView fieldView;
        Field field;

        Point oldPosition;

        public FieldViewPseudo3D()
        {
            InitializeComponent();
        }

        public void setContext(ViewContext context)
        {
            this.viewContext = context;
            this.fieldView.setContext(context);
        }

        public void setField(Field field)
        {
            this.field = field;
            fieldView = new FieldView();
            fieldView.setField(field);

            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(fieldView);
            resetCamera();
        }

        public CameraPosition setCameraPosition()
        {
            this.cameraPosition = new CameraPosition() { X = 0, Y = 0, Z = 1.2 };
            return this.cameraPosition;
        }

        public void setCameraPosition(CameraPosition cameraPosition)
        {
            this.cameraPosition = cameraPosition;
        }

        private void resetCamera()
        {
            this.resetCamera(this.mainCanvas.RenderSize);
        }

        double squareSize;

        private void resetCamera(Size s) {                  
            squareSize = Math.Min(s.Width/field.Width, s.Height/field.Heigth);
            squareSize /= this.cameraPosition.Z;
            if (squareSize > 10000) {
                squareSize = 44;
            }
            fieldView.Width = squareSize*(double)field.Width;
            fieldView.Height = squareSize*(double)field.Heigth;
            Canvas.SetLeft(fieldView, s.Width / 2 - fieldView.Width/2 + cameraPosition.X*squareSize);
            Canvas.SetTop(fieldView, s.Height / 2 - fieldView.Height/2 + cameraPosition.Y*squareSize);
        }

        private double moveCameraZ(double Z)
        {
            Z += this.cameraPosition.Z;
            Z = Math.Max(viewContext.MinZoom/Math.Min(field.Heigth, field.Width), Z);
            Z = Math.Min(viewContext.MaxZoomMultiplier, Z);
            double ret = Z - cameraPosition.Z;
            cameraPosition.Z = Z;
            this.resetCamera();
            return ret;
        }

        private void moveCameraXY(double X, double Y)
        {
            cameraPosition.X += X;
            cameraPosition.X = Math.Max(cameraPosition.X, -field.Width / 2.0);
            cameraPosition.X = Math.Min(cameraPosition.X, field.Width / 2.0);
            cameraPosition.Y += Y;
            cameraPosition.Y = Math.Max(cameraPosition.Y, -field.Heigth / 2.0);
            cameraPosition.Y = Math.Min(cameraPosition.Y, field.Heigth / 2.0);
            this.resetCamera();
        }

        /*private void BackgroundGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //this.moveCameraZ(e.Delta/100);
            double diff = cameraPosition.Z * 0.1;
            this.moveCameraZ(e.Delta < 0 ? diff : -diff);
        }

        private void BackgroundGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var newPosition = e.GetPosition(this.mainCanvas);
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {

                if (oldPosition != null)
                {
                    moveCameraXY(
                        (newPosition.X - oldPosition.X)/squareSize ,
                        (newPosition.Y - oldPosition.Y)/squareSize);
                }
            }
            oldPosition = newPosition;
        }

        private void BackgroundGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            resetCamera(e.NewSize);
        }*/

        private void pointerWheel(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            double diff = cameraPosition.Z * 0.1;
            var wheelDelta = e.GetCurrentPoint(this).Properties.MouseWheelDelta;
            diff = wheelDelta < 0 ? diff : -diff;
            this.moveCameraZ(diff);
        }

        private void pointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var newPosition = e.GetCurrentPoint(this.mainCanvas).Position;
            if (e.GetCurrentPoint(this.mainCanvas).Properties.IsLeftButtonPressed ||
                e.GetCurrentPoint(this.mainCanvas).Properties.IsRightButtonPressed)
            {
                if (oldPosition != null)
                {
                    moveCameraXY(
                        (newPosition.X - oldPosition.X) / squareSize,
                        (newPosition.Y - oldPosition.Y) / squareSize);
                }
            }
            oldPosition = newPosition;
        }

        private void sizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            resetCamera(e.NewSize);
        }
    }
}
