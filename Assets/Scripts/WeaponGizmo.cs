using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGizmo : MonoBehaviour
{
    //public GizmoType gizmoType;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Currently using only one type of Gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        //switch(gizmoType)
        //{
        //    case GizmoType.Sphere:
        //        Gizmos.DrawWireSphere(transform.position, radius);
        //        break;
        //    case GizmoType.Cube:
        //        break;
        //}
    }
}

public enum GizmoType
{
    Sphere,
    Cube
}
