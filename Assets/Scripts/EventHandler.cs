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
	public static event Action<int,int> ShowScoreEvent;
	public static void CallShowScoreEvent(int index1,int index2)
	{
		ShowScoreEvent?.Invoke(index1,index2);
	}
	public static event Action<int> UpdateDebugEvent;
	public static void CallUpdateDebugEvent(int index)
	{
		UpdateDebugEvent?.Invoke(index);
	}
	public static event Action<string> UpdateDebugStringEvent;
	public static void CallUpdateDebugStringEvent(string s)
	{
		UpdateDebugStringEvent?.Invoke(s);
	}
	public static event Action NewStartGameEvent;
	public static void CallNewStartGameEvent()
	{
		NewStartGameEvent?.Invoke();
	}
	public static event Action UpdateChessBoardEvent;
	public static void CallUpdateChessBoardEvent()
	{
		UpdateChessBoardEvent?.Invoke();
	}
	public static event Action<string,string> GenerateParticleEffectEvent;
	public static void CallGenerateParticleEffectEvent(string s, string path)
	{
		GenerateParticleEffectEvent?.Invoke(s,path);
	}
}
