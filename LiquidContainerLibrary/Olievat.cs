using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public class Olievat : LiquidContainerAbstract
    {
        public static readonly uint DefaultCapacity = 159;
        public Olievat(uint content = 0)
        {
            TotalCapacity = Olievat.DefaultCapacity;
            Fill(content);
        }
    }
}
