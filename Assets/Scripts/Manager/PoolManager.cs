using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//对象池管理
public class PoolManager : MonoBehaviour
{
    //对象池内的对象物体
    public List<GameObject> poolPrefabs;
    private List<ObjectPool<GameObject>> poolEffectList = new List<ObjectPool<GameObject>>();
    //音效堆栈
    private Queue<GameObject> soundQueue = new Queue<GameObject>();

    private void OnEnable()
    {
        EventHandler.InitSoundEffect += InitSoundEffect;
    }
    private void OnDisable()
    {
        EventHandler.InitSoundEffect -= InitSoundEffect;
    }

    /// <summary>
    /// 生成相应对象
    /// </summary>
    private void CreateSoundPool()
    {
        var parent = new GameObject(poolPrefabs[0].name).transform;
        parent.SetParent(transform);
        //设置20+个空对象防止报空
        for (int i = 0; i < 20; i++)
        {
            GameObject newObj = Instantiate(poolPrefabs[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }
    /// <summary>
    /// 获取对象组件
    /// </summary>
    /// <returns></returns>
    private GameObject GetPoolObject()
    {
        if (soundQueue.Count < 2)
            CreateSoundPool();
        return soundQueue.Dequeue();
    }

    /// <summary>
    /// 设置音效初始化
    /// </summary>
    /// <param name="soundDetails"></param>
    private void InitSoundEffect(SoundDetails soundDetails)
    {
        var obj = GetPoolObject();
        obj.GetComponent<Sound>().SetSound(soundDetails);
        obj.SetActive(true);
        StartCoroutine(DisableSound(obj, soundDetails.soundClip.length));
    }

    /// <summary>
    /// 关闭音效
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator DisableSound(GameObject obj,float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }

    /// <summary>
    /// 播放音效的子物体代码，可以内容写在需要播放的时候
    /// </summary>
    private void CallMusic()
    {
        //使用时请更改成相应的音效名称
        EventHandler.CallPlaySoundEvent(SoundName.none);
    }
}
