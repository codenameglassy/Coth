using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public GameObject deathVfx;
    public GameObject bloodVfx;
    public Transform deathVfxPos;

    public override void Death()
    {
        Instantiate(deathVfx, deathVfxPos.position, Quaternion.identity);
        EnemySpawnManager.instance.SpawnPigButcher();
        EnemySpawnManager.instance.activeEnemyInScene.Remove(transform);
        base.Death();
    }

    public override void Hurt()
    {
        Instantiate(bloodVfx, deathVfxPos.position, Quaternion.identity);
        base.Hurt();
    }

    public override void Start()
    {
        base.Start();
        EnemySpawnManager.instance.activeEnemyInScene.Add(transform);
    }

    public override void Takedamage(float damageAmount)
    {
        base.Takedamage(damageAmount);
    }
}
