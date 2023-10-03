using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    //继承投射武器类型的方法
    protected override void Start()
    {
        base.Start();
        EventHandler.CallPlaySoundEvent(SoundName.Shoot);//播放音效
    }

    void Update()
    {
        //设置移动
        transform.position += direction * currentSpeed * Time.deltaTime;    //Set the movement of the knife
    }
}
