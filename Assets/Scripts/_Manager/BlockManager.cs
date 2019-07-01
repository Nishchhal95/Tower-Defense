using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;
    public IWeaponPlaceable selectedBlock;

    public delegate void BlockEvents(IWeaponPlaceable selectedtBlock);
    public BlockEvents OnBlockSelected;

    private void Awake()
    {
        if (Instance == null)
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

    public void SelectBlock(IWeaponPlaceable block)
    {
        selectedBlock = block;
        OnBlockSelected?.Invoke(selectedBlock);
    }

    public void DeselectBlock()
    {
        selectedBlock = null;
    }
}
