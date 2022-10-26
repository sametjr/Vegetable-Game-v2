using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanIngredients : MonoBehaviour
{

    // [SerializeField] private List<string> mealsForLevel;
    private List<string> mealsForLevel;
    // [SerializeField] private int secondsToComplete;
    private int secondsToComplete;
    [HideInInspector] public List<GameObject> insidePan;
    private int currentMealIndex = 0;

    private List<string> currentReq;
    private List<int> currentCounts;

    private GameUI gameUI;



    private void Start()
    {
        insidePan = new List<GameObject>();
        currentReq = new List<string>();
        currentCounts = new List<int>();
        gameUI = GameObject.FindObjectOfType<GameUI>();
        GetCurrentLevelMeals();
        DetectCurrentRequests();
        SetTimer();
    }


    private void GetCurrentLevelMeals()
    {
        mealsForLevel = GameManager.Instance.GetLevelRequests();
        secondsToComplete = GameManager.Instance.GetLevelCompleteSeconds();

        Debug.Log("Meals For Level count ==> " + mealsForLevel.Count);
    }


    public void SetTimer()
    {

        gameUI.SetTimer(secondsToComplete);
    }



    private void DetectCurrentRequests()
    {


        currentReq.Clear();
        currentCounts.Clear();
        insidePan.Clear();


        if (mealsForLevel[currentMealIndex].CompareTo("soup") == 0)
        {
            currentReq = FoodManager.Soup.Keys.ToList<string>();
            currentCounts = FoodManager.Soup.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("beef") == 0)
        {
            currentReq = FoodManager.Beef.Keys.ToList<string>();
            currentCounts = FoodManager.Beef.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("loaf") == 0)
        {
            currentReq = FoodManager.Loaf.Keys.ToList<string>();
            currentCounts = FoodManager.Loaf.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("salad") == 0)
        {
            currentReq = FoodManager.Salad.Keys.ToList<string>();
            currentCounts = FoodManager.Salad.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("sphagetti") == 0)
        {
            currentReq = FoodManager.Sphagetti.Keys.ToList<string>();
            currentCounts = FoodManager.Sphagetti.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("sandwich") == 0)
        {
            currentReq = FoodManager.Sandwich.Keys.ToList<string>();
            currentCounts = FoodManager.Sandwich.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("cookie") == 0)
        {
            currentReq = FoodManager.Cookie.Keys.ToList<string>();
            currentCounts = FoodManager.Cookie.Values.ToList<int>();
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("muffin") == 0)
        {
            currentReq = FoodManager.Muffin.Keys.ToList<string>();
            currentCounts = FoodManager.Muffin.Values.ToList<int>();

            Debug.Log("Muffin counts == " + currentCounts.Count);
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("chips") == 0)
        {
            currentReq = FoodManager.Chips.Keys.ToList<string>();
            currentCounts = FoodManager.Chips.Values.ToList<int>();

            
        }

        else if (mealsForLevel[currentMealIndex].CompareTo("hotdog") == 0)
        {
            currentReq = FoodManager.Hotdog.Keys.ToList<string>();
            currentCounts = FoodManager.Hotdog.Values.ToList<int>();
        }


        gameUI.UpdateVegetables(currentReq, currentCounts, mealsForLevel[currentMealIndex]);
        // Update UI here
    }


    private void GoNextMeal()
    {
        currentMealIndex++;

    }

    public void SendObjectToPan(GameObject _veg)
    {
        if (!currentReq.Contains(_veg.transform.parent.name))
        {
            Debug.Log("Yemek " + _veg.transform.parent.name + " ögesini içermiyor!");
            SendObjectBackToPlatform(_veg);
            // TODO TWEEN
        }

        else if (GetHowManyInPan(_veg.transform.parent.name) >= currentCounts[currentReq.IndexOf(_veg.transform.parent.name)])
        {
            Debug.Log("Bu sebzeden yeterince var!");
            SendObjectBackToPlatform(_veg);
            // TODO TWEEN
        }

        else
        {
            AddObjectToPan(_veg);
        }
    }

    private void AddObjectToPan(GameObject _veg)
    {
        insidePan.Add(_veg);
        Destroy(_veg.GetComponent<TouchMechanic>());
        Debug.Log("Sebze Tavaya Eklendi!");
        // TODO TWEEN
        TweenManager.SuccessTween(_veg, GameManager.Instance.panPosition);


        if (ControlIfMealIsDone())
        {
            Debug.Log("Current Meal Index => " + currentMealIndex + " How many meals in level => " + mealsForLevel.Count);


            /*try*/
            if (currentMealIndex < mealsForLevel.Count - 1)
            {
                GoNextMeal();
                PerformMealFinishAnimation();
                Debug.Log("Yemek hazır! sıradaki yemeğe geçiliyor!");




            }
            /*catch*/
            else
            {
                Debug.Log("Go Next Level!");
                gameUI.hasWin = true;
                PerformLevelFinishAnimation();
                GameManager.Instance.GoNextLevel();
            }

            GameManager.Instance.Gold += 10;
            GameManager.Instance.SaveData();

        }
    }





    private void PerformMealFinishAnimation()
    {
        StartCoroutine(MealFinished());
        foreach (GameObject _veg in insidePan)
        {
            LeanTween.move(_veg, new Vector3(0, 0, -10), 1f);
            LeanTween.rotateAround(_veg, Vector3.up, 720, 1f).setOnComplete(() => Destroy(_veg));
        }
    }

    private IEnumerator MealFinished()
    {
        yield return new WaitForSecondsRealtime(.5f);
        DetectCurrentRequests();
        gameUI.UpdateGold();
    }

    private void PerformLevelFinishAnimation()
    {
        StartCoroutine(LevelFinished());
        foreach (GameObject _veg in insidePan)
        {
            LeanTween.move(_veg, new Vector3(0, 0, -10), 1f);
            LeanTween.rotateAround(_veg, Vector3.up, 720, 1f).setOnComplete(() => Destroy(_veg));
        }
    }

    private IEnumerator LevelFinished()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("LEVEL FINISHED!!!!!!!!!!!!!!!!!!!!!!!!!!!1");
        gameUI.WinPopUp();
        gameUI.UpdateGold();
    }

    private bool ControlIfMealIsDone()
    {
        bool flag = true;
        for (int i = 0; i < currentReq.Count; i++)
        {
            if (GetHowManyInPan(currentReq[i]) != currentCounts[i])
            {
                flag = false;
            }
        }

        return flag;

    }

    private int GetHowManyInPan(string _veg)
    {
        int count = 0;
        foreach (GameObject vegetable in insidePan)
        {
            if (vegetable.transform.parent.name.CompareTo(_veg) == 0)
            {
                count++;
            }
        }
        return count;
    }

    private void SendObjectBackToPlatform(GameObject _veg)
    {
        Debug.Log("Vegetable Thrown To Platform!");
        TweenManager.FailTween(_veg, GameManager.Instance.panPosition);
    }



}
