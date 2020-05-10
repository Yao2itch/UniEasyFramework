using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFramework
{
    public class FrameworkBehaviour : MonoBehaviour
    {
        public void CreateModule(IModule module)
        {
        }

        public void Initialize()
        {
            FrameManager.Instance.Initialize(gameObject);
        }

        void OnApplicationQuit()
        {
            FrameManager.Instance.Release();
        }

        public void AddModule(IModule m)
        {
            Debug.Log("## Uni Output ## cls:FrameManager func:GetModule info: get module Type " + m.GetType());

            FrameManager.Instance.AddModule(m);
        }

        public T GetModule<T>()
        {
            T m = default(T);

            m = FrameManager.Instance.GetModule<T>();

            return m;
        }

        public void RegisterEvent( IModule source, IModule target, string evtName,Action<IModule,EasyArgs> callback = null )
        {
            if ( source == null || target == null )
            {
                Debug.LogError("## Uni Output ## cls:FrameworkBehaviour func:RegisterModule info:Register Evt Failed ");

                return;
            }
            
            target.RegisterEvt( source, target, evtName, callback);
        }

        public void RegisterSingleEvent( IModule source, string evtName, Action<IModule, EasyArgs> callback = null)
        {
            if ( source == null )
            {
                Debug.LogError("## Uni Output ## cls:FrameworkBehaviour func:RegisterSingleModule info:Register Single Evt Failed ");

                return;
            }

            source.RegisterEvt( source, null, evtName, callback );
        }
    }
}
