using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace Demos
{
    public class DrwingPadHub:Hub
    {
        private static List<Point> _buffer = new List<Point>();
        public Task BroadcastPoint(int clickX, int clickY, bool clickDrag, string color)
        {
            Point p = new Point();
            p.ClickX = clickX;
            p.ClickY = clickY;
            p.ClickDrag = clickDrag;
            p.Color = color;
            _buffer.Add(p);

            return Clients.Others.DrawPoint(p);
        }
        public override Task OnConnected()
        {
            return Clients.Caller.Initialization(_buffer);
        }
        public Task Clear()
        {
            _buffer = new List<Point>();
            return Clients.Others.Clear();
        }
    }
    public class Point
    {
        public int ClickX { get; set; }
        public int ClickY { get; set; }
        public bool ClickDrag { get; set; }
        public string Color { get; set; }
    }


}