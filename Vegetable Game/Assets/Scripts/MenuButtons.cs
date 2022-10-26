using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Sprite orangeBG, greenBG, orangeDot, greenDot;
    [SerializeField] private Image soundImageBG, soundImageDot, hapticImageBG, hapticImageDot;
    [SerializeField] private GameObject soundDot, hapticDot;
    [SerializeField] private Button chestButton;
    [SerializeField] private GameObject chestGold;
    private int lastOpenedChestLevel;
    private float dotMoveDistance = 58f;
    private float dotAnimationDuration = .2f;

    private void Awake() {
        if(PlayerPrefs.HasKey("last-chest")) lastOpenedChestLevel = PlayerPrefs.GetInt("last-chest");
        else lastOpenedChestLevel = 0;
    }
    public void ControlChest()
    {
        chestButton.interactable = false;
        Debug.Log("last opened chest = " + lastOpenedChestLevel + " current level = " + GameManager.Instance.CurrentLevel);
        if(lastOpenedChestLevel < GameManager.Instance.CurrentLevel && GameManager.Instance.CurrentLevel % 3 == 0) chestButton.interactable = true;
    }

    public void OpenChest()
    {
        chestButton.interactable = false;
        chestGold.SetActive(true);
        SoundManager.Instance.PlayGoldSound();
        PlayerPrefs.SetInt("last-chest", GameManager.Instance.CurrentLevel);
        GameManager.Instance.ChestOpened();
    }

    public void ToggleSound()
    {
        SoundManager.Instance.PlaySwitchSound();
        if (!GameManager.Instance.IsSoundOn)
        {
            TurnSoundOn();
            GameManager.Instance.IsSoundOn = true;
        }
        else
        {
            TurnSoundOff();
            GameManager.Instance.IsSoundOn = false;
        }

        GameManager.Instance.SaveData();
    }

    public void ToggleHaptic()
    {
        SoundManager.Instance.PlaySwitchSound();
        if (!GameManager.Instance.IsHapticOn)
        {
            TurnHapticOn();
            GameManager.Instance.IsHapticOn = true;
        }
        else
        {
            TurnHapticOff();
            GameManager.Instance.IsHapticOn = false;
        }
        GameManager.Instance.SaveData();
    }

    public void InitButtons()
    {
        if (GameManager.Instance.IsSoundOn) TurnSoundOn();
        else TurnSoundOff();

        if (GameManager.Instance.IsHapticOn) TurnHapticOn();
        else TurnHapticOff();
    }

    private void TurnSoundOn()
    {
        soundImageBG.sprite = greenBG;
        soundImageDot.sprite = greenDot;

        LeanTween.moveLocalX(soundDot, dotMoveDistance, dotAnimationDuration).setEaseOutQuad();
    }
    private void TurnSoundOff()
    {
        soundImageBG.sprite = orangeBG;
        soundImageDot.sprite = orangeDot;

        LeanTween.moveLocalX(soundDot, -dotMoveDistance, dotAnimationDuration).setEaseOutQuad();
    }
    private void TurnHapticOn()
    {
        hapticImageBG.sprite = greenBG;
        hapticImageDot.sprite = greenDot;

        LeanTween.moveLocalX(hapticDot, dotMoveDistance, dotAnimationDuration).setEaseOutQuad();
    }
    private void TurnHapticOff()
    {
        hapticImageBG.sprite = orangeBG;
        hapticImageDot.sprite = orangeDot;

        LeanTween.moveLocalX(hapticDot, -dotMoveDistance, dotAnimationDuration).setEaseOutQuad();
    }
    public void LoadGameScene()
    {
        SoundManager.Instance.PlayClickSound();

        GameManager.Instance.LoadNextLevel();
    }

}
