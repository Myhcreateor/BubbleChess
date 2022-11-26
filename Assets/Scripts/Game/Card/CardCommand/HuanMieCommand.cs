using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuanMieCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;

	public HuanMieCommand(ref int[][] chessPieceArrays, int pieceType)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
	}
	public void Execute()
	{
		//释放幻灭技能
		Debug.Log("释放幻灭技能");
	}
}
