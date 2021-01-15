/****************************************************
    文件：AudioSourceManager.cs
	作者：Happy-11
    日期：2021年1月10日21:50:02
	功能：音效管理
*****************************************************/

using UnityEngine;

public class AudioSourceManager  
{
    //0.播放BGM  1.播放音效
    private AudioSource[] audioSources;

    private bool isPlayEffectMusic = true;
    private bool isPlayBGMusic = true;


    public AudioSourceManager()
    {
        //这里使用GetComponents（带S）获取到多个组件
        audioSources = GameManager.Instance.GetComponents<AudioSource>();
    }

    //切换背景音乐
    public void PlayBGMusic(AudioClip audioClip)
    {
        //没有播放音乐的时候，那就不用再次播放音乐（相同音乐的时候），音乐不同则要切换
        if (!audioSources[0].isPlaying||audioSources[0].clip!=audioClip)
        {
            audioSources[0].clip = audioClip;
            audioSources[0].Play();  
        }
    }

    //播放音效
    public void PlayEffect(AudioClip audioClip)
    {

        //查看是否在静音情况下
        if (isPlayEffectMusic)
        {
            //播放一次
            audioSources[1].PlayOneShot(audioClip);
        }
    }

    //切换静音状态
    public void CloseBGMusic()
    {
        audioSources[0].Stop(); 
    }
    public void OpenBGMusic()
    {
        audioSources[0].Play();
    }


}