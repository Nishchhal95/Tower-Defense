using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, IShootable
{
    public string weaponName;
    public float timeToConstruct;
    public float damage;
    public GameObject spawnEffect;
    public float weaponRadius;
    public Transform rotatingPivot;
    public Enemy closestEnemy = null;
    public Quaternion startingPivotRotation;
    public Transform bulletSpawnPointParent;
    public Bullet bulletPrefab;

    //One bulle every shootRade of a second
    public float shootRate = 1f;
    public float currentShootRate = 0;

    public float turnSpeed = 5f;

    public float timeLeftToSpawn;
    public bool startingSpawn = false;
    public bool spawned = false;
    public GameObject spawnedEffect;

    // Start is called before the first frame update
    public virtual void Start()
    {
        gameObject.AddComponent<WeaponGizmo>().radius = weaponRadius;
        startingPivotRotation = rotatingPivot.rotation;
    }

    // Update is called once per frame
    public virtual void Update()
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

    public virtual void SetupSpawnEffect()
    {
        Debug.Log("SetupSpawnEffect");
    }

    public virtual void DetectEnemies()
    {
        Debug.Log("Detect Enemies");
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

    public virtual void Shoot()
    {
        Debug.Log("Shoot");
    }

    //Faltu Funnctions
    public float GetPercentageRemainingToSpawn()
    {
        //remainingPercentage = 
        return ((100 / timeToConstruct) * timeLeftToSpawn);
        //return remainingPercentage;
    }
}
