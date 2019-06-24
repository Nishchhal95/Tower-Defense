using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float timeToConstruct;
    public float damage;
    public GameObject spawnEffect;
    public float weaponRadius;
    public Transform rotatingPivot;
    public Enemy closestEnemy = null;
    public Quaternion startingPivotRotation;
    public Transform bulletSpawnPoint;
    public Bullet bulletPrefab;

    //One bulle every shootRade of a second
    public float shootRate = 1f;
    private float currentShootRate = 0;

    public float turnSpeed = 5f;

    private float timeLeftToSpawn;
    private bool startingSpawn = false;
    private bool spawned = false;
    private GameObject spawnedEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<WeaponGizmo>().radius = weaponRadius;
        startingPivotRotation = rotatingPivot.rotation;
        //Only If we want to delay the first shot
        //currentShootRate = shootRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(startingSpawn)
        {
            timeLeftToSpawn -= Time.deltaTime;
            UpdateCanvas();
        }

        if(timeLeftToSpawn <= 0)
        {
            startingSpawn = false;
            SpawnObject();
        }

        if(spawned)
        {
            DetectEnemies();
        }

        if (closestEnemy != null)
        {
            //Shooting
            currentShootRate -= Time.deltaTime;
            if(currentShootRate <= 0)
            {
                Shoot();
            }

            //Rotation
            Vector3 dir = closestEnemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(rotatingPivot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            rotatingPivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        else
        {
            Vector3 rotation = Quaternion.Lerp(rotatingPivot.rotation, startingPivotRotation, Time.deltaTime * turnSpeed).eulerAngles;
            rotatingPivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public void Spawning()
    {
        if(timeToConstruct <= 0)
        {
            //Need to reduce number of These FInd Calls by pre refrencing them

            SpawnObject();
            return;
        }

        gameObject.transform.Find("GFX").gameObject.SetActive(false);
        gameObject.transform.Find("Canvas").gameObject.SetActive(true);

        timeLeftToSpawn = timeToConstruct;
        startingSpawn = true;
        //Time To Update Canvas
        //Currently Canvas Updating in Update Method

        SetupSpawnEffect();
    }

    private void UpdateCanvas()
    {
        //Divde by 100 to make in a scale of 0 to 1
        gameObject.transform.Find("Canvas").transform.Find("Slider").transform.Find("BG").transform.Find("Filler").GetComponent<Image>().fillAmount = GetPercentageRemainingToSpawn() / 100;
    }

    private void SpawnObject()
    {
        spawned = true;
        gameObject.transform.Find("GFX").gameObject.SetActive(true);
        gameObject.transform.Find("Canvas").gameObject.SetActive(false);

        if(spawnedEffect != null)
        {
            Destroy(spawnedEffect);
        }
    }

    private void SetupSpawnEffect()
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

    private void DetectEnemies()
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
            if(distance < smallestDistance)
            {
                nearestEnemy = nearbyEnemies[i];
                smallestDistance = distance;
            }
        }

        closestEnemy = nearestEnemy;

        //TODO : Add Smooth rotation 
        RotateToClosestEnemy(closestEnemy);

    }

    private void RotateToClosestEnemy(Enemy enemy)
    {
        
    }

    private void DefaultRotation()
    {
        GameObject nearestPath = null;
        float nearestPathDistance = Mathf.Infinity;
        Collider[] overlappingColliders = Physics.OverlapSphere(transform.position, weaponRadius);
        if (overlappingColliders == null || overlappingColliders.Length == 0)
        {
            return;
        }

        for (int i = 0; i < overlappingColliders.Length; i++)
        {
            if (overlappingColliders[i].transform.name.Contains("PathCube"))
            {
                float distance = Vector2.Distance(transform.position, overlappingColliders[i].transform.position);

                if(distance < nearestPathDistance)
                {
                    nearestPathDistance = distance;
                    nearestPath = overlappingColliders[i].transform.parent.gameObject;
                }
            }
        }

        transform.LookAt(nearestPath.transform);
    }

    private void Shoot()
    {
        currentShootRate = shootRate;

        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.SetTarget(closestEnemy.transform);
    }

    //Faltu Funnctions
    private float GetPercentageRemainingToSpawn()
    {
        //remainingPercentage = 
        return ((100 / timeToConstruct) * timeLeftToSpawn);
        //return remainingPercentage;
    }
}
