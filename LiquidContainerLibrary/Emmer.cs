using System;

namespace LiquidContainerLibrary
{
    public class Emmer : LiquidContainerAbstract
    {
        public Emmer() : this(0, 12)
        { }
        public Emmer(uint content) : this(content, 12)
        { }
        public Emmer(uint content, uint capacity)
        {
            if (capacity < 10) throw new ArgumentException();
            if (content < 0 || content > capacity) throw new ArgumentException();

            
            Capacity = capacity;
            Fill(content);
        }

        public uint Fill(LiquidContainerAbstract c)
        {
            if (this.Equals(c)) return 0;
            var content_taken = Fill(c.Content);
            c.Content -= content_taken;
            return content_taken;
        }

    }
}
