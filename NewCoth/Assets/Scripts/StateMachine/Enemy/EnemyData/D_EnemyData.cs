using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyEntityData", menuName = "EntityData/Enemy/EnemyEntityData")]
public class D_EnemyData : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public float checkPlayerInMaxDistance;
    public float checkPlayerInMinDistance;

    public float attackRange;
}
