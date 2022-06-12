using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfShips
{
    internal class Coords
    {
        public int x { get; }
        public int y { get; }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
