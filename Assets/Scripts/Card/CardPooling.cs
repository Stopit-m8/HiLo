using System.Collections.Generic;
using UnityEngine;

public class CardPooling : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int poolSize = 2;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private Transform spawnPoint;
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(cardPrefab, transform);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        transform.position = spawnPoint.position;
        obj.SetActive(false);
    }
}
