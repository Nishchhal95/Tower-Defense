using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [SerializeField] private Weapon selectedWeapon;

    public List<Weapon> weaponList = new List<Weapon>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSelectedWeapon(weaponList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedWeapon(Weapon weapon)
    {
        selectedWeapon = weapon;
    }

    public Weapon GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public Weapon GetWeaponFromWeaponID(int weaponID)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if(weaponList[i].weaponDetails.weaponID == weaponID)
            {
                return weaponList[i];
            }
        }

        return null;
    }

    public float GetWeaponHeight(Weapon weapon)
    {
        return weapon.transform.localScale.y;
    }
}
