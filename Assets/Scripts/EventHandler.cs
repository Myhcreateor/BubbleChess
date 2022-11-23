using System;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action<GameObject> GenerateChessEvent;
    public static void CallGenerateChessEvent(GameObject go)
	{
		GenerateChessEvent?.Invoke(go);
	}
	public static event Action GameOverEvent;
	public static void CallGameOverEvent()
	{
		GameOverEvent?.Invoke();
	}
}
