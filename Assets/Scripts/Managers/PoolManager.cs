using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();
    //
    static PoolManager _instance;

    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }
    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        if (!poolDictionary.ContainsKey(poolKey))
        {
            GameObject poolHolder = new GameObject(prefab.name + " pool");
            poolHolder.transform.parent = transform;
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public void ResuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objToResuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objToResuse);
            objToResuse.Resuse(position, rotation);
        }
    }

    public class ObjectInstance
    {
        GameObject gameObject;
        Transform transform;
        bool hasPoolObjectComponent;
        PoolObject poolObject;

        public ObjectInstance(GameObject objInstance)
        {
            this.gameObject = objInstance;
            transform = this.gameObject.transform;
            gameObject.SetActive(false);

            if (gameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObject = gameObject.GetComponent<PoolObject>();
            }
        }

        public void Resuse(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
            if (hasPoolObjectComponent)
            {
                poolObject.OnObjectResuse();
            }
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}
