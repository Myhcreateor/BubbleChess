using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CreateBoard : MonoBehaviour
{
	public static List<GameObject> floorList = new List<GameObject>();
	[MenuItem("ChessBoard/CreateBoard")]//��Unity�������������Ӵ�������ѡ�
	public static void Create()
	{
		for(int i = 1; i < 5; i++)
		{
			floorList.Add(Resources.Load<GameObject>("Prefabs/Floors/Grid" + i.ToString()));
			
		}
		
		for (int col = 0; col < 8; col++)//8��
		{
			for (int row = 0; row < 8; row++)//8��
			{
				//���ӱ߳�Ϊ80���趨�������ӵ����ĵ���Ϊ80
				float posx = 80 * col;
				float posy = 80 * row;
				GameObject creation = Instantiate(floorList[Random.Range(0,4)], new Vector3(posx, posy, 0), Quaternion.identity);//���������ϵĸ������
				if (!GameObject.Find("ChessBoardGridPraret"))
				{
					GameObject ChessBoardGridPraret = new GameObject("ChessBoardGridPraret");
					ChessBoardGridPraret.transform.SetParent(GameObject.Find("ChessBoard").transform);
					ChessBoardGridPraret.transform.SetAsFirstSibling();
				}
				creation.transform.SetParent(GameObject.Find("ChessBoardGridPraret").transform);//����ChessBoard��
				creation.name = $"{col},{row}";//�������������ʽ���Զ�Ϊ��������
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
	[MenuItem("ChessBoard/DeleteBoard")]//��Unity��������������ɾ������ѡ�
	public static void Delete()
	{
		DestroyImmediate(GameObject.Find("ChessBoardGridPraret"));
	}
}
