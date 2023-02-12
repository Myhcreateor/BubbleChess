using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChessBoardModel_SO", menuName = "Model/ChessBoardModel_SO")]
public class ChessBoardModel : ScriptableObject
{
	//得分链表
	public List<int> scoreList = new List<int>();
	public List<GameObject> piecesList;
	public Dictionary<int, int> scoreDictionary = new Dictionary<int, int>();
	//回合数
	public int roundNum;
	private void OnEnable()
	{
		for (int i = 0; i < scoreList.Count; i++)
		{
			scoreDictionary.Add(i, scoreList[i]);
		}
	}
	public int GetScore(int index)
	{
		return scoreDictionary[index];
	}
}
