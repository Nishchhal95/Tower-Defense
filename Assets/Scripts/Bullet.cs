using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 12f;
    private int damage = 10;
    private Rigidbody rb;
    private Transform target;
    public GameObject impactEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        //TODO : For now we can shoot multiple bullets to one target which we need to fix, tracking if that target is going to be hit or not
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float distanceToBeMovedThisFrame = bulletSpeed * Time.deltaTime;

            //We will hit the target this frame
            if (direction.magnitude <= distanceToBeMovedThisFrame)
            {
                Hit();
            }

            transform.Translate(direction.normalized * distanceToBeMovedThisFrame);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Hit()
    {
        GameObject effectItem = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        Destroy(effectItem, 2f);
        Enemy enemy = target.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        target = null;
        Destroy(gameObject);
    }
}
