using System;

namespace LiquidContainerLibrary
{
    public class Emmer : LiquidContainerAbstract
    {
        public Emmer(uint content = 0, uint capacity = 12)
        {
            if (capacity < 10) throw new ArgumentException();
            if (content < 0 || content > capacity) throw new ArgumentException();

            
            TotalCapacity = capacity;
            Fill(content);
        }
    }
}
