using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private int poolSize = 20;

    #region Queues
    private Queue<GameObject> beetQueue;
    private Queue<GameObject> brocoliQueue;
    private Queue<GameObject> caperQueue;
    private Queue<GameObject> carrotQueue;
    private Queue<GameObject> cornrQueue;
    private Queue<GameObject> mushroomQueue;
    private Queue<GameObject> eggplantQueue;
    private Queue<GameObject> pumpkinQueue;
    private Queue<GameObject> onionQueue;
    private Queue<GameObject> tomatoQueue;
    #endregion

    #region Prefabs
    [SerializeField] private GameObject beet;
    [SerializeField] private GameObject brocoli;
    [SerializeField] private GameObject caper;
    [SerializeField] private GameObject carrot;
    [SerializeField] private GameObject corn;
    [SerializeField] private GameObject mushroom;
    [SerializeField] private GameObject eggplant;
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private GameObject onion;
    [SerializeField] private GameObject tomato;
    #endregion

    private Dictionary<string, GameObject> vegetablesList;
    private static Dictionary<string, Queue<GameObject>> vegetables;

    private void Start() {
        // DontDestroyOnLoad(this.gameObject);
        InitVegetableList();
        InitVariables();
        InitDictionary();
        CreatePool();
    }

    private void InitVegetableList()
    {
        vegetablesList = new Dictionary<string, GameObject>();
        vegetablesList["beet"] = beet;
        vegetablesList["brocoli"] = brocoli;
        vegetablesList["caper"] = caper;
        vegetablesList["carrot"] = carrot;
        vegetablesList["corn"] = corn;
        vegetablesList["mushroom"] = mushroom;
        vegetablesList["eggplant"] = eggplant;
        vegetablesList["pumpkin"] = pumpkin;
        vegetablesList["onion"] = onion;
        vegetablesList["tomato"] = tomato;
    }

    private void InitVariables()
    {
        beetQueue = new Queue<GameObject>();
        brocoliQueue = new Queue<GameObject>();
        caperQueue = new Queue<GameObject>();
        carrotQueue = new Queue<GameObject>();
        cornrQueue = new Queue<GameObject>();
        mushroomQueue = new Queue<GameObject>();
        eggplantQueue = new Queue<GameObject>();
        pumpkinQueue = new Queue<GameObject>();
        onionQueue = new Queue<GameObject>();
        tomatoQueue = new Queue<GameObject>();

        vegetables = new Dictionary<string, Queue<GameObject>>();
    }

    private void InitDictionary()
    {
        vegetables["beet"] = beetQueue;
        vegetables["brocoli"] = brocoliQueue;
        vegetables["caper"] = caperQueue;
        vegetables["carrot"] = carrotQueue;
        vegetables["corn"] = cornrQueue;
        vegetables["mushroom"] = mushroomQueue;
        vegetables["eggplant"] = eggplantQueue;
        vegetables["pumpkin"] = pumpkinQueue;
        vegetables["onion"] = onionQueue;
        vegetables["tomato"] = tomatoQueue;
    }

    private void CreatePool()
    {
        foreach(string vegetable in vegetablesList.Keys)
        {

            for(int i = 0; i < poolSize; i++)
            {
                GameObject createdObject = Instantiate(vegetablesList[vegetable]);
                createdObject.name = vegetable;
                ArrangeObject(createdObject);
                vegetables[vegetable].Enqueue(createdObject);
            }
        }
    }

    private void ArrangeObject(GameObject _createdObject)
    {
        _createdObject.SetActive(false);
        _createdObject.transform.parent = this.transform;
        _createdObject.transform.GetChild(0).gameObject.AddComponent<MeshCollider>();
        _createdObject.transform.GetChild(0).gameObject.AddComponent<TouchMechanic>();
        _createdObject.transform.rotation = GetRandomQuaternion();
        _createdObject.transform.localScale /= 2;
    }

    public static GameObject GetObjectFromPool(string _vegetableName, bool _getActive = true)
    {
        GameObject head = vegetables[_vegetableName].Dequeue();
        if(_getActive) head.SetActive(true);
        return head;
    }

    public static GameObject GetObjectFromPool(string _vegetableName, Vector3 _pos, bool _getActive = true)
    {
        GameObject head = vegetables[_vegetableName].Dequeue();
        if(_getActive) head.SetActive(true);
        head.transform.position = _pos;
        return head;
    }

    // public static void AddObjectBackToQueue(GameObject _veg)
    // {
    //     _veg.AddComponent<TouchMechanic>();
    //     _veg.SetActive(false);
    //     vegetables[_veg.transform.parent.name].Enqueue(_veg);
    // }
    

    private Quaternion GetRandomQuaternion()
    {
        return new Quaternion(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), 1);
    }
}
