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
		man_MachinePlayer = transform.Find("Man_MachinePlayer").GetComponent<Man_MachinePlayer>();
		if (gameMode == GameMode.Man_Machine)
		{
			CharacterDetails characterDetails = characterModel.CharacterList[2];
			if (characterDetails.characterName==CharacterName.ZhiZhe)
			{
				Character c = new Character_ZhiZhe(ref ChessBoardController.Instance.chessPieceArrays, 2);
				man_MachinePlayer.SetCharacter(c);
			}
			else if (characterDetails.characterName == CharacterName.HuoYanNvWu)
			{
				Character c = new Character_HuoYanNvWu(ref ChessBoardController.Instance.chessPieceArrays, 2);
				man_MachinePlayer.SetCharacter(c);
			}
			else if (characterDetails.characterName == CharacterName.CiKe)
			{
				Character c = new Character_CiKe(ref ChessBoardController.Instance.chessPieceArrays, 2);
				man_MachinePlayer.SetCharacter(c);
			}
		}
	}
	public Man_MachinePlayer GetMan_MachinePlayer()
	{
		return man_MachinePlayer;
	}
	public void Man_MachinePlayerPlayChess(ref int[][] boardChessArrays, int pieceType)
	{
		man_MachinePlayer.Man_MachineFindChessTran(ref boardChessArrays,  pieceType);
	}
}
