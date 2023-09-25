using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    Transform enemyTransform;
    public Vector3 faceDir;

    void Awake()
    {
        enemy = GetComponent<EnemyStats>();
        enemyTransform = GetComponent<Transform>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        FacingDirection();
        Move();
    }

    private void Move()
    {
        if(!enemy.isDead)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);    //敌人持续靠近玩家
        }
       
    }
    /// <summary>
    /// 敌人转向逻辑
    /// </summary>
    void FacingDirection()
    {
        faceDir = new Vector3(transform.localScale.x,0, 0);
        if (enemyTransform.position.x - player.position.x >=0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
