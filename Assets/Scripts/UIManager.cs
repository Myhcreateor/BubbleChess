using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Button newStartButton;
    public Button exitGameButton;
    public GameObject selectPieceColorPlane;
    public GameObject gameOverPanel;
	public Text gameOverLeftScore;
	public Text gameOverRightScore;
	private void Awake()
	{
		base.Awake();
	}
	public void ShowSelectPieceColorPlane()
	{
		selectPieceColorPlane.SetActive(true);
		CloseChessBoardButton();
	}
	public void ShowGameOverPanel(int index1,int index2)
	{
		gameOverPanel.SetActive(true);
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
