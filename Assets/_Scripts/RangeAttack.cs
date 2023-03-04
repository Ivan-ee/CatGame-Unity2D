using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool UpView = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Attack();
    }

    private void Attack()
    {
        var bullet = Instantiate(bulletPref, gun.position, gun.rotation);
        if(UpView)
            bullet.GetComponent<Rigidbody2D>().AddForce(gun.up * bulletSpeed, ForceMode2D.Impulse);
        else
            bullet.GetComponent<Rigidbody2D>().AddForce(gun.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
