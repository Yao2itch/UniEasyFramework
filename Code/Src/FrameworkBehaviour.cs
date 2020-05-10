using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFramework
{
    public class FrameworkBehaviour : MonoBehaviour
    {
        private string _configPath;
        public string ConfigPath
        {
            get{ return _configPath; }
            set{ _configPath = value; }
        }

        private string _configName;
        public string ConfigName
        {
            get{ return _configName; }
            set{ _configName = value; }
        }

        public void CreateModule(IModule module)
        {
        }

        public void Initialize()
        {
            if( string.IsNullOrEmpty( _configPath ) )
            {
                Debug.Log(" ## Uni Output ## cls:FrameworkBehaviour func:Initialize info: not set conf path, use default path " + _configPath );
                
                _configPath = Application.streamingAssetsPath + "/easyframework/";
            }

            if ( string.IsNullOrEmpty( _configName ) )
            {
                Debug.Log(" ## Uni Output ## cls:FrameworkBehaviour func:Initialize info: not set conf file, use default file name " + _configName);

                _configName = "moduleConfig.json";
            }

            string fullPath = _configPath + _configName;

            if ( !File.Exists( fullPath ) )
            {
                Debug.LogWarning(" ## Uni Output ## cls:FrameworkBehaviour func:Initialize info: config not exist " + fullPath );
            }

            FrameManager.Instance.ParseModuleConfig( fullPath );

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
