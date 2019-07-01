using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryWeapon : Weapon
{
    #region OLD CODE
    //public Enemy closestEnemy = null;

    //// Start is called before the first frame update
    //public override void Start()
    //{
    //    base.Start();
    //}

    //// Update is called once per frame
    //public override void Update()
    //{
    //    base.Update();

    //    if (spawned)
    //    {
    //        DetectEnemies();
    //    }

    //    if (closestEnemy != null)
    //    {
    //        //Shooting
    //        currentShootRate -= Time.deltaTime;
    //        if (currentShootRate <= 0)
    //        {
    //            Shoot();
    //        }

    //        //Rotation
    //        Vector3 dir = closestEnemy.transform.position - transform.position;
    //        Quaternion lookRotation = Quaternion.LookRotation(dir);
    //        Vector3 rotation = Quaternion.Lerp(rotatingPivot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    //        rotatingPivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    //    }

    //    else
    //    {
    //        Vector3 rotation = Quaternion.Lerp(rotatingPivot.rotation, startingPivotRotation, Time.deltaTime * turnSpeed).eulerAngles;
    //        rotatingPivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    //    }
    //}

    //public override void SetupSpawnEffect()
    //{
    //    if (spawnEffect != null)
    //    {
    //        spawnedEffect = Instantiate(spawnEffect, transform.position, Quaternion.identity);
    //        ParticleSystem particleSystem = spawnedEffect.GetComponent<ParticleSystem>();
    //        ParticleSystem.MainModule mainModule = particleSystem.main;
    //        mainModule.duration = timeToConstruct;
    //        particleSystem.Play();
    //    }
    //}

    //public override void DetectEnemies()
    //{
    //    //TODO : Currently we are not using any layermask, but we need to use one
    //    //We got an array for the nearest 
    //    Collider[] overlappingColliders = Physics.OverlapSphere(transform.position, weaponRadius);
    //    if (overlappingColliders == null || overlappingColliders.Length == 0)
    //    {
    //        //Nothing nearby
    //        closestEnemy = null;
    //        return;
    //    }

    //    List<Enemy> nearbyEnemies = new List<Enemy>();

    //    for (int i = 0; i < overlappingColliders.Length; i++)
    //    {
    //        Enemy enemy = overlappingColliders[i].GetComponentInParent<Enemy>();
    //        if (enemy != null)
    //        {
    //            nearbyEnemies.Add(enemy);
    //        }
    //    }

    //    if (nearbyEnemies == null || nearbyEnemies.Count == 0)
    //    {
    //        //No enemies nearby
    //        closestEnemy = null;
    //        return;
    //    }

    //    Enemy nearestEnemy = null;
    //    float smallestDistance = Mathf.Infinity;
    //    for (int i = 0; i < nearbyEnemies.Count; i++)
    //    {
    //        float distance = Vector2.Distance(transform.position, nearbyEnemies[i].transform.position);
    //        if (distance < smallestDistance)
    //        {
    //            nearestEnemy = nearbyEnemies[i];
    //            smallestDistance = distance;
    //        }
    //    }

    //    closestEnemy = nearestEnemy;
    //}

    //public override void Shoot()
    //{
    //    currentShootRate = shootRate;

    //    for (int i = 0; i < bulletSpawnPointParent.transform.childCount; i++)
    //    {
    //        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPointParent.transform.GetChild(i).position, Quaternion.identity, _GameManager.Instance.bulletHolder);
    //        bullet.SetTarget(closestEnemy.transform);
    //    }
    //}

    #endregion

    private Enemy closestEnemy;
    public Transform rotatingPivot;

    public override void Update()
    {
        //This is called FOR UI updation
        base.Update();

        if (spawned)
        {
            DetectEnemies(OnGettingClosestEnemy);
        }
    }

    private void DetectEnemies(Action<Enemy> OnDetectionComplete)
    {
        //TODO : Currently we are not using any layermask, but we need to use one
        //We got an array for the nearest 
        Collider[] overlappingColliders = Physics.OverlapSphere(transform.position, weaponDetails.weaponRadius);
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

        OnDetectionComplete(closestEnemy);
    }

    private void OnGettingClosestEnemy(Enemy nearestEnemy)
    {
        if (CanShoot())
        {
            Shoot();
        }

        //Rotate To Closest Enemy
        RotateToClosestEnemy(nearestEnemy);
    }

    private void RotateToClosestEnemy(Enemy nearestEnemy)
    {
        Vector3 dir = closestEnemy.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatingPivot.rotation, lookRotation, Time.deltaTime * weaponDetails.turnRate).eulerAngles;
        rotatingPivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public override void Shoot()
    {
        //Resets the Shooting Rate
        base.Shoot();

        Bullet bullet = Instantiate(weaponDetails.bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity, _GameManager.Instance.bulletHolder);
        bullet.SetTarget(closestEnemy.transform);
    }
}
