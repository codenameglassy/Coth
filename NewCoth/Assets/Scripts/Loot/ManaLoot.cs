using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaLoot : MonoBehaviour, ILoot
{
    private bool isLooted = false;
    public GameObject lootVfx;
    public void Loot(GameObject Looter)
    {
        if (isLooted)
        {
            return;
        }
        isLooted = true;
        AudioManagerCS.instance.Play("manaLoot");
        PlayerManaManager.instance.AddMana(25);
        //(lootVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

 
}
