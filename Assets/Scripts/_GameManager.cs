﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static _GameManager Instance;
    public Weapon selectedWeapon;

    public SelectableBlock selectedBlock = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedBlock(SelectableBlock blockSelected)
    {
        Weapon selectedWeapon = GetSelectedWeapon();

        if(selectedWeapon != null)
        {
            blockSelected.PlaceWeapon(selectedWeapon);
            selectedBlock = null;
        }
    }

    private Weapon GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public float GetWeaponHeight(Weapon weapon)
    {
        return weapon.transform.localScale.y;   
    }
}