using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;//地块中生成的装饰与障碍的点位
    public List<GameObject> propPrefabs;//生成的内容


    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()//在地块的指定点位中随机生成装饰物
    {
        //Spawn a random prop at every spawn point
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;  //Move spawned object into map
        }
    }
}
