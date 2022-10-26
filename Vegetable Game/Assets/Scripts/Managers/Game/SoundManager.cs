using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SoundManager").AddComponent<SoundManager>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    #endregion
    [SerializeField] private AudioClip failSound, successSound;
    [SerializeField] private AudioClip clickSound, switchSound;
    [SerializeField] private AudioClip rolloverSound;
    [SerializeField] private AudioClip goldSound;

    [SerializeField] private AudioSource audioSource;
    // public enum Sounds{
    //     failSound = 0,
    //     successSound = 1,

    // }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void PlaySuccessSound()
    {
        if (GameManager.Instance.IsSoundOn)
            audioSource.PlayOneShot(successSound);
    }

    public void PlayFailSound()
    {
        if (GameManager.Instance.IsSoundOn)
            audioSource.PlayOneShot(failSound);
    }

    public void PlayClickSound()
    {
        if (GameManager.Instance.IsSoundOn)
            audioSource.PlayOneShot(clickSound);
    }

    public void PlaySwitchSound()
    {
        if (GameManager.Instance.IsSoundOn)
            audioSource.PlayOneShot(switchSound);
    }

    public void PlayRolloverSound()
    {
        if (GameManager.Instance.IsSoundOn)
            audioSource.PlayOneShot(rolloverSound);
    }


    public void PlayGoldSound()
    {
        if (GameManager.Instance.IsSoundOn)
            audioSource.PlayOneShot(goldSound);
    }

}
