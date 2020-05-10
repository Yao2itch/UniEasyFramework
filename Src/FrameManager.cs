using System;
using System.Collections.Generic;
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
