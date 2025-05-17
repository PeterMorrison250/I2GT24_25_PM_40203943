using System;
using Gyrobo.Enums;

namespace Gyrobo
{
    public class GravityChangedEventArgs : EventArgs
    {
        public GravityDirection GravityDirection { get; set; }
    }
}