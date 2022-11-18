using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public IEnumerator ThrowEffectGenerate(GameObject effect, Vector3 throwPos, int[] horizontalEffect, int[] verticalEffect, float xPos, float yPos, int damage)
    {
        yield return new WaitForSeconds(1);
        if (horizontalEffect.Length != 0)
        {
            for (int i = 0; i < horizontalEffect.Length; i++)
            {
                Vector3 pos = new(horizontalEffect[i] * xPos, 0, 0);
                Instantiate(effect, throwPos + pos, Quaternion.identity);
            }
        }
        if (verticalEffect.Length != 0)
        {
            for (int i = 0; i < verticalEffect.Length; i++)
            {
                Vector3 pos = new(0, verticalEffect[i] * yPos, 0);
                Instantiate(effect, throwPos + pos, Quaternion.identity);
            }
        }
    }

    public void AreaSteal(BattleZone zone, GameObject enemy, GameObject dummy)
    {
        int enemyArea = zone.EnemyArray[0].BattleZoneArray.Count;
        int selfArea = zone.SelfArray[0].BattleZoneArray.Count;
        for (int i = 0; i < zone.EnemyArray.Count; i++)
        {
            if (enemyArea <= zone.EnemyArray[i].BattleZoneArray.Count)
                enemyArea = zone.EnemyArray[i].BattleZoneArray.Count;
        }
        for (int i = 0; i < zone.SelfArray.Count; i++)
        {
            if (enemyArea <= zone.SelfArray[i].BattleZoneArray.Count)
                selfArea = zone.SelfArray[i].BattleZoneArray.Count;
        }
        int enemyPosX = 0;
        int enemyPosY = 0;
        if (enemy != null)
        {

        }
        if (dummy != null)
        {
            enemyPosX = dummy.GetComponent<DummyManager>().horiPos;
            enemyPosY = dummy.GetComponent<DummyManager>().verPos;
        }
        for (int i = 0; i < zone.EnemyArray.Count; i++)
        {
            if (enemyArea == zone.EnemyArray[i].BattleZoneArray.Count)
            {
                if (enemyPosY == i && enemyPosX == zone.EnemyArray[i].BattleZoneArray.Count - 1)
                    continue;
                zone.SelfArray[i].BattleZoneArray.Add(zone.EnemyArray[i].BattleZoneArray[zone.EnemyArray[i].BattleZoneArray.Count - 1]);
                zone.EnemyArray[i].BattleZoneArray.RemoveAt(zone.EnemyArray[i].BattleZoneArray.Count - 1);
                zone.SelfArray[i].BattleZoneArray[zone.SelfArray[i].BattleZoneArray.Count - 1].GetComponent<SpriteRenderer>().color = new Color(0 / 255, 31 / 255, 255 / 255);
            }
        }
    }

    public void Slash(GameObject effect, int[] horizontalEffect, int[] verticalEffect, float xPos, float yPos, Vector3 slashPos)
    {
        if (horizontalEffect.Length != 0)
        {
            for (int i = 0; i < horizontalEffect.Length; i++)
            {
                Vector3 pos = new(horizontalEffect[i] * xPos, 0, 0);
                Instantiate(effect, slashPos + pos, Quaternion.identity);
            }
        }
        if (verticalEffect.Length != 0)
        {
            for (int i = 0; i < verticalEffect.Length; i++)
            {
                Vector3 pos = new(xPos, verticalEffect[i] * yPos, 0);
                Instantiate(effect, slashPos + pos, Quaternion.identity);
            }
        }
    }
}