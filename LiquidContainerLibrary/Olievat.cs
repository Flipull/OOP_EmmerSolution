using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public class Olievat : LiquidContainerAbstract
    {
        public const uint DefaultCapacity = 159;
        public Olievat() : this(0)
        { }
        public Olievat(uint content)
        {
            Capacity = Olievat.DefaultCapacity;
            Fill(content);
        }
    }
}
