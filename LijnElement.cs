using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;

public class LijnElement : Element
{
    private Point p1, p2;
    private Pen pen;

    public LijnElement(Point a, Point b, Pen p)
    {
        p1 = a;
        p2 = b;
        pen = p;
    }

    public override void Teken(Graphics g)
    {
        g.DrawLine(pen, p1, p2);
    }


    //https://www.csharphelper.com/howtos/howto_point_segment_distance.html gekopieerde code onderaan

    // Calculate the distance between
    // point pt and the segment p1 --> p2.
    private double FindDistanceToSegment(
        PointF pt, PointF p1, PointF p2, out PointF closest)
    {
        float dx = p2.X - p1.X;
        float dy = p2.Y - p1.Y;
        if ((dx == 0) && (dy == 0))
        {
            // It's a point not a line segment.
            closest = p1;
            dx = pt.X - p1.X;
            dy = pt.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        // Calculate the t that minimizes the distance.
        float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) /
                  (dx * dx + dy * dy);

        // See if this represents one of the segment's
        // end points or a point in the middle.
        if (t < 0)
        {
            closest = new PointF(p1.X, p1.Y);
            dx = pt.X - p1.X;
            dy = pt.Y - p1.Y;
        }
        else if (t > 1)
        {
            closest = new PointF(p2.X, p2.Y);
            dx = pt.X - p2.X;
            dy = pt.Y - p2.Y;
        }
        else
        {
            closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
            dx = pt.X - closest.X;
            dy = pt.Y - closest.Y;
        }

        return Math.Sqrt(dx * dx + dy * dy);
    }

    public override bool Raak(Point p)
    {
        const float marge = 5f; //als het afstand kleiner dan 5px is, dan is het raak
        PointF closest;
        double d = FindDistanceToSegment(
            new PointF(p.X, p.Y),
            new PointF(p1.X, p1.Y),
            new PointF(p2.X, p2.Y),
            out closest
        );
        return d <= marge;
    }

    public override string ZichzelfOpslaan()
    {
        return $"Lijn {p1.X} {p1.Y} {p2.X} {p2.Y} {pen.Color.Name}";
    }
}