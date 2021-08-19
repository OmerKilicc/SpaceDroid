using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public float healt = 200f;
    public Slider healtBar;
    public GameObject deathEffect, powerManager;
    private void Start()
    {
        healtBar.maxValue = healt;
        healtBar.value = healt;
        healtBar.minValue = 0;
    }
    private void FixedUpdate()
    {
        healtBar.value = Mathf.Lerp(healtBar.value, healt, 0.2f);
    }
    public void GetDamage()
    {   
        healt -= 20f;
        if(healt <=0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            //GameOver
            healtBar.value = 0f;
            Invoke("Restart", 3f);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Heart"))
        {
            if (healt <= 180)
            {
                Destroy(collision.gameObject);
                healt += 20;
            }
            else
            {
                Destroy(collision.gameObject);
                healt = 200;
            }
        }
        else if (collision.transform.CompareTag("SlowObj"))
        {
            powerManager.GetComponent<PowerManager>().StopTime();
            Destroy(collision.gameObject);
        }
        if (collision.transform.CompareTag("StopObj"))
        {
            powerManager.GetComponent<PowerManager>().StopTime();
            Destroy(collision.gameObject);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
