using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("音乐数据库")]
    public SoundDetailsList_SO soundDetailsData;
    public SceneSoundList_SO sceneSoundData;
    [Header("Audio Source")]
    public AudioSource ambientSource;
    public AudioSource gameSource;

    //随机播放时间
    //public float MusicStartSecond => Random.Range(5f,15f);
    //private Coroutine soundRoutine;

    private void OnEnable()
    {
        //EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        //EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    //private void OnAfterSceneLoadedEvent()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;

    //    SceneSoundItem sceneSound = sceneSoundData.GetSceneSoundItem(currentScene);

    //    if (sceneSound == null)
    //        return;

    //    SoundDetails ambient = soundDetailsData.GetSoundDetails(sceneSound.ambient);
    //    SoundDetails music = soundDetailsData.GetSoundDetails(sceneSound.music);

    //    PlayerAmbientClip(ambient);
    //    PlayerMusicClip(music);

    //    //if (soundRoutine != null)
    //    //{
    //    //    StopCoroutine(soundRoutine);
    //    //}
    //    //else
    //    //{
    //    //    soundRoutine = StartCoroutine(PlaySoundRoutine(music, ambient));
    //    //}
    //}

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayerMusicClip(SoundDetails soundDetails)
    {
        gameSource.clip = soundDetails.soundClip;
        if (gameSource.isActiveAndEnabled)
            gameSource.Play();
    }

    /// <summary>
    /// 播放环境音
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayerAmbientClip(SoundDetails soundDetails)
    {
        ambientSource.clip = soundDetails.soundClip;
        if (gameSource.isActiveAndEnabled)
            gameSource.Play();
    }

    //延迟播放携程
    //private IEnumerator PlaySoundRoutine(SoundDetails music, SoundDetails ambient)
    //{
    //    if (music != null && ambient != null)
    //    {
    //        PlayerAmbientClip(ambient);
    //        yield return new WaitForSeconds(MusicStartSecond);
    //        PlayerMusicClip(music);
    //    }
    //}
}
