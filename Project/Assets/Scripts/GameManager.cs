using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private FrameworkBehaviour _easyFramework;
	
	void Start ()
	{
		//string strType = "Assembly-CSharp" + "." + GetType().FullName;
		string strType = "LoginController";
		
		Type type = Type.GetType(strType);
		if ( type == null )
		{
			Debug.LogError( " - - - " + strType );
		}
		
		_easyFramework = gameObject.GetComponent<FrameworkBehaviour>();
		if ( _easyFramework == null )
		{
			_easyFramework = gameObject.AddComponent<FrameworkBehaviour>();
		}
		
		if ( _easyFramework != null )
		{
			_easyFramework.Initialize();
		}
	}
}
