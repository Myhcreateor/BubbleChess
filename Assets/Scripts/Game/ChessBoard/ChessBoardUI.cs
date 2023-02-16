using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardUI : MonoBehaviour
{
	public Text debugText;
	private Transform ChessBoardPieces;
	private Transform chessBoardGridParent;
	public List<GameObject> allPiecesList;
	private ChessBoardController chessBoardController;
	
	void Awake()
	{
		chessBoardController = ChessBoardController.Instance;
		debugText = transform.parent.Find("DebugText").GetComponent<Text>();
		ChessBoardPieces = transform.Find("ChessBoardPieces");
		chessBoardGridParent = transform.Find("ChessBoardGridPraret");
	}

	private void OnEnable()
	{
		EventHandler.ShowScoreEvent += OnShowScoreEvent;
		EventHandler.GenerateChessEvent += OnGenerateChessEvent;
		EventHandler.GameOverEvent += OnGameOverEvent;
		EventHandler.UpdateDebugEvent += OnUpdateDebugEvent;
		EventHandler.UpdateDebugStringEvent += OnUpdateDebugStringEvent;
		EventHandler.NewStartGameEvent += OnNewStartGameEvent;
		EventHandler.UpdateChessBoardEvent += OnUpdateChessBoardEvent;
		EventHandler.GenerateParticleEffectEvent += OnGenerateParticleEffectEvent;
	}


	private void OnDisable()
	{
		EventHandler.ShowScoreEvent -= OnShowScoreEvent;
		EventHandler.GenerateChessEvent -= OnGenerateChessEvent;
		EventHandler.GameOverEvent -= OnGameOverEvent;
		EventHandler.UpdateDebugEvent -= OnUpdateDebugEvent;
		EventHandler.NewStartGameEvent -= OnNewStartGameEvent;
		EventHandler.UpdateDebugStringEvent -= OnUpdateDebugStringEvent;
		EventHandler.UpdateChessBoardEvent -= OnUpdateChessBoardEvent;
		EventHandler.GenerateParticleEffectEvent -= OnGenerateParticleEffectEvent;
	}

	private void OnUpdateDebugStringEvent(string s)
	{
		debugText.text = s;
	}

	private GameObject FindPiecesListWithName(int col, int row)
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
					piece = FindPiecesListWithName(col, row);
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
		ChessBoardController.Instance.CalculateScore();
	}

	private void OnGenerateChessEvent(GameObject go)
	{
		if (GameController.Instance.gameMode == GameMode.Test)
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
			GameController.Instance.Man_MachinePlayerPlayChess(ref chessBoardController.chessPieceArrays, 2);
			EventHandler.CallUpdateChessBoardEvent();
			chessBoardController.isRoundOver(chessBoardController.twoSideRoundNum);
		}
		else if (GameController.Instance.gameMode == GameMode.Stand_Alone)
		{
			chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (chessBoardController.twoSideRoundNum % 2) + 1);
			chessBoardController.HandOffPlayer();
			EventHandler.CallUpdateChessBoardEvent();
			chessBoardController.twoSideRoundNum++;
			chessBoardController.isRoundOver(chessBoardController.twoSideRoundNum);
		}
		else if (GameController.Instance.gameMode == GameMode.NetWorking)
		{
			if (chessBoardController.IsPlayeChess)
			{
				chessBoardController.IsPlayeChess = false;
				debugText.text = "�ȴ��Է�����";
				chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), GameController.Instance.GetPlayer());
				EventHandler.CallUpdateChessBoardEvent();
				//�����ݷ��͸�������,����chessBoardController.chessPieceArrays,Userid,��������
				ProtocolManager.UpdateChessBroad(go.name, (s, res) =>
				{
					if (res == UpdateResult.Failed)
					{
						Debug.LogError("����״̬ͬ��ʧ��");
					}
					chessBoardController.UpdateChessPieceArrays(int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1]), GameController.Instance.GetOpponent());
					EventHandler.CallUpdateChessBoardEvent();
					chessBoardController.IsPlayeChess = true;
					debugText.text = "��ĻغϿ�ʼ";
					//�ж��Ƿ����
					if (GameController.Instance.player==Player.One)
					{
						//����ģʽ����һ�����ƣ�Ŀǰ�ǵ���/��/ʮ���غ�����һ�����ƣ�����ʱ�򲻳���
						if (chessBoardController.playerOneRoundNum % 4 == 0&& chessBoardController.playerOneRoundNum<=12&& chessBoardController.playerOneRoundNum > 1) 
						{
							CardManager.Instance.isAddCardToHand = true;
						}
					}
					else
					{
						if (chessBoardController.playerTwoRoundNum % 4 == 0 && chessBoardController.playerTwoRoundNum <= 12&& chessBoardController.playerTwoRoundNum>1)
						{
							CardManager.Instance.isAddCardToHand = true;
						}
					}
				});
				if (GameController.Instance.player == Player.One)
				{
					chessBoardController.playerOneRoundNum++;
				}
				else
				{
					chessBoardController.playerTwoRoundNum++;
					chessBoardController.isNetworkRoundOver(chessBoardController.playerTwoRoundNum);
				}
			}
		}

	}
	private void OnShowScoreEvent(int index1, int index2)
	{
		debugText.text = chessBoardController.piecesList[0].name + index1 + "    " + chessBoardController.piecesList[1].name + index2;
	}
	

	private void OnGenerateParticleEffectEvent(string s, string path)
	{
		Transform trans = chessBoardGridParent.Find(s);

		if (trans != null)
		{
			GameObject go = Resources.Load<GameObject>(path);
			if (go != null)
			{
				GameObject effectGo = Instantiate(go, trans.position, Quaternion.identity, trans);
				Destroy(effectGo, 2f);
			}
		}
	}
}
