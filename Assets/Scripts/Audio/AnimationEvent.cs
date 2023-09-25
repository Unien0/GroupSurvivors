using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    //方法名前可以加上相应的动画播放内容
    //例如：动画名+AnimationSoundPlay
    //之后只需要调整对应的（SoundName.音效名称）就能在动画中播放相应音效
    public void AnimationSoundPlay()
    {
        //根据动画调整此处播放的声音（备注：通过AnimationEvent触发）
        EventHandler.CallPlaySoundEvent(SoundName.none);
    }

}
