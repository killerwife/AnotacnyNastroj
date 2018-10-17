using Projekt.DrawObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class CompareObj
    {
        public BoundingBox Figure { get; set; }
        public int Frame { get; set; }
        public int XPlus { get; set; }//-1 pohyb -, 1 pohyb +, 0 bez zmeny
        public int YPlus { get; set; }
        public int XGrow { get; set; }//-1 zmensenie -, 1 zvacsenie +, 0 bez zmeny
        public int YGrow { get; set; }
        private List<BoundingBox> adepts;
        private double score;
        public bool Found { get; set; }

        public CompareObj(BoundingBox f)
        {
            Figure = f;
            Frame = 0;
            XPlus = 0;
            YPlus = 0;
            XGrow = 0;
            YGrow = 0;
            adepts = new List<BoundingBox>();
            score = -1;
            Found = false;
        }

        public bool isSuitable(int searchWindow, int sizeChange, BoundingBox pa, bool metrics)
        {
            bool pxSwitch = metrics;
            bool sWin = false;            
            if (pxSwitch)
            {
                sWin = Math.Abs(pa.PointA.X - Figure.PointA.X) <= searchWindow && Math.Abs(pa.PointA.Y - Figure.PointA.Y) <= searchWindow;
            }
            else
            { 
                int xWidthPerc = (Math.Abs(pa.PointA.X - Figure.PointA.X) * 100)/(Figure.PointB.X - Figure.PointA.X);
                int xHeightPerc = (Math.Abs(pa.PointA.Y - Figure.PointA.Y) * 100)/(Figure.PointB.Y - Figure.PointA.Y);
                sWin = xWidthPerc <= searchWindow && xHeightPerc <= searchWindow; 
            }
            int widthPerc = ((pa.PointB.X - pa.PointA.X) * 100) / (Figure.PointB.X - Figure.PointA.X);
            int heightPerc = ((pa.PointB.Y - pa.PointA.Y) * 100) / (Figure.PointB.Y - Figure.PointA.Y);            
            
            return sWin
                && Math.Abs(100 - widthPerc) <= sizeChange
                && Math.Abs(100 - heightPerc) <= sizeChange;
        }

        public void checkAdept(BoundingBox pa, double movementWeight, double windowWeight, double sizeWeight, int min)
        {
            double tmp = 0;
            double tmp2 = 0;
            tmp2 = pa.PointA.X - Figure.PointA.X;
            tmp += Math.Abs(tmp2) * windowWeight;
            if (tmp2 < 0)
            {
                if (XPlus != -1)
                    tmp += 1 * movementWeight;                
            }
            else if (tmp2 > 0)
            {
                if (XPlus != 1)
                    tmp += 1 * movementWeight;                
            }
            else
            {
                if (XPlus != 0)
                    tmp += 1 * movementWeight;                
            }
            tmp2 = pa.PointA.Y - Figure.PointA.Y;
            tmp += Math.Abs(tmp2) * windowWeight;
            if (tmp2 < 0)
            {
                if (YPlus != -1)
                    tmp += 1 * movementWeight;
            }
            else if (tmp2 > 0)
            {
                if (YPlus != 1)
                    tmp += 1 * movementWeight;
            }
            else
            {
                if (YPlus != 0)
                    tmp += 1 * movementWeight;
            }
            tmp2 = (pa.PointB.X - pa.PointA.X) - (Figure.PointB.X - Figure.PointA.X);
            tmp += Math.Abs(tmp2) * sizeWeight;
            if (tmp2 < 0)
            {
                if (XGrow != -1)
                    tmp += 1 * movementWeight;
            }
            else if (tmp2 > 0)
            {
                if (XGrow != 1)
                    tmp += 1 * movementWeight;
            }
            else
            {
                if (XGrow != 0)
                    tmp += 1 * movementWeight;
            }
            tmp2 = (pa.PointB.Y - pa.PointA.Y) - (Figure.PointB.Y - Figure.PointA.Y);
            tmp += Math.Abs(tmp2) * sizeWeight;
            if (tmp2 < 0)
            {
                if (YGrow != -1)
                    tmp += 1 * movementWeight;
            }
            else if (tmp2 > 0)
            {
                if (YGrow != 1)
                    tmp += 1 * movementWeight;
            }
            else
            {
                if (YGrow != 0)
                    tmp += 1 * movementWeight;
            }

            if (score == -1)
            {
                adepts.Add(pa);
                score = tmp;
            }
            else
            {
                if (score == tmp)
                {
                    adepts.Add(pa);
                }
                else if (score > tmp && tmp + min >= score)
                {
                    adepts.Add(pa);
                    score = tmp;
                }
                else if (score > tmp)
                {
                    adepts.Clear();
                    adepts.Add(pa);
                    score = tmp;
                }                
            }
        }

        public void clearAdepts()
        {
            adepts.Clear();
            score = -1;
        }

        public int adeptsCount()
        {
            return adepts.Count;
        }

        public void swap()
        {
            adepts[0].Properties = Figure.Properties;
            int tmp = 0;
            tmp = adepts[0].PointA.X - Figure.PointA.X;
            if (tmp < 0)
                XPlus = -1;
            else if (tmp == 0)
                XPlus = 0;
            else
                XPlus = 1;
            tmp = adepts[0].PointA.Y - Figure.PointA.Y;
            if (tmp < 0)
                YPlus = -1;
            else if (tmp == 0)
                YPlus = 0;
            else
                YPlus = 1;
            tmp = (adepts[0].PointB.X - adepts[0].PointA.X) - (Figure.PointB.X - Figure.PointA.X);
            if (tmp < 0)
                XGrow = -1;
            else if (tmp == 0)
                XGrow = 0;
            else
                XGrow = 1;
            tmp = (adepts[0].PointB.Y - adepts[0].PointA.Y) - (Figure.PointB.Y - Figure.PointA.Y);
            if (tmp < 0)
                YGrow = -1;
            else if (tmp == 0)
                YGrow = 0;
            else
                YGrow = 1;
            Figure = adepts[0];
        }

        public BoundingBox getAdept()
        {
            return adepts[0];
        }
    }
}
