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
                Debug.Log("û�����DragNoTarget��ק�ű�");
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
                Debug.Log("û�����DragHasTarget��ק�ű�");
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

    //��קʱ��Ԥ��
    public void DragPreview()
    {
        //1.�ر���ͨԤ������
        EnablePreview = false;
        //2.������קԤ��״̬
        StartDragPreView();
    }

    //������ק
    public void EndDrag()
    {
        transform.localScale = Vector3.one;
        //��������transform.DOMove(savePos, 0.2f).SetEase(Ease.Linear);
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

    //���濨�Ƶĳ�ʼ״̬
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
        //����һ������¼�
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        //�����λ�÷���һ�����ߣ����߼�⵽���Ƿ���UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult g in raycastResults)
        {
            if (g.gameObject == gameObject)
            {
                Debug.Log("����Ԥ��");
                StartPreView();
            }
        }
    }

}

public enum IsDragTarget
{
    DragHasTarget,DragNoTarget
}
