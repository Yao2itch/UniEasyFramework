using System;
using UnityEngine;

namespace EasyFramework
{
    public interface IInputManager
    {
        void AddListener(InputHelper.KeyInputType inputType, KeyCode keyCode,
            Action<InputHelper.KeyInputType, KeyCode> callback);
        void DealInputEvent();
        void Release();
    }
}