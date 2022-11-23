using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : Singleton<UIManager>
{
    public Button newStartButton;
    public Button exitGameButton;
    public GameObject selectPieceColorPlane;
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

	public void LoadPath(string color)
	{
		loadPath = "Prefabs/Pieces/" + color+  "Piece";
	}
	public void ChangeModelPlayer1()
	{
		chessBoardModel.piecesList[0] = Resources.Load<GameObject>(loadPath);
		//动画过渡
		selectPieceColorPlane.transform.DOMoveX(-500, 2.0f);
	}
	public void ChangeModelPlayer2()
	{
		chessBoardModel.piecesList[1] = Resources.Load<GameObject>(loadPath);
		if (chessBoardModel.piecesList[1].name.Equals(chessBoardModel.piecesList[0].name))
		{
			Debug.Log("请重新选择");
		}
		else
		{
			selectPieceColorPlane.SetActive(false);
		}
	}
	public void ShowSelectPieceColorPlane()
	{
		selectPieceColorPlane.SetActive(true);
		CloseChessBoardButton();
	}
	public void ShowGameOverPanel(int index1,int index2)
	{
		gameOverPanel.SetActive(true);
		leftPlayerImage.sprite = chessBoardModel.piecesList[0].GetComponent<Image>().sprite;
		rightPlayerImage.sprite = chessBoardModel.piecesList[1].GetComponent<Image>().sprite;
		gameOverLeftScore.text = "Score:" + index1;
		gameOverRightScore.text = "Score:" + index2;
		CloseChessBoardButton();
	}
	public void CloseSelectPieceColorPlane()
	{
		selectPieceColorPlane.SetActive(false);
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
