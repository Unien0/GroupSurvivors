using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


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

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Snapshots")]
    public AudioMixerSnapshot normalSnapShot;
    public AudioMixerSnapshot muteSnapShot;

    //设置音乐过度时间
    private float musicTransitionSecond = 3f;


    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.PlaySoundEvent += OnPlaySoundEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.PlaySoundEvent -= OnPlaySoundEvent;
    }

    private void OnPlaySoundEvent(SoundName soundName)
    {
        var soundDetails = soundDetailsData.GetSoundDetails(soundName);
        if (soundDetails != null)
            EventHandler.CallInitSoundEffect(soundDetails);
    }

    private void OnAfterSceneLoadedEvent()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        SceneSoundItem sceneSound = sceneSoundData.GetSceneSoundItem(currentScene);

        if (sceneSound == null)
            return;

        SoundDetails ambient = soundDetailsData.GetSoundDetails(sceneSound.ambient);
        SoundDetails music = soundDetailsData.GetSoundDetails(sceneSound.music);

        PlayerAmbientClip(ambient,0.5f);
        PlayerMusicClip(music,musicTransitionSecond);

        //if (soundRoutine != null)
        //{
        //    StopCoroutine(soundRoutine);
        //}
        //else
        //{
        //    soundRoutine = StartCoroutine(PlaySoundRoutine(music, ambient));
        //}
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayerMusicClip(SoundDetails soundDetails , float transitionTime)
    {
        audioMixer.SetFloat("MusicVolume", ConertSoundVolume(soundDetails.soundVolume));
        gameSource.clip = soundDetails.soundClip;
        if (gameSource.isActiveAndEnabled)
            gameSource.Play();

        normalSnapShot.TransitionTo(transitionTime);
    }

    /// <summary>
    /// 播放环境音
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayerAmbientClip(SoundDetails soundDetails, float transitionTime)
    {
        audioMixer.SetFloat("AmbientVolume", ConertSoundVolume(soundDetails.soundVolume));
        ambientSource.clip = soundDetails.soundClip;
        if (gameSource.isActiveAndEnabled)
            gameSource.Play();

        normalSnapShot.TransitionTo(transitionTime);


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

    //将音乐的音量调整成（-80,20）的范围区间
    private float ConertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }
}
