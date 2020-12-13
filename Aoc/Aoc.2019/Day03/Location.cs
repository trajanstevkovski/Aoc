namespace Aoc._2019.Day03
{
    public class Location
    {
        public int Moves { get; set; }
        public Direction Direction { get; set; }

        public Point[] MoveFromPoint(Point startPoint)
        {
            Point[] points = new Point[Moves];
            for (int i = 1; i <= Moves; i++)
            {
                var point = new Point();
                switch (Direction)
                {
                    case Direction.Up:
                        point.X = startPoint.X;
                        point.Y = startPoint.Y + i;
                        points[i - 1] = point;
                        continue;
                    case Direction.Down:
                        point.X = startPoint.X;
                        point.Y = startPoint.Y - i;
                        points[i - 1] = point;
                        continue;
                    case Direction.Left:
                        point.X = startPoint.X - i;
                        point.Y = startPoint.Y;
                        points[i - 1] = point;
                        continue;
                    case Direction.Right:
                        point.X = startPoint.X + i;
                        point.Y = startPoint.Y;
                        points[i - 1] = point;
                        continue;
                }
            }
            return points;
        }
    }
}