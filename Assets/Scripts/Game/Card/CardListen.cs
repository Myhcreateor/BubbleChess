using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardListen : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public static bool EnablePreview = true;
    public IsDragTarget isDragTarget;
    private Vector3 savePos;
    private int siblingIndex;
    private DragTarget dragTarget;
    private void Awake()
    {
		if (isDragTarget == IsDragTarget.DragNoTarget)
		{
            if (GetComponent<DragNoTarget>() != null)
            {
                dragTarget = GetComponent<DragNoTarget>();
            }
            else
            {
                Debug.Log("没有添加DragNoTarget拖拽脚本");
            }
		}
		else
		{
            if (GetComponent<DragHasTarget>() != null)
            {
                dragTarget = GetComponent<DragHasTarget>();
            }
            else
            {
                Debug.Log("没有添加DragHasTarget拖拽脚本");
            }
        }
       

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SaveCardSate();
        if (EnablePreview)
        {
            StartPreView();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EnablePreview)
        {
            EndPreView();
        }
    }

    //拖拽时的预览
    public void DragPreview()
    {
        //1.关闭普通预览功能
        EnablePreview = false;
        //2.进入拖拽预览状态
        StartDragPreView();
    }

    //结束拖拽
    public void EndDrag()
    {
        transform.localScale = Vector3.one;
        //缓动函数transform.DOMove(savePos, 0.2f).SetEase(Ease.Linear);
        transform.position = savePos;
        CheckHoverInThisCrd();
        transform.SetSiblingIndex(siblingIndex);
        EnablePreview = true;
    }

    private void StartPreView()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    private void EndPreView()
    {
        transform.DOMove(savePos, 0.1f);
        transform.localScale = Vector3.one;
        transform.SetSiblingIndex(siblingIndex);
    }

    private void StartDragPreView()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    //储存卡牌的初始状态
    private void SaveCardSate()
    {
        if (!dragTarget.dragging)
        {
            savePos = transform.position;
        }
    }
    private void CheckHoverInThisCrd()
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //创建一个鼠标事件
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        //向鼠标位置发射一条射线，射线检测到的是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject == gameObject)
            {
                Debug.Log("保持预览");
                StartPreView();
            }
        }
    }

}

public enum IsDragTarget
{
    DragHasTarget,DragNoTarget
}
