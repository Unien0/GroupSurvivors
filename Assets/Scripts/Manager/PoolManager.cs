using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public List<GameObject> poolPrefabs;
    private List<ObjectPool<GameObject>> poolEffectList = new List<ObjectPool<GameObject>>();
    private Queue<GameObject> soundQueue = new Queue<GameObject>();

    private void OnEnable()
    {
        EventHandler.InitSoundEffect += InitSoundEffect;
    }
    private void OnDisable()
    {
        EventHandler.InitSoundEffect -= InitSoundEffect;
    }

    private void CreateSoundPool()
    {
        var parent = new GameObject(poolPrefabs[0].name).transform;
        parent.SetParent(transform);
        for (int i = 0; i < 20; i++)
        {
            GameObject newObj = Instantiate(poolPrefabs[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }
    private GameObject GetPoolObject()
    {
        if (soundQueue.Count < 2)
            CreateSoundPool();
        return soundQueue.Dequeue();
    }

    private void InitSoundEffect(SoundDetails soundDetails)
    {
        var obj = GetPoolObject();
        obj.GetComponent<Sound>().SetSound(soundDetails);
        obj.SetActive(true);
        StartCoroutine(DisableSound(obj, soundDetails.soundClip.length));
    }
    private IEnumerator DisableSound(GameObject obj,float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }
}
