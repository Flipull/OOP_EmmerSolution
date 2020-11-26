using System;

namespace LiquidContainerLibrary
{
    public class Emmer : LiquidContainerAbstract
    {
        public Emmer(): this(0, 12)
        { }
        public Emmer(uint content, uint capacity)
        {
            if (capacity < 10) throw new ArgumentException();
            if (content < 0 || content > capacity) throw new ArgumentException();

            
            Capacity = capacity;
            Fill(content);
        }

    }
}
