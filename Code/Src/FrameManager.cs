using EasyFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class FrameManager
    {
        private static FrameManager _instance;
        public static FrameManager Instance
        {
            get
            {
                if( _instance == null )
                {
                    _instance = new FrameManager();
                }
                return _instance;
            }
        }

        private List<IModule> _moduleSet;

        public List<IModule> ModuleSet
        {
            get
            {
                return _moduleSet;
            }
        }

        private InputSystem _inputSysm;
        public InputSystem InputSysm
        {
            get { return _inputSysm; }
            set { _inputSysm = value; }
        }

        public FrameManager()
        {
            _moduleSet = new List<IModule>();
        }

        public void Initialize( GameObject rootGObj )
        {
            if ( rootGObj != null )
            {
                _inputSysm = rootGObj.GetComponent<InputSystem>();
                if ( _inputSysm == null )
                {
                    _inputSysm = rootGObj.AddComponent<InputSystem>();
                }   
            }
            
            if( _moduleSet != null )
            {
                for( int i = 0; i < _moduleSet.Count; ++i )
                {
                    _moduleSet[i].InputSysm = _inputSysm;
                    _moduleSet[i].Initialize();
                }
            }
        }

        public void ParseModuleConfig( string fullPath )
        {
            if( string.IsNullOrEmpty( fullPath ) )
            {
                Debug.LogWarning(" ## Uni Output ## cls:FrameManager func:ParseModuleConfig info: config not exist ");

                return;
            }

            byte[] data = File.ReadAllBytes( fullPath );

            if ( data != null )
            {
                string strData = Encoding.UTF8.GetString(data);

                if ( !string.IsNullOrEmpty( strData ) )
                {
                    JObject jObj = JObject.Parse(strData);
                    JToken jToken = null;

                    if ( jObj.TryGetValue( "Modules", out jToken ) )
                    {
                        JArray jArray = jToken.ToObject<JArray>();

                        if ( jArray != null )
                        {
                            for( int i = 0; i < jArray.Count; ++i )
                            {
                                JObject obj = jArray[i].ToObject<JObject>();
                                if( obj != null )
                                {
                                    JToken token = null;

                                    string moduleAssembly = string.Empty;
                                    string moduleType = string.Empty;

                                    if ( obj.TryGetValue( "Assembly", out token ) )
                                    {
                                        moduleAssembly = token.ToString();
                                    }

                                    if ( obj.TryGetValue( "Type", out token ) )
                                    {
                                        moduleType = token.ToString();
                                    }

                                    if ( obj.TryGetValue( "Mode", out token ) )
                                    {
                                        string strMode = token.ToString();
                                        if( !string.IsNullOrEmpty( strMode ) )
                                        {
                                            if ( strMode.Equals("Common") )
                                            {
                                                CommModule commModule = Activator.CreateInstance( Type.GetType( moduleAssembly + moduleType ) ) as CommModule;
                                                if( commModule != null )
                                                {
                                                    commModule.Parse(obj);
                                                }

                                                if( !_moduleSet.Contains( commModule ) )
                                                {
                                                    _moduleSet.Add( commModule );
                                                }
                                            }
                                            else if( strMode.Equals( "Mono" ) )
                                            {
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddModule( IModule module )
        {
            if ( module != null
               &&  _moduleSet != null
               && !_moduleSet.Contains( module ) )
            {
                _moduleSet.Add( module );
            }
        }

        public T GetModule<T>()
        {
            T m = default(T);

            if( _moduleSet != null )
            {
                m = (T)_moduleSet.Find( module =>
                        module.GetType() == typeof(T)
                    );
            }

            return m;
        }
        
        public void Release()
        {
            if( _moduleSet != null )
            {
                for( int i = 0;i < _moduleSet.Count; ++i )
                {
                    _moduleSet[i].Release();
                }

                _moduleSet.Clear();
            }
        }
    }
}
