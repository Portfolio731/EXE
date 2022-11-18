using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//チップ（攻撃のみ）のマスターデータ:外部から変更しない
[CreateAssetMenu]
public class AttackChipBase : ScriptableObject
{
    //チップ番号、名前、画像、説明、与ダメ、回復、自傷、横方向範囲、縦方向範囲、オブジェクト
    [SerializeField] private int number;
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private int damage;
    [SerializeField] private int recover;
    [SerializeField] private int recoil;
    [SerializeField] private int[] horizonEffect;
    [SerializeField] private int[] verticalEffect;
    [SerializeField] private GameObject attackObj, attackObj2;
    [SerializeField] private ChipType type;
    [SerializeField] private int time;

    public int Number
    { get => number; }
    public string Name { get => name; }
    public Sprite Image { get => image; }
    public string Description { get => description; }
    public int Damage { get => damage; }
    public int Recover { get => recover; }
    public int Recoil { get => recoil; }
    public int[] HorizonEffect { get => horizonEffect; }
    public int[] VerticalEffect { get => verticalEffect; }
    public GameObject AttackObj { get => attackObj; }
    public GameObject AttackObj2 { get => attackObj2; }
    public ChipType Type { get => type; }
    public int Time { get => time; }
}

public enum ChipType
{
    Throw,
    Fall,
    Steal,
    Slash
}
