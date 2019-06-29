using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, ISelectable, IWeaponPlaceable
{
    private Vector3 pointToPlaceWeapon;
    private Weapon holdingWeapon = null;

    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        pointToPlaceWeapon = transform.position + new Vector3(0, transform.localScale.y / 2, 0);
        originalColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Select();
    }

    private void OnMouseExit()
    {
        Deselect();
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
        Select();
    }

    private Weapon SpawnWeapon(Weapon weapon, Vector3 pos, Quaternion rot)
    {
        Weapon wp = Instantiate(weapon, pos, rot, _GameManager.Instance.weaponHolder);

        wp.Spawning();

        return wp;
    }

    public void Select()
    {
        if (holdingWeapon == null)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;
            return;
        }
        this.GetComponent<MeshRenderer>().material.color = Color.red;

    }

    public void Deselect()
    {
        this.GetComponent<MeshRenderer>().material.color = originalColor;
    }
}
