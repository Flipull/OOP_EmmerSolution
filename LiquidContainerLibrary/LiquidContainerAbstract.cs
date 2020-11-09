using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public delegate void IsFull();
    public delegate bool WillOverflow();
    public delegate void HasOverflown(uint amount);

    public abstract class LiquidContainerAbstract
    {
        //public readonly int TotalCapacity;
        public uint TotalCapacity { get; protected set; }
        public uint Content { get; private set; }
        public uint CapacityLeft {
            get { return TotalCapacity - Content; }
            private set { }
        }
        
        public event IsFull isFull;
        public event WillOverflow willOverflow;
        public event HasOverflown hasOverflown;

        public void Fill(LiquidContainerAbstract c)
        {
            if (this.Equals(c)) throw new ArgumentException();
            
            if (CapacityLeft < c.Content)
            {
                var continue_filling = true;
                if (willOverflow != null)
                    foreach (var invmethods in willOverflow.GetInvocationList())
                    {
                        continue_filling = continue_filling &
                            (bool)invmethods.DynamicInvoke(null);
                        //if (!continue_filling) return;
                        //would be more performant, but will end invocation of delegates prematurely
                    }
                    if (!continue_filling) return;
            }

            var content_total = c.Content;
            c.Empty();//empty other bucket first

            var content_transferred = Math.Min(content_total, CapacityLeft);
            var overflowamount = content_total - content_transferred;
            
            Content += content_transferred;
            if(overflowamount > 0)
                hasOverflown?.Invoke(overflowamount);

            if (CapacityLeft == 0)
                isFull?.Invoke();
        }
        
        public void Fill(uint amount) {
            if (CapacityLeft < amount)
            {
                var continue_filling = true;
                if (willOverflow != null)
                    foreach(var invmethods in willOverflow.GetInvocationList() )
                    {
                        continue_filling = continue_filling & 
                            (bool)invmethods.DynamicInvoke(null);
                        if (!continue_filling) return;
                    }

                var overflowamount = amount - CapacityLeft;
                Content += CapacityLeft;
                hasOverflown?.Invoke(overflowamount);
            } else
            {
                Content += amount;
            }
            if (CapacityLeft == 0)
                isFull?.Invoke();
        }
        public void Empty() => Empty(Content);
        public void Empty(uint amount)
        {
            if (amount < 0 || Content < amount)
            {
                throw new ArgumentException();
            }
            Content -= amount;
        }
    }
}
