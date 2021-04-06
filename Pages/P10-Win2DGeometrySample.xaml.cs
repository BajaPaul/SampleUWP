using LibraryCoder.UtilitiesMisc;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SampleUWP.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P10 : Page
    {
        // Setup the canvas geometries.  These can then be used in the CanvasDrawingSession.DrawGeometry Method.
        // http://microsoft.github.io/Win2D/html/Overload_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawGeometry.htm
        private CanvasGeometry leftGeometry;
        private CanvasGeometry rightGeometry;
        private CanvasGeometry combinedGeometry;

        private Matrix3x2 interGeometryTransform;

        private bool showSourceGeometry;

        private float currentDistanceOnContourPath;
        private float totalDistanceOnContourPath;
        private Vector2 pointOnContourPath;
        private Vector2 tangentOnContourPath;

        private bool showTessellation;
        private CanvasTriangleVertices[] tessellation;

        private bool needsToRecreateResources;
        private bool enableTransform;
        
        public enum GeometryType { Rectangle, RoundedRectangle, Ellipse, Star, Text, Group }
        public enum FillOrStroke { Fill, Stroke }
        public enum ContourTracingAnimationOption { None, Slow, Fast }

        // Place enums into list to use in ComboBox controls.
        public List<GeometryType> Geometries { get { return LibUM.GetEnumAsList<GeometryType>(); } }
        public List<FillOrStroke> FillOrStrokeOptions { get { return LibUM.GetEnumAsList<FillOrStroke>(); } }
        public List<CanvasGeometryCombine> CanvasGeometryCombines { get { return LibUM.GetEnumAsList<CanvasGeometryCombine>(); } }  // This is a MS enum, not set here.
        public List<ContourTracingAnimationOption> ContourTracingAnimationOptions { get { return LibUM.GetEnumAsList<ContourTracingAnimationOption>(); } }

        public GeometryType LeftGeometryType { get; set; }
        public GeometryType RightGeometryType { get; set; }
        public FillOrStroke UseFillOrStroke { get; set; }
        public ContourTracingAnimationOption CurrentContourTracingAnimation { get; set; }
        public CanvasGeometryCombine WhichCombineType { get; set; }

        public P10()
        {
            this.InitializeComponent();


            LeftGeometryType = GeometryType.Rectangle;
            RightGeometryType = GeometryType.Star;
            WhichCombineType = CanvasGeometryCombine.Union;

            interGeometryTransform = Matrix3x2.CreateTranslation(200, 100);

            CurrentContourTracingAnimation = ContourTracingAnimationOption.None;

            showSourceGeometry = false;
            showTessellation = false;
            enableTransform = false;

            needsToRecreateResources = true;
        }


        /// <summary>
        /// This is how you actually create CanvasGeometry for use in  the CanvasDrawingSession.DrawGeometry method.
        /// http://microsoft.github.io/Win2D/html/Overload_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawGeometry.htm
        /// </summary>
        /// <param name="resourceCreator"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private CanvasGeometry CreateGeometry(ICanvasResourceCreator resourceCreator, GeometryType type)
        {
            switch (type)
            {
                case GeometryType.Rectangle:
                    return CanvasGeometry.CreateRectangle(resourceCreator, 100, 100, 300, 350);

                case GeometryType.RoundedRectangle:
                    return CanvasGeometry.CreateRoundedRectangle(resourceCreator, 80, 80, 400, 400, 100, 100);

                case GeometryType.Ellipse:
                    return CanvasGeometry.CreateEllipse(resourceCreator, 275, 275, 225, 275);

                case GeometryType.Star:
                    return Win2DUtilities.CreateStarGeometry(resourceCreator, 250, new Vector2(250, 250));

                case GeometryType.Text:
                    {
                        var textFormat = new CanvasTextFormat
                        {
                            FontFamily = "Comic Sans MS",
                            FontSize = 400,
                        };

                        var textLayout = new CanvasTextLayout(resourceCreator, "2D", textFormat, 1000, 1000);

                        return CanvasGeometry.CreateText(textLayout);
                    }

                case GeometryType.Group:
                    {
                        CanvasGeometry geo0 = CanvasGeometry.CreateRectangle(resourceCreator, 100, 100, 100, 100);
                        CanvasGeometry geo1 = CanvasGeometry.CreateRoundedRectangle(resourceCreator, 300, 100, 100, 100, 50, 50);

                        CanvasPathBuilder pathBuilder = new CanvasPathBuilder(resourceCreator);
                        pathBuilder.BeginFigure(200, 200);
                        pathBuilder.AddLine(500, 200);
                        pathBuilder.AddLine(200, 350);
                        pathBuilder.EndFigure(CanvasFigureLoop.Closed);
                        CanvasGeometry geo2 = CanvasGeometry.CreatePath(pathBuilder);

                        CanvasGeometry canvasGeometry = CanvasGeometry.CreateGroup(resourceCreator, new CanvasGeometry[] { geo0, geo1, geo2 });
                        geo0.Dispose();
                        geo1.Dispose();
                        return canvasGeometry;
                    }
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (type).");
            }
        }

        /// <summary>
        /// Animation code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            Matrix3x2 displayTransform = Win2DUtilities.GetDisplayTransform(sender.Size.ToVector2(), new Vector2(1000, 1000));
            args.DrawingSession.Transform = displayTransform;

            args.DrawingSession.FillGeometry(combinedGeometry, Colors.Blue);

            if (showSourceGeometry)
            {
                args.DrawingSession.DrawGeometry(leftGeometry, Colors.Red, 5.0f);

                args.DrawingSession.Transform = interGeometryTransform * displayTransform;
                args.DrawingSession.DrawGeometry(rightGeometry, Colors.Red, 5.0f);
                args.DrawingSession.Transform = displayTransform;
            }

            if (showTessellation)
            {
                foreach (var triangle in tessellation)
                {
                    args.DrawingSession.DrawLine(triangle.Vertex1, triangle.Vertex2, Colors.Gray);
                    args.DrawingSession.DrawLine(triangle.Vertex2, triangle.Vertex3, Colors.Gray);
                    args.DrawingSession.DrawLine(triangle.Vertex3, triangle.Vertex1, Colors.Gray);
                }
            }

            if (CurrentContourTracingAnimation != ContourTracingAnimationOption.None)
            {
                args.DrawingSession.FillCircle(pointOnContourPath, 2, Colors.White);

                const float arrowSize = 10.0f;

                Vector2 tangentLeft = new Vector2(
                    -tangentOnContourPath.Y,
                    tangentOnContourPath.X);

                Vector2 tangentRight = new Vector2(
                    tangentOnContourPath.Y,
                    -tangentOnContourPath.X);

                Vector2 bisectorLeft = new Vector2(
                    tangentOnContourPath.X + tangentLeft.X,
                    tangentOnContourPath.Y + tangentLeft.Y);

                Vector2 bisectorRight = new Vector2(
                    tangentOnContourPath.X + tangentRight.X,
                    tangentOnContourPath.Y + tangentRight.Y);

                Vector2 arrowheadFront = new Vector2(
                    pointOnContourPath.X + (tangentOnContourPath.X * arrowSize * 2),
                    pointOnContourPath.Y + (tangentOnContourPath.Y * arrowSize * 2));

                Vector2 arrowheadLeft = new Vector2(
                    arrowheadFront.X - (bisectorLeft.X * arrowSize),
                    arrowheadFront.Y - (bisectorLeft.Y * arrowSize));

                Vector2 arrowheadRight = new Vector2(
                    arrowheadFront.X - (bisectorRight.X * arrowSize),
                    arrowheadFront.Y - (bisectorRight.Y * arrowSize));

                const float strokeWidth = arrowSize / 4.0f;
                args.DrawingSession.DrawLine(pointOnContourPath, arrowheadFront, Colors.White, strokeWidth);
                args.DrawingSession.DrawLine(arrowheadFront, arrowheadLeft, Colors.White, strokeWidth);
                args.DrawingSession.DrawLine(arrowheadFront, arrowheadRight, Colors.White, strokeWidth);
            }
        }

        /// <summary>
        /// Invoked by XAML <canvas:CanvasAnimatedControl/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (needsToRecreateResources)       // Flag set by mutiple methods indicating a update is needed.
            {
                RecreateGeometry(sender);
                needsToRecreateResources = false;
            }

            if (CurrentContourTracingAnimation != ContourTracingAnimationOption.None)
            {
                float animationDistanceThisFrame = CurrentContourTracingAnimation == ContourTracingAnimationOption.Slow ? 1.0f : 20.0f;
                currentDistanceOnContourPath = (currentDistanceOnContourPath + animationDistanceThisFrame) % totalDistanceOnContourPath;

#if WINDOWS_UWP
#else
                Microsoft.Graphics.Canvas.Numerics.Vector2 outTangent;
#endif
                pointOnContourPath = combinedGeometry.ComputePointOnPath(currentDistanceOnContourPath, out Vector2 outTangent);
                tangentOnContourPath = outTangent;
            }
        }


        /// <summary>
        /// This updates the canvas and is called from Canvas_Update() above.
        /// </summary>
        /// <param name="resourceCreator"></param>
        private void RecreateGeometry(ICanvasResourceCreator resourceCreator)
        {
            leftGeometry = CreateGeometry(resourceCreator, LeftGeometryType);
            rightGeometry = CreateGeometry(resourceCreator, RightGeometryType);

            if (enableTransform)
            {
                Matrix3x2 placeNearOrigin = Matrix3x2.CreateTranslation(-200, -200);
                Matrix3x2 undoPlaceNearOrigin = Matrix3x2.CreateTranslation(200, 200);

                Matrix3x2 rotate0 = Matrix3x2.CreateRotation((float)Math.PI / 4.0f); // 45 degrees
                Matrix3x2 scale0 = Matrix3x2.CreateScale(1.5f);

                Matrix3x2 rotate1 = Matrix3x2.CreateRotation((float)Math.PI / 6.0f); // 30 degrees
                Matrix3x2 scale1 = Matrix3x2.CreateScale(2.0f);

                leftGeometry = leftGeometry.Transform(placeNearOrigin * rotate0 * scale0 * undoPlaceNearOrigin);
                rightGeometry = rightGeometry.Transform(placeNearOrigin * rotate1 * scale1 * undoPlaceNearOrigin);
            }

            combinedGeometry = leftGeometry.CombineWith(rightGeometry, interGeometryTransform, WhichCombineType);

            if (UseFillOrStroke == FillOrStroke.Stroke)
            {
                CanvasStrokeStyle strokeStyle = new CanvasStrokeStyle
                {
                    DashStyle = CanvasDashStyle.Dash
                };
                combinedGeometry = combinedGeometry.Stroke(15.0f, strokeStyle);
            }

            totalDistanceOnContourPath = combinedGeometry.ComputePathLength();

            if (showTessellation)
            {
                tessellation = combinedGeometry.Tessellate();
            }
        }


        /// <summary>
        /// Invoked by XAML <canvas:CanvasAnimatedControl/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Canvas_CreateResources(CanvasAnimatedControl sender, object args)
        {
            _ = sender;     // Discard unused parameter.
            _ = args;       // Discard unused parameter.
            needsToRecreateResources = true;
        }


        /// <summary>
        /// Invoked by all ComboBoxes but one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            needsToRecreateResources = true;
        }


        // Handle three ToggleButtons
        private void ShowSourceGeometry_Checked(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            showSourceGeometry = true;
        }

        private void ShowSourceGeometry_Unchecked(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            showSourceGeometry = false;
        }

        private void ShowTessellation_Checked(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            showTessellation = true;
            needsToRecreateResources = true;
        }

        private void ShowTessellation_Unchecked(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            showTessellation = false;
        }

        private void EnableTransform_Checked(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            enableTransform = true;
            needsToRecreateResources = true;
        }

        private void EnableTransform_Unchecked(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            enableTransform = false;
            needsToRecreateResources = true;
        }

        /// <summary>
        /// Explicitly remove references to allow the Win2D controls to get garbage collected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

    }
}
