using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragTarget : MonoBehaviour
{
    public bool dragging = false;
    protected bool selectMode = true;
    protected Image grow;
    protected CardListen preView;

    protected void Awake()
    {
        if (GetComponent<CardListen>() != null)
        {
            preView = GetComponent<CardListen>();
            grow = transform.Find("Grow").GetComponent<Image>();
        }
        else
        {
            Debug.Log("没找到CardListen组件");
        }
    }
}
