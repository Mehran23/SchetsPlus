using System;
using System.Drawing;
using System.IO;

public class VolRechthoekElement : Element
{
	private Rectangle kader;
	private Brush kwast;

	public VolRechthoekElement(Rectangle r, Brush b)
	{
		kader = r;
		kwast = b;
	}

	public override void Teken(Graphics g)
	{
		g.FillRectangle(kwast, kader);
	}

	public override bool Raak(Point p)
	{
		return kader.Contains(p);
	}

    public override string ZichzelfOpslaan()
    {
        return $"VolRechtHoek {kader.X} {kader.Y} {kader.Width} {kader.Height} ";
    }
}






