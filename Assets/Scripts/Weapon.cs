using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float timeToConstruct;
    public float damage;
    public GameObject spawnEffect;

    private float timeLeftToSpawn;
    private bool startingSpawn = false;
    private GameObject spawnedEffect;
    //[SerializeField]private float remainingPercentage;

    // Start is called before the first frame update
    void Start()
    {
        
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

    //Faltu Funnctions
    private float GetPercentageRemainingToSpawn()
    {
        //remainingPercentage = 
        return ((100 / timeToConstruct) * timeLeftToSpawn);
        //return remainingPercentage;
    }
}
