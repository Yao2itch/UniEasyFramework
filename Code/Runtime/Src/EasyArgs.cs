using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyFramework
{
    public class EasyArgs : EventArgs
    {
        public string EvtName;
        public int EvtState;
        public object Data;
    }
}
