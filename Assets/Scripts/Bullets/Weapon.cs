using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Firepoints")]
    public Transform firePoint;
    public Transform firePoint2;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    public Bullet bullet1, bullet2, bullet3;
    public GameObject flameThrower;
    ParticleSystem ps;

    bool isBullet2 = false;
    private PlayerAttributes playerStats;
    // Start is called before the first frame update
    void Start()
    {
        ps = flameThrower.GetComponent<ParticleSystem>();
        ps.Stop();
        bulletPrefab = bullet1;
        playerStats = gameObject.GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }
        if (Input.GetButtonDown("Weapon 1"))
        {
            bulletPrefab = bullet1;
            isBullet2 = false;
        }
        else if (Input.GetButtonDown("Weapon 2"))
        {
            bulletPrefab = bullet2;
            isBullet2 = true;
        }
        else if (Input.GetButtonDown("Weapon 3"))
        {
            bulletPrefab = bullet3;
            isBullet2 = false;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ps.Stop();
        }
        if (isBullet2)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (isBullet2)
        {
            if (playerStats.getCurrentHeat() > 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    ps.Play();
                }
                PlayerMovement player = GameObject.Find("Player2").GetComponent<PlayerMovement>();
                Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.playerDir = player.dir;
                playerStats.SetCurrentHeat(playerStats.getCurrentHeat() - 1);
            } else
            {
                ps.Stop();
            }
        } else
        {
            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }

    }
}
