using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardController : MonoBehaviour
{
	private Dictionary<int, int> scoreDictionary = new Dictionary<int, int>();
	private List<GameObject> piecesList;
	private int pieceType = 0;
	private int roundNum;
	private Transform ChessBoardPieces;
	public int blackScore;
	public int writeScore;
	public Text debugText;

	//计算分数的数组
	private int[][] chessPieceArrays = new int[8][];
	private int[][] transposeArrays = new int[8][];
	private int[][] reversalArrays = new int[8][];
	private int[][] slantArrays = new int[15][];
	private int[][] transposeReversalArray = new int[15][];
	private void Awake()
	{
		debugText = transform.parent.Find("DebugText").GetComponent<Text>();
		InitChessPieceArrays();
		InitScoreDictionary();
		ChessBoardPieces = transform.Find("ChessBoardPieces");
		roundNum = GameController.singleton.roundNum;
		piecesList = new List<GameObject>()
		{
			Resources.Load<GameObject>("Prefabs/Pieces/BlackPiece"),
			Resources.Load<GameObject>("Prefabs/Pieces/WritePiece")
		};
	}
	private void OnEnable()
	{
		EventHandler.GenerateChessEvent += OnGenerateChessEvent;
		EventHandler.GameOverEvent += OnGameOverEvent;


	}
	private void OnDisable()
	{
		EventHandler.GenerateChessEvent -= OnGenerateChessEvent;
		EventHandler.GameOverEvent -= OnGameOverEvent;
	}

	private void OnGameOverEvent()
	{
		Debug.Log("游戏结束");
		CalculateScore();
	}

	private void OnGenerateChessEvent(GameObject go)
	{
		Instantiate(piecesList[pieceType%2], go.transform.position, Quaternion.identity, ChessBoardPieces);
		UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (pieceType%2)+1);
		pieceType++;
		if (roundNum * 2 <= pieceType)
		{
			//游戏结束
			EventHandler.CallGameOverEvent();
		}
	}
	//初始化scoreDictionary
	private void InitScoreDictionary()
	{
		scoreDictionary.Add(0, 0);
		scoreDictionary.Add(1, 0);
		scoreDictionary.Add(2, 0);
		scoreDictionary.Add(3, 1);
		scoreDictionary.Add(4, 3);
		scoreDictionary.Add(5, 8);
		scoreDictionary.Add(6, 20);
		scoreDictionary.Add(7, 50);
		scoreDictionary.Add(8, 200);
	}
	//初始化棋子数组
	private void InitChessPieceArrays()
	{
		for (int col = 0; col < 8; col++)//8行
		{
			chessPieceArrays[col] = new int[8];
			transposeArrays[col] = new int[8];
		}
		for (int col = 0; col < 8; col++)//8行
		{
			for (int row = 0; row < 8; row++)//8列
			{
				chessPieceArrays[col][row] = 0;
			}
		}
	}

	//更新棋盘，如果是黑色棋子值为1，白色棋子值为2
	private void UpdateChessPieceArrays(int col, int row, int pieceType)
	{
		chessPieceArrays[col][row] = pieceType;
	}
	/// <summary>
	/// 计算分数代码
	/// </summary>
	public void CalculateScore()
	{
		blackScore = 0;
		writeScore = 0;
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
		blackScore += CheckOneLine(s)[0];
		writeScore += CheckOneLine(s)[1];
		//Debug.Log(s);
		debugText.text = "blackScore:" + blackScore + "writeScore:" + writeScore;
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
						writeScore += scoreDictionary[currentLinkNum];
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
						blackScore += scoreDictionary[currentLinkNum];
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
					blackScore += scoreDictionary[currentLinkNum];
				}
				else if (currentType == 2)
				{
					writeScore += scoreDictionary[currentLinkNum];
				}
				currentLinkNum = 1;
				currentType = 0;
			}
		}
		return new int[2] { blackScore, writeScore };
	}

}
