using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BridgeInteractTest : MonoBehaviour, IInteractable
{

    public GameObject bridgePart1;
    public GameObject bridgePart2;
  

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact(GameObject interacter)
    {
        StartCoroutine(Enum_Interact());
    }

    IEnumerator Enum_Interact()
    {
        yield return null;
        bridgePart1.transform.DOLocalMove(new Vector3(0, 2, -5), 1f);
        AudioManagerCS.instance.Play("heavyMove");
        yield return new WaitForSeconds(1f);
        bridgePart2.transform.DOLocalMove(new Vector3(0, 2, 0), 1f);
        AudioManagerCS.instance.Play("heavyMove");
        PlayerManaManager.instance.RemoveMana(50);
    }


}
