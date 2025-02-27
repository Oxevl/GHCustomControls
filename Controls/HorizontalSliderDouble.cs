﻿using GH_IO.Types;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFNumericUpDown;
/*
Original work Copyright (c) 2021 Ali Torabi (ali@parametriczoo.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
*/
namespace GHCustomControls
{
    public class HorizontalSliderDouble : HorizontalSlider
    {
       
       
        
        public HorizontalSliderDouble(string title,string description,double val,float min,float max, int decimals, string suffix="", bool showTitle = true) : base(title, description,val,min,max,showTitle,$"N{decimals}",decimals, suffix) 
        {
 
            CurrentValue = val;    
        }
 


        public override GH_Types DataType => GH_Types.gh_double;
        public override GH_ParamAccess Access => GH_ParamAccess.item;

        public override string GetValueAsString(float x)
        {
            double v = _min + x * (_max - _min);
            return v.ToString(FormatString) + Suffix;
        }

        public override void UpdatePos()
        {
            X = GetPos(Convert.ToSingle( CurrentValue));
        }

        public override void UpdateValue()
        {
            CurrentValue = (double)GetCurrentValue();
        }

        internal override void MouseRightClick(GH_Canvas sender, GHCustomComponent customComponent, GH_CanvasMouseEvent e, ref GHMouseEventResult result)
        {
            base.MouseRightClick(sender, customComponent, e, ref result);
            if (result.HasFlag(GHMouseEventResult.Handled))
                return;
            double d = (double)CurrentValue;
            //NumericUpDownData<double> numeric = new NumericUpDownData<double>(d, _min, _max,FormatString);

            //float s = GH_FontServer.MeasureString("A", SmallFont).Height * sender.Viewport.Zoom / 20;
            //if (numeric.GetInput(PointToScreen(sender,Pos),  out double val))
            DoubleNumber number = new DoubleNumber((double)CurrentValue, _min, _max, (_max - _min) / 100, _decimals);
            if (Helpers.GetDoubleInput(PointToScreen(sender, Pos),number))
            {
                CurrentValue = number.Value;
                result = result | GHMouseEventResult.UpdateSolution | GHMouseEventResult.Handled;
            }
            else
            {
                result = result | GHMouseEventResult.Handled;
            }
            showLabel = true;
        }





    }
}
