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
using System.Windows.Media.Media3D;
using MineSweeper.Document;

namespace MineSweeper.View
{
    /// <summary>
    /// Interaction logic for FieldView3D.xaml
    /// </summary>
    public partial class FieldView3D : UserControl, ContextSensitive
    {
        ViewContext viewContext;

        FieldView fieldView;
        Field field;

        PerspectiveCamera camera = new PerspectiveCamera();
        DirectionalLight light = new DirectionalLight();
        Point3D cameraPosition;

        Viewport2DVisual3D fieldModel;
        
        System.Windows.Point oldPosition;

        public FieldView3D()
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

            cameraPosition = new Point3D(0, 0, -Math.Max(this.field.Heigth, this.field.Width) * 0.5);
            camera.Position = cameraPosition;
            camera.LookDirection = new Vector3D(0, 0, 1);
            camera.UpDirection = new Vector3D(0, -1, 0);
            camera.FieldOfView = 90;
            Space.Camera = camera;

            fieldModel = new Viewport2DVisual3D();
            fieldModel.Geometry = createGeometry(field,0,0,false);
            fieldModel.Visual = fieldView;
            Material material = new DiffuseMaterial(Brushes.Green);
            Viewport2DVisual3D.SetIsVisualHostMaterial(material, true);
            fieldModel.Material = material;
            Space.Children.Add(fieldModel);
        }

        private MeshGeometry3D createGeometry(Field field, double focusx, double focusy, bool dofocuse)
        {
            MeshGeometry3D tableGeometry = new MeshGeometry3D();
            if (viewContext!=null && viewContext.IsFocusEnabled)
            {
                for (int y = 0; y <= field.Heigth; y++) for (int x = 0; x <= field.Width; x++)
                    {
                        double z;
                        if (dofocuse)
                        {
                            double rsquare = (x - focusx) * (x - focusx) + (y - focusy) * (y - focusy);
                            z = Math.Exp(-rsquare / 25) * camera.Position.Z/2;
                        }
                        else z = 0;
                        tableGeometry.Positions.Add(new Point3D(x - (field.Width / 2.0), y - (field.Heigth / 2.0), z));
                        tableGeometry.TextureCoordinates.Add(new Point((double)x / field.Width, (double)y / field.Heigth));
                    }
                for (int y = 0; y < field.Heigth; y++) for (int x = 0; x < field.Width; x++)
                    {
                        tableGeometry.TriangleIndices.Add(y * (field.Width + 1) + x);
                        tableGeometry.TriangleIndices.Add((y + 1) * (field.Width + 1) + x);
                        tableGeometry.TriangleIndices.Add(y * (field.Width + 1) + x + 1);
                        tableGeometry.TriangleIndices.Add((y + 1) * (field.Width + 1) + x);
                        tableGeometry.TriangleIndices.Add((y + 1) * (field.Width + 1) + x + 1);
                        tableGeometry.TriangleIndices.Add(y * (field.Width + 1) + x + 1);
                    }
            }
            else
            {
                tableGeometry.Positions.Add(new Point3D(- (field.Width / 2.0), - (field.Heigth / 2.0), 0));
                tableGeometry.TextureCoordinates.Add(new Point(0,0));

                tableGeometry.Positions.Add(new Point3D((field.Width / 2.0), -(field.Heigth / 2.0), 0));
                tableGeometry.TextureCoordinates.Add(new Point(1, 0));

                tableGeometry.Positions.Add(new Point3D(-(field.Width / 2.0), (field.Heigth / 2.0), 0));
                tableGeometry.TextureCoordinates.Add(new Point(0, 1));

                tableGeometry.Positions.Add(new Point3D((field.Width / 2.0), (field.Heigth / 2.0), 0));
                tableGeometry.TextureCoordinates.Add(new Point(1, 1));

                tableGeometry.TriangleIndices.Add(0);
                tableGeometry.TriangleIndices.Add(2);
                tableGeometry.TriangleIndices.Add(1);

                tableGeometry.TriangleIndices.Add(2);
                tableGeometry.TriangleIndices.Add(3);
                tableGeometry.TriangleIndices.Add(1);
            }
            return tableGeometry;
        }

        private double getMaxHigh()
        {
            return Math.Max(this.field.Heigth, this.field.Width)*0.5*viewContext.MaxZoomMultiplier;
        }

        private double moveCameraZ(double Z)
        {
            Z += cameraPosition.Z;
            Z = Math.Min(-viewContext.MinZoom, Z);
            Z = Math.Max(-getMaxHigh(), Z);
            double ret = Z - cameraPosition.Z;
            cameraPosition.Z = Z;
            camera.Position = cameraPosition;
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
            camera.Position = cameraPosition;
        }

        private void Space_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double delta = e.Delta * viewContext.MouseWheelSpeed;
            double ZChange = moveCameraZ(delta);
            var position = e.GetPosition(Space);
            double X = position.X - BackgroundGrid.ActualWidth / 2.0;
            double Y = position.Y - BackgroundGrid.ActualHeight / 2.0;
            double radius = Math.Sqrt(X * X + Y * Y);
            moveCameraXY(
                (X / (Math.Min(BackgroundGrid.ActualWidth, BackgroundGrid.ActualHeight) / 2.0)) * ZChange * viewContext.MouseWheelSideSpeed,
                (Y / (Math.Min(BackgroundGrid.ActualWidth, BackgroundGrid.ActualHeight) / 2.0)) * ZChange * viewContext.MouseWheelSideSpeed);
        }

        private void Space_MouseMove(object sender, MouseEventArgs e)
        {
            var newPosition = e.GetPosition(Space);
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                
                if (oldPosition != null)
                {
                    moveCameraXY(
                        (newPosition.X - oldPosition.X) * 2 * cameraPosition.Z / BackgroundGrid.ActualWidth,
                        (newPosition.Y - oldPosition.Y) * 2 * cameraPosition.Z / BackgroundGrid.ActualWidth);
                }
            }
            oldPosition = e.GetPosition(Space);

            if (this.viewContext.IsFocusEnabled)
            {
                fieldModel.Geometry = createGeometry(field,
                    camera.Position.X - ((newPosition.X - BackgroundGrid.ActualWidth / 2)  *2* cameraPosition.Z / BackgroundGrid.ActualWidth) + field.Width/2.0,
                    camera.Position.Y - ((newPosition.Y - BackgroundGrid.ActualWidth / 2) * 2 * cameraPosition.Z / BackgroundGrid.ActualWidth) + field.Heigth / 2.0,
                    true);
            }
        }
    }
}
