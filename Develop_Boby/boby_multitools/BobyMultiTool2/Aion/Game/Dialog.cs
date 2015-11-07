using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MemoryLib;

namespace Aion_Game
{
    class Dialog
    {
        protected long _node;
        protected string _name;
        protected bool _open;
        protected double _left;
        protected double _top;
        protected double _width;
        protected double _height;
        protected double _padding_left;
        protected double _padding_top;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public bool Open
        {
            get
            {
                return _open;
            }
            set
            {
                if (_node != 0)
                {
                    byte state = SplMemory.ReadByte(_node + Offset.Dialog.State);
                    if (state % 2 == 1 && value == true)
                        SplMemory.WriteMemory(_node + Offset.Dialog.State, state + 1);
                    else if (state % 2 == 0 && value == false)
                        SplMemory.WriteMemory(_node + Offset.Dialog.State, state - 1);
                    _open = value;
                }
            }
        }

        public double Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        public double Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public double PaddingLeft
        {
            get
            {
                return _padding_left;
            }
            set
            {
                _padding_left = value;
            }
        }

        public double PaddingTop
        {
            get
            {
                return _padding_top;
            }
            set
            {
                _padding_top = value;
            }
        }

        public Dialog(long  node)
        {
            DefaultValue();
            if (node == 0)
                return;
            _node = node;
            if (SplMemory.ReadInt(node + Offset.Dialog.BufferSize) > 7)
            {
                long tmpNode = SplMemory.ReadLong(node);
                if (tmpNode == 0 || tmpNode == 0xCDCDCDCD)
                {
                    DefaultValue();
                    return;
                }
                _name = SplMemory.ReadChar(tmpNode + Offset.Dialog.Buffer, 30);
            }
            else
            {
                _name = SplMemory.ReadChar(node + Offset.Dialog.Buffer, 7);
            }
        }

        private void DefaultValue()
        {
            _node = 0;
            _name = string.Empty;
            _open = false;
            _left = 0;
            _top = 0;
            _width = 0;
            _height = 0;
            _padding_left = 0;
            _padding_top = 0;
        }
    }
}
