using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// ����
/// </summary>
public class SlideScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    private RectTransform contentTrans;
    private float beginMousePositionX;
    private float endMousePositionX;
    private ScrollRect scrollRect;

    public int cellLength;
    public int spacing;
    public int leftOffset;
    private float moveOneItemLength;

    private Vector3 currentContentLocalPos;//��һ�ε�λ��
    private Vector3 contentInitPos;//Content��ʼλ��
    private Vector2 contentTransSize;//Content��ʼ��С

    public int totalItemNum;
    [SerializeField]
    public int currentIndex;

    public bool needSendMessage;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentTrans = scrollRect.content;
        moveOneItemLength = cellLength + spacing;
        currentContentLocalPos = contentTrans.localPosition;
        contentTransSize = contentTrans.sizeDelta;
        contentInitPos = contentTrans.localPosition;
        currentIndex = 1;

    }

    public void Init()
    {
        currentIndex = 1;

        if (contentTrans != null)
        {
            contentTrans.localPosition = contentInitPos;
            currentContentLocalPos = contentInitPos;
        }
    }

    /// <summary>
    /// ͨ����ק���ɿ�����ɷ�ҳЧ��
    /// </summary>
    /// <param name="eventData"></param>

    public void OnEndDrag(PointerEventData eventData)
    {
        endMousePositionX = Input.mousePosition.x;
        float offSetX = 0;
        float moveDistance = 0;//������Ҫ�����ľ���
        offSetX = beginMousePositionX - endMousePositionX;

        if (offSetX > 0)//�һ�
        {
            if (currentIndex >= totalItemNum)
            {
                return;
            }
            moveDistance = -moveOneItemLength;
            currentIndex++;
            if (needSendMessage)
            {
                UpdatePanel(true);
            }
        }
        else//��
        {
            if (currentIndex <= 1)
            {
                return;
            }
            moveDistance = moveOneItemLength;
            currentIndex--;
            if (needSendMessage)
            {
                UpdatePanel(false);
            }
        }

        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    /// <summary>
    /// ��ť�����Ʒ���Ч��
    /// </summary>

    public void ToNextPage()
    {
        float moveDistance = 0;
        if (currentIndex >= totalItemNum)
        {
            return;
        }

        moveDistance = -moveOneItemLength;
        currentIndex++;
        if (needSendMessage)
        {
            UpdatePanel(true);
        }
        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    public void ToLastPage()
    {
        float moveDistance = 0;
        if (currentIndex <= 1)
        {
            return;
        }

        moveDistance = moveOneItemLength;
        currentIndex--;

        if (needSendMessage)
        {
            UpdatePanel(false);
        }
        DOTween.To(() => contentTrans.localPosition, lerpValue => contentTrans.localPosition = lerpValue, currentContentLocalPos + new Vector3(moveDistance, 0, 0), 0.5f).SetEase(Ease.OutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePositionX = Input.mousePosition.x;
    }

    //����Content�Ĵ�С
    public void SetContentLength(int itemNum)
    {
        contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x + (cellLength + spacing) * (itemNum - 1), contentTrans.sizeDelta.y);
        totalItemNum = itemNum;
    }

    //��ʼ��Content�Ĵ�С
    public void InitScrollLength()
    {
        contentTrans.sizeDelta = contentTransSize;
    }

    //���ͷ�ҳ��Ϣ�ķ���
    public void UpdatePanel(bool toNext)
    {
            gameObject.SendMessageUpwards("UpdatePanelText");
    }
}