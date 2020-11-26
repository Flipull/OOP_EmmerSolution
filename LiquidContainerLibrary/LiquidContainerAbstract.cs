using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidContainerLibrary
{
    public delegate void ContainerFull();
    public delegate bool ContainerOverflows();
    public delegate void ContainerOverflown(uint amount);

    public abstract class LiquidContainerAbstract
    {
        public uint Capacity { get; protected set; }
        public uint Content { get; private set; }
        public uint CapacityLeft {
            get { return Capacity - Content; }
            private set { }
        }
        
        public event ContainerFull Full;
        public event ContainerOverflows Overflows;
        public event ContainerOverflown Overflown;

        public void Fill(LiquidContainerAbstract c)
        {
            if (this.Equals(c)) throw new ArgumentException();
            
            if (CapacityLeft < c.Content)
            {
                var continue_filling = true;
                if (Overflows != null)
                    foreach (var invmethods in Overflows.GetInvocationList())
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
                Overflown?.Invoke(overflowamount);

            if (CapacityLeft == 0)
                Full?.Invoke();
        }
        
        public void Fill(uint amount) {
            if (CapacityLeft < amount)
            {
                var continue_filling = true;
                if (Overflows != null)
                    foreach(var invmethods in Overflows.GetInvocationList() )
                    {
                        continue_filling = continue_filling & 
                            (bool)invmethods.DynamicInvoke(null);
                        if (!continue_filling) return;
                    }

                var overflowamount = amount - CapacityLeft;
                Content += CapacityLeft;
                Overflown?.Invoke(overflowamount);
            } else
            {
                Content += amount;
            }
            if (CapacityLeft == 0)
                Full?.Invoke();
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
