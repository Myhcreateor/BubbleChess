using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class DragHasTarget : DragTarget, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject hintImage;
    private void Awake()
    {
        base.Awake();
        hintImage = Instantiate(Resources.Load<GameObject>("Prefabs/HintImage"), new Vector3(0, 0, 0), Quaternion.identity, transform.parent) ;
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
                this.gameObject.GetComponent<Image>().enabled = false;
                hintImage.GetComponent<Image>().enabled = true;
                hintImage.transform.position = g.gameObject.transform.position;
                isInChessBoard = true;

			}
        }
		if (!isInChessBoard)
		{
            hintImage.GetComponent<Image>().enabled = false;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        bool isExecute = false;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向鼠标位置发射一条射线，射线检测到的是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject.tag == "Floor")
            {
                //如果放在合适的位置则触发这个牌的效果
                this.gameObject.GetComponent<Card>().ExecuteCommand();
                HandCardLayout.Instance.RemoveCard(this.transform);
                isExecute = true;
                hintImage.GetComponent<Image>().enabled = false;
            }
        }
		if (!isExecute)
		{
            hintImage.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponent<Image>().enabled = true;
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
        Image enlargedImage = transform.parent.Find("ShowEnlargedIamge").GetComponent<Image>();
        enlargedImage.DOColor(new Color(255, 255, 255, 255), 0.2f);
        enlargedImage.sprite = this.GetComponent<Card>().cardDetails.cardSprite;
        enlargedImage.DOFade(0, 5f);


    }

    //拖拽中每帧更新位置
    private void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Input.mousePosition;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

    //取消拖拽，返回原来状态
    private void EndThisDrag()
    {
        dragging = false;
        preView.EndDrag();
    }
}