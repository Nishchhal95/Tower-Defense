using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable<int>, IHealthable<int>
{
    public int health { get; set; }
    public int maxHealth { get; set; }
    private Transform target;
    [SerializeField]private int currentWayPointIndex = 0;

    [Range(0, 100)]
    [SerializeField] private float speed = 10f;

    public GameObject destroyEffect;

    private MeshRenderer meshRenderer;
    private Color originalColor;
    private float colorResetTime = .1f;

    private Image fillImage;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
        fillImage = transform.Find("Canvas").transform.Find("HealthBar").transform.Find("Base").transform.Find("Filler").GetComponent<Image>();
        maxHealth = 100;
        health = maxHealth;
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
        DamageEffect();

        health = health - damage;
        UpdateHealthBar();

        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        DeathEffect();
        Destroy(gameObject);
    }

    public void DamageEffect()
    {
        meshRenderer.material.color = Color.red;
        StartCoroutine(ResetColor(colorResetTime));
    }

    public void DeathEffect()
    {
        GameObject destEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(destEffect, 2f);
    }

    private IEnumerator ResetColor(float resetTime)
    {
        yield return new WaitForSecondsRealtime(resetTime);
        meshRenderer.material.color = originalColor;
    }

    public void Regen(int regenAmount)
    {
        throw new System.NotImplementedException();
    }

    private void UpdateHealthBar()
    {
        fillImage.fillAmount = GetHealthPercentage() / 100;
    }

    private float GetHealthPercentage()
    {
        //remainingPercentage = 
        return ((100 / maxHealth) * health);
        //return remainingPercentage;
    }
}
