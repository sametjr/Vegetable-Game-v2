using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _pan;
    [SerializeField] private GameObject _goldSection;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private Canvas _menuCanvas;
    private PanIngredients panIngredients;
    private int _totalGold;
    private int currentLevel = 1;
    private bool _isSoundOn = false;
    private bool _isHapticOn = false;
    private bool _playerCanInteract = true;
    private struct Level
    {
        public int levelNumber;
        public int timeInSeconds;
        public List<string> foodsToMake;

        public Level(int _lvl, int _time, List<string> _foods)
        {
            levelNumber = _lvl;
            timeInSeconds = _time;
            foodsToMake = _foods;
        }
    }

    private Level level1, level2, level3, level4, level5, level6, level7, level8, level9, level10, level11, level12;
    private List<Level> levels;

    public Camera cam => this._cam;
    public Vector2 panPosition => this._pan.transform.position;
    public Bounds panBounds => this._pan.GetComponent<SpriteRenderer>().bounds;
    public Vector3 goldSectionPosition => this._goldSection.transform.position;
    public float menuCanvasResolutionX => this._menuCanvas.GetComponent<CanvasScaler>().referenceResolution.x;
    public int CurrentLevel => this.currentLevel;
    public int Gold
    {
        get
        {
            return this._totalGold;
        }
        set
        {
            this._totalGold = value;
        }
    }
    public bool IsSoundOn
    {
        get
        {
            return this._isSoundOn;
        }
        set
        {
            _isSoundOn = value;
        }
    }
    public bool IsHapticOn
    {
        get
        {
            return this._isHapticOn;
        }
        set
        {
            _isHapticOn = value;
        }
    }
    public bool CanPlayerInteract
    {
        get
        {
            return this._playerCanInteract;
        }
        set
        {
            this._playerCanInteract = value;
        }
    }
    private void Start()
    {
        LoadData();
        Debug.Log("GameManager Started!, Data Loaded!");
        InitLevels();
        panIngredients = GameObject.FindObjectOfType<PanIngredients>();
    }

    private void InitLevels()
    {
        levels = new List<Level>();


        List<string> foodList1 = new List<string>();
        foodList1.Add("soup");
        foodList1.Add("beef");
        level1 = new Level(1, 15, foodList1);
        levels.Add(level1);

        // ---------------- // 2

        List<string> foodList2 = new List<string>();
        foodList2.Add("soup");
        foodList2.Add("loaf");
        level2 = new Level(2, 140, foodList2);
        levels.Add(level2);

        // ---------------- // 3

        List<string> foodList3 = new List<string>();
        foodList3.Add("loaf");
        foodList3.Add("salad");
        level3 = new Level(3, 130, foodList3);
        levels.Add(level3);

        // ---------------- // 4

        List<string> foodList4 = new List<string>();
        foodList4.Add("soup");
        foodList4.Add("sphagetti");
        foodList4.Add("sandwich");
        level4 = new Level(4, 120, foodList4);
        levels.Add(level4);

        // ---------------- // 5

        List<string> foodList5 = new List<string>();
        foodList5.Add("sphagetti");
        foodList5.Add("sandwich");
        foodList5.Add("cookie");
        level5 = new Level(5, 110, foodList5);
        levels.Add(level5);

        // ---------------- // 6

        List<string> foodList6 = new List<string>();
        foodList6.Add("salad");
        foodList6.Add("muffin");
        level6 = new Level(6, 100, foodList6);
        levels.Add(level6);

        // ---------------- // 7

        List<string> foodList7 = new List<string>();
        foodList7.Add("beef");
        foodList7.Add("loaf");
        foodList7.Add("sandwich");
        level7 = new Level(7, 90, foodList7);
        levels.Add(level7);

        // ---------------- // 8

        List<string> foodList8 = new List<string>();
        foodList8.Add("sandwich");
        foodList8.Add("cookie");
        foodList8.Add("muffin");
        level8 = new Level(8, 80, foodList8);
        levels.Add(level8);

        // ---------------- // 8

        List<string> foodList9 = new List<string>();
        foodList9.Add("muffin");
        foodList9.Add("chips");
        level9 = new Level(9, 70, foodList9);
        levels.Add(level9);

        // ---------------- // 8

        List<string> foodList10 = new List<string>();
        foodList10.Add("cookie");
        foodList10.Add("salad");
        foodList10.Add("loaf");
        level10 = new Level(10, 60, foodList10);
        levels.Add(level10);

        // ---------------- // 8

        List<string> foodList11 = new List<string>();
        foodList11.Add("beef");
        foodList11.Add("sphagetti");
        foodList11.Add("sandwich");
        level11 = new Level(11, 50, foodList11);
        levels.Add(level11);
        
        // ---------------- // 8

        List<string> foodList12 = new List<string>();
        foodList12.Add("muffin");
        foodList12.Add("hotdog");
        foodList12.Add("chips");
        level12 = new Level(12, 50, foodList12);
        levels.Add(level12);

    }

    public List<string> GetLevelRequests()
    {
        return levels[currentLevel - 1].foodsToMake;
    }

    public int GetLevelCompleteSeconds()
    {
        return levels[currentLevel - 1].timeInSeconds;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void GoNextLevel()
    {
        currentLevel++;
        SaveData();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("level" + currentLevel.ToString());
    }

    public void GiveExtraTime()
    {
        if (this._totalGold >= 5)
        {
            this._totalGold -= 5;
            gameUI.countdownSeconds = 30;
            gameUI.UpdateGoldText();
        }
    }

    public void RestartLevel()
    {
        // foreach(GameObject _veg in panIngredients.insidePan)
        // {
        //     ObjectPool.AddObjectBackToQueue(_veg);
        // }
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("gold"))
        {
            this._totalGold = PlayerPrefs.GetInt("gold");
        }

        if(PlayerPrefs.HasKey("level"))
        {
            this.currentLevel = PlayerPrefs.GetInt("level");
        }
        if(PlayerPrefs.HasKey("sound"))
        {
            this._isSoundOn = PlayerPrefs.GetInt("sound") == 0 ? false : true;
        }
        if(PlayerPrefs.HasKey("haptic"))
        {
            this._isHapticOn = PlayerPrefs.GetInt("haptic") == 0 ? false : true;
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("gold", this._totalGold);
        PlayerPrefs.SetInt("level", this.currentLevel);
        PlayerPrefs.SetInt("sound", this._isSoundOn ? 1 : 0);
        PlayerPrefs.SetInt("haptic", this._isHapticOn ? 1 : 0);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChestOpened()
    {
        this._totalGold += 15;
        SaveData();
    }


    public float GetRandomXFromPan()
    {
        return UnityEngine.Random.Range(GameManager.Instance.panBounds.min.x, GameManager.Instance.panBounds.max.x - .5f);
    }
}
