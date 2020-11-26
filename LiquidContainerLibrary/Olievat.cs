using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public class Olievat : LiquidContainerAbstract
    {
        public const uint DefaultCapacity = 159;
        public Olievat(uint content = 0)
        {
            Capacity = Olievat.DefaultCapacity;
            Fill(content);
        }
    }
}
