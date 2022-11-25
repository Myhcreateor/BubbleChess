using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardListen : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public static bool EnablePreview = true;

    [SerializeField]
    private Vector3 savePos;
    [SerializeField]
    private float upmove;
    [SerializeField]
    private int siblingIndex;

    private DragNoTarget dragNoTarget;

    private void Awake()
    {
        if (GetComponent<DragNoTarget>() != null)
        {
            dragNoTarget = GetComponent<DragNoTarget>();
        }
        else
        {
            Debug.Log("û�������ק�ű�");
        }

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SaveCardSate();
        Debug.Log("��ʼԤ��");
        if (EnablePreview)
        {
            StartPreView();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("�˳�Ԥ��");
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
        EnablePreview = true;
    }

    private void StartPreView()
    {
        transform.DOMoveY(upmove, 0.1f);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    private void EndPreView()
    {
        //transform.DOMoveY(-upmove, 0.1f);
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
        if (!dragNoTarget.dragging)
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
