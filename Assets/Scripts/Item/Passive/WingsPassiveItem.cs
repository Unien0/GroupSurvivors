using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        //base.ApplyModifier();
        player.CurrentMoveSpeed *= 1 + passiveItemData.Multipler / 100f;
    }
}
