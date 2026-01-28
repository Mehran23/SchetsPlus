using System.Drawing;

public class CirkelElement : Element
{
    private Rectangle kader;
    private Pen pen;

    public CirkelElement(Rectangle r, Pen p)
    {
        kader = r;
        pen = p;
    }

    public override void Teken(Graphics g)
    {
        g.DrawEllipse(pen, kader);
    }

    public override bool Raak(Point p)
    {
        return kader.Contains(p);
    }

    public override string ZichzelfOpslaan()
    {
        return $"Cirkel {kader.X} {kader.Y} {kader.Width} {kader.Height} ";
    }
}