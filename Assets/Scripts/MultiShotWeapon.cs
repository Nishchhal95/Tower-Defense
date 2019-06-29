using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotWeapon : Weapon
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void SetupSpawnEffect()
    {
        if (spawnEffect != null)
        {
            spawnedEffect = Instantiate(spawnEffect, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = spawnedEffect.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.duration = timeToConstruct;
            particleSystem.Play();
        }
    }

    public override void DetectEnemies()
    {
        //TODO : Currently we are not using any layermask, but we need to use one
        //We got an array for the nearest 
        Collider[] overlappingColliders = Physics.OverlapSphere(transform.position, weaponRadius);
        if (overlappingColliders == null || overlappingColliders.Length == 0)
        {
            //Nothing nearby
            closestEnemy = null;
            return;
        }

        List<Enemy> nearbyEnemies = new List<Enemy>();

        for (int i = 0; i < overlappingColliders.Length; i++)
        {
            Enemy enemy = overlappingColliders[i].GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                nearbyEnemies.Add(enemy);
            }
        }

        if (nearbyEnemies == null || nearbyEnemies.Count == 0)
        {
            //No enemies nearby
            closestEnemy = null;
            return;
        }

        Enemy nearestEnemy = null;
        float smallestDistance = Mathf.Infinity;
        for (int i = 0; i < nearbyEnemies.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, nearbyEnemies[i].transform.position);
            if (distance < smallestDistance)
            {
                nearestEnemy = nearbyEnemies[i];
                smallestDistance = distance;
            }
        }

        closestEnemy = nearestEnemy;
    }

    public override void Shoot()
    {
        currentShootRate = shootRate;

        for (int i = 0; i < bulletSpawnPointParent.transform.childCount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPointParent.transform.GetChild(i).position, Quaternion.identity, _GameManager.Instance.bulletHolder);
            bullet.SetTarget(closestEnemy.transform);
        }
    }
}
