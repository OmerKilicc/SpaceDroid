using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    private Rigidbody2D rb;
    Animator anim;
    public float moveSpeed;
    public float currentSpeed;
    private Vector2 movement;
    public GameObject deathEffect, crashEffect;
    public GameObject[] dropItems;
    public float Healt = 100f;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        moveSpeed= Random.Range(3,7f);
        currentSpeed = moveSpeed;
        player = GameObject.Find("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.activeSelf)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
        else if(player.GetComponent<PlayerManager>().healt <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
        if (PlayerPrefs.GetInt("Time", 1) == 0)
        {
            currentSpeed = 0;
            anim.speed = 0;
        }
        else if(PlayerPrefs.GetInt("Time", 1) == 1)
        {
            currentSpeed = moveSpeed;
            anim.speed = 1;
        }
        else if(PlayerPrefs.GetInt("Time", 1) == -1)
        {
            currentSpeed = moveSpeed / 2;
            anim.speed = 0.5f;
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * currentSpeed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().GetDamage();
            Instantiate(crashEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void GetDamage()
    {
        Healt -= 33.5f;
        if(Healt <= 0)
        {
            PlayerPrefs.SetInt("KilledEnemy", PlayerPrefs.GetInt("KilledEnemy", 0) + 1);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
            if (Random.value <= 0.25) // drop heart, %25 chance.
            {
                var heart = Instantiate(dropItems[0], transform.position, Quaternion.identity);
                Destroy(heart, 10f);
            }
            else if (PlayerPrefs.GetInt("KilledEnemy", 0) % 5 == 0 && PlayerPrefs.GetInt("KilledEnemy", 0) != 0)
            {
                if (PlayerPrefs.GetInt("KilledEnemy", 0) % 10 == 0 && Random.value <= 0.50f) // drop Stop.
                {
                    var dropObj = Instantiate(dropItems[1], transform.position, Quaternion.identity);
                    Destroy(dropObj, 10f);
                    //stop
                }
                else if (PlayerPrefs.GetInt("KilledEnemy", 0) % 10 != 0 && Random.value <= 0.50f) // drop Slow.
                {
                    var dropObj = Instantiate(dropItems[2], transform.position, Quaternion.identity);
                    Destroy(dropObj, 10f);
                    //slow
                }
               
            }
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score",0)+10);
        }
    }
    

}
