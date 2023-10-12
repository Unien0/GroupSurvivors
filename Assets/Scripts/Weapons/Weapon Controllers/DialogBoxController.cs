using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedDialogBox = Instantiate(weaponData.Prefab);//生成投掷物
        spawnedDialogBox.transform.position = transform.position; //将位置指定为与该玩家的父对象相同
        spawnedDialogBox.GetComponent<DialogBoxBehaviour>().DirectionChecker(pm.lastMovedVector);   //参考并设置方向,关于物体颠倒
    }
}
