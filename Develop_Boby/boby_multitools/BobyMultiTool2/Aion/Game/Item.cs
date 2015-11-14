using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MemoryLib;

namespace Aion_Game
{
    public class Item
    {
        protected long _node;
        protected int _id;
        protected int _amount;
        protected string _name;
        protected long _cooldown_time;
        protected long _cooldown_time_remaining;
        protected long _type;
        protected long _slot;

        public int Id
        {
            get { return _id; }
        }
        public int Amount
        {
            get { return _amount; }
        }
        public string Name
        {
            get { return _name; }
        }
        public long CooldownTime
        {
            get { return _cooldown_time; }
        }
        public long CooldownTimeRemaining
        {
            get { return _cooldown_time_remaining; }
        }

        public Item(long node)
        {
            DefaultValue();
            _node = node;
            if (node == 0)
                return;
            Update();
        }

        private void DefaultValue()
        {
            _node = 0;
            _name = string.Empty;
            _id = 0;
            _amount = 0;
            _cooldown_time = 0;
            _cooldown_time_remaining = 0;
            _type = 0;
            _slot = 0;
        }

        public void Update()
        {
            _id = SplMemory.ReadInt(_node + Offset.Cube.Id);
            int buffer_size = SplMemory.ReadInt(_node + Offset.Cube.BufferSize);
            try
            {
                if (buffer_size == 0)
                    return;
                if (buffer_size > 7)
                    _name = SplMemory.ReadWchar(SplMemory.ReadLong(_node + Offset.Cube.Buffer), buffer_size);
                else
                    _name = SplMemory.ReadWchar(_node + Offset.Cube.Buffer, buffer_size);
                _amount = SplMemory.ReadInt(_node + Offset.Cube.Amount);
                _cooldown_time = SplMemory.ReadLong(_node + Offset.Cube.CooldownTime);
                _cooldown_time_remaining = SplMemory.ReadLong(_node + Offset.Cube.CooldownTimeRemaining);
                _type = SplMemory.ReadLong(_node + Offset.Cube.Type);
                _slot = SplMemory.ReadLong(_node + Offset.Cube.Slot);
            }
            catch { }
        }
    }
}
