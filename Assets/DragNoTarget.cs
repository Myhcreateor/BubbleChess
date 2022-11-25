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
            Debug.Log("û�ҵ�CardListen���");
        }
    }

    //��קģʽ
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!dragging)
            {
                Debug.Log("��ס������");
                dragging = true;
                selectMode = false;
                //��ʼ��ק״̬��Ԥ��
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
        //�����λ�÷���һ�����ߣ����߼�⵽���Ƿ���UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject.tag == "ChessBoardRegion")
            {
                Destroy(this.gameObject);
                //��������Ƶ�Ч��
                this.gameObject.GetComponent<Card>().ExecuteCommand();
            }
        }
        EndThisDrag();
    }

    //�Ŵ���Ч��
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    //��ק��ÿ֡����λ��
    private void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Input.mousePosition;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

    //ȡ����ק������ԭ��״̬
    private void EndThisDrag()
    {
        dragging = false;
        preView.EndDrag();
    }
}