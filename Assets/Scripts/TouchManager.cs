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
            _GameManager.Instance.selectedBlock = null;
        }
    }

    private void DetectClick()
    {
        RaycastHit hit;
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(raycast, out hit);

        if (hit.collider != null)
        {
            _GameManager.Instance.selectedBlock = hit.collider.gameObject.GetComponent<IWeaponPlaceable>();

            if(_GameManager.Instance.selectedBlock != null)
            {
                //We now have a Block Selected
                //Debug.Log("I Hit " + hit.collider.gameObject.name);
                _GameManager.Instance.ClickedBlock(_GameManager.Instance.selectedBlock);
            }
        }
    }
}
