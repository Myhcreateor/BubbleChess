using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public PiecesType piecesType;
    public GameObject pieceGo;
    private Transform pieceTrans;
}
public enum PiecesType
{
    write,black
}
