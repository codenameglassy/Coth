using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCompanionEntityData", menuName = "EntityData/Companion/NewEntityData")]
public class CompanionEntityData : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public float checkPlayerInMinRange;
}
