using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : Card
{
	protected string clickTrans;
	private GameObject grow;
	protected virtual void Awake()
	{
		grow= transform.Find("Grow").gameObject;
	}
	public void Update()
	{
		RefreshSelfLuminous();
	}
	public void RefreshSelfLuminous()
	{
		if (grow == null)
		{
			grow = transform.Find("Grow").gameObject;
		}
		if (cardDetails.costNum <= ChessBoardController.Instance.crystalManager.CrystalNum&& ChessBoardController.Instance.IsPlayeChess)
		{
			grow.GetComponent<Image>().enabled = true;
			//grow.GetComponent<Image>().color = new Color(1,1,1,1);
		}
		else
		{
			grow.GetComponent<Image>().enabled = false;
			//grow.GetComponent<Image>().color = new Color(0, 0, 0, 0);
		}
	}
	public override bool ExecuteCommand()
	{
		return false;
	}

	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}

}
