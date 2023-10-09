using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundBehaviour : MeleeWeaponBehaviour
{

    public float rotationalSpeed;

    private void Awake()
    {

    }

    protected override void Start()
    {
        base.Start();

    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationalSpeed));
    }

   
}
