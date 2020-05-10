using System;
using System.Collections.Generic;
using System.Net.Configuration;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

namespace EasyFramework
{
    public class InputSystem : MonoBehaviour
    {
        private List<IInputManager> _inputMgrSet = new List<IInputManager>();
        
        void Awake()
        {
            KeyboardInputMgr keyboardInputMgr = new KeyboardInputMgr();
            _inputMgrSet.Add( keyboardInputMgr );
        }

        public void RegisterKeyboardEvt(InputHelper.KeyInputType inputType, KeyCode keyCode, Action<InputHelper.KeyInputType, KeyCode> callback)
        {
            if ( _inputMgrSet != null )
            {
                _inputMgrSet[0].AddListener( inputType, keyCode, callback );
            }
        }

        void Update()
        {
            for ( int i = 0; i < _inputMgrSet.Count; ++i )
            {
                _inputMgrSet[i].DealInputEvent();
            }
        }

        private void OnDestroy()
        {
            if ( _inputMgrSet != null )
            {
                for( int i = 0; i < _inputMgrSet.Count; ++i )
                {
                    _inputMgrSet[i].Release();
                }
                
                _inputMgrSet.Clear();
            }
        }
    }
}