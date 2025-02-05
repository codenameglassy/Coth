using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerEntityData", menuName = "EntityData/Player/PlayerEntityData")]

public class PlayerEntityData : ScriptableObject
{
    [Header("Combat")]
    public float attackDeltaDistance;
    public float checkEnemyRange;
    public LayerMask whatIsEnemy;
    public float attackRange;

    [Header("Pray - Interactive")]
    public GameObject prayVfxPrefab;
    public GameObject prayPulseVfxPrefab;
    public Quaternion prayvfxRotaion;
    public LayerMask whatIsPrayInteract;
    public float interactRange;

    [Header("Control - Interactive")]
    public GameObject castControlVfx;
}
