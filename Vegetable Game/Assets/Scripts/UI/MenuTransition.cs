using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTransition : MonoBehaviour
{

    [SerializeField] private GameObject pages;
    [SerializeField] private GameObject shopBtn, homeBtn, settingsBtn;
    [SerializeField] Text levelText;
    [SerializeField] private MenuButtons menuButtons;
    private float animationDuration = 1f;
    private float resolutionX;

    private void Start()
    {
        resolutionX = GameManager.Instance.menuCanvasResolutionX;
    }
    public void GoHomePage()
    {
        SoundManager.Instance.PlayClickSound();
        LeanTween.moveLocalX(pages, -resolutionX, animationDuration).setEaseOutElastic();
        LeanTween.rotateAroundLocal(homeBtn, Vector3.forward, 360, .3f).setEaseOutElastic();
        levelText.text = "Level " + GameManager.Instance.CurrentLevel.ToString();
        menuButtons.ControlChest();

    }

    public void GoShopPage()
    {
        SoundManager.Instance.PlayClickSound();

        LeanTween.moveLocalX(pages, 0, animationDuration).setEaseOutElastic();
        // LeanTween.moveLocalX(pages, 0, .3f);
        LeanTween.rotateAroundLocal(shopBtn, Vector3.forward, 360, .3f).setEaseOutElastic();


    }

    public void GoSettingsPage()
    {
        SoundManager.Instance.PlayClickSound();

        LeanTween.moveLocalX(pages, -2 * resolutionX, animationDuration).setEaseOutElastic();
        LeanTween.rotateAroundLocal(settingsBtn, Vector3.forward, 360, .3f).setEaseOutElastic();
        menuButtons.InitButtons();
    }



}
