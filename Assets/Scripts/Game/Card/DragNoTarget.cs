using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class DragNoTarget : DragTarget, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private bool isRelease=false;
    private void Awake()
    {
        base.Awake();
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

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        selectMode = true;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向鼠标位置发射一条射线，射线检测到的是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject.tag == "ChessBoardRegion")
            {
                //触发这个牌的效果
                isRelease = this.gameObject.GetComponent<Card>().ExecuteCommand();
                if (isRelease)
                {
                    Destroy(this.gameObject);
                    HandCardLayout.Instance.RemoveCard(this.transform);
                    //isExecute = true;
                }
            }
        }
        EndThisDrag();
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
        enlargedImage.DOFade(0,5f);


    }

    //拖拽中每帧更新位置
    private void Update()
    {
        if (dragging)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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