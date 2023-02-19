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
		if (GameController.Instance.gameMode==GameMode.NetWorking&& cardDetails.costNum <= ChessBoardController.Instance.crystalManager.CrystalNum&& ChessBoardController.Instance.IsPlayeChess)
		{
			grow.GetComponent<Image>().enabled = true;
		}
		else if (GameController.Instance.gameMode == GameMode.Man_Machine && cardDetails.costNum <= ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			grow.GetComponent<Image>().enabled = true;
		}
		else if(GameController.Instance.gameMode == GameMode.Stand_Alone)
		{
			if( ChessBoardController.Instance.GetPlayer() == Player.One && cardDetails.costNum <= ChessBoardController.Instance.crystalManager.CrystalNum1)
			{
				grow.GetComponent<Image>().enabled = true;
			}
			else if (ChessBoardController.Instance.GetPlayer() == Player.Two && cardDetails.costNum <= ChessBoardController.Instance.crystalManager.CrystalNum2)
			{
				grow.GetComponent<Image>().enabled = true;
			}
			else
			{
				grow.GetComponent<Image>().enabled = false;
			}
		}
		else
		{
			grow.GetComponent<Image>().enabled = false;
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
	protected bool CheckIsSufficientCost()
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone && cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			return false;
		}
		if (GameController.Instance.gameMode == GameMode.NetWorking && !ChessBoardController.Instance.IsPlayeChess)
		{
			return false;
		}
		if (GameController.Instance.gameMode == GameMode.Stand_Alone)
		{
			if (ChessBoardController.Instance.player == Player.One && cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum1)
			{
				return false;
			}
			else if (ChessBoardController.Instance.player == Player.Two && cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum2)
			{
				return false;
			}
		}
		return true;
	}
}
