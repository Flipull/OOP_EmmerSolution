﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public class Regenton: LiquidContainerAbstract
    {
        public enum Size { Small = 80, Medium = 120, Large = 160};

        public Regenton(): this(0, Size.Small)
        { }
        public Regenton(uint content, Size size = Size.Small)
        {
            Capacity = (uint)size;
            
            if (content < 0 || content > Capacity)
                throw new ArgumentException();
            Fill(content);
        }
    }
}
