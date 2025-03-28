using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour, IInteractable
{
    public Transform echoVfxSpawnPos;
    public GameObject echoVfxPrefab;
    public Quaternion echoVfxRotation;

    [Header("Interact")]
    public LayerMask whatIsPrayInteract;
    public float interactRange;
    public Transform interactPos;

    private Bell interacterBell;


    public void Interact(GameObject interacter)
    {
        interacterBell = interacter.GetComponent<Bell>();
        SpawnPrayVfxRoutine();
    }

    public void AlterInteract()
    {
        Collider[] altars = Physics.OverlapSphere(interactPos.position, interactRange, whatIsPrayInteract);

        if (altars.Length >= 1)
        {
            for (int i = 0; i < altars.Length; i++)
            {
                IInteractable interact = altars[i].GetComponent<IInteractable>();
                Bell currentBell = altars[i].GetComponent<Bell>();

                if (currentBell != interacterBell && currentBell != this)
                {
                    if (interact != null)
                    {
                        interact.Interact(gameObject);
                    }
                }

            }
            /*foreach (Collider altar in altars)
            {

                IInteractable interact = altar.GetComponent<IInteractable>();
                Bell currentBell = GetComponent<Bell>();

                if(currentBell != interacterBell)
                {
                    if (interact != null)
                    {
                        interact.Interact(gameObject);
                    }
                }

            }*/
        }
    }

    public void SpawnPrayVfxRoutine()
    {
        StartCoroutine(Enum_SpawnPrayVfx());
    }

    IEnumerator Enum_SpawnPrayVfx()
    {
        yield return new WaitForSeconds(1f);
        SpawnPrayVfx();
       
        yield return new WaitForSeconds(1f);
        AlterInteract();

    }

    public void SpawnPrayVfx()
    {
        Instantiate(echoVfxPrefab, echoVfxSpawnPos.position, echoVfxRotation);
        AudioManagerCS.instance.Play("bellToll");
    }

 
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(echoVfxSpawnPos.position, interactRange);
    }
}
