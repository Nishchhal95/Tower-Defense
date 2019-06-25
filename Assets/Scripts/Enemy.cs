using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    private Transform target;
    [SerializeField]private int currentWayPointIndex = 0;

    [Range(0, 100)]
    [SerializeField] private float speed = 10f;

    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LerpEnemy();
    }

    private void LerpEnemy()
    {
        if (transform.position == Waypoints.Instance.wayPoints[currentWayPointIndex].position)
        {
            if (currentWayPointIndex < Waypoints.Instance.wayPoints.Length - 1)
            {
                currentWayPointIndex++;
            }

            else
            {
                Destroy(gameObject);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, Waypoints.Instance.wayPoints[currentWayPointIndex].position, Time.deltaTime * speed);

        //var targetRotation = Quaternion.LookRotation(Waypoints.Instance.wayPoints[currentWayPointIndex].position - transform.position);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(int damage)
    {
        //TODO : We need a health mechanism
        GameObject destEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(destEffect, 2f);
        health = health - damage;
        if(health<=0)
        { Destroy(gameObject); }
        //IF COndition for health
        //If falls below 0 then do this
    }
}
