using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSoundList_SO", menuName = "Sound/SceneSoundList")]
public class SceneSoundList_SO : ScriptableObject
{
    public List<SceneSoundItem> sceneSoundList;
    public SceneSoundItem GetSceneSoundItem(string name)
    {
        //使用名字查找
        return sceneSoundList.Find(s => s.sceneName == name);
    }
}

[System.Serializable]
public class SceneSoundItem
{
    public string sceneName;
    public SoundName ambient;
    public SoundName music;
}
