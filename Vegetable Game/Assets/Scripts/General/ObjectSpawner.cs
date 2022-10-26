using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<string> spawningVegetables;
    [SerializeField] private int howManyForEach = 15;
    [SerializeField] private float minX, maxX, minY, maxY, minZ, maxZ;

    private void Start() {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        foreach(string vegetable in spawningVegetables)
        {
            for(int i = 0; i < howManyForEach; i++)
            {
                GameObject obj = ObjectPool.GetObjectFromPool(vegetable, GetRandomPosition());
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
    }

}
