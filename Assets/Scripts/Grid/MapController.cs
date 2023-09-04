using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;//地块类型，里面放着不同放置物点位的地块，之后可以增加新种类地形
    public GameObject player;
    public float checkerRadius;//搜索半径，超出一定半径后产生新的地块用
    public LayerMask terrainMask;//地形遮罩
    public GameObject currentChunk;//当前地块
    Vector3 playerLastPosition;//玩家位置，当玩家移动和被推开时候根据相对位置生成新的地块
    //PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;//增加的地块
    GameObject latestChunk;//最新的地块
    public float maxOpDist; //地块上限，超过一定数值时将最旧地块删除
    float opDist;
    float optimizerCooldown;//优化
    public float optimizerCooldownDur;//优化CD



    void Start()
    {
        playerLastPosition = player.transform.position;//玩家相对位置
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimzer();
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);
        if (directionName.Contains("Up"))//根据玩家移动方向设置相应点位的地块
        {
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }

    }

    void CheckAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position,checkerRadius,terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }
    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))//地块和方向的检测
        {
            if (direction.y>0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if(direction.y <-0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimzer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;   //Check every 1 second to save cost, change this value to lower to check more times
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
