using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFramework
{
    public class MonoModule<T> : MonoSingletonBase<T> , IModule where T : MonoBehaviour
    {
        private List<EasyEvent> _evts = new List<EasyEvent>();
        public List<EasyEvent> Evts
        {
            get {
                return _evts;
            }

            set{
                _evts = value;
            }
        }

        private InputSystem _inputSysm;

        public InputSystem InputSysm
        {
            get { return _inputSysm; }
            set { _inputSysm = value; }
        }

        public FeedbackDelegate FeedBackCallback
        {
            get;
            set;
        }

        public virtual void CreateEvt(string evtName)
        {
            if ( _evts != null
                && _evts.Find(e => e.Name == evtName) == null)
            {
                EasyEvent evt = new EasyEvent();
                evt.Name      = evtName;
                evt.Source    = this;

                Debug.Log(" ## Uni Output ## cls:MonoModule func:CreateEvt info: Create Event !! " + evtName);

                _evts.Add(evt);
            }
            else
            {
                Debug.Log(" ## Uni Output ## cls:MonoModule func:CreateEvt info: Create Event Failed " + evtName);
            }
        }

        public virtual void Initialize()
        {
            Debug.Log(" ## Uni Output ## cls:MonoModule func:Initialize info: Module Init !! ");
        }

        public virtual void Release()
        {   
        }

        public virtual void OnEvtListen(object target, EasyArgs args)
        {
            Debug.Log("## Uni Output ## cls:MonoModule func:OnEvtListen info: listen ");
        }

        public virtual void RegisterKeyboardEvent(InputHelper.KeyInputType inputType, KeyCode keyCode)
        {
            if ( _inputSysm != null )
            {
                _inputSysm.RegisterKeyboardEvt(inputType,keyCode,OnKeyboardEventListen);
            }
            else
            {
                Debug.LogWarning(" ## Uni Output Warning ## inputSysm not found !! ");
            }
        }

        public virtual void OnKeyboardEventListen(InputHelper.KeyInputType inputType, KeyCode keyCode)
        {
            
        }

        public virtual void RegisterEvt( IModule source, IModule target, string evtName,Action<IModule,EasyArgs> callback = null )
        {
            if ( source != null
                && source.Evts != null)
            {
                Debug.Log("## Uni Output ## cls:MonoModule func:RegisterEvt info: Register evt " + source.GetType());

                EasyEvent evt = source.Evts.Find( e => e.Name == evtName );
                
                if ( evt != null )
                {
                    evt.Name = evtName;
                    evt.AddTarget( target );

                    evt.EvtHandler  += OnEvtListen;

                    if( callback != null )
                        evt.EvtCallback = callback;

                    if ( evt.EvtHandler == null )
                        Debug.Log("## Uni Output ## cls:MonoModule func:RegisterEvt info: Event Invoke !! EvtHandler Null");

                }
                else
                {
                    Debug.LogError( "## Uni Output ## cls:MonoModule func:RegisterEvt info:Register Evt Failed " + evtName );
                }
            }
            else
            {
                Debug.LogError("## Uni Output ## cls:MonoModule func:RegisterEvt info: Register Evt Failed ");
            }
        }

        public virtual void PublishEvt( string evtName, EasyArgs arg, Action<IModule, EasyArgs> callback = null)
        {
            if ( _evts != null)
            {
                EasyEvent evt = _evts.Find( e => e.Name == evtName );
                
                if ( evt != null
                    && evt.EvtHandler != null )
                {
                    Debug.Log("## Uni Output ## cls:MonoModule func:PublishEvt info: Event Invoke !! " + _evts.Count);
                    Debug.Log("## Uni Output ## cls:MonoModule func:PublishEvt info: Event Invoke !! " + evtName);
                    
                    if( evt.EvtCallback != null )
                    {
                        evt.EvtCallback( this, arg );
                    }
                    
                    evt.EvtHandler.Invoke( this, arg );

                    if ( callback != null )
                    {
                        evt.RegisterFeedback( (module, args) => {
                                if( callback != null )
                                    callback(module, args);
                            }
                        );
                    }
                }
                else
                {
                    Debug.LogError("## Uni Output ## cls:MonoModule func:PublishEvt info:Publish Evt Failed " + evtName);
                }
            }
        }
    }
}
