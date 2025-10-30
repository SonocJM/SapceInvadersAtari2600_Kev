using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject prefabToCreate;
    public int initialAmount = 10;
    private List<GameObject> createdObjects = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < initialAmount; i++)
        {
            CreateObject(Vector3.zero).SetActive(false);
        }
    }
    public GameObject AskForObject(Vector3 position = new Vector3())
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            if (!createdObjects[i].activeInHierarchy)
            {
                createdObjects[i].transform.position = position;
                createdObjects[i].SetActive(true);
                return createdObjects[i];
            }
        }
        return CreateObject(position);

    }

    private GameObject CreateObject(Vector3 pos)
    {
        GameObject newObject = Instantiate(prefabToCreate, pos, Quaternion.identity);
        newObject.SetActive(true);
        createdObjects.Add(newObject);
        return newObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}


