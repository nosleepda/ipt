using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicLibrary
{
    public class Graphic
    {
        private List<Data> _graphList = new List<Data>();

        public Graphic(string title, string xAxis, string yAxis, string output = "xwin")
        {
            dislin.metafl(output);
            dislin.disini();
            dislin.titlin(title, 3);
            dislin.name(xAxis, "X");
            dislin.name(yAxis, "Y");
        }

        public void SetSizeWindow(int width, int height)
        {
            dislin.axslen(height, width);
        }

        public void SetPlane(float xStart, float xEnd, float xOrigin, float xStep, float yStart, float yEnd,
            float yOrigin,
            float yStep)
        {
            dislin.graf(xStart, xEnd, xOrigin, xStep, yStart, yEnd, yOrigin, yStep);
        }

        public void SetPlane(double xStart, double xEnd, double xOrigin, double xStep, double yStart, double yEnd,
            double yOrigin,
            double yStep)
        {
            dislin.graf(ConverterFloat(xStart), ConverterFloat(xEnd), ConverterFloat(xOrigin),
                ConverterFloat(xStep), ConverterFloat(yStart), ConverterFloat(yEnd),
                ConverterFloat(yOrigin), ConverterFloat(yStep));
        }

        public void SetPlane(int xStart, int xEnd, int xOrigin, int xStep, int yStart, int yEnd, int yOrigin,
            int yStep)
        {
            dislin.graf(xStart, xEnd, xOrigin, xStep, yStart, yEnd, yOrigin, yStep);
        }

        public void AddGraph(float[] x, float[] y, string color = "red")
        {
            _graphList.Add(new Data(x, y, color));
        }

        public void AddGraph(double[] x, double[] y, string color = "red")
        {
            AddGraph(Array.ConvertAll(x, ConverterFloat), Array.ConvertAll(y, ConverterFloat), color);
        }

        public void AddGraph(List<double> x, List<double> y, string color = "red")
        {
            AddGraph(x.ToArray(), y.ToArray(), color);
        }

        public void AddGraph(List<float> x, List<float> y, string color = "red")
        {
            AddGraph(x.ToArray(), y.ToArray(), color);
        }

        public void AddGraph(int[] x, int[] y, string color = "red")
        {
            AddGraph(Array.ConvertAll(x, ConverterFloat), Array.ConvertAll(x, ConverterFloat), color);
        }

        public void AddGraph(double[] x, int[] y, string color = "red")
        {
            AddGraph(Array.ConvertAll(x, ConverterFloat), Array.ConvertAll(x, ConverterFloat), color);
        }

        public void AddGraph(int[] x, double[] y, string color = "red")
        {
            AddGraph(Array.ConvertAll(x, ConverterFloat), Array.ConvertAll(x, ConverterFloat), color);
        }

        public void DrawBarsGraph(float[] y, string color = "red")
        {
            var y1 = new[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
            var xx = Enumerable.Range(1, y.Length + 1);
            dislin.labels("none", "x");
            dislin.labels("second", "bars");
            dislin.labpos("outside", "bars");
            dislin.color(color);
            dislin.bars(Array.ConvertAll(xx.ToArray(), ConverterFloat), y1, y, y.Length);
            dislin.endgrf();
            dislin.disfin();
        }

        public void DrawBarsGraph(double[] y, string color = "red")
        {
            DrawBarsGraph(Array.ConvertAll(y, ConverterFloat), color);
        }

        public void DrawBarsGraph(int[] y, string color = "red")
        {
            DrawBarsGraph(Array.ConvertAll(y, ConverterFloat), color);
        }

        public void DrawGraph()
        {
            foreach (var graph in _graphList)
            {
                dislin.color(graph.Color);
                dislin.curve(graph.X, graph.Y, graph.Y.Length);
            }

            dislin.endgrf();
            dislin.disfin();
        }

        public void DrawFrequencyPolygon(float[] y, bool isJoin = true, string color = "red")
        {
            var x = Enumerable.Range(1, y.Length);
            dislin.polcrv("SPLINE");
            dislin.color(color);
            dislin.marker(16);
            dislin.incmrk(1);
            dislin.curve(Array.ConvertAll(x.ToArray(), ConverterFloat), y, y.Length);
            if (isJoin) return;
            dislin.endgrf();
            dislin.disfin();
        }
        
        public void DrawFrequencyPolygon(int[] y, bool isJoin = true, string color = "red")
        {
            DrawFrequencyPolygon(Array.ConvertAll(y.ToArray(), ConverterFloat), isJoin, color);
        }
        
        public void DrawFrequencyPolygon(double[] y, bool isJoin = true, string color = "red")
        {
            DrawFrequencyPolygon(Array.ConvertAll(y.ToArray(), ConverterFloat), isJoin, color);
        }

        private static float ConverterFloat(double value)
        {
            return Convert.ToSingle(value);
        }

        private static float ConverterFloat(int value)
        {
            return Convert.ToSingle(value);
        }

        private class Data
        {
            internal string Color { get; set; }

            public Data(float[] x, float[] y, string color)
            {
                X = x;
                Y = y;
                Color = color;
            }

            internal float[] X { get; set; }

            internal float[] Y { get; set; }
        }
    }
}