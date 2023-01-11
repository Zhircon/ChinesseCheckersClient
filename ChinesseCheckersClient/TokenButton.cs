using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChinesseCheckersClient
{
    class TokenButton : Button
    {
        private char hideContent;
        private int owner;
        private Point position;
        public char HideContent{
            get { return hideContent; }
            set { hideContent = value; }
        }
        public int Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
