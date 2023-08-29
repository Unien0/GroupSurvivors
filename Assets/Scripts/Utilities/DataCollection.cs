using UnityEngine;

[System.Serializable]
public class PersonaDetails
{
    [Header("角色数据，ID以1开头")]
    [Header("ID与头像")]
    public int playerID;
    public string playerName;
    public Sprite playerIcon;
    public Sprite playerOnWorldSprite;
    public string playerDescription;

    [Header("基础数据")]
    public int playerHp;
    public float playerSpeed;
    public int playerDamage;
    public float playerAttackCD;

    [Header("角色状态")]
    public bool isDead;
    public bool attackable;
    public bool removable;
    public bool invincible;
}

[System.Serializable]
public class ItemDetails
{
    [Header("物品数据，ID以2开头")]
    [Header("ID与头像")]
    public int itemID;
    public string itemName;
    public ItemType ItemType;
    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDescription;

    [Header("基础数据")]
    public int itemUseRadius;
    [Range(0,5)]
    public int itemPrice;

    [Header("物品状态")]
    public bool canPickedup;

}

