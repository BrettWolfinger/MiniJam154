using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatMasterList", menuName = "MiniJam154/NewStatMasterList", order = 0)]
public class StatsMasterListSO : ScriptableObject
{
    [field:SerializeField] public UpgradeableStatSO[] statsList {get;private set;}

    [field:SerializeField] public UpgradeableStatSO Ammo {get;private set;}
    [field:SerializeField] public UpgradeableStatSO MaxHealth {get;private set;}
    [field:SerializeField] public UpgradeableStatSO Speed {get;private set;}
}
