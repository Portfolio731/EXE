using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>é©êwÇ∆ìGêw </summary>
public class BattleZone : MonoBehaviour
{
    [SerializeField] private List<BattleZoneList> selfArray = new List<BattleZoneList>();
    [SerializeField] private List<BattleZoneList> enemyArray = new List<BattleZoneList>();

    public List<BattleZoneList> SelfArray { get => selfArray; set => selfArray = value; }
    public List<BattleZoneList> EnemyArray { get => enemyArray; set => enemyArray = value; }
}
[System.Serializable]
public class BattleZoneList
{
    [SerializeField] List<GameObject> battleZoneArray = new List<GameObject>();

    public List<GameObject> BattleZoneArray { get => battleZoneArray; set => battleZoneArray = value; }
}
