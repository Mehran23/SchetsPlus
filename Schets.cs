using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

public class Schets
{
    private Bitmap bitmap;
    public List<Element> elementen = new List<Element>();

    public Schets()
    {
        bitmap = new Bitmap(1, 1);
    }
    public Graphics BitmapGraphics
    {
        get { return Graphics.FromImage(bitmap); }
    }

    public void TekenOpnieuw()
    {
        Graphics g = Graphics.FromImage(bitmap);
        g.Clear(Color.White);

        foreach (Element e in elementen)
            e.Teken(g);
    }

    public void VoegToe(Element e)
    {
        elementen.Add(e);
        TekenOpnieuw();
    }

    public void Gum(Point p)
    {
        for (int i = elementen.Count - 1; i >= 0; i--)
        {
            if (elementen[i].Raak(p))
            {
                elementen.RemoveAt(i);
                TekenOpnieuw();
                return;
            }
        }
    }

    public void VeranderAfmeting(Size sz)
    {
        if (sz.Width > bitmap.Size.Width || sz.Height > bitmap.Size.Height)
        {
            Bitmap nieuw = new Bitmap(Math.Max(sz.Width, bitmap.Size.Width)
                                     , Math.Max(sz.Height, bitmap.Size.Height)
                                     );
            Graphics gr = Graphics.FromImage(nieuw);
            gr.FillRectangle(Brushes.White, 0, 0, sz.Width, sz.Height);
            gr.DrawImage(bitmap, 0, 0);
            bitmap = nieuw;
        }
    }
    public void Teken(Graphics gr)
    {
        gr.DrawImage(bitmap, 0, 0);
    }
    public void Schoon()
    {
        elementen.Clear(); //zonder dit blijven de elementen staan na klikken op clear, ze komen weer tevoorschijn
        Graphics gr = Graphics.FromImage(bitmap);
        gr.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
    }
    public void Roteer()
    {
        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
    }

    public void Opslaan(string filenaam)
    {
        StreamWriter w = new StreamWriter(filenaam);
        foreach (Element e in elementen)
            w.WriteLine(e.ZichzelfOpslaan);
        w.Close();
    }

    public void Inlezen(string filenaam)
    {
        StreamReader r = new StreamReader(filenaam);
        string regel;
        string[] woorden;
        elementen.Clear();

        while ((regel = r.ReadLine()) != null)
        {
            woorden = regel.Split(" ");

            if (woorden[0] == "Rechthoek")
            {
                Rectangle rect = new Rectangle(int.Parse(woorden[1]), int.Parse(woorden[2]), int.Parse(woorden[3]), int.Parse(woorden[4]));
                VoegToe(new RechthoekElement(rect, Pens.Black));
            }

            else if (woorden[0] == "VolRechtHoek")
            {

            }

            else if (woorden[0] == "Cirkel")
            {

            }

            else if (woorden[0] == "VolCirkel")
            {

            }

            else if (woorden[0] == "lijn")
            {

            }
        }
        r.Close();
        TekenOpnieuw();
    }

}



