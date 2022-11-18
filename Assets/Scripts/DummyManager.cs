using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>プラクティスモードの相手の設定 </summary>
public class DummyManager : MonoBehaviour
{
    public int hp;
    public int verPos, horiPos;
    [SerializeField] GameObject enemyHp;
    public GameObject deathEffectPrefab;
    private void Start()
    {
        enemyHp.GetComponent<Text>().text = "" + hp;
    }
    public void OnDamage(int damage)
    {
        hp -= damage;
        enemyHp.GetComponent<Text>().text = "" + hp;
        if (hp <= 0)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(enemyHp);
            Destroy(gameObject);
        }
    }
}
