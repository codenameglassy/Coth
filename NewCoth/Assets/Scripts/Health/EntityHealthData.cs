using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityHealthData", menuName = "EntityHealthData/EntityHealthData")]
public class EntityHealthData : ScriptableObject
{
    public float maxHealth;
    public GameObject hitVfx;
    public GameObject deathVfx;

}
