using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
}

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    private void Awake()
    {
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            GameObject tmp;

            for (int i = 0; i < item.amountToPool; i++)
            {
                tmp = Instantiate(item.objectToPool, this.transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                Debug.Log(pooledObjects[i].name);

                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, this.transform);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}