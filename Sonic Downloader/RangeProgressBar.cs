using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader
{
   
    public partial class RangeProgressBar : Control
    {

        public List<Range> Ranges = new List<Range>();
        private Color rangeColor = Color.DodgerBlue;
        public Color RangeColor
        {
            get
            {
                return rangeColor;
            }
            set
            {
                rangeColor = value;
                Invalidate();
            }
        }


        public RangeProgressBar()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            FillBoxes(pe.Graphics);
            DrawBoxes(pe.Graphics);

        }
        private void DrawBoxes(Graphics e)
        {
            if (Ranges.Count <= 0)
                return;

            int l = Ranges.Count;
            int indWidth = ClientRectangle.Width / Ranges.Count;
            for(int i=0;i<l;i++)
            {
                Rectangle r = new Rectangle(i * indWidth, 0, indWidth, Height-1);
                e.DrawRectangle(new Pen(ForeColor), r);
            }
            
        }
        private void FillBoxes(Graphics e)
        {
            if (Ranges.Count <= 0)
                return;

            int l = Ranges.Count;
            int indWidth = ClientRectangle.Width / Ranges.Count;
            for (int i = 0; i < l; i++)
            {
                Rectangle r = new Rectangle(i * indWidth, 0,(int)(indWidth*Per(Ranges[i])), Height - 1);
                e.FillRectangle(new SolidBrush(RangeColor), r);
            }

        }
        private float Per(Range r)
        {

            long diff = r.End - r.Start;
            if (diff == 0)
                return 0;
            return r.Downloaded * 1.0f / diff;
        }
        public class Range
        {
            public long Start=0;
            public long End=0;
            public long Downloaded=0;
        }
    }
}
