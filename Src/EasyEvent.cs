using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFramework
{
    public class EasyEvent
    {
        public string  Name;
       
        public IModule Source;
        public List<IModule> Targets;
        public EventHandler<EasyArgs> EvtHandler;
        public Action<IModule,EasyArgs> EvtCallback;

        public EasyEvent()
        {
            Targets = new List<IModule>();
        }

        public void AddTarget( IModule module )
        {
            if( module == null)
            {
                return;
            }

            if( Targets != null 
               && !Targets.Contains( module ) )
            {
                Targets.Add( module );
            }
        }

        public void RegisterFeedback( FeedbackDelegate callback )
        {
            if( Targets != null )
            {
                Debug.Log("## Uni Output ## cls: func:RegisterFeedback info: count " + Targets.Count);
                Debug.Log("## Uni Output ## cls: func:RegisterFeedback info: count " + Targets.Count);

                for ( int i = 0; i < Targets.Count; ++i )
                {
                    if ( callback != null )
                        Targets[i].FeedBackCallback += callback;
                }
            }
        }
    }
}
