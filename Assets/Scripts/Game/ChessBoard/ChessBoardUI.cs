using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardUI : MonoBehaviour
{
	public Text debugText;
	private Transform ChessBoardPieces;
	
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
		//�������̵���������
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
		chessBoardController.twoSideRoundNum = 0;
		for (int i = 0; i < ChessBoardPieces.childCount; i++)
		{
			Destroy(ChessBoardPieces.GetChild(i).gameObject); ;
		}
		CardManager.Instance.RemoveAllCard();
		EventHandler.CallUpdateDebugEvent(chessBoardController.RamainingRound(chessBoardController.twoSideRoundNum));
	}

	private void OnUpdateDebugEvent(int index)
	{
		debugText.text = "��Ϸ�����У���ʣ" + index + "���غ�";

	}

	private void OnGameOverEvent()
	{
		Debug.Log("��Ϸ����");
		ChessBoardController.Instance.CalculateScore();
	}

	private void OnGenerateChessEvent(GameObject go)
	{
		if (GameController.Instance.gameMode == GameMode.Test|| GameController.Instance.gameMode == GameMode.Stand_Alone)
		{
			chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (chessBoardController.twoSideRoundNum % 2) + 1);
			EventHandler.CallUpdateChessBoardEvent();
			chessBoardController.twoSideRoundNum++;
			chessBoardController.isRoundOver(chessBoardController.twoSideRoundNum);
		}
		else if (chessBoardController.gameMode == GameMode.Man_Machine)
		{
			//���Ŀǰ����Ļغ������壬������˻��Ļغϣ��˻����Լ��µ����ʵ�λ�ã�Ŀǰ�����˻���ս��ض�������
			chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (chessBoardController.twoSideRoundNum % 2) + 1);
			EventHandler.CallUpdateChessBoardEvent();
			chessBoardController.twoSideRoundNum += 2;
			if (chessBoardController.twoSideRoundNum % 10 == 0)
			{
				//�˻�ģʽ����һ�����ƣ�Ŀǰ��ÿ��غ�����һ������
				//Todo:��Ҫ��ɿ��Ƶ�����
				Man_MachineCardManager.Instance.isAddCardToHand = true;
			}
			GameController.Instance. Man_MachinePlayerPlayChess(ref chessBoardController.chessPieceArrays, 2);
			EventHandler.CallUpdateChessBoardEvent();
			chessBoardController.isRoundOver(chessBoardController.twoSideRoundNum);
		}

	}
	private void OnShowScoreEvent(int index1, int index2)
	{
		debugText.text = chessBoardController.piecesList[0].name + index1 + "    " + chessBoardController.piecesList[1].name + index2;
	}


}
