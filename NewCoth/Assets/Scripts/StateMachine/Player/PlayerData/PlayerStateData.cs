using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerStateData", menuName = "StateData/Player/PlayerStateData")]
public class PlayerStateData : ScriptableObject
{
   
    public float dodgeTime;
    public float attackTime;

    [Header("Interaction")]
    public float prayTime;
    public float castControlTime;
    public float wakeUpTime;

    [Header("Weapon")]
    public float sheatheTime;
    public float unSheatheTime;

    [Header("Elemental Infusion")]
    public float infusionTime;
    public float infuseDelay;
    public float staffInteractTime;
}
