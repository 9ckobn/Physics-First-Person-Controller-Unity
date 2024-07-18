using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IShotable
{
    [SerializeField] private Transform bulletOrigin;

    private Bullet originBulletPrefab;

    private List<Bullet> availableBullet = new();

    private bool canShoot = false;
    private float timeBetweenShotsElapsed = 0;
    private float timeBetweenShots = 0f;

    public void SetupWeapon(Bullet bulletPrefab, int bulletPoolCount, int BPM)
    {
        timeBetweenShots = (BPM / 60) / 100;

        bulletPrefab.gameObject.SetActive(false);

        for (int i = 0; i < bulletPoolCount; i++)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.onDestroy += ReturnBulletToPool;

            availableBullet.Add(bullet);
        }
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        availableBullet.Add(bullet);
    }

    private Bullet GetBullet()
    {
        if (availableBullet[0] == null)
        {
            Debug.Log("Reload!");
            return null;
        }

        var bullet = availableBullet[0];

        availableBullet.RemoveAt(0);

        bullet.transform.position = bulletOrigin.position;
        bullet.transform.rotation = bulletOrigin.rotation;

        return bullet;
    }

    public void Shot()
    {
        if (canShoot)
        {
            canShoot = false;

            var bullet = GetBullet();

            bullet?.gameObject.SetActive(true);
        }
        else
        {
            timeBetweenShotsElapsed += Time.deltaTime;

            if (timeBetweenShotsElapsed >= timeBetweenShots)
            {
                canShoot = true;
            }
        }
    }
}