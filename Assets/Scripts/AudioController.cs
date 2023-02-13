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
	/// ����ĳ����Ƶ
	/// </summary>
	/// <param name="i"></param>
	public void PlayAudio(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Play();
    }
    /// <summary>
    /// ֹͣ�������е���Ƶ
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
    /// ֹͣ����ĳ����Ƶ
    /// </summary>
    /// <param name="i"></param>
    public void StopplayOne(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Stop();
    }

    /// <summary>
    /// ѭ������ĳ����Ƶ
    /// </summary>
    /// <param name="i"></param>
    public void playLoop(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.loop = true;//��������Ϊѭ������ ;
        audioSource.Play();
    }
}