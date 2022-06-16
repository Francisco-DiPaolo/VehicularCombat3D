using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    private bool canShot;

    void Start()
    {
        canShot = true;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shooting(bulletPrefab, bulletForce);
        }
    }

    void Shooting(GameObject bulletPrefab, float bulletForce)
    {
        if (!canShot) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        StartCoroutine(ShotCD(3f));

        Destroy(bullet, 30);
    }

    public IEnumerator ShotCD(float delay)
    {
        canShot = false;
        yield return new WaitForSeconds(delay);
        canShot = true;
    }
}
