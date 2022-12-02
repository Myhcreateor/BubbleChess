using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Character : MonoBehaviour
{
    public bool isReleaseSkill = false;
    public bool isFiveConsecutive = false;
    public bool isFiveRoundDetection = false;
    public virtual void PassiveSkill()
    {

    }
    public virtual void ShowSkillImage(string skillName)
	{
        Image enlargedImage = GameObject.Find("ShowEnlargedIamge").GetComponent<Image>();
        enlargedImage.DOColor(new Color(255, 255, 255, 255), 0.2f);
        Sprite sprite = Resources.Load<Sprite>("Sprite/" + skillName);
		if (sprite != null)
		{
            enlargedImage.sprite = sprite;
            enlargedImage.DOFade(0, 5f);
        }
        
        
    }
}
