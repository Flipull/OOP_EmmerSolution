using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public class Regenton: LiquidContainerAbstract
    {
        public enum Size { Small = 80, Medium = 120, Large = 160};
        
        public Regenton(uint content = 0, Regenton.Size size = Size.Small)
        {
            Capacity = (uint)size;
            
            if (content < 0 || content > Capacity)
                throw new ArgumentException();
            Fill(content);
        }
    }
}
