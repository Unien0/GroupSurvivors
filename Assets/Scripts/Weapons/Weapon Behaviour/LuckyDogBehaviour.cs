using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyDogBehaviour : ProjectileWeaponBehaviour
{
    private Transform targetEnemy;
    protected override void Start()
    {
        base.Start();
        FindNearestEnemy();
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            Vector3 moveDirection = (targetEnemy.position - transform.position).normalized;
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            FindNearestEnemy();
        }
    }
    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // 根据需要修改标签

        float closestDistance = Mathf.Infinity;
        targetEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                targetEnemy = enemy.transform;
            }
        }
    }
}
