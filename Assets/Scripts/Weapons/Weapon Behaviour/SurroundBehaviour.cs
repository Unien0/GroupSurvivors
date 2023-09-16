using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;
    CircleCollider2D subObjectCollector;

    public float rotationalSpeed;

    private void Awake()
    {
        subObjectCollector = GetComponentInChildren<CircleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationalSpeed));
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamge());

            markedEnemies.Add(col.gameObject);  //Mark the enemy
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamege(GetCurrentDamge());

                markedEnemies.Add(col.gameObject);
            }
        }
    }
}
