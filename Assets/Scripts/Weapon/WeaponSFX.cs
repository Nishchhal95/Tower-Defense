using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSFX : MonoBehaviour
{
    public GameObject buildingEffectPrefab;

    private GameObject buildingEffect;

    private bool startedBuilding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Building()
    {
        if (!startedBuilding)
        {
            startedBuilding = true;
            //Spawn Building Effect
            SpawnBuildingEffect();
        }
    }

    public void Built()
    {
        Destroy(buildingEffect);
    }

    private void SpawnBuildingEffect()
    {
        buildingEffect = Instantiate(buildingEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = buildingEffect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.duration = GetComponent<Weapon>().weaponDetails.timeToConstruct;
        particleSystem.Play();
    }
}
