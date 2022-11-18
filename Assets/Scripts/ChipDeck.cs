using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDeck : MonoBehaviour
{
    //ScriptableObject‚ðŠi”[
    [SerializeField] List<AttackChipBase> attackChips;

    public List<AttackChipBase> AttackChips { get => attackChips; set => attackChips = value; }

}
