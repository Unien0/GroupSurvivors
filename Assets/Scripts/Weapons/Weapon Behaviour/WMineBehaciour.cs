using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMineBehaciour : ProjectileWeaponBehaviour
{
    List<GameObject> markedEnemies;
    private bool isTrigger;
    //继承投射武器类型的方法
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
        EventHandler.CallPlaySoundEvent(SoundName.Shoot);//播放音效
    }

    void Update()
    {
        //设置移动
        //地雷不移动
        isTrigger = GetComponentInChildren<WeaponTrigger>().onTrigger;
    }
    //总之先设置覆盖，修改ProjectileWeaponBehaviour对触发的判定
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && isTrigger)
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamge());//造成伤害
            EventHandler.CallPlaySoundEvent(SoundName.none);//播放音效,爆炸
            //生成动画效果，可以使用对象池
            markedEnemies.Add(col.gameObject);  //Mark the enemy
            //物体之后可以直接消失,音效和特效交给对象池生成处理
            Destroy(gameObject);
        }        
    }
}
