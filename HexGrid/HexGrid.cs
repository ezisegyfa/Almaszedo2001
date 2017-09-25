using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TurnBasedTableStrategyGame.HexGrid.MathsConstants;

namespace TurnBasedTableStrategyGame.HexGrid
{
    class HexGrid
    {
        private Hex[,] hexes;
        private int rowCount;
        private int columnCount;
        private int radiusSin30;
        private int radiusCos30;
        private int hexWidth;
        private int hexHeight;
        private Point originTop;
        private Point originTopRight;
        private Point originBottomRight;
        private Point originBottom;
        private Point originBottomLeft;
        private Point originTopLeft;
        private int radius;
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                radiusSin30 = (int)(radius * Sin30);
                radiusCos30 = (int)(radius * Cos30);
                setPointOriginValues();
            }
        }
        private Point originCentre;
        public Point OriginCentre
        {
            set
            {
                originCentre = value;
                setPointOriginValues();
            }
        }

        public HexGrid(int rowCount, int columnCount, Point originCentre, int radius)
        {
            this.OriginCentre = originCentre;
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.Radius = radius;
            hexes = new Hex[columnCount, rowCount];
            for (int y = 0; y < columnCount; ++y)
                for (int x = 0; x < rowCount; ++x)
                    hexes[y,x] = new Hex();
        }

        private void setPointOriginValues()
        {
            hexWidth = 2 * radiusCos30;
            hexHeight = 3 * radiusSin30;
            originTopRight = pointOffset(originCentre, new Point(radiusCos30, radiusSin30));
            originBottomRight = pointOffset(originCentre, new Point(radiusCos30, -radiusSin30));
            originBottom = pointOffset(originCentre, new Point(0, -radius));
            originBottomLeft = pointOffset(originCentre, new Point(-radiusCos30, -radiusSin30));
            originTopLeft = pointOffset(originCentre, new Point(-radiusCos30, radiusSin30));
            originTop = pointOffset(originCentre, new Point(0, radius));
        }

        public void Draw(Graphics g, Point topLeftCorn, Size layoutSize)
        {
            int firstHexRow = topLeftCorn.Y / hexHeight;
            if (firstHexRow > 0)
                --firstHexRow;
            int firstHexColumn = topLeftCorn.X / hexWidth;
            if (firstHexColumn > 0)
                --firstHexColumn;
            int hexRowCount = layoutSize.Height / hexHeight;
            int hexColumnCount = layoutSize.Width / hexWidth;
            int lastHexRow = firstHexRow + hexRowCount;
            if (lastHexRow >= rowCount)
                lastHexRow = rowCount - 1;
            else if (lastHexRow < rowCount - 1)
                ++lastHexRow;
            int lastHexColumn = firstHexColumn + hexColumnCount;
            if (lastHexColumn >= columnCount)
                lastHexColumn = columnCount - 1;
            else if (lastHexColumn < columnCount - 1)
                ++lastHexColumn;
            for (int x = firstHexColumn; x <= lastHexColumn; ++x)
                for (int y = firstHexRow; y <= lastHexRow; ++y)
                    DrawHex(g, y, x, topLeftCorn);
        }

        public void DrawHex(Graphics g, int rowNum, int columnNum, Point topLeftCorn)
        {
            Point hexCentre = pointOffset(GetHexCentre(rowNum, columnNum), negatePoint(topLeftCorn));
            var hexpoints = new[]
            {
                pointOffset(hexCentre, originTopRight),
                pointOffset(hexCentre, originBottomRight),
                pointOffset(hexCentre, originBottom),
                pointOffset(hexCentre, originBottomLeft),
                pointOffset(hexCentre, originTopLeft),
                pointOffset(hexCentre, originTop)
            };
            var bitmapPoints = hexpoints.Select(p => new PointF(p.X, p.Y)).ToArray();
            g.FillPolygon(new SolidBrush(Color.Blue), bitmapPoints);
            g.DrawPolygon(Pens.Black, bitmapPoints);
        }

        public Point GetHexCentre(int rowNum, int columnNum)
        {
            int x = 2 * radiusCos30 * columnNum;
            if (rowNum % 2 != 0)
            {
                x = x + radiusCos30;
            }
            int y = (Radius + radiusSin30) * rowNum;
            return new Point(x, y);
        }

        private static Point pointOffset(params Point[] points)
        {
            Point sum = new Point(0, 0);
            foreach (Point currentPoint in points)
                sum.Offset(currentPoint);
            return sum;
        }

        private static Point negatePoint(Point point)
        {
            return new Point(-point.X, -point.Y);
        }
    }
}
