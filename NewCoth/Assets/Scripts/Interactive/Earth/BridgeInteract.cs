using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BridgeInteract : MonoBehaviour, IInteractable
{
    public Vector3 finalPos;
    public float moveTime;

    public void Interact(GameObject interacter)
    {
        transform.DOLocalMove(finalPos, moveTime);
    }
}
