using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Character_HuoYanNvWu : Character
{
	//  被动技能狂热
	//	当对手形成了5个子或以上的连对，则释放一次蒸发或者释放一次火舞
	//	技能1 蒸发
	//	随机消灭一个的敌方棋子
	//  技能2 火舞
	//  在棋盘边缘处的位置随机生成两个己方棋子

	private int[][] boardChessArrays;
	private int pieceType;
	private List<string> skillCrazeTransList = new List<string>();
	public Character_HuoYanNvWu(ref int[][] chessPieceArrays, int pieceType)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		isFiveConsecutive = true;
	}

	public override void PassiveSkill()
	{
		if (Random.Range(0, 2) == 0)
		{
			ShowSkillImage("ZhengFa");
			SkillEvaporation(ref boardChessArrays, pieceType);
		}
		else
		{
			ShowSkillImage("KuangRe");
			SkillCraze(ref boardChessArrays, pieceType);
		}
		EventHandler.CallUpdateChessBoardEvent();
	}

	public void SkillCraze(ref int[][] chessPieceArrays, int pieceType)
	{
		for(int i=0;i< 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if(i==0||i== 7||j==0|| j== 7)
				{
					if (chessPieceArrays[i][j] == 0)
					{
						string str = i.ToString() + "," + j.ToString();
						if (!skillCrazeTransList.Contains(str))
						{
							skillCrazeTransList.Add(str);
						}
					}
				}
			}
		}
		for(int k = 0; k < 2; k++)
		{
			if (skillCrazeTransList.Count != 0)
			{
				int randomNum = Random.Range(0, skillCrazeTransList.Count);
				string skillCrazeTrans = skillCrazeTransList[randomNum];
				chessPieceArrays[int.Parse(skillCrazeTrans.Split(',')[0])][int.Parse(skillCrazeTrans.Split(',')[1])] = pieceType;
				skillCrazeTransList.RemoveAt(randomNum);
			}
		}
		EventHandler.CallUpdateChessBoardEvent();

	}
	public void SkillEvaporation(ref int[][] chessPieceArrays, int pieceType)
	{
		//随机消灭一个对方棋子
		int enemyType = 0;
		if (pieceType == 1) enemyType = 2;
		else enemyType = 1;
		List<string> skillEvaporationTransList = new List<string>();
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (chessPieceArrays[i][j] == enemyType)
				{
					string str = i.ToString() + "," + j.ToString();
					skillEvaporationTransList.Add(str);
				}

			}
		}
		int randomNum = Random.Range(0, skillEvaporationTransList.Count);
		string skillEvaporationTrans = skillEvaporationTransList[randomNum];
		chessPieceArrays[int.Parse(skillEvaporationTrans.Split(',')[0])][int.Parse(skillEvaporationTrans.Split(',')[1])] =0;
		EventHandler.CallUpdateChessBoardEvent();
	}

}
