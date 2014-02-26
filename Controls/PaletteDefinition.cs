using System.Drawing;
using SF.Space;

namespace SF.Controls
{
    public class ColorSelector<T>
    {
        public T Default { get; set; }
        public T My { get; set; }
        public T Friendly { get; set; }
        public T Hostile { get; set; }
        public T Neutral { get; set; }
        public T Select(Ship OwnShip, Ship ship)
        {
            if (ship == OwnShip)
                return My;
            if (ship.Nation == null)
                return Neutral;
            if (OwnShip != null && ship.Nation == OwnShip.Nation)
                return Friendly;
            if (OwnShip != null && ship.Nation != OwnShip.Nation)
                return Hostile;
            return Default;
        }
    }

    public class PaletteDefinition
    {
        public Brush WhitePaper;
        public Brush BlackInk;
        public Brush ControlPaper;
        public Pen BlackPencil;
        public Pen BlackPen;
        public Pen NavyPen;
        public Pen SignalPen;
        public Brush NavyBrush;
        public Brush SignalBrush;
        public ColorSelector<Pen> VulnerableSectors;
        public ColorSelector<Pen> MissileCircles;
        public ColorSelector<Pen> ShipHulls;
        public ColorSelector<Brush> ShipNames;
        public Color BackColor;
        public Color SecondaryBackColor;
        public Color ForeColor;
        public Color SecondaryForeColor;

        public static PaletteDefinition White = new PaletteDefinition()
        {
            WhitePaper = Brushes.White,
            BlackInk = Brushes.Black,
            ControlPaper = Brushes.AliceBlue,
            BlackPencil = Pens.DarkGray,
            BlackPen = Pens.Black,
            NavyPen = Pens.Navy,
            SignalPen = Pens.Firebrick,
            NavyBrush = Brushes.Navy,
            SignalBrush = Brushes.Firebrick,
            BackColor = Color.WhiteSmoke,
            SecondaryBackColor = Color.LightGray,
            ForeColor = Color.Black,
            SecondaryForeColor = Color.Black,
            VulnerableSectors = new ColorSelector<Pen>
            {
                Default = Pens.Black,
                My = Pens.Firebrick,
                Friendly = Pens.DarkGray,
                Hostile = Pens.DarkGray,
                Neutral = Pens.DarkGray,
            },
            MissileCircles = new ColorSelector<Pen>
            {
                Default = Pens.Black,
                My = Pens.Navy,
                Friendly = Pens.DarkGray,
                Hostile = Pens.DarkRed,
                Neutral = Pens.DarkGray,
            },
            ShipHulls = new ColorSelector<Pen>
            {
                Default = Pens.Black,
                My = Pens.Navy,
                Friendly = Pens.Navy,
                Hostile = Pens.Firebrick,
                Neutral = Pens.DarkGreen,
            },
            ShipNames = new ColorSelector<Brush>
            {
                Default = Brushes.DarkGray,
                My = Brushes.Black,
                Friendly = Brushes.Navy,
                Hostile = Brushes.Firebrick,
                Neutral = Brushes.DarkGreen,
            },
        };

        public static PaletteDefinition Black = new PaletteDefinition
            {
                ForeColor = Color.White,
                SecondaryForeColor = Color.White,
                BackColor = Color.Black,
                SecondaryBackColor = Color.DarkGray,
                BlackInk = Brushes.WhiteSmoke,
                ControlPaper = Brushes.Gray,
                BlackPencil = Pens.LightBlue,
                BlackPen = Pens.WhiteSmoke,
                NavyPen = Pens.MediumTurquoise,
                SignalPen = Pens.LightCoral,
                NavyBrush = Brushes.MediumTurquoise,
                SignalBrush = Brushes.LightCoral,
                WhitePaper = Brushes.DarkSlateGray,
                MissileCircles = new ColorSelector<Pen>
                {
                    Default = Pens.LightGray,
                    Friendly = Pens.LightGray,
                    Hostile = Pens.Tomato,
                    Neutral = Pens.LightGray,
                    My = Pens.Turquoise,
                },
                ShipHulls = new ColorSelector<Pen>
                {
                    Default = Pens.WhiteSmoke,
                    Friendly = Pens.LightSeaGreen,
                    Hostile = Pens.OrangeRed,
                    Neutral = Pens.LightGreen,
                    My = Pens.LightSeaGreen,
                },
                ShipNames = new ColorSelector<Brush>
                {
                    Default = Brushes.White,
                    Friendly = Brushes.LightSeaGreen,
                    Hostile = Brushes.OrangeRed,
                    Neutral = Brushes.LightGreen,
                    My = Brushes.WhiteSmoke,
                },
                VulnerableSectors = new ColorSelector<Pen>
                {
                    Default = Pens.LightGray,
                    Friendly = Pens.LightGray,
                    Hostile = Pens.LightGray,
                    Neutral = Pens.LightGray,
                    My = Pens.Tomato,
                },
            };
    }
}
