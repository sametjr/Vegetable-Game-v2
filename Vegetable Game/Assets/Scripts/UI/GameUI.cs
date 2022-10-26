using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    [SerializeField] private Text goldText, countdownText, levelText;
    [HideInInspector] public int countdownSeconds;

    [SerializeField] private Image firstVegImage, secondVegImage, thirdVegImage;
    [SerializeField] private Text firstVegCount, secondVegCount, thirdVegCount;
    [SerializeField] private Text finalFoodNameText;

    [SerializeField] private Sprite beet, brocoli, caper, carrot, corn, mushroom, eggplant, pumpkin, onion, tomato;

    [SerializeField] private Dictionary<string, Sprite> foodImagePairs;
    [SerializeField] private GameObject secondPlusSign;

    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject navbar;
    [SerializeField] private GameObject losePopUp, winPopUp;
    private Vector3 goldDefaultPosition;
    private Vector3 goldDefaultScale;
    private bool isLoseSoundPlayed = false;
    [HideInInspector] public bool hasWin = false;

    private int mins, secs;

    private void Start()
    {
        InitDictionary();
        UpdateGoldText();
        UpdateLevelText();
        goldDefaultPosition = gold.transform.position;
        goldDefaultScale = gold.transform.localScale;
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + GameManager.Instance.CurrentLevel.ToString();
    }

    private void InitDictionary()
    {
        foodImagePairs = new Dictionary<string, Sprite>();
        foodImagePairs["beet"] = beet;
        foodImagePairs["brocoli"] = brocoli;
        foodImagePairs["caper"] = caper;
        foodImagePairs["carrot"] = carrot;
        foodImagePairs["corn"] = corn;
        foodImagePairs["mushroom"] = mushroom;
        foodImagePairs["eggplant"] = eggplant;
        foodImagePairs["pumpkin"] = pumpkin;
        foodImagePairs["onion"] = onion;
        foodImagePairs["tomato"] = tomato;
    }

    public void UpdateGoldText()
    {
        goldText.text = GameManager.Instance.Gold.ToString();
        Debug.Log("Gold Updated!");
    }

    public void SetTimer(int _secs)
    {
        countdownSeconds = _secs;
        StartCoroutine(TickTimer());
    }

    private void UpdateTime()
    {
        string betw;
        mins = countdownSeconds / 60;
        secs = countdownSeconds % 60;


        if (secs < 10) betw = ":0";
        else betw = ":";
        countdownText.text = mins + betw + secs;
    }

    public void UpdateVegetables(List<string> _names, List<int> _counts, string _foodName)
    {

        TweenManager.NavbarChangeAnimation(navbar);

        firstVegImage.sprite = foodImagePairs[_names[0]];
        firstVegCount.text = "x" + _counts[0].ToString();

        secondVegImage.sprite = foodImagePairs[_names[1]];
        secondVegCount.text = "x" + _counts[1].ToString();

        if(_names.Count == 3)
        {
            thirdVegCount.text = "x" + _counts[2].ToString();
            thirdVegImage.sprite = foodImagePairs[_names[2]];
            thirdVegImage.color = new Color(255, 255, 255, 255);
            secondPlusSign.SetActive(true);
        }
        else
        {
            thirdVegImage.color = new Color(0, 0, 0, 0);
            secondPlusSign.SetActive(false);
            Debug.Log("Nothing to catch");
        }

        finalFoodNameText.text = "= " + _foodName.ToUpper();
        finalFoodNameText.fontSize = 30;
    }

    public void UpdateGold()
    {
        SoundManager.Instance.PlayGoldSound();
        StartCoroutine(TweenGold());
        UpdateGoldText();
    }

    private IEnumerator TweenGold()
    {
        yield return new WaitForSecondsRealtime(1f);
        gold.transform.position = goldDefaultPosition;
        gold.transform.localScale = goldDefaultScale;
        gold.SetActive(true);
        TweenManager.GoldTween(gold);
    }
    private IEnumerator TickTimer()
    {
        WaitForSecondsRealtime waitASec = new WaitForSecondsRealtime(1);
        while (!hasWin)
        {
            countdownSeconds--;

            if (countdownSeconds <= 0)
            {
                GameManager.Instance.GameOver();
                LosePopUp();
                countdownSeconds = 0;
            }
            UpdateTime();
            yield return waitASec;
        }
    }

    private void LosePopUp()
    {
        LeanTween.moveLocalX(losePopUp, 0, .2f).setEaseInQuad();
        GameManager.Instance.CanPlayerInteract = false;
        if(!isLoseSoundPlayed)
        {
            SoundManager.Instance.PlayFailSound();
            isLoseSoundPlayed = true;
        } 
    }

    public void WinPopUp()
    {
        LeanTween.moveLocalX(winPopUp, 0, .2f).setEaseInQuad();
        GameManager.Instance.CanPlayerInteract = false;
        SoundManager.Instance.PlaySuccessSound();
    }

    public void GiveExtraSeconds()
    {
        isLoseSoundPlayed = false;
        SoundManager.Instance.PlayClickSound();
        GameManager.Instance.GiveExtraTime();
        GameManager.Instance.CanPlayerInteract = true;
        HideLosePopUp();
    }

    public void HideLosePopUp()
    {
        LeanTween.moveLocalX(losePopUp, 800, .2f);
    }

    public void HideWinPopUp()
    {
        LeanTween.moveLocalX(winPopUp, -800, .2f);
    }
}
