using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiXianCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
	public DiXianCommand(ref int[][] chessPieceArrays, int pieceType, string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			this.pieceType = pieceType;
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				this.pieceType = 1;
			}
			else
			{
				this.pieceType = 2;
			}
		}
		this.clickTrans = clickTrans;
	}
	public void Execute()
	{
		int x = int.Parse(clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		if (boardChessArrays[x][y] == 0)
		{
			isSuccessRelease = true;
			//TODO:�»غ϶�������λ�������clickTrans������
			//��������˷�����Ϣ���ı���isUserHuanMie��huanMieTrans
			//ChessBoardController.Instance.isUserHuanMie = true;
			//ChessBoardController.Instance.huanMieTrans = clickTrans;
			Debug.Log("TODO:�»غ϶�������λ�������" + clickTrans + "������");
		}
	}
}

