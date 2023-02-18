using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class DragHasTarget : DragTarget, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject hintGo;
    private Image hintImage;
    private bool isRelease;

    private void Awake()
    {
        base.Awake();
        hintGo = Instantiate(Resources.Load<GameObject>("Prefabs/HintImage"), new Vector3(0, 0, 0), Quaternion.identity, transform.parent) ;
        hintImage = hintGo.GetComponent<Image>();
        hintImage.enabled = false;
    }

    //拖拽模式
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!dragging)
            {
                dragging = true;
                selectMode = false;
                //开始拖拽状态的预览
                preView.DragPreview();
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        selectMode = true;
        bool isInChessBoard = false;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向鼠标位置发射一条射线，射线检测到的是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if(g.gameObject.tag == "Floor")
			{
                transform.Find("Image").GetComponent<Image>().enabled = false;
                grow.color = new Color(grow.color.r, grow.color.g, grow.color.b, 0);
                hintImage.enabled = true;
                hintGo.transform.position = g.gameObject.transform.position;
                isInChessBoard = true;
                Debug.Log("在棋盘里");
			}
        }
		if (!isInChessBoard)
		{
            hintImage.enabled = false;
            transform.Find("Image").GetComponent<Image>().enabled = true;
            grow.color = new Color(grow.color.r, grow.color.g, grow.color.b, 1);
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        bool isExecute = false;
        Card card = gameObject.GetComponent<Card>();
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向鼠标位置发射一条射线，射线检测到的是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject.tag == "Floor")
            {
                //如果放在合适的位置则触发这个牌的效果
                string s = g.gameObject.name;
                card.SetClickTrans(s);
                isRelease= card.ExecuteCommand();
				if (isRelease)
				{
                    HandCardLayout.Instance.RemoveCard(this.transform);
                    isExecute = true;
                }
                hintImage.enabled = false;
            }
        }
		if (!isExecute)
		{
            hintImage.enabled = false;
            transform.Find("Image").GetComponent<Image>().enabled = true;
            grow.color = new Color(grow.color.r, grow.color.g, grow.color.b, 1);
            EndThisDrag();
        }
    }

    //放大卡牌效果
    public void OnPointerClick(PointerEventData eventData)
    {
        if (dragging)
        {
            return;
        }
        Image enlargedImage = GameObject.FindGameObjectWithTag("ShowEnlargedIamge").GetComponent<Image>();
        enlargedImage.DOColor(new Color(255, 255, 255, 255), 0.2f);
        enlargedImage.sprite = this.GetComponent<Card>().cardDetails.cardSprite;
        enlargedImage.DOFade(0, 5f);


    }

    //拖拽中每帧更新位置
    private void Update()
    {
        if (dragging)
        {
           Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//屏幕坐标转换世界坐标
            //Vector2 uiPos = c.transform.InverseTransformPoint(worldPos);//世界坐标转换位本地坐标

           //Vector3 mousePos = Input.mousePosition;
            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        }
    }

    //取消拖拽，返回原来状态
    private void EndThisDrag()
    {
        dragging = false;
        preView.EndDrag();
    }
}