﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject weaponUIItemPrefab;
    public Transform weaponUIListParent;

    // Start is called before the first frame update
    void Start()
    {
        SetupWeaponUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void SetupWeaponUI()
    {
        for (int i = 0; i < WeaponManager.Instance.weaponList.Count; i++)
        {
            SpawWeaponUIItem(i);
        }
    }

    private void SpawWeaponUIItem(int index)
    {
        GameObject weaponUIItem = Instantiate(weaponUIItemPrefab, weaponUIListParent);

        InitSelectionWeaponUI(weaponUIItem, index);
    }

    private void InitSelectionWeaponUI(GameObject weaponUIItem, int index)
    {
        weaponUIItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { WeaponManager.Instance.SetSelectedWeapon(WeaponManager.Instance.weaponList[index]); });
        weaponUIItem.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = WeaponManager.Instance.weaponList[index].weaponDetails.weaponName;
    }
}
