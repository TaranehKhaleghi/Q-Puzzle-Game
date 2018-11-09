using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKhaleghiAssignment1
{
        public struct Point2D
        {
            public float X;
            public float Y;

            public Point2D(float X, float Y)
            {
                this.X = X;
                this.Y = Y;
            }

            public float Distance(Point2D otherPoint)
            {
                return (float)Math.Sqrt(Math.Pow((otherPoint.X - X), 2) + Math.Pow((otherPoint.Y - Y), 2));
            }
        }

        public struct Rectangle2D
        {
            public Point2D origin;
            public float width;
            public float height;

            public Rectangle2D(Point2D Origin, float Width, float Height)
            {
                origin = Origin;
                width = Width;
                height = Height;
            }

            public float Area()
            {
                return width * height;
            }
        }
    
}
