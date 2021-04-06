using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Numerics;
using Windows.UI;

// LibBeam01: Simple Beam Concentrated Load Any Point

namespace SampleUWP.BeamDesign
{
    internal class BDType01    // Shorthand for BeamDesignType01 (BDType01).
    {
        // Copy LibBeamDesignCommon (BDLib) settings here for local use.  Any changes to these values need happen only once there.
        private static readonly float pen1 = BDLib.pen1;
        private static readonly float pen2 = BDLib.pen2;
        private static readonly float pen3 = BDLib.pen3;
        private static readonly float pen4 = BDLib.pen4;

        private static readonly CanvasStrokeStyle longDash   = BDLib.longDash;
        private static readonly CanvasStrokeStyle solid      = BDLib.solid;

        /* Following items not used.
        private static readonly CanvasStrokeStyle dash       = BDLib.dash;
        private static readonly CanvasStrokeStyle dot        = BDLib.dot;
        private static readonly CanvasStrokeStyle dashDot    = BDLib.dashDot;
        private static readonly CanvasStrokeStyle dashDotDot = BDLib.dashDotDot;
        */

        // private static readonly CanvasTextFormat textCC = BDLib.textCC;   // Insertion point center center of text.
        private static readonly CanvasTextFormat textCB = BDLib.textCB;   // Insertion point center bottom of text.
        private static readonly CanvasTextFormat textCT = BDLib.textCT;   // Insertion point center top of text.
        private static readonly CanvasTextFormat textLB = BDLib.textLB;   // Insertion point left bottom of text.
        private static readonly CanvasTextFormat textRB = BDLib.textRB;   // insertion point right bottom of text.
        private static readonly CanvasTextFormat textLT = BDLib.textLT;   // Insertion point left top of text.
        private static readonly CanvasTextFormat textRT = BDLib.textRT;   // insertion point right top of text.

        private static readonly float fOff1 = BDLib.fOff1;   // Offset text insert point from horz line.
        private static readonly float fOff2 = BDLib.fOff2;   // Offset text insert point from vert vectors

        /* Following items not used.
        private static readonly float fOff3 = BDLib.fOff3;
        private static readonly float fOff4 = BDLib.fOff4;
        private static readonly float fOff5 = BDLib.fOff5;
        */

        private static Color colorBeam   = BDLib.colorBeamDefault;
        private static Color colorShear  = BDLib.colorShearDefault;
        private static Color colorMoment = BDLib.colorMomentDefault;
        private static Color colorVector = BDLib.colorVectorDefault;
        private static Color colorText   = BDLib.colorTextDefault;

        /* Following items not used.
        private static readonly string pTextLength       = BDLib.pTextLength;
        private static readonly string pTextLoadPosition = BDLib.pTextLoadPosition;
        private static readonly string pTextLoad         = BDLib.pTextLoad;
        private static readonly string pTextElasticity   = BDLib.pTextElasticity;
        private static readonly string pTextInertia      = BDLib.pTextInertia;
        private static readonly string symDist           = BDLib.symDist;
        */

        private static readonly string symForce          = BDLib.symForce;
        private static readonly string symMoment         = BDLib.symMoment;

        internal static string beamDiagramLable = "Simple Beam - Concentrated Load Any Point";

        /// <summary>
        /// Draw the beam using input parameters.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="leftTop"></param>
        /// <param name="rightBot"></param>
        /// <param name="dsLength"></param>
        /// <param name="dsLoadPosition"></param>
        /// <param name="dsLoad"></param>
        /// <param name="dsReactionL"></param>
        /// <param name="dsReactionR"></param>
        /// <param name="dsShearL"></param>
        /// <param name="dsShearR"></param>
        /// <param name="dsMoment"></param>
        /// <param name="dsLoadMax"></param>
        /// <param name="dsShearMax"></param>
        /// <param name="dsMomentMax"></param>
        internal static void BeamDraw(CanvasDrawingSession ds, Vector2 leftTop, Vector2 rightBot, float dsLength, float dsLoadPosition, float dsLoad,
                                        float dsReactionL, float dsReactionR, float dsShearL, float dsShearR, float dsMoment,
                                        float dsLoadMax, float dsShearMax, float dsMomentMax)
        {
            // Ensure scaling factors are always positive so proper sign transfers with scaled value.
            dsLoadMax = Math.Abs(dsLoadMax);
            dsShearMax = Math.Abs(dsShearMax);
            dsMomentMax = Math.Abs(dsMomentMax);

            float width = rightBot.X - leftTop.X;
            float height = rightBot.Y - leftTop.Y;

            // Split horizontal space up.  Left portion used for beam diagrams and right portion used for associated Nomenclature.
            // Split above is best since most draw items will pertain to the diagrams.  Also eliminates chance for text to overlay 
            // the diagrams with window size changes.

            float diagramWidthFactor = 0.75f;
            float titleHeightFactor = 0.05f;                // Height of title above beam diagrams.
            float wDiagrams = width * diagramWidthFactor;
            // float wText = width - wDiagrams;
            float hTitle = height * titleHeightFactor;
            float hDiagrams = height - hTitle;

            // Calc horizontal placement of left and right ends of beam, shear, and moment diagrams.
            float beamHorzOffset = wDiagrams * 0.025f;
            float xBeamLeft = leftTop.X + beamHorzOffset;
            float xBeamRight = leftTop.X + wDiagrams - beamHorzOffset;

            // Calc vertical centerline placements for beam, shear, and moment diagrams.
            float verticalItems = 3;                    // Split vertical pane up by this value.
            float vSpacing = hDiagrams / verticalItems;
            float yBeamAxis = leftTop.Y + hTitle + vSpacing / 2;
            float yShearAxis = yBeamAxis + vSpacing;
            float yMomentAxis = yShearAxis + vSpacing;

            // Calc horizontal position of load vector based on lengths.
            float xLoad = (xBeamRight - xBeamLeft) * (dsLoadPosition / dsLength);

            // Calc vectors for beam endpoints and load point.
            Vector2 beamL = new Vector2(xBeamLeft, yBeamAxis);
            Vector2 beamR = new Vector2(xBeamRight, yBeamAxis);
            Vector2 loadPt = new Vector2(xLoad, yBeamAxis);

            // Calc percent of space above and below the diagram axis to for scaling maximum values.
            float displayOffset = 0.9f;
            float dsOffsetMax = vSpacing / 2 * displayOffset;

            // Calc vectors for surrounding dashed grid.
            float xGridRight = leftTop.X + wDiagrams;
            float yg0 = leftTop.Y + hTitle;                 // Y value of gridline between title and beam.
            float yg1 = yg0 + vSpacing;                     // Y value of gridline between beam and shear.
            float yg2 = yg1 + vSpacing;                     // Y value of gridline between shear and moment.
            Vector2 g0L = new Vector2(leftTop.X, yg0);
            Vector2 g0R = new Vector2(xGridRight, yg0);
            Vector2 g1L = new Vector2(leftTop.X, yg1);
            Vector2 g1R = new Vector2(xGridRight, yg1);
            Vector2 g2L = new Vector2(leftTop.X, yg2);
            Vector2 g2R = new Vector2(xGridRight, yg2);
            Vector2 gV1 = new Vector2(xGridRight, leftTop.Y);
            Vector2 gV2 = new Vector2(xGridRight, rightBot.Y);

            // Entities drawn at extents get clipped by half of pen width so offset outer rectangle by equal amount.
            // DrawRectangle has no Vector2 overrides!  (x,y) is upper left corner of DrawRectangle.

            float gridPen = pen1;
            ds.DrawRectangle(leftTop.X + gridPen / 2, leftTop.Y + gridPen / 2, width - gridPen, height - gridPen, colorVector, gridPen, longDash);
            ds.DrawLine(g0L, g0R, colorVector, gridPen, longDash);
            ds.DrawLine(g1L, g1R, colorVector, gridPen, longDash);
            ds.DrawLine(g2L, g2R, colorVector, gridPen, longDash);
            ds.DrawLine(gV1, gV2, colorVector, gridPen, longDash);

            // Draw Beam Diagram.
            ds.DrawLine(beamL, beamR, colorBeam, pen4);   // draw the beam
            BDLib.VectorDraw(ds, loadPt, dsLoad, dsLoadMax, dsOffsetMax, colorVector, pen3);         // Draw the load vector
            BDLib.VectorDraw(ds, beamL, dsReactionL, dsLoadMax, dsOffsetMax, colorVector, pen3);     // Draw the left reaction vector
            BDLib.VectorDraw(ds, beamR, dsReactionR, dsLoadMax, dsOffsetMax, colorVector, pen3);     // Draw the right reaction vector
            float bValue = dsLength - dsLoadPosition;                   // Calculate the b dimension
            dsLoadPosition = (float)Math.Round(dsLoadPosition, 4);      // Need to test for zero below so round to 4 digits.
            bValue = (float)Math.Round(bValue, 4);                      // Need to test for zero below so round to 4 digits.
            float thresholdH = 0.08f;                                   // Change text postioning when a or b less than thresholdH.

            string L = string.Format("{0:0.##}{1}", dsLength, BDLib.symDist);
            string a = string.Format("{0:0.##}{1}", dsLoadPosition, BDLib.symDist);
            string b = string.Format("{0:0.##}{1}", bValue, BDLib.symDist);
            string P = string.Format("{0:#,#}{1}", dsLoad, BDLib.symForce);
            string Rl = string.Format("{0:#,#}{1}", dsReactionL, BDLib.symForce);
            string Rr = string.Format("{0:#,#}{1}", dsReactionR, BDLib.symForce);

            // Need to test below if dsLoadPosition approaching ends of beam and adjust values accordingly.
            ds.DrawText(L, beamL.X + (beamR.X - beamL.X) / 2, beamL.Y + fOff1, colorText, textCT);   // Add dim L
            if (dsLoadPosition > 0)      // Do not draw if zero.
            {
                if (dsLoadPosition / dsLength < thresholdH)  // Move label with load at thresholdH.
                    ds.DrawText(a, loadPt.X - fOff2, beamL.Y - fOff1, colorText, textRB);       // Add dim a
                else    // Center in space
                    ds.DrawText(a, beamL.X + (loadPt.X - beamL.X) / 2, beamL.Y - fOff1, colorText, textCB);  // Add dim a
            }
            if (bValue > 0)             // Do not draw if zero.
            {
                if (bValue / dsLength < thresholdH)  // Move label with load at thresholdH.
                    ds.DrawText(b, loadPt.X + fOff2, beamL.Y - fOff1, colorText, textLB);               // Add dim b
                else
                    ds.DrawText(b, loadPt.X + (beamR.X - loadPt.X) / 2, beamL.Y - fOff1, colorText, textCB); // Add dim b
            }
            if (dsLoadPosition <= dsLength / 2)   // Draw load right of load vector
                ds.DrawText(P, loadPt.X + fOff2, beamL.Y - dsOffsetMax / 2, colorText, textLB);  // Add dsLoad
            else        // Draw load left of load vector.
                ds.DrawText(P, loadPt.X - fOff2, beamL.Y - dsOffsetMax / 2, colorText, textRB);  // Add dsLoad
            if (dsReactionL > 1)  // Do not show if decimal or zero.
                ds.DrawText(Rl, beamL.X + fOff2, beamL.Y + dsOffsetMax / 2, colorText, textLT);  // Add dsReactionL
            if (dsReactionR > 1)   // Do not show if decimal or zero.
                ds.DrawText(Rr, beamR.X - fOff2, beamL.Y + dsOffsetMax / 2, colorText, textRT);  // Add dsReactionR

            // Draw Shear Diagram.
            float shearOffsetL = dsShearL / dsShearMax * dsOffsetMax;     // Proportion dsShearL by value to fit in available space.
            float shearOffsetR = dsShearR / dsShearMax * dsOffsetMax;     // Proportion dsShearR by value to fit in available space (negative value).
            Vector2 leftShear = new Vector2(beamL.X, yShearAxis - shearOffsetL);
            Vector2 rightShear = new Vector2(beamR.X, yShearAxis - shearOffsetR);
            Vector2 loadPtShear = new Vector2(loadPt.X, yShearAxis);
            Vector2 shearAxisL = new Vector2(beamL.X, yShearAxis);
            Vector2 shearAxisR = new Vector2(beamR.X, yShearAxis);
            BDLib.RectDraw(ds, leftShear, loadPtShear, colorShear, pen2, solid);
            BDLib.RectDraw(ds, loadPtShear, rightShear, colorShear, pen2, solid);
            ds.DrawLine(shearAxisL, shearAxisR, colorBeam, pen2);

            string Vl = string.Format("{0:#,#}{1}", dsShearL, symForce);
            string Vr = string.Format("{0:#,#}{1}", dsShearR, symForce);

            if (dsShearL > 1)  // Do not show if decimal or zero.
                ds.DrawText(Vl, beamL.X + (loadPt.X - beamL.X) / 2, yShearAxis + fOff1, colorText, textCT);  // Add dsShearL
            if (dsShearR < -1)  // Do not show if decimal or zero (right shear negative).
                ds.DrawText(Vr, loadPt.X + (beamR.X - loadPt.X) / 2, yShearAxis - fOff1, colorText, textCB);  // Add dsShearR

            // Draw Moment Diagram.
            float momentOffset = dsMoment / dsMomentMax * dsOffsetMax;    // Proportion moment by value to fit in available space.
            Vector2 mL = new Vector2(beamL.X, yMomentAxis);
            Vector2 mR = new Vector2(beamR.X, yMomentAxis);
            Vector2 mPeak = new Vector2(loadPt.X, yMomentAxis - momentOffset);
            Vector2 mAxisPt = new Vector2(loadPt.X, yMomentAxis);
            ds.DrawLine(mL, mPeak, colorMoment, pen2);
            ds.DrawLine(mPeak, mR, colorMoment, pen2);
            ds.DrawLine(mPeak, mAxisPt, colorMoment, pen2);       // Add vertical line at peak
            ds.DrawLine(mL, mR, colorBeam, pen2);

            string Mp = string.Format("{0:#,#}{1}", dsMoment, symMoment);

            if (dsMoment > 1)   // Do not show if decimal or zero.
                ds.DrawText(Mp, loadPt.X, yMomentAxis + fOff1, colorText, textCT);      // Add moment
        }

    }
}
 
 
 