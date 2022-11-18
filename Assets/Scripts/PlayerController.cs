using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

/// <summary>対戦中の操作 </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] BattleZone battleZone;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject dummy;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject chargeBulletPrefab;
    [SerializeField] GameObject chargeEffect;
    [SerializeField] ChipDeck chipDeck;
    [SerializeField] CustomGauge customGauge;
    [SerializeField] EffectManager effectManager;
    [SerializeField] SpriteRenderer holdChip;
    public UnityAction chipSelectDisplay;
    public List<int> chipNum;
    int[] selectChipNum;
    int hp;
    float chargeTime;
    float horiMove = 1.58f;
    float verMove = 0.82f;
    int verPos, horiPos;
    Vector3 shotPos;
    Vector3 pos = new Vector3(0.4f, 0, 0);
    public void HandleUpdate()
    {
        PlayerMove();
        HoldChip(chipNum);
        ChipCheck(chipNum, chipDeck);
        ChipSelectDisplay();

    }
    /// <summary>移動 </summary>
    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (verPos != 0)
            {
                if (battleZone.SelfArray[verPos - 1].BattleZoneArray[horiPos] != null && verPos >= 1)
                {
                    verPos -= 1;
                    this.transform.Translate(0, verMove, 0);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (verPos <= 2)
            {
                if (battleZone.SelfArray[verPos + 1].BattleZoneArray[horiPos] != null)
                {
                    verPos += 1;
                    this.transform.Translate(0, -verMove, 0);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (horiPos < battleZone.SelfArray[verPos].BattleZoneArray.Count - 1)
            {
                horiPos += 1;
                this.transform.Translate(horiMove, 0, 0);
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (horiPos >= 1)
            {
                horiPos -= 1;
                this.transform.Translate(-horiMove, 0, 0);
            }
        }
        Shot();
    }
    /// <summary> 射撃 </summary>
    void Shot()
    {
        if (Input.GetMouseButton(0))
        {//時間を足す
            chargeTime += Time.deltaTime;
            if (chargeTime >= 3)
            {
                chargeEffect.SetActive(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (chargeTime < 3)
            {//通常弾
                shotPos = transform.position;
                shotPos += pos;
                Instantiate(bulletPrefab, shotPos, Quaternion.identity);
                SoundManager.Instance.PlaySE(SESoundData.SE.Shot);
            }
            else if (chargeTime >= 3)
            {//チャージショット
                chargeEffect.SetActive(false);
                shotPos = transform.position;
                shotPos += pos;
                Instantiate(chargeBulletPrefab, shotPos, Quaternion.identity);
                SoundManager.Instance.PlaySE(SESoundData.SE.ChargeShot);
            }
            chargeTime = 0;
        }
    }

    void HoldChip(List<int> chipNum)
    {
        if (chipNum.Count != 0)
        {
            for (int i = 0; i < chipDeck.AttackChips.Count; i++)
            {
                if (chipNum[0] == chipDeck.AttackChips[i].Number)
                {
                    holdChip.color = new Color(255 / 255, 255 / 255, 255 / 255, 255 / 255);
                    holdChip.sprite = chipDeck.AttackChips[i].Image;
                }
            }
        }
        if (chipNum.Count == 0)
        {
            holdChip.color = new Color(255 / 255, 255 / 255, 255 / 255, 0 / 255);
        }
    }
    void ChipCheck(List<int> chipNum, ChipDeck chipDeck)
    {
        if (Input.GetMouseButtonDown(1) && chipNum.Count != 0)
            for (int k = 0; k < chipDeck.AttackChips.Count; k++)
            {
                if (chipNum[0] == chipDeck.AttackChips[k].Number)
                {
                    GameObject obj = chipDeck.AttackChips[k].AttackObj;
                    GameObject obj2 = chipDeck.AttackChips[k].AttackObj2;
                    UseChip(obj, obj2, chipDeck.AttackChips[k].Type,
                        chipDeck.AttackChips[k].HorizonEffect, chipDeck.AttackChips[k].VerticalEffect, chipDeck.AttackChips[k].Damage);
                    chipNum.RemoveAt(0);
                    break;
                }
            }
    }

    void UseChip(GameObject obj, GameObject obj2, ChipType type, int[] horizontalEffect, int[] verticalEffect, int damage)
    {
        //Typeごとに処理
        if (ChipType.Throw == type)
        {
            GameObject throwObj = Instantiate(obj, transform.position, Quaternion.identity);
            SpriteRenderer throwObjSr = throwObj.GetComponent<SpriteRenderer>();
            throwObjSr.transform.DOJump(transform.position + new Vector3(horiMove * 3, -0.3f), 3, 1, 1);
            if (obj2 != null)
            {
                StartCoroutine(effectManager.ThrowEffectGenerate(obj2, transform.position + new Vector3(horiMove * 3, -0.3f)
                    , horizontalEffect, verticalEffect, horiMove, verMove, damage));
            }
        }
        if (ChipType.Steal == type)
        {
            effectManager.AreaSteal(battleZone, enemy, dummy);
        }

        if (ChipType.Slash == type)
        {
            Vector3 slashPos = new Vector3(horiMove, -0.3f);
            effectManager.Slash(obj, horizontalEffect, verticalEffect, horiMove, verMove, transform.position);
        }
    }

    void ChipSelectDisplay()
    {
        if (customGauge.seconds >= 12 && Input.GetKeyDown(KeyCode.Q))
        {
            customGauge.chargeGauge = false;
            customGauge.chipSelect = false;
            customGauge.seconds = 0;
            chipSelectDisplay();
        }
    }
}