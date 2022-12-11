using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Man_MachineUIManager : Singleton<Man_MachineUIManager>
{
	public Button newStartButton;
	public Button exitGameButton;
	public GameObject gameOverPanel;
	private Text gameOverTitleText;
	public Text gameOverLeftScore;
	public Text gameOverRightScore;
	private string loadPath;
	private Button nextGameButton;
	public ChessBoardModel chessBoardModel;
	public Image leftPlayerImage;
	public Image rightPlayerImage;
	private void Awake()
	{
		base.Awake();
		gameOverTitleText = transform.Find("GameOverPanel/SelectPieceColorTitle/TitleText").gameObject.GetComponent<Text>();
		nextGameButton = transform.Find("GameOverPanel/NextGameButton").gameObject.GetComponent<Button>();
	}
	private void Start()
	{
		nextGameButton.onClick.AddListener(() =>
		{
			ChessBoardController.Instance.NewStartGame();
			CloseGameOverPanel();
		});
	}

	public void LoadPath(string color)
	{
		loadPath = "Prefabs/Pieces/" + color + "Piece";
	}
	public void ChangeModelPlayer1()
	{
		chessBoardModel.piecesList[0] = Resources.Load<GameObject>(loadPath);
	}
	public void ChangeModelPlayer2()
	{
		chessBoardModel.piecesList[1] = Resources.Load<GameObject>(loadPath);
		if (chessBoardModel.piecesList[1].name.Equals(chessBoardModel.piecesList[0].name))
		{
			Debug.Log("请重新选择");
		}
	}
	public void ShowGameOverPanel(int index1, int index2)
	{
		gameOverPanel.SetActive(true);
		leftPlayerImage.sprite = chessBoardModel.piecesList[0].GetComponent<Image>().sprite;
		rightPlayerImage.sprite = chessBoardModel.piecesList[1].GetComponent<Image>().sprite;
		if (index1 > index2)
		{
			gameOverTitleText.text = "战斗胜利";
			nextGameButton.onClick.RemoveAllListeners();
			nextGameButton.onClick.AddListener(() =>
			{
				ChessBoardController.Instance.NextMan_MachineGame();
				CloseGameOverPanel();
			});

		}
		else if (index1 == index2)
		{
			gameOverTitleText.text = "平分秋色";
			nextGameButton.onClick.RemoveAllListeners();
			nextGameButton.onClick.AddListener(() =>
			{
				ChessBoardController.Instance.NewStartGame();
				CloseGameOverPanel();
			});
		}
		else
		{
			gameOverTitleText.text = "对战失败";
			nextGameButton.onClick.RemoveAllListeners();
			nextGameButton.onClick.AddListener(() =>
			{
				ChessBoardController.Instance.NewStartGame();
				CloseGameOverPanel();
			});
		}

		gameOverLeftScore.text = "Score:" + index1;
		gameOverRightScore.text = "Score:" + index2;
		CloseChessBoardButton();
	}
	public void CloseSelectPieceColorPlane()
	{
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
