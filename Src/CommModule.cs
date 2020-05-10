using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFramework
{
    public class CommModule : IModule
    {
        private List<EasyEvent> evts = new List<EasyEvent>();
        public List<EasyEvent> Evts
        {
            get {
                return evts;
            }
            set{
                evts = value;
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

        public virtual void Initialize()
        {
            Debug.Log(" ## Uni Output ## cls:CommModule func:Initialize info: Module Init !! ");
        }
        
        public virtual void Release()
        {
            if ( evts != null )
            {
                evts.Clear();
            }
        }

        public virtual void CreateEvt( string name )
        {
            if ( evts != null 
                && evts.Find( e => e.Name == name ) == null )
            {
                EasyEvent evt = new EasyEvent();
                evt.Name = name;
                evt.Source = this;

                evts.Add( evt );
            }
            else
            {

            }
        }

        public virtual void RegisterEvt( IModule source,IModule target, string evtName,Action<IModule,EasyArgs> callback = null)
        {
            if( source != null 
               && source.Evts != null )
            {
                Debug.Log("## Uni Output ## cls:CommModule func:RegisterEvt info: Register evt " + source.GetType());

                EasyEvent evt = source.Evts.Find( e => e.Name == evtName );

                if( evt != null )
                {
                    evt.Name = evtName;
                    evt.AddTarget(target);
                    evt.EvtHandler += OnEvtListen;
                    if( callback != null )
                        evt.EvtCallback = callback;

                    if ( evt.EvtHandler == null )
                        Debug.Log("## Uni Output ## cls:CommModule func:PublishEvt info: Event Invoke !! EvtHandler Null");

                }
                else
                {
                    Debug.LogError("## Uni Output ## cls:CommModule func:RegisterEvt info:Register Evt Failed " + evtName);
                }
            }
            else
            {
                Debug.LogError("## Uni Output ## cls:CommModule func:RegisterEvt info: Register Evt Failed ");
            }
        }

        public virtual void OnEvtListen( object target, EasyArgs args )
        {
            Debug.Log("## Uni Output ## cls:CommModule func:OnEvtListen info: listen ");
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

        public virtual void PublishEvt( string evtName, EasyArgs arg, Action<IModule,EasyArgs> callback = null )
        {
            Debug.Log( "## Uni Output ## cls:CommModule func:PublishEvt info: Event Invoke !! ");
            
            if ( evts != null )
            {
                EasyEvent evt = evts.Find( e => e.Name == evtName );
               
                if ( evt != null
                    && evt.EvtHandler != null )
                {
                    Debug.Log( "## Uni Output ## cls:CommModule func:PublishEvt info: Event Invoke !! ");

                    if (evt.EvtCallback != null)
                    {
                        evt.EvtCallback(this, arg);
                    }

                    evt.EvtHandler.Invoke( this, arg );

                    if ( callback != null )
                    {
                        evt.RegisterFeedback((module, args) => {
                                if (callback != null)
                                    callback(module, args);
                            }
                        );
                    }
                }
            }
        }
        
    }
}
