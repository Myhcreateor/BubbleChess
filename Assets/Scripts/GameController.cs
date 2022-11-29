using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameController : Singleton<GameController>
{
	public int roundNum=15;
	public GameMode gameMode;
	private void Awake()
	{
		base.Awake();
	}
	
}
