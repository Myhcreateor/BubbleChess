using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardLayout : Singleton<HandCardLayout>
{
	private List<Transform> mCardList;
	private List<Transform> mCardPlayerTwoList;
	private List<Vector3> mTargetPositions;
	private List<Quaternion> mTargetRotations;
	private List<Vector3> mTargetPlayerTwoPositions;
	private List<Quaternion> mTargetPlayerTwoRotations;
	private GameObject player1;
	private GameObject player2;
	void Awake()
	{
		base.Awake();
	}
	private void Start()
	{
		mCardList = new List<Transform>();
		mTargetPositions = new List<Vector3>();
		mTargetRotations = new List<Quaternion>();
		if (GameController.Instance.gameMode == GameMode.Stand_Alone)
		{
			mCardPlayerTwoList = new List<Transform>();
			mTargetPlayerTwoPositions = new List<Vector3>();
			mTargetPlayerTwoRotations = new List<Quaternion>();
			player1 = GameObject.Find("Player1");
			player2 = GameObject.Find("Player2");
		}
	}
	void Update()
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			for (int i = 0; i < mCardList.Count; i++)
			{
				mCardList[i].localPosition = Vector3.Lerp(mCardList[i].localPosition, mTargetPositions[i], Time.deltaTime * 10f);
				mCardList[i].localRotation = Quaternion.Lerp(mCardList[i].localRotation, mTargetRotations[i], Time.deltaTime * 10f);
			}
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				player2.SetActive(false);
				player1.SetActive(true);
				for (int i = 0; i < mCardList.Count; i++)
				{
					mCardList[i].localPosition = Vector3.Lerp(mCardList[i].localPosition, mTargetPositions[i], Time.deltaTime * 10f);
					mCardList[i].localRotation = Quaternion.Lerp(mCardList[i].localRotation, mTargetRotations[i], Time.deltaTime * 10f);
				}
			}
			else
			{
				player2.SetActive(true);
				player1.SetActive(false);
				for (int i = 0; i < mCardPlayerTwoList.Count; i++)
				{
					mCardPlayerTwoList[i].localPosition = Vector3.Lerp(mCardPlayerTwoList[i].localPosition, mTargetPlayerTwoPositions[i], Time.deltaTime * 10f);
					mCardPlayerTwoList[i].localRotation = Quaternion.Lerp(mCardPlayerTwoList[i].localRotation, mTargetPlayerTwoRotations[i], Time.deltaTime * 10f);
				}
			}
		}
	}
	public void AddCard(Transform card)
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			mCardList.Add(card);
			SetLayout();
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				mCardList.Add(card);
				SetLayout();
			}
			else
			{
				mCardPlayerTwoList.Add(card);
				SetLayout();
			}
		}
	}

	public void RemoveCard(Transform card)
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			mCardList.Remove(card);
			SetLayout();
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				mCardList.Remove(card);
				SetLayout();
			}
			else
			{
				mCardPlayerTwoList.Remove(card);
				SetLayout();
			}
		}
	}
	public void RemoveAllCard()
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			mCardList.Clear();
			SetLayout();
		}
		else
		{
			mCardList.Clear();
			mCardPlayerTwoList.Clear();
			SetLayout();
		}
	}
	private void SetLineLayout()
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			mTargetPositions.Clear();
			mTargetRotations.Clear();
			float positionx = (1f - mCardList.Count) * 100f;
			if (mCardList.Count == 1)
			{
				mTargetPositions.Add(new Vector3(0f, 0f, 1f));
				mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
			}
			else if (mCardList.Count > 1)
			{
				for (int i = 0; i < mCardList.Count; i++)
				{
					Vector3 cardTran = new Vector3(Mathf.Lerp(positionx, -positionx, i / (mCardList.Count - 1f)), 0f, 1);
					mTargetPositions.Add(cardTran);
					mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
				}
			}
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				mTargetPositions.Clear();
				mTargetRotations.Clear();
				float positionx = (1f - mCardList.Count) * 100f;
				if (mCardList.Count == 1)
				{
					mTargetPositions.Add(new Vector3(0f, 0f, 1f));
					mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
				}
				else if (mCardList.Count > 1)
				{
					for (int i = 0; i < mCardList.Count; i++)
					{
						Vector3 cardTran = new Vector3(Mathf.Lerp(positionx, -positionx, i / (mCardList.Count - 1f)), 0f, 1);
						mTargetPositions.Add(cardTran);
						mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
					}
				}
			}
			else
			{
				mTargetPlayerTwoPositions.Clear();
				mTargetPlayerTwoRotations.Clear();
				float x = (1f - mCardPlayerTwoList.Count) * 100f;
				if (mCardPlayerTwoList.Count == 1)
				{
					mTargetPlayerTwoPositions.Add(new Vector3(0f, 0f, 1f));
					mTargetPlayerTwoRotations.Add(Quaternion.Euler(Vector3.zero));
				}
				else if (mCardPlayerTwoList.Count > 1)
				{
					for (int i = 0; i < mCardPlayerTwoList.Count; i++)
					{
						Vector3 cardTran = new Vector3(Mathf.Lerp(x, -x, i / (mCardPlayerTwoList.Count - 1f)), 0f, 1);
						mTargetPlayerTwoPositions.Add(cardTran);
						mTargetPlayerTwoRotations.Add(Quaternion.Euler(Vector3.zero));
					}
				}
			}
		}
	}

	private void SetCircleLayout()
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			mTargetPositions.Clear();
			mTargetRotations.Clear();
			float startAngle = Mathf.PI * 105f / 180f;
			float EndAngle = Mathf.PI * 75f / 180f;
			float radius = 1000f;
			if (mCardList.Count == 1)
			{
				mTargetPositions.Add(new Vector3(0f, 0f, 1f));
				mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
			}
			else if (mCardList.Count > 1)
			{
				for (int i = 0; i < mCardList.Count; i++)
				{
					float angle = Mathf.Lerp(startAngle, EndAngle, i / (mCardList.Count - 1f));
					mTargetPositions.Add(new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle), 0));
					mTargetRotations.Add(Quaternion.Euler(0, 0, Mathf.Lerp(15f, -15f, i / (mCardList.Count - 1f))));
				}
			}
		}
		else
		{
			float startAngle = Mathf.PI * 105f / 180f;
			float EndAngle = Mathf.PI * 75f / 180f;
			float radius = 1000f;
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				mTargetPositions.Clear();
				mTargetRotations.Clear();
				if (mCardList.Count == 1)
				{
					mTargetPositions.Add(new Vector3(0f, 0f, 1f));
					mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
				}
				else if (mCardList.Count > 1)
				{
					for (int i = 0; i < mCardList.Count; i++)
					{
						float angle = Mathf.Lerp(startAngle, EndAngle, i / (mCardList.Count - 1f));
						mTargetPositions.Add(new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle), 0));
						mTargetRotations.Add(Quaternion.Euler(0, 0, Mathf.Lerp(15f, -15f, i / (mCardList.Count - 1f))));
					}
				}
			}
			else
			{
				mTargetPlayerTwoPositions.Clear();
				mTargetPlayerTwoRotations.Clear();
				if (mCardPlayerTwoList.Count == 1)
				{
					mTargetPlayerTwoPositions.Add(new Vector3(0f, 0f, 1f));
					mTargetPlayerTwoRotations.Add(Quaternion.Euler(Vector3.zero));
				}
				else if (mCardPlayerTwoList.Count > 1)
				{
					for (int i = 0; i < mCardPlayerTwoList.Count; i++)
					{
						float angle = Mathf.Lerp(startAngle, EndAngle, i / (mCardPlayerTwoList.Count - 1f));
						mTargetPlayerTwoPositions.Add(new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle), 0));
						mTargetPlayerTwoRotations.Add(Quaternion.Euler(0, 0, Mathf.Lerp(15f, -15f, i / (mCardPlayerTwoList.Count - 1f))));
					}
				}
			}
		}


	}

	public void SetLayout()
	{
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			if (mCardList.Count > 3)
			{
				SetCircleLayout();
			}
			else
			{
				SetLineLayout();
			}
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				if (mCardList.Count > 3)
				{
					SetCircleLayout();
				}
				else
				{
					SetLineLayout();
				}
			}
			else
			{
				if (mCardPlayerTwoList.Count > 3)
				{
					SetCircleLayout();
				}
				else
				{
					SetLineLayout();
				}
			}
		}
	}
}

