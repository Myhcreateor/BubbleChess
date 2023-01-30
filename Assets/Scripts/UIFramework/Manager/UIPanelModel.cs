using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIPanelModel_SO", menuName = "Model/UIPanelModel_SO")]
public class UIPanelModel: ScriptableObject
{
    public List<UIPanelInfo> uiPanelInfoList;
}
[System.Serializable]
public class UIPanelInfo
{
    public UIPanelType panelType;
    public string path;
}