using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
public class PowerManager : MonoBehaviour
{
    public float timer;
    public float coolDown, nextJumpTime;
    public GameObject player, teleportEffect, volumeObj;
    public Image teleportImage;
    PostProcessVolume volume;
    public Joystick joyStick;
    ChromaticAberration chromatic;
    float chromValue=0;
    bool isTeleporting=false;
    private void Start()
    {
        PlayerPrefs.SetInt("Time", 1);
        teleportImage.fillAmount = 1;
        volume = volumeObj.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromatic);
    }
    void Update()
    {   
        //CircleSliderValues
        if (nextJumpTime >= Time.timeSinceLevelLoad)
        {
            teleportImage.fillAmount = (coolDown - (nextJumpTime - Time.timeSinceLevelLoad)) / coolDown;
        }
        else
        {
            teleportImage.fillAmount = 1;
        }
   
        // Teleport Chromatic Effect 
        if (chromValue != 0)
        {
            chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, chromValue, 0.2f);
        }
        else if(chromatic.intensity.value != 0 && chromValue == 0)
        {
            chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, chromValue , 0.1f);
        }
   }
    public void StartTeleport()
    {//Start Teleporting
        if (nextJumpTime <= Time.timeSinceLevelLoad && player.GetComponent<PlayerManager>().healt > 0)
        {
            float teleportDistance = Mathf.Abs(joyStick.Horizontal) + Mathf.Abs(joyStick.Vertical);
           if (teleportDistance > 1 || teleportDistance == 0)
            {
                teleportDistance = 1;
            }
            Instantiate(teleportEffect, player.transform.position, Quaternion.identity);
            player.SetActive(false);
            player.transform.DOMove(player.transform.position + player.transform.GetChild(0).up * teleportDistance * 10f, 0.6f);
            Invoke("Teleport", 0.5f);
            chromValue = 1f;
            isTeleporting = true;
            PlayerPrefs.SetInt("Time", -1);
            nextJumpTime = coolDown + Time.timeSinceLevelLoad;
        }
    }
    void Teleport()
    {
        Instantiate(teleportEffect, player.transform.position, Quaternion.identity);
        Invoke("EndTeleport", 0.1f);
    }
    void EndTeleport()
    {// end Teleporting
        isTeleporting = false;
        PlayerPrefs.SetInt("Time", 1);
        chromValue = 0;
        chromatic.intensity.value = 0;
        player.SetActive(true);

    }
    public void StopTime()
    {
       if(PlayerPrefs.GetInt("Time", 1) == 1)
        {
            chromValue = 0.3f;
            PlayerPrefs.SetInt("Time", 0);
            Invoke("StartTime", timer);
        }
        
    }
    public void SetSlowTime()
    {
        if (PlayerPrefs.GetInt("Time", 1) == 1)
        {
            chromValue = 0.2f;
            PlayerPrefs.SetInt("Time", -1);
            Invoke("StartTime", timer);
        }

    }
    void StartTime()
    {
        if (!isTeleporting)
        {
            chromValue = 0f;
        }
        PlayerPrefs.SetInt("Time", 1);
    }

}
