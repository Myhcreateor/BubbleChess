using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNoTarget : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragging = false;
    private bool selectMode = true;
    private CardListen preView;

    private void Awake()
    {
        if (GetComponent<CardListen>() != null)
        {
            preView = GetComponent<CardListen>();
        }
        else
        {
            Debug.Log("没找到CardListen组件");
        }
    }

    //拖拽模式
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!dragging)
            {
                Debug.Log("按住鼠标左键");
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
                Destroy(this.gameObject);
                //触发这个牌的效果
                this.gameObject.GetComponent<Card>().ExecuteCommand();
            }
        }
        EndThisDrag();
    }

    //放大卡牌效果
    public void OnPointerClick(PointerEventData eventData)
    {
        
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