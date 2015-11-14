using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MemoryLib;

namespace Aion_Game
{
    public class Dialog
    {
        public Dictionary<string, Dialog> Childs = new Dictionary<string, Dialog>();

        protected long _node;
        protected Dialog _parent;
        protected string _name;
        protected int _index;
        protected bool _open;
        protected bool _enable;
        protected double _relative_left;
        protected double _relative_top;
        protected double _absolute_left;
        protected double _absolute_top;
        protected double _width;
        protected double _height;
        protected double _padding_left;
        protected double _padding_top;

        public long Node
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;
            }
        }
        public Dialog Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
        public string Name
        {
            get
            {
                if (_name == string.Empty)
                    return _index.ToString();
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        public bool Open
        {
            get
            {
                return (_parent == null || _parent._open) && ((int)SplMemory.ReadByte(_node + Offset.Dialog.State) & 1) == 1;
            }
            set
            {
                if (_node != 0)
                {
                    int state = (int)SplMemory.ReadByte(_node + Offset.Dialog.State);
                    if (value == true)
                        state |= 1;
                    else
                        state &= -2;
                    SplMemory.WriteMemory(_node + Offset.Dialog.State, (byte)state);
                    _open = value;
                }
            }
        }
        public bool Enable
        {
            get
            {
                return (_parent == null || _parent._open) && ((int)SplMemory.ReadByte(_node + Offset.Dialog.State) & 2) == 2;
            }
            set
            {
                if (_node != 0)
                {
                    int state = (int)SplMemory.ReadByte(_node + Offset.Dialog.State);
                    if (value == true)
                        state |= 2;
                    else
                        state &= -3;
                    SplMemory.WriteMemory(_node + Offset.Dialog.State, (byte)state);
                    _enable = value;
                }
            }
        }
        public double RelativeLeft
        {
            get
            {
                return _relative_left;
            }
            set
            {
                _relative_left = value;
            }
        }
        public double RelativeTop
        {
            get
            {
                return _relative_top;
            }
            set
            {
                _relative_top = value;
            }
        }

        public double Left
        {
            get
            {
                return _absolute_left;
            }
            set
            {
                _absolute_left = value;
            }
        }

        public double Top
        {
            get
            {
                return _absolute_top;
            }
            set
            {
                _absolute_top = value;
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

        public Dialog(long node, int index = 0, Dialog parent = null)
        {
            DefaultValue();
            if (node == 0)
                return;
            _node = node;
            _name = GetName();
            _index = index;
            _parent = parent;
            byte state = SplMemory.ReadByte(_node + Offset.Dialog.State);
            _open = (_parent == null || _parent._open) && (state & 1) == 1;
            _enable = (_parent == null || _parent._enable) && (state & 2) == 2;
            _padding_left = SplMemory.ReadDouble(_node + Offset.Dialog.Padding.Left);
            _padding_top = SplMemory.ReadDouble(_node + Offset.Dialog.Padding.Top);
            _width = SplMemory.ReadDouble(_node + Offset.Dialog.Size.Width);
            _height = SplMemory.ReadDouble(_node + Offset.Dialog.Size.Height);
            _relative_left = SplMemory.ReadDouble(_node + Offset.Dialog.Position.Left);
            _relative_top = SplMemory.ReadDouble(_node + Offset.Dialog.Position.Top);
            if (_parent == null)
            {
                _absolute_left = _relative_left;
                _absolute_top = _relative_top;
            }
            else
            {
                _absolute_left = _relative_left + _parent._padding_left + _parent._absolute_left;
                _absolute_top = _relative_top + _parent._padding_top + _parent._absolute_top;
            }
        }

        private void DefaultValue()
        {
            _node = 0;
            _name = string.Empty;
            _open = false;
            _relative_left = 0;
            _relative_top = 0;
            _absolute_left = 0;
            _absolute_top = 0;
            _width = 0;
            _height = 0;
            _padding_left = 0;
            _padding_top = 0;
        }

        public string GetName()
        {
            try
            {
                string name;
                long size = SplMemory.ReadInt(_node + Offset.Dialog.BufferSize);
                if (size == 0 || size == 0xCDCDCDCD || _node == 0)
                    return string.Empty;
                if (size > 30)
                    name = SplMemory.ReadChar(SplMemory.ReadLong(_node + Offset.Dialog.Buffer), (int)size);
                else
                    name = SplMemory.ReadChar(_node + Offset.Dialog.Buffer, (int)size);
                return name;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void Click()
        {
            try
            {
                int x = (int)(this.Left + this.Width / 2);
                int y = (int)(this.Top + this.Height / 2);
                if (x <= 0 || y <= 0)
                    return;

                SplMemory.WriteMemory(Aion_Process.Game.Base + Offset.Game.Mouse_X, x);
                SplMemory.WriteMemory(Aion_Process.Game.Base + Offset.Game.Mouse_Y, y);
                Windows_And_Process.Send_Key.PostMessage(Aion_Process.Game.Whandle, 0x0201, (IntPtr)0x0001, IntPtr.Zero);
                Windows_And_Process.Send_Key.PostMessage(Aion_Process.Game.Whandle, 0x0202, (IntPtr)0x0001, IntPtr.Zero);
            }
            catch { }
        }

        public void Update()
        {
            byte state = SplMemory.ReadByte(_node + Offset.Dialog.State);
            _open = (_parent == null || _parent._open) && (state & 1) == 1;
            _enable = (_parent == null || _parent._enable) && (state & 2) == 2;
            _padding_left = SplMemory.ReadDouble(_node + Offset.Dialog.Padding.Left);
            _padding_top = SplMemory.ReadDouble(_node + Offset.Dialog.Padding.Top);
            _width = SplMemory.ReadDouble(_node + Offset.Dialog.Size.Width);
            _height = SplMemory.ReadDouble(_node + Offset.Dialog.Size.Height);
            _relative_left = SplMemory.ReadDouble(_node + Offset.Dialog.Position.Left);
            _relative_top = SplMemory.ReadDouble(_node + Offset.Dialog.Position.Top);
            if (_parent == null)
            {
                _absolute_left = _relative_left;
                _absolute_top = _relative_top;
            }
            else
            {
                _absolute_left = _relative_left + _padding_left + _parent._absolute_left;
                _absolute_top = _relative_top + _padding_top + _parent._absolute_top;
            }
        }

        public void UpdateChild()
        {
            if (_node == 0 || _node == 0xCDCDCDCD)
                return;

            int size = SplMemory.ReadInt(_node + Offset.Dialog.ChildSize);
            if (size == 0)
                return;
            long search = SplMemory.ReadLong(_node + Offset.Dialog.ChildMap);
            if (search == 0 || search == 0xCDCDCDCD)
                return;
            search = SplMemory.ReadLong(search);
            if (search == 0 || search == 0xCDCDCDCD)
                return;
            int index = 0;
            Childs = new Dictionary<string, Dialog>();
            while (index < size)
            {
                Dialog child = new Dialog(SplMemory.ReadLong(search + 0x8), index, this);
                Childs[child.Name] = child;
                index++;
                search = SplMemory.ReadLong(search);
            }
        }
    }
}
