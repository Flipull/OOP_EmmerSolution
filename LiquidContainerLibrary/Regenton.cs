using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public class Regenton: LiquidContainerAbstract
    {
        public enum Size { Small, Medium, Large};
        
        public Regenton(uint content = 0, Regenton.Size size = Size.Small)
        {
            switch (size)
            {
                case Size.Small:
                    Capacity = 80;
                    break;
                case Size.Medium:
                    Capacity = 120;
                    break;
                case Size.Large:
                    Capacity = 160;
                    break;
                default:
                    throw new ArgumentException();
            }

            if (content < 0 || content > Capacity)
                throw new ArgumentException();

            Fill(content);
        }
    }
}
