using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CreateBoard : MonoBehaviour
{
	public static List<GameObject> floorList = new List<GameObject>();
	[MenuItem("ChessBoard/CreateBoard")]//在Unity顶部工具栏增加创建棋盘选项卡
	public static void Create()
	{
		for(int i = 1; i < 5; i++)
		{
			floorList.Add(Resources.Load<GameObject>("Prefabs/Floors/Grid" + i.ToString()));
			
		}
		
		for (int col = 0; col < 8; col++)//8列
		{
			for (int row = 0; row < 8; row++)//8行
			{
				//棋子边长为80，设定两个棋子的中心点间距为80
				float posx = 80 * col;
				float posy = 80 * row;
				GameObject creation = Instantiate(floorList[Random.Range(0,4)], new Vector3(posx, posy, 0), Quaternion.identity);//创建棋盘上的各个棋格
				if (!GameObject.Find("ChessBoardGridPraret"))
				{
					GameObject ChessBoardGridPraret = new GameObject("ChessBoardGridPraret");
					ChessBoardGridPraret.transform.SetParent(GameObject.Find("ChessBoard").transform);
					ChessBoardGridPraret.transform.SetAsFirstSibling();
				}
				creation.transform.SetParent(GameObject.Find("ChessBoardGridPraret").transform);//置于ChessBoard下
				creation.name = $"{col},{row}";//以棋盘坐标的形式，自动为棋子命名
			}
		}
		GameObject.Find("ChessBoardGridPraret").transform.position = new Vector3(720, 270, 0);
	}
	[MenuItem("ChessBoard/CreateBoard", true)]
	private static bool isCreateChess()
	{
		if (!GameObject.Find("ChessBoardGridPraret"))
		{
			return true;
		}return false;
	}
	[MenuItem("ChessBoard/DeleteBoard")]//在Unity顶部工具栏增加删除棋盘选项卡
	public static void Delete()
	{
		DestroyImmediate(GameObject.Find("ChessBoardGridPraret"));
	}
}
