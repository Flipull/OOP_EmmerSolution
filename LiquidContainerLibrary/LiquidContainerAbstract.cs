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
        
        private uint _content = 0;
        public uint Content {
            get 
            { 
                return _content; 
            }
            set 
            {
                if (value >= 0 && value <= Capacity)
                    _content = value;
                else
                    throw new ArgumentOutOfRangeException();
            } 
        }
        protected uint CapacityLeft {
            get { return Capacity - Content; }
        }
        
        public event ContainerFull Full;
        public event ContainerOverflows Overflows;
        public event ContainerOverflown Overflown;
        
        public uint Fill(uint amount) {
            if (CapacityLeft < amount)
            {
                var continue_filling = Overflows?.Invoke() ?? true;
                if (!continue_filling) return 0;
            }
            var transferred = Math.Min(amount, CapacityLeft);
            var overflowamount = amount - transferred;
            Content += transferred;

            if (overflowamount > 0)
                Overflown?.Invoke(overflowamount);
            if (CapacityLeft == 0)
                Full?.Invoke();
            return transferred;
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
