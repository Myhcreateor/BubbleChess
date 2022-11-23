using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
	{
		get { return instance; }
	}
	protected virtual void Awake()
	{
		if (instance != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = (T)this;
		}
	}
	public static bool IsInitialzed
	{
		get { return instance != null; }
	}
	public virtual void OnDestory()
	{
		if (instance == null)
		{
			instance = null;
		}
	}
}