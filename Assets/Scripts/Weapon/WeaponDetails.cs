using System;

[Serializable]
public class WeaponDetails
{
    public string weaponName;
    public int weaponID;
    public float timeToConstruct;
    public float turnRate;
    public float shootRate;
    public float weaponRadius;
    public Bullet bulletPrefab;

    public float GetPercentageRemainingToSpawn(float timeLeftToSpawn)
    {
        return ((100 / timeToConstruct) * timeLeftToSpawn);
    }
}
