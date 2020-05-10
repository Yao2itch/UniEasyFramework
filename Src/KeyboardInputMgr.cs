using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace EasyFramework
{
    public class KeyboardInputMgr : IInputManager
    {
        private Dictionary<InputHelper.KeyInputType,List<InputEvent>> _inputEvtDic = new Dictionary<InputHelper.KeyInputType,List<InputEvent>>();
        
        public void AddListener( InputHelper.KeyInputType inputType, KeyCode keyCode, Action<InputHelper.KeyInputType,KeyCode> callback )
        {
            if ( _inputEvtDic.ContainsKey( inputType ) )
            {
                List<InputEvent> evts = _inputEvtDic[inputType];
                if ( evts != null )
                {
                    InputEvent evt = evts.Find( e => e.InputCode == keyCode );
                    if ( evt != null )
                    {
                        evt.InputAct += callback;
                    }
                    else
                    {
                        evt = new InputEvent();
                        evt.InputCode = keyCode;
                        evt.InputAct += callback;
                    }
                }
            }
            else
            {
                InputEvent evt = new InputEvent();
                evt.InputCode = keyCode;
                evt.InputAct += callback;
                
                _inputEvtDic.Add( inputType, new List<InputEvent>{ evt } );
            }
        }

        public void RemoveKeyEvent( InputHelper.KeyInputType inputType, KeyCode keyCode, Action<InputHelper.KeyInputType,KeyCode> callback )
        {
            if ( _inputEvtDic != null 
                 && _inputEvtDic.ContainsKey( inputType ) )
            {
                List<InputEvent> evts = _inputEvtDic[inputType];
                if ( evts != null )
                {
                    InputEvent inputEvt = evts.Find(e => e.InputCode == keyCode);
                    if ( inputEvt != null )
                    {
                        if ( inputEvt.InputAct != null )
                        {
                            inputEvt.InputAct -= callback;
                        }
                    }
                }
            }
        }

        public void DealInputEvent()
        {
            if ( _inputEvtDic == null )
            {
                return;
            }

            foreach (var key in _inputEvtDic.Keys)
            {
                List<InputEvent> evts = _inputEvtDic[key];
                
                if ( key == InputHelper.KeyInputType.KEY_INPUT_DOWN )
                {
                    for ( int i = 0; i < evts.Count; i++ )
                    {
                        if ( Input.GetKeyDown( evts[i].InputCode ) )
                        {
                            if ( evts[i].InputAct != null )
                            {
                                evts[i].InputAct( key, evts[i].InputCode );
                            }
                            return;
                        }
                    }    
                }
                else if ( key == InputHelper.KeyInputType.KEY_INPUT_GET )
                {
                    for ( int i = 0; i < evts.Count; i++ )
                    {
                        if ( Input.GetKey( evts[i].InputCode ) )
                        {
                            if ( evts[i].InputAct != null )
                            {
                                evts[i].InputAct( key, evts[i].InputCode );
                            }
                            return;
                        }
                    }    
                }
                else if ( key == InputHelper.KeyInputType.KEY_INPUT_UP )
                {
                    for ( int i = 0; i < evts.Count; i++ )
                    {
                        if ( Input.GetKeyUp( evts[i].InputCode ) )
                        {
                            if ( evts[i].InputAct != null )
                            {
                                evts[i].InputAct( key, evts[i].InputCode );
                            }
                            return;
                        }
                    }    
                }
            }
        }

        public void Release()
        {
            if ( _inputEvtDic != null )
            {
                _inputEvtDic.Clear();
            }
        }
    }
}