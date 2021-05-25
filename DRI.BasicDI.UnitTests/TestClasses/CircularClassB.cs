using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.UnitTests.TestClasses
{
    internal class CircularClassB
    {
        public CircularClassB(CircularClassC circularClassC)
        {
        }        
    }
}