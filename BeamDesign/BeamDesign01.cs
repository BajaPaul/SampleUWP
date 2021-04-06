using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Numerics;
using Windows.UI;

namespace SampleUWP.BeamDesign
{
    internal class BDLib     // Shorthand for BeamDesignLibrary (BDLib). 
    {
        // Default stroke widths so they do not need to be passed as a parameter.
        internal static float pen1 = 1;
        internal static float pen2 = 2;
        internal static float pen3 = 3;
        internal static float pen4 = 4;

        /* Define default StrokeStyles for use.  More on CanvasStrokeStyle Class at:
        http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasStrokeStyle.htm
        http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasDashStyle.htm
        http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasCapStyle.htm
        http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasLineJoin.htm   */

        internal static float[] lDash = { 4, 4 };       // Default Dash is { 2, 2 }.  A zero describes a dot.
        internal static CanvasStrokeStyle longDash = new CanvasStrokeStyle
        {
            CustomDashStyle = lDash,            // Use float array above to define CustomDashStyle.
            StartCap = CanvasCapStyle.Square,
            EndCap   = CanvasCapStyle.Square,
            DashCap  = CanvasCapStyle.Square,
            LineJoin = CanvasLineJoin.Miter
        };
        
        internal static CanvasStrokeStyle solid = new CanvasStrokeStyle
        {
            DashStyle = CanvasDashStyle.Solid,
            StartCap = CanvasCapStyle.Square,
            EndCap = CanvasCapStyle.Square,
            DashCap = CanvasCapStyle.Square,
            LineJoin = CanvasLineJoin.Miter
        };

        internal static CanvasStrokeStyle dash = new CanvasStrokeStyle
        {
            DashStyle = CanvasDashStyle.Dash,
            StartCap = CanvasCapStyle.Square,
            EndCap = CanvasCapStyle.Square,
            DashCap = CanvasCapStyle.Square,
            LineJoin = CanvasLineJoin.Miter
        };

        internal static CanvasStrokeStyle dot = new CanvasStrokeStyle
        {
            DashStyle = CanvasDashStyle.Dot,        // Need to use Round with Dots
            StartCap = CanvasCapStyle.Round,
            EndCap = CanvasCapStyle.Round,
            DashCap = CanvasCapStyle.Round,
            LineJoin = CanvasLineJoin.Round
        };

        internal static CanvasStrokeStyle dashDot = new CanvasStrokeStyle
        {
            DashStyle = CanvasDashStyle.DashDot,    // Need to use Round with Dots
            StartCap = CanvasCapStyle.Round,
            EndCap = CanvasCapStyle.Round,
            DashCap = CanvasCapStyle.Round,
            LineJoin = CanvasLineJoin.Round
        };

        internal static CanvasStrokeStyle dashDotDot = new CanvasStrokeStyle
        {
            DashStyle = CanvasDashStyle.DashDotDot, // Need to use Round with Dots
            StartCap = CanvasCapStyle.Round,
            EndCap = CanvasCapStyle.Round,
            DashCap = CanvasCapStyle.Round,
            LineJoin = CanvasLineJoin.Round
        };

        /* This procedure only works in methods().  Use to define StrokeStyles on the fly.
        internal static float[] lDot = { 0, 4 };                // Zero describes a dot.
        CanvasStrokeStyle longDot = new CanvasStrokeStyle();
        longDot.CustomDashStyle = lDot;
        longDot.StartCap = CanvasCapStyle.Round;
        longDot.strokeStyle1.EndCap = CanvasCapStyle.Round;
        longDot.DashCap = CanvasCapStyle.Round;
        longDot.LineJoin = CanvasLineJoin.Miter;
        */

        internal static int fontSize = 15;

        internal static CanvasTextFormat textCC = new CanvasTextFormat      // Insertion point center center of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Center,
        };

        internal static CanvasTextFormat textCB = new CanvasTextFormat      // Insertion point center bottom of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Bottom,
        };

        internal static CanvasTextFormat textCT = new CanvasTextFormat      // Insertion point center top of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Top,
        };

        internal static CanvasTextFormat textLB = new CanvasTextFormat      // Insertion point left bottom of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Left,
            VerticalAlignment = CanvasVerticalAlignment.Bottom,
        };

        internal static CanvasTextFormat textRB = new CanvasTextFormat      // insertion point right bottom of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Right,
            VerticalAlignment = CanvasVerticalAlignment.Bottom,
        };

        internal static CanvasTextFormat textLT = new CanvasTextFormat      // Insertion point left top of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Left,
            VerticalAlignment = CanvasVerticalAlignment.Top,
        };

        internal static CanvasTextFormat textRT = new CanvasTextFormat      // insertion point right top of text.
        {
            FontFamily = "Segoe UI",
            FontSize = fontSize,
            HorizontalAlignment = CanvasHorizontalAlignment.Right,
            VerticalAlignment = CanvasVerticalAlignment.Top,
        };

        // Default format offsets (pixels) used for text placement.
        internal static float fOff1 = 4;    // Offset text insert point from horz line.
        internal static float fOff2 = 8;    // Offset text insert point from vert vectors
        internal static float fOff3 = 12;
        internal static float fOff4 = 16;
        internal static float fOff5 = 20;

        // Default display beam item colors.
        internal static Color colorBeamDefault = Colors.Blue;
        internal static Color colorShearDefault = Colors.Green;
        internal static Color colorMomentDefault = Colors.Red;
        internal static Color colorVectorDefault = Colors.Yellow;
        internal static Color colorTextDefault = Colors.White;

        // Actual beam item display/print colors.  Print RGB colors on display.  Print Black when printing.
        // Methods below set colors before display or print.  ColorsDisplay() and ColorsPrint().
        // Initialize with display colors.
        internal static Color colorBeam   = colorBeamDefault;
        internal static Color colorShear  = colorShearDefault;
        internal static Color colorMoment = colorMomentDefault;
        internal static Color colorVector = colorVectorDefault;
        internal static Color colorText   = colorTextDefault;

        /// <summary>
        /// Draw all items using display color.  Only applies to colors defined in Blib.
        /// </summary>
        internal static void ColorsDisplay()
        {
            colorBeam = colorBeamDefault;
            colorShear = colorShearDefault;
            colorMoment = colorMomentDefault;
            colorVector = colorVectorDefault;
            colorText = colorTextDefault;
        }

        /// <summary>
        /// Draw all items using print color (Black).  Only applies to colors defined in Blib.
        /// </summary>
        internal static void ColorsPrint()
        {
            colorBeam = Colors.Black;
            colorShear = Colors.Black;
            colorMoment = Colors.Black;
            colorVector = Colors.Black;
            colorText = Colors.Black;
        }

        /*
        private double bLength;
        private double bLoadPosition;
        private double bLoad;           // Enter from TextBox as positive but convert to negative in calculations!!!
        private double bElasticity;
        private double bInertia;
        */

        // Fixed placeholder text values for beam pages to reference.  These values are set by the UnitsUSC and UnitsSI methods below.
        internal static string pTextLength;
        internal static string pTextLoadPosition;
        internal static string pTextLoad;
        internal static string pTextElasticity;
        internal static string pTextInertia;
        internal static string symDist;
        internal static string symForce;
        internal static string symMoment;

        internal static void UnitsUSC()
        {
            pTextLength = "Length (ft)";
            pTextLoadPosition = "Load Position (ft)";
            pTextLoad = "Load (Kips)";
            pTextElasticity = "29,000 ksi";
            pTextInertia = "100 in^4";
            symDist = "'";                      // Distance = Feet
            symForce = " k";                    // force = Pounds
            symMoment = " '-k";                 // force = Feet-Pounds
        }

        internal static void UnitsSI()
        {
            pTextLength = "Length (m)";
            pTextLoadPosition = "Load Position (m)";
            pTextLoad = "Load (kN)";
            pTextElasticity = "200 (MPa)";
            pTextInertia = "100 mm^4";
            symDist = "m";                      // Distance = m
            symForce = " kN";                   // Force = KiloNewton
            symMoment = " m-kN";                // force = Meter-KiloNewton
        }

        /// <summary>
        /// Draw a vertical vector and scale it's length proportional to it's value vesus maxValue and availaable dsOffsetMax space.  
        /// Placement point is the vector point.  Vector pointing down has value that is negative.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="startPoint">Vector origination point, the point of the arrowhead.</param>
        /// <param name="value">Numerical value of vector.  Positive points down.</param>
        /// <param name="maxValue">Max value of vector.  Used to proportion vector to fit space available.</param>
        /// <param name="maxOffset">Max distance used for scaling vector.</param>
        /// <param name="color">Color of vector</param>
        /// <param name="pen">Lineweight of vector</param>
        internal static void VectorDraw(CanvasDrawingSession ds, Vector2 startPoint, float value, float maxValue, float maxOffset, Color color, float pen)
        {
            maxValue = Math.Abs(maxValue);          // Make sure positive so only 'value' determines vector direction.
            maxOffset = Math.Abs(maxOffset);        // Make sure positive so only 'value' determines vector direction.
            value = (float)Math.Round(value, 4);    // Round to 4 digits before testing if zero.
            if (value > 0 || value < 0)             // Do not draw anything if zero.
            {
                float hDist = 3.5f;    // Vector head width offset
                float vDist = 10;       // Vector head height offset
                float valueLength = value / maxValue * maxOffset;       // Proportion length to value and maxoffset.
                valueLength = (float)Math.Round(valueLength, 4);        // Round to 4 digits before testing if zero.
                if (valueLength > 0 && valueLength < vDist)             // Set minimum vector length to vertical arrow head size. Check for sign too!
                    valueLength = vDist;
                if (valueLength > -vDist && valueLength < 0)
                    valueLength = -vDist;
                float yEndpoint = startPoint.Y + valueLength;
                Vector2 endPoint = new Vector2(startPoint.X, yEndpoint);
                Vector2 arrowL;
                Vector2 arrowR;
                if (value > 0)      // Positive, so vector points up.
                {
                    arrowL = new Vector2(startPoint.X - hDist, startPoint.Y + vDist);
                    arrowR = new Vector2(startPoint.X + hDist, startPoint.Y + vDist);
                }
                else                // Negative, so vector points down.
                {
                    arrowL = new Vector2(startPoint.X - hDist, startPoint.Y - vDist);
                    arrowR = new Vector2(startPoint.X + hDist, startPoint.Y - vDist);
                }
                ds.DrawLine(endPoint, startPoint, color, pen);    // Draw vector point-to-point.
                ds.DrawLine(startPoint, arrowL, color, pen);
                ds.DrawLine(arrowL, arrowR, color, pen);
                ds.DrawLine(arrowR, startPoint, color, pen);
            }
        }

        /// <summary>
        /// Draw rectangles using vectors versus points or Rect structure.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="leftTop"></param>
        /// <param name="rightBot"></param>
        /// <param name="color"></param>
        /// <param name="pen"></param>
        /// <param name="stroke"></param>
        internal static void RectDraw(CanvasDrawingSession ds, Vector2 leftTop, Vector2 rightBot, Color color, float pen, CanvasStrokeStyle stroke)
        {
            // Note: DrawRectangle has no Vector2 overrides!  (x, y) is leftTop of DrawRectangle.
            ds.DrawRectangle(leftTop.X, leftTop.Y, rightBot.X - leftTop.X, rightBot.Y - leftTop.Y, color, pen, stroke);
        }

        /// <summary>
        /// Draw a '+' to mark the tickPoint.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tickPoint"></param>
        /// <param name="tickSize"></param>
        /// <param name="tickColor"></param>
        internal static void TickMark(CanvasDrawingSession ds, Vector2 tickPoint, float tickSize, Color tickColor)
        {
            ds.DrawLine(tickPoint.X - tickSize, tickPoint.Y, tickPoint.X + tickSize, tickPoint.Y, tickColor, pen1);
            ds.DrawLine(tickPoint.X, tickPoint.Y - tickSize, tickPoint.X, tickPoint.Y + tickSize, tickColor, pen1);
        }
        
    }
}
 