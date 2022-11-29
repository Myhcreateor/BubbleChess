using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameController : Singleton<GameController>
{
	public int roundNum=20;
	public GameMode gameMode;
	private Man_MachinePlayer man_MachinePlayer;
	public Man_MachineCharacterModel characterModel;
	private void Awake()
	{
		base.Awake();
		if (gameMode == GameMode.Man_Machine)
		{
			CharacterDetails characterDetails = characterModel.CharacterList[0];
			if (characterDetails.characterName==CharacterName.ZhiZhe)
			{
				man_MachinePlayer = new Man_MachinePlayer(new Character_ZhiZhe(ref ChessBoardController.Instance.chessPieceArrays, 2));
			}
			 
		}
	}
	
	public void Man_MachinePlayerPlayChess(ref int[][] boardChessArrays, int pieceType)
	{
		man_MachinePlayer.Man_MachineFindChessTran(ref boardChessArrays,  pieceType);
	}
}
