namespace Aoc._2019.Day03
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static bool operator== (Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;
        public static bool operator!= (Point p1, Point p2) => p1.X != p2.X || p1.Y != p2.Y;

        private bool Equals(Point point)
        {
            if(point is null)
            {
                return false;
            }
            return X == point.X && Y == point.Y;
        }

        public override bool Equals(object obj) => Equals(obj as Point);
        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}