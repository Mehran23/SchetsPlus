using System.Drawing;

public class VolCirkelElement : Element
{
    private Rectangle kader;
    private Brush kwast;

    public VolCirkelElement(Rectangle r, Brush b)
    {
        kader = r;
        kwast = b;
    }

    public override void Teken(Graphics g)
    {
        g.FillEllipse(kwast, kader);
    }

    public override bool Raak(Point p)
    {
        return kader.Contains(p);
    }
}
