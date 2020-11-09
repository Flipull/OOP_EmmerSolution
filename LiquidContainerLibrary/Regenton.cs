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
                    TotalCapacity = 80;
                    break;
                case Size.Medium:
                    TotalCapacity = 120;
                    break;
                case Size.Large:
                    TotalCapacity = 160;
                    break;
                default:
                    throw new ArgumentException();
            }

            if (content < 0 || content > TotalCapacity)
                throw new ArgumentException();

            Fill(content);
        }
    }
}
