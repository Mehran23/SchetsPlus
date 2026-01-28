using System.Drawing;

public abstract class Element
{
    public abstract void Teken(Graphics g);
    public abstract bool Raak(Point p);
    public abstract string ZichzelfOpslaan();
}