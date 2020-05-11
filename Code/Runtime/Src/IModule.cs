using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EasyFramework
{
    public delegate void FeedbackDelegate( IModule sender, EasyArgs arg );

    public interface IModule
    {
        ModuleData Data { get; set; }
        InputSystem InputSysm { get; set; }
        FeedbackDelegate FeedBackCallback { get; set; }
        List<EasyEvent> Evts { get; set; }
        void Parse(JObject jObj);
        void Initialize();
        void Release();
        void CreateEvt( string evtName );
        void PublishEvt(string evtName, EasyArgs arg, Action<IModule,EasyArgs> callback = null);
        void RegisterEvt(IModule source,IModule target,string evtName,Action<IModule,EasyArgs> callback = null);
        void OnEvtListen(object target, EasyArgs args);
        void RegisterKeyboardEvent(InputHelper.KeyInputType inputType, KeyCode keyCode);
        void OnKeyboardEventListen(InputHelper.KeyInputType inputType, KeyCode keyCode);
    }
}
