using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    //玩家临时变量
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;

    #region 调用并显示玩家SO属性数值
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentHealthDisplay.text = "血量：" + currentHealth;
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "恢复：" + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "速度：" + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "力量：" + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "灵敏：" + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "采集：" + currentMagnet;
                }
            }
        }
    }

    #endregion

    //public List<GameObject> spawnedWeapons;

    [Header("经验值/等级")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;//所需经验值（浮动项，根据当前等级改编）
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;//等级列表

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TextMeshProUGUI levelText;



    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        SpawnedWeapon(characterData.StartingWeapon);
    }

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        //绑定esc后出现的数值
        GameManager.instance.currentHealthDisplay.text = "血量：" + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "恢复：" + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "速度：" + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "力量：" + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "灵敏：" + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "采集：" + currentMagnet;

        GameManager.instance.AssignChosenCharacterUI(characterData);

        UpdataHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }
    private void Update()
    {
        if (invincibilityTimer>0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();

        UpdateExpBar();
    }

    void LevelUpChecker()//升级鉴定
    {
        if (experience >= experienceCap)
        {            
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
                UpdateLevelText();
                GameManager.instance.StartLevelUp();
            
        }
    }

    void UpdateExpBar()//经验值UI显示
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()//等级UI显示
    {
        levelText.text = "Lv" + level.ToString();
    }

   public void TakeDamage(float dmg)//伤害判定
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdataHealthBar();
        }
    }

    void UpdataHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemUI(inventory.weaponUISlot, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
        //Destroy(gameObject);
    }

    public void RestoreHealth(float amount)//回血
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;

            if (CurrentHealth> characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnedWeapon(GameObject weapon)//武器增加
    {
        if (weaponIndex >= inventory.weaponSlots.Count-1)
        {
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        //spawnedWeapons.Add(spawnedWeapon);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }
    public void SpawnedPassiveItem(GameObject passiveItem)//道具增加
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        //spawnedWeapons.Add(spawnedWeapon);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}
