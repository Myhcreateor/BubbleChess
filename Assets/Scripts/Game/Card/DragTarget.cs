using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    public bool dragging = false;
    protected bool selectMode = true;
    protected CardListen preView;

    protected void Awake()
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
}
