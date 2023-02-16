using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GaveOverPanel : BasePanel
{
    private Button closeButton;
	private Button confirmButton;
	private Image leftPlayerImage;
	private Image rightPlayerImage;
	private Text gameOverLeftScore;
	private Text gameOverRightScore;
	private ChessBoardModel chessBoardModel;
	public  void Start()
	{
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		confirmButton = transform.Find("ConfirmButton").GetComponent<Button>();
		leftPlayerImage = transform.Find("LeftPlayer").GetComponent<Image>();
		rightPlayerImage = transform.Find("RightPlayer").GetComponent<Image>();
		gameOverLeftScore= transform.Find("LeftText").GetComponent<Text>();
		gameOverRightScore = transform.Find("RightText").GetComponent<Text>();
		chessBoardModel = ChessBoardController.Instance.chessBoardModel;
		closeButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PopPanel();
		});
		confirmButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PopPanel();
			SceneManager.LoadScene(0);
		});
		gameOverLeftScore.text = "Score:" + ChessBoardController.Instance.firstScore;
		gameOverRightScore.text = "Score:" + ChessBoardController.Instance.secondScore;
		leftPlayerImage.sprite = chessBoardModel.piecesList[0].GetComponent<Image>().sprite;
		rightPlayerImage.sprite = chessBoardModel.piecesList[1].GetComponent<Image>().sprite;				
	}
	public override void OnEnter()
	{
		base.OnEnter();
		
	}
}
