using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxBehaviour : ProjectileWeaponBehaviour
{
    private Vector3 mouseTargetPosition;//鼠标位置
    private Vector3 bulletDirection;//子弹方向

    //继承投射武器类型的方法
    protected override void Start()
    {
        base.Start();
        //绑定方向
        mouseTargetPosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseTargetPosition.x, mouseTargetPosition.y, 
            Camera.main.transform.position.z));
        bulletDirection = (worldMousePosition - transform.position).normalized;

        EventHandler.CallPlaySoundEvent(SoundName.Shoot);//播放音效
    }

    void Update()
    {
        //设置移动
        transform.position += bulletDirection * currentSpeed * Time.deltaTime;    //Set the movement
    }
}
