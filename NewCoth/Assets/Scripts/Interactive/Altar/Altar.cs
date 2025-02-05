using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Altar : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject altarMovingGO;
    [SerializeField] private float finalAtarMoveGoPos;
    [SerializeField] private float moveTime;
    [SerializeField] private GameObject altarVfxCrystal;
    // Start is called before the first frame update
    void Start()
    {
        altarVfxCrystal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interacter)
    {
        altarMovingGO.transform.DOMove(new Vector3(altarMovingGO.transform.position.x, altarMovingGO.transform.position.y + finalAtarMoveGoPos,
            altarMovingGO.transform.position.z), moveTime);
        altarVfxCrystal.SetActive(true);
        AudioManagerCS.instance.Play("altarInteract");
        Debug.Log("Interacted with " + gameObject.name);
    }

}
