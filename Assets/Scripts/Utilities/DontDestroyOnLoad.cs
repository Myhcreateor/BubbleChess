using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static bool isHave = false;
	private void Awake()
	{
		if (!isHave)
		{
			DontDestroyOnLoad(this);
			isHave = true;
		}
	}
}
