﻿using System.Windows.Forms;

namespace WinFormsCoreLib.ControlsExtensions
{
    public static partial class ControlExtension
    {
        public static TControl OnMouseMove<TControl>(this TControl that, MouseEventHandler handler)
            where TControl: Control
        {
            that.MouseMove += handler;
            return that;
        }
    }
}
