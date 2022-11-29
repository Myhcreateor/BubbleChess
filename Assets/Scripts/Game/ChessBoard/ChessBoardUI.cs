using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardUI : MonoBehaviour
{
	public Text debugText;
	private Transform ChessBoardPieces;
	private int twoSideRoundNum = 0;
	
	public List<GameObject> allPiecesList;
	private ChessBoardController chessBoardController;
	// Start is called before the first frame update
	void Awake()
	{
		chessBoardController = ChessBoardController.Instance;
		debugText = transform.parent.Find("DebugText").GetComponent<Text>();
		ChessBoardPieces = transform.Find("ChessBoardPieces");
	}
	private void OnEnable()
	{
		EventHandler.ShowScoreEvent += OnShowScoreEvent;
		EventHandler.GenerateChessEvent += OnGenerateChessEvent;
		EventHandler.GameOverEvent += OnGameOverEvent;
		EventHandler.UpdateDebugEvent += OnUpdateDebugEvent;
		EventHandler.NewStartGameEvent += OnNewStartGameEvent;
		EventHandler.UpdateChessBoardEvent += OnUpdateChessBoardEvent;
	}
	private void OnDisable()
	{
		EventHandler.ShowScoreEvent -= OnShowScoreEvent;
		EventHandler.GenerateChessEvent -= OnGenerateChessEvent;
		EventHandler.GameOverEvent -= OnGameOverEvent;
		EventHandler.UpdateDebugEvent -= OnUpdateDebugEvent;
		EventHandler.NewStartGameEvent -= OnNewStartGameEvent;
		EventHandler.UpdateChessBoardEvent -= OnUpdateChessBoardEvent;
	}
	private GameObject FindPiecesListWithName(int col,int row)
	{
		return allPiecesList.Find(i => i.name == col.ToString() + "," + row.ToString());
	}
	private void OnUpdateChessBoardEvent()
	{
		//更新棋盘的所有棋子
		//chessBoardController.chessPieceArrays
		GameObject piece = null;
		for (int col = 0; col < 8; col++)
		{
			for (int row = 0; row < 8; row++)
			{
				if (chessBoardController.chessPieceArrays[col][row] == 0)
				{
					piece = FindPiecesListWithName(col,row);
					if (piece)
					{
						allPiecesList.Remove(piece);
						Destroy(piece);
					}
				}
				else if (chessBoardController.chessPieceArrays[col][row] == 1)
				{
					piece = FindPiecesListWithName(col, row);
					if (!piece)
					{
						GameObject pieceGo = Instantiate(chessBoardController.piecesList[0], chessBoardController.GetChessBoardGridTransform().Find(col.ToString() + "," + row.ToString()).position, Quaternion.identity, ChessBoardPieces);
						pieceGo.name = col.ToString() + "," + row.ToString();
						pieceGo.GetComponent<Piece>().pieceNum = 1;
						allPiecesList.Add(pieceGo);

					}
					if (piece != null && piece.GetComponent<Piece>().pieceNum != 1)
					{
						Destroy(piece);
						allPiecesList.Remove(piece);
						GameObject pieceGo = Instantiate(chessBoardController.piecesList[0], chessBoardController.GetChessBoardGridTransform().Find(col.ToString() + "," + row.ToString()).position, Quaternion.identity, ChessBoardPieces);
						pieceGo.name = col.ToString() + "," + row.ToString();
						pieceGo.GetComponent<Piece>().pieceNum = 1;
						allPiecesList.Add(pieceGo);

					}
				}
				else if (chessBoardController.chessPieceArrays[col][row] == 2)
				{
					piece = FindPiecesListWithName(col, row);
					if (!piece)
					{
						GameObject pieceGo = Instantiate(chessBoardController.piecesList[1], chessBoardController.GetChessBoardGridTransform().Find(col.ToString() + "," + row.ToString()).position, Quaternion.identity, ChessBoardPieces);
						pieceGo.name = col.ToString() + "," + row.ToString();
						pieceGo.GetComponent<Piece>().pieceNum = 2;
						allPiecesList.Add(pieceGo);

					}
					if (piece != null && piece.GetComponent<Piece>().pieceNum != 2)
					{
						Destroy(piece);
						allPiecesList.Remove(piece);
						GameObject pieceGo = Instantiate(chessBoardController.piecesList[1], chessBoardController.GetChessBoardGridTransform().Find(col.ToString() + "," + row.ToString()).position, Quaternion.identity, ChessBoardPieces);
						pieceGo.name = col.ToString() + "," + row.ToString();
						pieceGo.GetComponent<Piece>().pieceNum = 2;
						allPiecesList.Add(pieceGo);
					}

				}
				//Instantiate(chessBoardController.piecesList[pieceType % 2], go.transform.position, Quaternion.identity, ChessBoardPieces);
				//chessBoardController.chessPieceArrays[col][row]
			}
		}

	}

	private void OnNewStartGameEvent()
	{
		twoSideRoundNum = 0;
		for (int i = 0; i < ChessBoardPieces.childCount; i++)
		{
			Destroy(ChessBoardPieces.GetChild(i).gameObject); ;
		}
		EventHandler.CallUpdateDebugEvent(chessBoardController.RamainingRound(twoSideRoundNum));
	}

	private void OnUpdateDebugEvent(int index)
	{
		debugText.text = "游戏进行中，还剩" + index + "个回合";

	}

	private void OnGameOverEvent()
	{
		Debug.Log("游戏结束");
		ChessBoardController.Instance.CalculateScore();
	}

	private void OnGenerateChessEvent(GameObject go)
	{
		if (GameController.Instance.gameMode == GameMode.Test)
		{
			chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (twoSideRoundNum % 2) + 1);
			EventHandler.CallUpdateChessBoardEvent();
			twoSideRoundNum++;
			chessBoardController.isRoundOver(twoSideRoundNum);
		}
		else if (GameController.Instance.gameMode == GameMode.Man_Machine)
		{
			//如果目前是你的回合则下棋，如果是人机的回合，人机会自己下到合适的位置，假设人机对战你必定是先手
			chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (twoSideRoundNum % 2) + 1);
			EventHandler.CallUpdateChessBoardEvent();
			//twoSideRoundNum += 1;
			//Man_MachinePlayer.Instance.Man_MachineFindChessTran(ref chessBoardController.chessPieceArrays, twoSideRoundNum);
			//EventHandler.CallUpdateChessBoardEvent();

			Man_MachinePlayer.Instance.Man_MachineFindChessTran(ref chessBoardController.chessPieceArrays, 2);
			EventHandler.CallUpdateChessBoardEvent();
			twoSideRoundNum += 2;
			chessBoardController.isRoundOver(twoSideRoundNum);
		}

	}
	private void OnShowScoreEvent(int index1, int index2)
	{
		debugText.text = chessBoardController.piecesList[0].name + index1 + "    " + chessBoardController.piecesList[1].name + index2;
	}


}
