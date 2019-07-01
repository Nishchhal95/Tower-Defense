using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectClick();
        }

        if(Input.GetMouseButtonUp(0))
        {
            BlockManager.Instance.DeselectBlock();
        }
    }

    private void DetectClick()
    {
        RaycastHit hit;
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(raycast, out hit);

        if (hit.collider != null)
        {
            IWeaponPlaceable selectedBlock = hit.collider.gameObject.GetComponent<IWeaponPlaceable>();

            if (selectedBlock != null)
            {
                BlockManager.Instance.SelectBlock(selectedBlock);
            }
        }
    }
}
