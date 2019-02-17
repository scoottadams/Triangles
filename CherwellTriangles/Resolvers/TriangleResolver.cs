namespace CherwellTriangles.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface ITriangleResolver
    {
        List<PointF> Resolve(char charLetter, int number);

        string Resolve(PointF rightAngle, PointF topLeft, PointF bottomRight);
    }

    public class TriangleResolver : ITriangleResolver
    {
        public List<PointF> Resolve(char charLetter, int number)
        {
            if (number > 12)
                throw new ArgumentOutOfRangeException($"Number value {number} is out of range");
            var gridNumber = number - 1; // decrement to start the grid at zero

            var letter = char.ToUpper(charLetter) - 64;
            if (letter > 12)
                throw new ArgumentOutOfRangeException($"Character value {charLetter} is out of range");
            var gridLetter = letter - 1; // decrement to start the grid at zero

            var topLeftX = (gridNumber / 2) * 10;
            var topLeftY = gridLetter * 10;
            var topLeft = new PointF(topLeftX, topLeftY);

            var bottomRightX = topLeftX + 10;
            var bottomRightY = topLeftY + 10;
            var bottomRight = new PointF(bottomRightX, bottomRightY);

            PointF rightAngle;
            if (number % 2 == 0)
            {
                var rightAngleX = bottomRightX;
                var rightAngleY = topLeftY;
                rightAngle = new PointF(rightAngleX, rightAngleY);
            }
            else
            {
                var rightAngleX = topLeftX;
                var rightAngleY = bottomRightY;
                rightAngle = new PointF(rightAngleX, rightAngleY);
            }
            
            return new List<PointF> { rightAngle, topLeft, bottomRight };
        }

        public string Resolve(PointF rightAngle, PointF topLeft, PointF bottomRight)
        {
            var rightAngleX = rightAngle.X;
            var rightAngleNumber = ((rightAngleX / 10) * 2) + 1;

            var topLeftY = topLeft.Y;
            var charNumber = ((topLeftY / 10) + 64) + 1;

            var charLetter = (char)charNumber;

            return $"({charLetter}, {rightAngleNumber})";
        }
    }
}