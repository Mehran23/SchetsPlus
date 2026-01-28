using System.Drawing;

public class RechthoekElement : Element
{
    private Rectangle kader;
    private Pen pen;

    public RechthoekElement(Rectangle r, Pen p)
    {
        kader = r;
        pen = p;
    }

    public override void Teken(Graphics g)
    {
        g.DrawRectangle(pen, kader);
    }

    public override bool Raak(Point p)
    {
        return kader.Contains(p);
    }
}