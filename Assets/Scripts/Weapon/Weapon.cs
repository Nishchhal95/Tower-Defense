using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//When We Add Component and Call a method then Sequence becomes 
//Awake() -> OnEnable() -> That Method Call We made after adding component() -> Start()

[RequireComponent(typeof(WeaponUI), typeof(WeaponSFX))]
public abstract class Weapon : MonoBehaviour, IShootable
{
    #region OLD CODE
    //public string weaponName;
    //public float timeToConstruct;
    //public float damage;

    //public GameObject spawnEffect;

    //public float weaponRadius;

    //public Transform rotatingPivot;
    //public Quaternion startingPivotRotation;
    //public Transform bulletSpawnPointParent;
    //public Bullet bulletPrefab;

    ////One bulle every shootRade of a second
    //public float shootRate = 1f;
    //public float currentShootRate = 0;

    //public float turnSpeed = 5f;

    //public float timeLeftToSpawn;
    //public bool startingSpawn = false;
    //public bool spawned = false;
    //public GameObject spawnedEffect;

    //// Start is called before the first frame update
    //public virtual void Start()
    //{
    //    gameObject.AddComponent<WeaponGizmo>().radius = weaponRadius;
    //    startingPivotRotation = rotatingPivot.rotation;
    //}

    //// Update is called once per frame
    //public virtual void Update()
    //{
    //    if(startingSpawn)
    //    {
    //        timeLeftToSpawn -= Time.deltaTime;
    //        UpdateCanvas();
    //    }

    //    if(timeLeftToSpawn <= 0)
    //    {
    //        startingSpawn = false;
    //        SpawnObject();
    //    }
    //}

    //public void Spawning()
    //{
    //    if(timeToConstruct <= 0)
    //    {
    //        //Need to reduce number of These FInd Calls by pre refrencing them

    //        SpawnObject();
    //        return;
    //    }

    //    gameObject.transform.Find("GFX").gameObject.SetActive(false);
    //    gameObject.transform.Find("Canvas").gameObject.SetActive(true);

    //    timeLeftToSpawn = timeToConstruct;
    //    startingSpawn = true;
    //    //Time To Update Canvas
    //    //Currently Canvas Updating in Update Method

    //    SetupSpawnEffect();
    //}

    //private void UpdateCanvas()
    //{
    //    //Divde by 100 to make in a scale of 0 to 1
    //    gameObject.transform.Find("Canvas").transform.Find("Slider").transform.Find("BG").transform.Find("Filler").GetComponent<Image>().fillAmount = GetPercentageRemainingToSpawn() / 100;
    //}

    //private void SpawnObject()
    //{
    //    spawned = true;
    //    gameObject.transform.Find("GFX").gameObject.SetActive(true);
    //    gameObject.transform.Find("Canvas").gameObject.SetActive(false);

    //    if(spawnedEffect != null)
    //    {
    //        Destroy(spawnedEffect);
    //    }
    //}

    //public virtual void SetupSpawnEffect()
    //{
    //    Debug.Log("SetupSpawnEffect");
    //}

    //public virtual void DetectEnemies()
    //{
    //    Debug.Log("Detect Enemies");
    //}

    //private void RotateToClosestEnemy(Enemy enemy)
    //{

    //}

    //private void DefaultRotation()
    //{
    //    GameObject nearestPath = null;
    //    float nearestPathDistance = Mathf.Infinity;
    //    Collider[] overlappingColliders = Physics.OverlapSphere(transform.position, weaponRadius);
    //    if (overlappingColliders == null || overlappingColliders.Length == 0)
    //    {
    //        return;
    //    }

    //    for (int i = 0; i < overlappingColliders.Length; i++)
    //    {
    //        if (overlappingColliders[i].transform.name.Contains("PathCube"))
    //        {
    //            float distance = Vector2.Distance(transform.position, overlappingColliders[i].transform.position);

    //            if(distance < nearestPathDistance)
    //            {
    //                nearestPathDistance = distance;
    //                nearestPath = overlappingColliders[i].transform.parent.gameObject;
    //            }
    //        }
    //    }

    //    transform.LookAt(nearestPath.transform);
    //}

    //public abstract void Shoot();

    ////Faltu Funnctions
    //public float GetPercentageRemainingToSpawn()
    //{
    //    //remainingPercentage = 
    //    return ((100 / timeToConstruct) * timeLeftToSpawn);
    //    //return remainingPercentage;
    //}

    //Some Weapon Properties


    #endregion

    public WeaponUI weaponUI;
    public WeaponSFX weaponSFX;
    public WeaponDetails weaponDetails;

    private float timeLeftToSpawn;
    private float currentShootRate;

    protected bool spawned = false;

    private GameObject weaponGFXgameObject;
    private GameObject weaponUIgameObject;

    [SerializeField] protected Transform bulletSpawnPoint;

    private void Awake()
    {
        Debug.Log("Weapon Awake");
        weaponUI = GetComponent<WeaponUI>();
        weaponSFX = GetComponent<WeaponSFX>();

        weaponGFXgameObject = transform.Find("GFX").gameObject;
        weaponUIgameObject = transform.Find("Canvas").gameObject;

        //Enable UI and Disable GFX
        weaponUIgameObject.SetActive(true);
        weaponGFXgameObject.SetActive(false);

        timeLeftToSpawn = weaponDetails.timeToConstruct;
        currentShootRate = weaponDetails.shootRate;

        //ADD GIZMO
        gameObject.AddComponent<WeaponGizmo>().radius = weaponDetails.weaponRadius;
    }

    public virtual void Start()
    {
        Debug.Log("Weapon Start");
    }

    public virtual void OnEnable()
    {
        Debug.Log("Weapon OnEnable");
    }

    public virtual void Update()
    {
        //Debug.Log("Weapon Update");
        ConstructionUI();
        ShootingRate();
    }

    public virtual void Init()
    {
        Debug.Log("Weapon INIT");
    }

    private void ConstructionUI()
    {
        weaponSFX.Building();
        timeLeftToSpawn -= Time.deltaTime;
        weaponUI.UpdateConstructionProgressBar(weaponDetails.GetPercentageRemainingToSpawn(timeLeftToSpawn) / 100);

        if (timeLeftToSpawn <= 0)
        {
            //Its Time to Spawn The Weapon
            SpawnWeapon();
        }
    }

    private void ShootingRate()
    {
        currentShootRate -= Time.deltaTime;
        currentShootRate = Mathf.Clamp(currentShootRate, 0, weaponDetails.shootRate);
    }

    protected bool CanShoot()
    {
        if(currentShootRate <= 0)
        {
            return true;
        }

        return false;
    }

    public virtual void BuildingWeapon()
    {
        
    }

    public virtual void SpawnWeapon()
    {
        weaponSFX.Built();

        weaponUIgameObject.SetActive(false);
        weaponGFXgameObject.SetActive(true);

        spawned = true;
    }

    public virtual void Shoot()
    {
        currentShootRate = weaponDetails.shootRate;
    }
}
