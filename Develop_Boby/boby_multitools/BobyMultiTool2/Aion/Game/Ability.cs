using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aion_Process;
using MemoryLib;

namespace Aion_Game
{
    public class Ability
    {
        protected long _node;
        protected int _id;
        protected string _name;
        protected long _cooldown_time;
        protected long _cooldown_time_remaining;
        protected long _maintain;
        protected int _lvl;

        public long Node
        {
            get
            {
                return _node;
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public long CooldownTime
        {
            get
            {
                return _cooldown_time;
            }
        }

        public long CooldownTimeRemaining
        {
            get
            {
                return _cooldown_time_remaining;
            }
        }
        public bool Maintain
        {
            get
            {
                return _maintain != 0;
            }
        }
        public int Lvl
        {
            get
            {
                return _lvl;
            }
        }

        public Ability(long node)
        {
            DefaultValue();
            this._node = node;
            if (node == 0)
                return;
            Update();
        }
        private void DefaultValue()
        {
            _node = 0;
            _name = string.Empty;
            _id = 0;
            _cooldown_time = 0;
            _cooldown_time_remaining = 0;
            _maintain = 0;
            _lvl = 0;
        }
        public void Update()
        {
            _id = SplMemory.ReadInt(_node + Offset.Ability.Id);
            int buffer_size = SplMemory.ReadInt(_node + Offset.Ability.BufferSize);
            try
            {
                if (buffer_size == 0)
                    return;
                if (buffer_size > 7)
                    _name = SplMemory.ReadWchar(SplMemory.ReadLong(_node + Offset.Ability.Buffer), buffer_size);
                else
                    _name = SplMemory.ReadWchar(_node + Offset.Ability.Buffer, buffer_size);
                _cooldown_time = SplMemory.ReadLong(_node + Offset.Ability.CooldownTime);
                _cooldown_time_remaining = SplMemory.ReadLong(_node + Offset.Ability.CooldownTimeRemaining);
                _maintain = SplMemory.ReadLong(_node + Offset.Ability.Maintain);
                _lvl = SplMemory.ReadInt(_node + Offset.Ability.Lvl);
            }
            catch { }
        }
    }
}
