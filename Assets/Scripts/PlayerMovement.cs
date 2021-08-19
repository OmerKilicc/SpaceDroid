using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnim;
    public float speed, bulletSpeed, coolDown;
    float nextShoot = 0;
    public GameObject bullet, muzzle1, muzzle2, shootButton;
    public Joystick joyStickMove, rotationStick;
    bool muzzleChooser = true;
    GameObject obj;
    Vector3 target;
    public AudioSource Sound;
    float lookAngleValue = -270;
 
    void FixedUpdate()
    {  //MOVEMENT
        transform.Translate(Vector3.up * joyStickMove.Vertical * speed * Time.deltaTime);
        transform.Translate(Vector2.right * joyStickMove.Horizontal * speed * Time.deltaTime);
        //ROTATİON AND AİM
        if (rotationStick.Horizontal != 0 && rotationStick.Vertical != 0)
        {
            Vector3 mousePos = new Vector3(0, 0, 0f) - transform.position;
            mousePos.x = rotationStick.Horizontal;
            mousePos.y = rotationStick.Vertical;
            float direction = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; //Convert Vector to Degree
            Quaternion lookRotation = Quaternion.AngleAxis(direction - lookAngleValue, Vector3.forward);
            transform.GetChild(0).rotation = lookRotation;
            if (nextShoot <= Time.timeSinceLevelLoad)
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        if (nextShoot <= Time.timeSinceLevelLoad)
        {
            float shootDistance = Mathf.Abs(joyStickMove.Horizontal) + Mathf.Abs(joyStickMove.Vertical);
            if (shootDistance > 1 || shootDistance == 0)
            {
                shootDistance = 1;
            }

            if (muzzleChooser)
            {
                obj = Instantiate(bullet, muzzle1.transform.position, Quaternion.identity);
                target = transform.position + transform.GetChild(0).up * shootDistance * 10f - muzzle1.transform.position;
                muzzleChooser = false;
                playerAnim.SetTrigger("ShootLeft");
            }
            else
            {
                obj = Instantiate(bullet, muzzle2.transform.position, Quaternion.identity);
                target = transform.position + transform.GetChild(0).up * shootDistance * 10f - muzzle2.transform.position;
                muzzleChooser = true;
                playerAnim.SetTrigger("ShootRight");
            }
            obj.GetComponent<BulletManager>().direction = target;
            nextShoot = Time.timeSinceLevelLoad + coolDown;
            Sound.Play();
        }

    } 
}
