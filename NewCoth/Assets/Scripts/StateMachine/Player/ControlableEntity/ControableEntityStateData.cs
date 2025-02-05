using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newControableEntityStateData", menuName = "StateData/Controable/ControableStateData")]
public class ControableEntityStateData : ScriptableObject
{
    public float revertToTplayerTime;
    public float waitTime;
}
