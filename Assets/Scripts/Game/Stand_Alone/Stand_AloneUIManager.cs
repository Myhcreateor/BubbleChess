using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Stand_AloneUIManager : Singleton<Stand_AloneUIManager>
{
	public Button newStartButton;
	public Button exitGameButton;
	public GameObject selectCardPlane;
	public GameObject gameOverPanel;
	public Text gameOverLeftScore;
	public Text gameOverRightScore;
	public string loadPath;
	public ChessBoardModel chessBoardModel;
	public Image leftPlayerImage;
	public Image rightPlayerImage;
	private void Awake()
	{
		base.Awake();
	}
	public void Start()
	{
		ShowSelectCardPlane();
	}
	public void ShowSelectCardPlane()
	{
		selectCardPlane.SetActive(true);
		CardTurnOver[] turnOvers= selectCardPlane.transform.Find("PlayerOneSelectCardPlane/Card_TurnOver").GetComponentsInChildren<CardTurnOver>();
		foreach (var i in turnOvers)
		{
			i.ToTurnOver();
		}	
		CloseChessBoardButton();
	}
	public void ShowGameOverPanel(int index1, int index2)
	{
		gameOverPanel.SetActive(true);
		leftPlayerImage.sprite = chessBoardModel.piecesList[0].GetComponent<Image>().sprite;
		rightPlayerImage.sprite = chessBoardModel.piecesList[1].GetComponent<Image>().sprite;
		gameOverLeftScore.text = "Score:" + index1;
		gameOverRightScore.text = "Score:" + index2;
		CloseChessBoardButton();
	}
	public void CloseSelectCardPlane()
	{
		selectCardPlane.SetActive(false);
		ShowChessBoardButton();
	}
	public void CloseGameOverPanel()
	{
		gameOverPanel.SetActive(false);
		ShowChessBoardButton();
	}
	public void CloseChessBoardButton()
	{
		newStartButton.gameObject.SetActive(false);
		exitGameButton.gameObject.SetActive(false);
	}
	public void ShowChessBoardButton()
	{
		newStartButton.gameObject.SetActive(true);
		exitGameButton.gameObject.SetActive(true);
	}
}
