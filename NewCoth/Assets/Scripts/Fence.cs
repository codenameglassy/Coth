using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour, IDamageable
{
    public GameObject destructVfx;
    public Vector3 destructVfxSpawnOffsetPos;
   
    public void Takedamage(float damageAmount)
    {
        AudioManagerCS.instance.Play("woodBreak");
        // Offset the spawn position
        Vector3 spawnPosition = transform.position + destructVfxSpawnOffsetPos;

        //Instantiate(destructVfx, spawnPosition, Quaternion.identity);
        Destroy(gameObject);

    }
}
