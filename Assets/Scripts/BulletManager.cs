using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float speed;
    Vector3 defaultPos;
    public Vector3 direction;
    public GameObject hitEffect;
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage();
            var effect= Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.transform.SetParent(collision.transform);
            Destroy(gameObject);
        }
    }
}
