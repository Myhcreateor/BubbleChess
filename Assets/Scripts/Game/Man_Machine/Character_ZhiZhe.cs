using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_ZhiZhe : Character
{
	private int[][] boardChessArrays;
	private int pieceType;
	public Character_ZhiZhe(ref int[][] chessPieceArrays, int pieceType)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		isReleaseSkill = true;
	}

	public override void PassiveSkill()
	{
		boardChessArrays[Random.Range(0, 8)][Random.Range(0, 8)] = pieceType;
		int randomNumx = Random.Range(0, 8);
		int randomNumy = Random.Range(0, 8);
		if (boardChessArrays[randomNumx][randomNumy] == 0)
		{
			boardChessArrays[randomNumx][randomNumy] = pieceType;
		}
		EventHandler.CallUpdateChessBoardEvent();
	}
}
