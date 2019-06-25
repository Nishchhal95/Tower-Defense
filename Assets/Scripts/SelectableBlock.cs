using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBlock : MonoBehaviour
{
    private Vector3 pointToPlaceWeapon;
    private Weapon holdingWeapon = null;

    // Start is called before the first frame update
    void Start()
    {
        pointToPlaceWeapon = transform.position + new Vector3(0, transform.localScale.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceWeapon(Weapon weapon)
    {
        if(holdingWeapon != null)
        {
            Debug.Log("ERROR! Weapon Already Placed!");
            return;
        }

        Vector3 weaponPos = pointToPlaceWeapon + new Vector3(0, _GameManager.Instance.GetWeaponHeight(weapon) / 2, 0);

        holdingWeapon = SpawnWeapon(weapon, weaponPos, Quaternion.identity);

    }

    private Weapon SpawnWeapon(Weapon weapon, Vector3 pos, Quaternion rot)
    {
        Weapon wp = Instantiate(weapon, pos, rot, _GameManager.Instance.weaponHolder);

        wp.Spawning();

        return wp;
    }
}
