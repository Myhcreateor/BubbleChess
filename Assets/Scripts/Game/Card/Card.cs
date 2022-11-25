using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public CardDetails cardDetails;
    public abstract void ExecuteCommand();
    public abstract CardDetails GetCardDetails();
}
