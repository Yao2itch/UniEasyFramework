using System;
using UnityEngine;

namespace EasyFramework
{
    public class InputEvent
    {
        public Action<InputHelper.KeyInputType,KeyCode> InputAct;
        public KeyCode InputCode;
    }
}