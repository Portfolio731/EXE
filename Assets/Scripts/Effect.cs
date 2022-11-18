using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dummy"))
        {
            collision.GetComponent<DummyManager>().OnDamage(damage);
        }
    }
}
