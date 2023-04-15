using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public float m_PowerUpMovementSpeed = 10.0f;
    public static bool GoingOn = true;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * m_PowerUpMovementSpeed * Time.deltaTime, Space.World);

        if(transform.position.z < PlayerMovement.ultimateZSizeBackwardLimit) {
            GoingOn = false;
            Destroy(this);
            Destroy(this.gameObject);   
        }
    }


    private void OnTriggerEnter(Collider collision) {
        if(collision.transform.tag == "Player") {
            PlayerShootAction a = collision.GetComponent<PlayerShootAction>();
            GunType b = GunType.LazerPrefabOddNumberShot;
            GunScriptableObject gun = a.Guns.Find(gun => gun.m_Type == b);
            a.ActiveGun = gun;
            a.ActiveGun.Spawn(a);

            PowerUpHelper.CanUsePowerUp = true;
            a.PowerUpSeconds = Mathf.FloorToInt(Time.time);

            GoingOn = false;
            Destroy(this);
            Destroy(this.gameObject);
        }
    }
}
