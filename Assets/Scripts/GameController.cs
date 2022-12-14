using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameController : Singleton<GameController>
{
	public int roundNum = 20;
	public GameMode gameMode;
	public GameState gameState;
	private Man_MachinePlayer man_MachinePlayer;
	[HideInInspector]
	public int currentCharacterIndex;
	public Man_MachineCharacterModel characterModel;
	private void Awake()
	{
		base.Awake();
		gameState = GameState.Running;
		if (gameMode == GameMode.Man_Machine)
		{
			currentCharacterIndex = 0;
			man_MachinePlayer = transform.Find("Man_MachinePlayer").GetComponent<Man_MachinePlayer>();
			ChooseMan_MachinePlayer(currentCharacterIndex);
		}else if (gameMode == GameMode.Stand_Alone)
		{
			//当前模式是双人本机对战模式
		}
	}
	//在人机模式将人机角色串联起来
	public void ChooseMan_MachinePlayer(int index)
	{
		CharacterDetails characterDetails = characterModel.CharacterList[index];
		man_MachinePlayer.SetCharacter(SetMan_MachinePlayer(characterDetails));
	}

	public Character SetMan_MachinePlayer(CharacterDetails characterDetails)
	{
		Character c = null;
		if (characterDetails.characterName == CharacterName.ZhiZhe)
		{
			c = new Character_ZhiZhe(ref ChessBoardController.Instance.chessPieceArrays, 2);
		}
		else if (characterDetails.characterName == CharacterName.HuoYanNvWu)
		{
			c = new Character_HuoYanNvWu(ref ChessBoardController.Instance.chessPieceArrays, 2);
		}
		else if (characterDetails.characterName == CharacterName.CiKe)
		{
			c = new Character_CiKe(ref ChessBoardController.Instance.chessPieceArrays, 2);
		}
		return c;
	}
	public Man_MachinePlayer GetMan_MachinePlayer()
	{
		return man_MachinePlayer;
	}
	public void Man_MachinePlayerPlayChess(ref int[][] boardChessArrays, int pieceType)
	{
		man_MachinePlayer.Man_MachineFindChessTran(ref boardChessArrays, pieceType);
	}
}
public enum GameState
{
	Running, Pause
}
