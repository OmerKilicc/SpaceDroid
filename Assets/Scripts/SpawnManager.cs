using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player, enemyPrefab;
    float nextSpawnTime=0;
    public int radius = 8;
    public float coolDown=1.5f;
    void Update()
    {
        if(nextSpawnTime <= Time.timeSinceLevelLoad && PlayerPrefs.GetInt("Time", 1) == 1 && player.activeSelf)
            { 
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = player.transform.position.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = player.transform.position.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = player.transform.position.z;
            Instantiate(enemyPrefab, pos, Quaternion.identity);
             nextSpawnTime = Time.timeSinceLevelLoad + coolDown;
           }
    }
  
}
