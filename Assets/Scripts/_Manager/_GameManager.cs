using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static _GameManager Instance;
    public Weapon selectedWeapon;

    public IWeaponPlaceable selectedBlock = null;

    public Transform enemyHolder, weaponHolder, bulletHolder;

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

    private void OnEnable()
    {
        BlockManager.Instance.OnBlockSelected += BlockSelected;
    }

    private void OnDisable()
    {
        BlockManager.Instance.OnBlockSelected -= BlockSelected;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BlockSelected(IWeaponPlaceable selectedBlock)
    {
        Weapon selectedWeapon = WeaponManager.Instance.GetSelectedWeapon();

        if (selectedWeapon != null)
        {
            selectedBlock.PlaceWeapon(selectedWeapon);
            selectedBlock = null;
        }
    }
}
