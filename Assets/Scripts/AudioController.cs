using UnityEngine;
using System.Collections;

public class AudioController : Singleton<AudioController>
{
    public AudioClip[] audioClip;
    public AudioSource audioSource;
	protected override void Awake()
	{
		base.Awake();
	}

	/// <summary>
	/// 播放某个音频
	/// </summary>
	/// <param name="i"></param>
	public void PlayAudio(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Play();
    }
    /// <summary>
    /// 停止播放所有的音频
    /// </summary>
    public void StopplayAll()
    {

        for (int i = 0; i < audioClip.Length; i++)
        {
            audioSource.clip = audioClip[i];
            audioSource.Stop();
        }
    }
    /// <summary>
    /// 停止播放某个音频
    /// </summary>
    /// <param name="i"></param>
    public void StopplayOne(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Stop();
    }

    /// <summary>
    /// 循环播放某个音频
    /// </summary>
    /// <param name="i"></param>
    public void playLoop(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.loop = true;//设置声音为循环播放 ;
        audioSource.Play();
    }
}