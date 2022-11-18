using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>’Êí‚Ì’e‚Ìİ’è </summary>
public class BulletManager : MonoBehaviour
{
    public GameObject impactPrefab;
    public Vector3 impactPos;
    public float speed = 10f;
    public int damage;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dummy"))
        {
            collision.GetComponent<DummyManager>().OnDamage(damage);
            Instantiate(impactPrefab, transform.position + impactPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
