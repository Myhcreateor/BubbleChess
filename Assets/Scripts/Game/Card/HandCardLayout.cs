using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardLayout : Singleton<HandCardLayout>
{
    private List<Transform> mCardList;
    private List<Vector3> mTargetPositions;
    private List<Quaternion> mTargetRotations;
    void Awake()
    {
        base.Awake();
    }
	private void Start()
	{
        mCardList = new List<Transform>();
        mTargetPositions = new List<Vector3>();
        mTargetRotations = new List<Quaternion>();
    }
	void Update()
    {
        for(int i = 0;i<mCardList.Count; i++)
		{
            mCardList[i].localPosition = Vector3.Lerp(mCardList[i].localPosition, mTargetPositions[i], Time.deltaTime*10f);
            mCardList[i].localRotation = Quaternion.Lerp(mCardList[i].localRotation, mTargetRotations[i], Time.deltaTime * 10f);
        }
    }
    public void AddCard(Transform card)
	{
        mCardList.Add(card);
        SetLayout();
    }

    public void RemoveCard(Transform card)
	{
        mCardList.Remove(card);
        SetLayout();
    }

    private void SetLineLayout()
	{
        mTargetPositions.Clear();
        mTargetRotations.Clear();
        float positionx = (1f - mCardList.Count) * 100f;
		if (mCardList.Count == 1)
		{
            mTargetPositions.Add(new Vector3(0f, 0f, 1f));
            mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
		}
		else if(mCardList.Count>1)
		{
            for(int i = 0; i < mCardList.Count; i++)
			{
                Vector3 cardTran = new Vector3(Mathf.Lerp(positionx, -positionx, i / (mCardList.Count - 1f)), 0f, 1);
                mTargetPositions.Add(cardTran);
                mTargetRotations.Add(Quaternion.Euler(Vector3.zero));
            }
		}
	}

    private void SetCircleLayout()
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
                mTargetRotations.Add(Quaternion.Euler(0,0,Mathf.Lerp(15f,-15f,i/(mCardList.Count-1f))));
            }
        }
    }
    public void SetLayout()
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
}
