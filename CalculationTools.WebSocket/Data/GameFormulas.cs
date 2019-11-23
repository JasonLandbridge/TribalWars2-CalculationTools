using CalculationTools.Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CalculationTools.WebSocket
{
    public static class GameFormulas
    {
        /// <summary>
        /// Scales down the given rect into a grid and returns its coordinates.
        /// </summary>
        /// <param name="village"></param>
        /// <returns></returns>
        public static List<Point> ScaledGridCoordinates(IVillage village)
        {
            int x = village.X;
            int y = village.Y;
            int w = 25;
            int h = 25;
            int gridSize = 25;
            int minX = (int)Math.Floor((decimal)x / gridSize);
            int minY = (int)Math.Floor((decimal)y / gridSize);
            int maxX = (int)Math.Ceiling(((decimal)x + w) / gridSize);
            int maxY = (int)Math.Ceiling(((decimal)y + h) / gridSize);
            List<Point> gridCoordinates = new List<Point>();

            if (w == 1 && h == 1)
            {
                gridCoordinates.Add(new Point(minX, minY));
                return gridCoordinates;
            }

            for (x = minX; x < maxX; x++)
            {
                for (y = minY; y < maxY; y++)
                {
                    gridCoordinates.Add(new Point(x, y));
                }
            }

            return gridCoordinates;
        }
    }

}
