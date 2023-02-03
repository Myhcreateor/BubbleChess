using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ChessBoardController : Singleton<ChessBoardController>
{
	public ChessBoardModel chessBoardModel;
	public int firstScore;
	public int secondScore;
	public List<GameObject> piecesList;
	public GameMode gameMode;
	public int twoSideRoundNum = 0;
	private Player player;
	//计算分数的数组
	public int[][] chessPieceArrays = new int[8][];
	private int[][] transposeArrays = new int[8][];
	private int[][] reversalArrays = new int[8][];
	private int[][] slantArrays = new int[15][];
	private int[][] transposeReversalArray = new int[15][];
	private void Awake()
	{
		base.Awake();
		InitChessPieceArrays();
		piecesList = chessBoardModel.piecesList;
	}

	private void Start()
	{
		gameMode = GameController.Instance.gameMode;
		if (gameMode == GameMode.Stand_Alone)
		{
			player = Player.One;
		}
	}
	public Transform GetChessBoardGridTransform()
	{
		return transform.Find("ChessBoardGridPraret");
	}
	public Transform GetChessBoardPiecesTransform()
	{
		return transform.Find("ChessBoardPieces");
	}
	public void HandOffPlayer()
	{
		if (player == Player.One)
		{
			player = Player.Two;
		}
		else
		{
			player = Player.One;
		}
	}
	public Player GetPlayer()
	{
		return player;
	}
	//初始化棋子数组
	private void InitChessPieceArrays()
	{
		for (int col = 0; col < 8; col++)
		{
			chessPieceArrays[col] = new int[8];
			transposeArrays[col] = new int[8];
		}
		for (int col = 0; col < 8; col++)
		{
			for (int row = 0; row < 8; row++)
			{
				chessPieceArrays[col][row] = 0;
			}
		}
	}
	public int RamainingRound(int pieceNum)
	{
		return chessBoardModel.roundNum - pieceNum / 2;
	}
	public void NewStartGame()
	{
		firstScore = 0;
		secondScore = 0;
		InitChessPieceArrays();
		EventHandler.CallUpdateChessBoardEvent();
		EventHandler.CallNewStartGameEvent();
		GameController.Instance.ChooseMan_MachinePlayer(GameController.Instance.currentCharacterIndex);
	}
	public void NextMan_MachineGame()
	{
		firstScore = 0;
		secondScore = 0;
		InitChessPieceArrays();
		EventHandler.CallUpdateChessBoardEvent();
		EventHandler.CallNewStartGameEvent();
		GameController.Instance.currentCharacterIndex++;
		GameController.Instance. ChooseMan_MachinePlayer(GameController.Instance.currentCharacterIndex);
	}
	//判断回合数是否结束
	public bool isRoundOver(int pieceNum)
	{
		EventHandler.CallUpdateDebugEvent(chessBoardModel.roundNum-pieceNum/2);
		if (chessBoardModel.roundNum * 2 <= pieceNum)
		{
			//游戏结束
			EventHandler.CallGameOverEvent();
			if (gameMode == GameMode.Man_Machine)
			{
				Man_MachineUIManager.Instance.ShowGameOverPanel(firstScore, secondScore);
			}
			else 
			{
				UIOldManager.Instance.ShowGameOverPanel(firstScore, secondScore);
			}
			return true;
		}
		return false;
	}
	//更新棋盘，如果是黑色棋子值为1，白色棋子值为2
	public void UpdateChessPieceArrays(int col, int row, int pieceType)
	{
		chessPieceArrays[col][row] = pieceType;
		//更新棋盘UI
	}
	/// <summary>
	/// 计算分数代码
	/// </summary>
	public void CalculateScore()
	{
		firstScore = 0;
		secondScore = 0;
		for (int index = 0; index < 8; index++)
		{
			CalculateLineScore(chessPieceArrays[index]);
		}
		transposeArrays = TransposeArray(chessPieceArrays);
		for (int index = 0; index < 8; index++)
		{
			CalculateLineScore(transposeArrays[index]);
		}
		slantArrays = SlantArray(chessPieceArrays);
		for (int index = 0; index < 15; index++)
		{
			CalculateLineScore(slantArrays[index]);
		}
		reversalArrays = ReversalArray(chessPieceArrays);
		transposeReversalArray = SlantArray(reversalArrays);
		for (int index = 0; index < 15; index++)
		{
			CalculateLineScore(transposeReversalArray[index]);
		}
	}
	public int CalculateScoreGap(int type)
	{
		firstScore = 0;
		secondScore = 0;
		for (int index = 0; index < 8; index++)
		{
			CalculateLineScore(chessPieceArrays[index]);
		}
		transposeArrays = TransposeArray(chessPieceArrays);
		for (int index = 0; index < 8; index++)
		{
			CalculateLineScore(transposeArrays[index]);
		}
		slantArrays = SlantArray(chessPieceArrays);
		for (int index = 0; index < 15; index++)
		{
			CalculateLineScore(slantArrays[index]);
		}
		reversalArrays = ReversalArray(chessPieceArrays);
		transposeReversalArray = SlantArray(reversalArrays);
		for (int index = 0; index < 15; index++)
		{
			CalculateLineScore(transposeReversalArray[index]);
		}

		return (type == 1) ? firstScore - secondScore : secondScore - firstScore;
	}
	//反转数组
	public int[][] ReversalArray(int[][] arr)
	{
		int[][] arrNew = new int[8][];
		for (int col = 0; col < 8; col++)
		{
			arrNew[col] = new int[8];
		}
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				arrNew[i][j] = arr[i][7-j];
			}
		}
		return arrNew;
	}
	//转置数组
	public int[][] TransposeArray(int[][] arr)
	{
		int[][] arrNew = new int[8][];
		for (int col = 0; col < 8; col++)
		{
			arrNew[col] = new int[8];
		}
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				arrNew[j][i] = arr[i][j];
			}
		}
		return arrNew;
	}
	//斜置数组
	public int[][] SlantArray(int[][] arr)
	{
		int[][] arrNew = new int[15][];
		for (int col = 0; col < 15; col++)
		{
			arrNew[col] = new int[15];
			for (int row = 0; row < 8; row++)
			{
				arrNew[col][row] = 0;
			}
		}
		for (int col = 0; col < 15; col++)
		{
			int k = 0;
			arrNew[col] = new int[15];
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (i + j == col)
					{
						arrNew[col][k++] = arr[i][j];
					}
				}
			}
		}
		return arrNew;
	}
	public void CalculateLineScore(int[] line)
	{
		string s = "";
		for (int i = 0; i < line.Length; i++)
		{
			s += line[i].ToString();
		}
		s += '0';
		firstScore += CheckOneLine(s)[0];
		secondScore += CheckOneLine(s)[1];
		//Debug.Log(s);
		//UI显示分数
		EventHandler.CallShowScoreEvent(firstScore,secondScore);
	}

	public int[] CheckOneLine(string line)
	{
		int writeScore = 0;
		int blackScore = 0;
		int currentLinkNum = 1;
		int currentType = 0;
		foreach (var i in line)
		{
			if (i == '1')
			{
				if (currentType != 1)
				{
					if (currentType == 2)
					{
						writeScore += chessBoardModel.GetScore(currentLinkNum);
					}
					currentLinkNum = 1;
					currentType = 1;
				}
				else
				{
					currentLinkNum++;
				}
			}
			else if (i == '2')
			{
				if (currentType != 2)
				{
					if (currentType == 1)
					{
						blackScore += chessBoardModel.GetScore(currentLinkNum);
					}
					currentLinkNum = 1;
					currentType = 2;
				}
				else
				{
					currentLinkNum++;
				}
			}
			else if (i == '0')
			{
				if (currentType == 1)
				{
					blackScore += chessBoardModel.GetScore(currentLinkNum);
				}
				else if (currentType == 2)
				{
					writeScore += chessBoardModel.GetScore(currentLinkNum);
				}
				currentLinkNum = 1;
				currentType = 0;
			}
		}
		return new int[2] { blackScore, writeScore };
	}

}
