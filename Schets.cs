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
            w.WriteLine(e.ZichzelfOpslaan());
        w.Close();
    }

    public void Inlezen(string filenaam)
    {
        elementen.Clear();

        using StreamReader r = new StreamReader(filenaam);
        string regel;

        while ((regel = r.ReadLine()) != null)
        {
            string[] w = regel.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (w.Length == 0) continue;

            string token = w[0];

            switch (token)
            {
                case "Rechthoek":
                    {
                        Rectangle rect = new Rectangle(int.Parse(w[1]), int.Parse(w[2]), int.Parse(w[3]), int.Parse(w[4]));
                        Color kleur = Color.FromName(w[5]);
                        elementen.Add(new RechthoekElement(rect, new Pen(kleur, 3)));
                        break;
                    }

                case "VolRechthoek":
                    {
                        Rectangle rect = new Rectangle(int.Parse(w[1]), int.Parse(w[2]), int.Parse(w[3]), int.Parse(w[4]));
                        Color kleur = Color.FromName(w[5]);
                        elementen.Add(new VolRechthoekElement(rect, new SolidBrush(kleur)));
                        break;
                    }

                case "Cirkel":
                    {
                        Rectangle rect = new Rectangle(int.Parse(w[1]), int.Parse(w[2]), int.Parse(w[3]), int.Parse(w[4]));
                        Color kleur = Color.FromName(w[5]);
                        elementen.Add(new CirkelElement(rect, new Pen(kleur, 3)));
                        break;
                    }

                case "VolCirkel":
                    {
                        Rectangle rect = new Rectangle(int.Parse(w[1]), int.Parse(w[2]), int.Parse(w[3]), int.Parse(w[4]));
                        Color kleur = Color.FromName(w[5]);
                        elementen.Add(new VolCirkelElement(rect, new SolidBrush(kleur)));
                        break;
                    }

                case "Lijn":
                    {
                        Point a = new Point(int.Parse(w[1]), int.Parse(w[2]));
                        Point b = new Point(int.Parse(w[3]), int.Parse(w[4]));
                        Color kleur = Color.FromName(w[5]);
                        elementen.Add(new LijnElement(a, b, new Pen(kleur, 3)));
                        break;
                    }
            }
        }

        TekenOpnieuw();
    }


}




