﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;

// Utilitiy methods used by Win2D samples on various pages.  Considerable overhead so keep separate.

namespace SampleUWP.Common
{
    static class Win2DUtilities
    {
        /// <summary>
        /// Create Star shaped geometry.
        /// </summary>
        /// <param name="resourceCreator"></param>
        /// <param name="scale"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static CanvasGeometry CreateStarGeometry(ICanvasResourceCreator resourceCreator, float scale, Vector2 center)
        {
            Vector2[] points =
            {
                new Vector2(-0.24f, -0.24f),
                new Vector2(0, -1),
                new Vector2(0.24f, -0.24f),
                new Vector2(1, -0.2f),
                new Vector2(0.4f, 0.2f),
                new Vector2(0.6f, 1),
                new Vector2(0, 0.56f),
                new Vector2(-0.6f, 1),
                new Vector2(-0.4f, 0.2f),
                new Vector2(-1, -0.2f),
            };
            var transformedPoints = from point in points select point * scale + center;

#if WINDOWS_UWP
            var convertedPoints = transformedPoints;
#else
            // Convert the System.Numerics.Vector2 type that we normally work with to the
            // Microsoft.Graphics.Canvas.Numerics.Vector2 struct used by WinRT. These casts
            // are usually inserted automatically, but auto conversion does not work for arrays.
            var convertedPoints = from point in transformedPoints select (Microsoft.Graphics.Canvas.Numerics.Vector2)point;
#endif
            return CanvasGeometry.CreatePolygon(resourceCreator, convertedPoints.ToArray());
        }


        public static Matrix3x2 GetDisplayTransform(Vector2 outputSize, Vector2 sourceSize)
        {
            // Scale the display to fill the control.
            var scale = outputSize / sourceSize;
            var offset = Vector2.Zero;

            // Letterbox or pillarbox to preserve aspect ratio.
            if (scale.X > scale.Y)
            {
                scale.X = scale.Y;
                offset.X = (outputSize.X - sourceSize.X * scale.X) / 2;
            }
            else
            {
                scale.Y = scale.X;
                offset.Y = (outputSize.Y - sourceSize.Y * scale.Y) / 2;
            }

            return Matrix3x2.CreateScale(scale) *
                   Matrix3x2.CreateTranslation(offset);
        }


        //public static float DegreesToRadians(float angle)
        //{
        //    return angle * (float)Math.PI / 180;
        //}


        // static readonly Random random = new Random();


        //public static Random Random
        //{
        //    get { return random; }
        //}


        //public static float RandomBetween(float min, float max)
        //{
        //    return min + (float)random.NextDouble() * (max - min);
        //}


        //public static async Task<byte[]> ReadAllBytes(string filename)
        //{
        //    var uri = new Uri("ms-appx:///" + filename);
        //    var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
        //    var buffer = await FileIO.ReadBufferAsync(file);

        //    return buffer.ToArray();
        //}


        //public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout)
        //{
        //    if (task == await Task.WhenAny(task, Task.Delay(timeout)))
        //    {
        //        return await task;
        //    }
        //    else
        //    {
        //        throw new TimeoutException();
        //    }
        //}


        //public struct WordBoundary { public int Start; public int Length; }


        //public static List<WordBoundary> GetEveryOtherWord(string str)
        //{
        //    List<WordBoundary> result = new List<WordBoundary>();

        //    for (int i = 0; i < str.Length; ++i)
        //    {
        //        if (str[i] == ' ')
        //        {
        //            int nextSpace = str.IndexOf(' ', i + 1);
        //            int limit = nextSpace == -1 ? str.Length : nextSpace;

        //            WordBoundary wb = new WordBoundary
        //            {
        //                Start = i + 1,
        //                Length = limit - i - 1
        //            };
        //            result.Add(wb);
        //            i = limit;
        //        }
        //    }
        //    return result;
        //}

    }
}
