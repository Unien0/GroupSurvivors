using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : Singleton<AudioManager>
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

    //AudioMixer中的音频状态
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

    /// <summary>
    /// 设置播放事件
    /// </summary>
    /// <param name="soundName"></param>
    private void OnPlaySoundEvent(SoundName soundName)
    {
        var soundDetails = soundDetailsData.GetSoundDetails(soundName);
        if (soundDetails != null)
            EventHandler.CallInitSoundEffect(soundDetails);
    }

    /// <summary>
    /// 设置场景BGM播放事件
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        //当相关场景启动时
        string currentScene = SceneManager.GetActiveScene().name;

        //获取相应场景音乐
        SceneSoundItem sceneSound = sceneSoundData.GetSceneSoundItem(currentScene);

        //防止报空
        if (sceneSound == null)
            return;

        //读取相应环境音和BGM
        SoundDetails ambient = soundDetailsData.GetSoundDetails(sceneSound.ambient);
        SoundDetails music = soundDetailsData.GetSoundDetails(sceneSound.music);

        //设置音乐渐入效果
        PlayerAmbientClip(ambient,0.5f);
        PlayerMusicClip(music,musicTransitionSecond);

        ////延迟播放相关
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
        //获取AudioMixer的相应音轨
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
