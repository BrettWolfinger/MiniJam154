using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeableStat", menuName = "MiniJam154/CreateNewStat", order = 0)]
public class UpgradeableStatSO : ScriptableObject
{
    [field:SerializeField] public float baseValue {get;private set;}
    [field:SerializeField] public float changePerUpgrade {get;private set;}
    [field:SerializeField] public float costToUpgrade {get;private set;}
}
