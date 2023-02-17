using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalManager : MonoBehaviour
{
	private int crystalTotalNum = 5;
	private int crystalNum = 0;
	public int CrystalNum
	{
		get { return crystalNum; }
		set
		{
			if (value >= 5) { crystalNum = 5; }
			else if(value <= 0){ crystalNum = 0; }
			else
			{
				crystalNum = value;
			};
		}
	}
	private Image crystalImage;
	private List<Image> crystalImageList;
	private void Start()
	{
		crystalImageList = new List<Image>();
		crystalImageList.Add(transform.Find("CrystalShell0/Crystal").GetComponent<Image>());
		crystalImageList.Add(transform.Find("CrystalShell1/Crystal").GetComponent<Image>());
		crystalImageList.Add(transform.Find("CrystalShell2/Crystal").GetComponent<Image>());
		crystalImageList.Add(transform.Find("CrystalShell3/Crystal").GetComponent<Image>());
		crystalImageList.Add(transform.Find("CrystalShell4/Crystal").GetComponent<Image>());
		for(int i = crystalNum; i < crystalTotalNum; i++)
		{
			crystalImageList[i].color = new Color(1, 1, 1, 0);
		}
	}
	private void OnEnable()
	{
		EventHandler.UpdateCrytralEvent += OnUpdateCrytralEvent;
	}
	private void OnDisable()
	{
		EventHandler.UpdateCrytralEvent -= OnUpdateCrytralEvent;
	}
	public void UpdateCrystalNum()
	{
		for (int i = 0; i < crystalNum; i++)
		{
			crystalImageList[i].color = new Color(1, 1, 1, 1);
		}
		for (int i = crystalNum; i < crystalTotalNum; i++)
		{
			crystalImageList[i].color = new Color(1, 1, 1, 0);
		}
	}
	public void OnUpdateCrytralEvent(int index)
	{
		UpdateCrystalNum();
	}
}
