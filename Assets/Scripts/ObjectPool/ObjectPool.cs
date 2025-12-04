using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// 可池化对象接口
public interface IPoolable
{
    void OnPoolGet();      // 从对象池取出时调用
    void OnPoolReturn();   // 返回对象池时调用
}
public class ObjectPool:MonoBehaviour
{
	[System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int initialSize;
    }
    public List<PoolItem> poolItems = new List<PoolItem>();
    
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, GameObject> prefabDictionary;
    
    public static ObjectPool Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            InitializePool();
        }
        else
        {
            Debug.LogWarning($"销毁重复的{nameof(ObjectPool)}：{gameObject.name}");
            Destroy(gameObject);
        }
    }
    
    void InitializePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        prefabDictionary = new Dictionary<string, GameObject>();
        
        foreach (PoolItem item in poolItems)
        {
            string objectName = item.prefab.name;
            prefabDictionary[objectName] = item.prefab;
            
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < item.initialSize; i++)
            {
                GameObject obj = CreateNewObject(objectName);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary[objectName] = objectPool;
        }
    }
    
    public GameObject GetObject(string objectName)
    {
        if (!poolDictionary.ContainsKey(objectName))
        {
            Debug.LogWarning($"对象池中没有找到名为 {objectName} 的对象");
            return null;
        }
        
        Queue<GameObject> pool = poolDictionary[objectName];
        
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return CreateNewObject(objectName);
        }
    }
    
    public void ReturnObject(string objectName, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(objectName))
        {
            Debug.LogWarning($"对象池中没有找到名为 {objectName} 的对象");
            return;
        }
        
        obj.SetActive(false);
        poolDictionary[objectName].Enqueue(obj);
    }
    
    private GameObject CreateNewObject(string objectName)
    {
        if (!prefabDictionary.ContainsKey(objectName))
        {
            Debug.LogError($"未找到名为 {objectName} 的预制体");
            return null;
        }
        
        GameObject prefab = prefabDictionary[objectName];
        GameObject obj = Instantiate(prefab);
        obj.name = objectName;
        obj.SetActive(false);
        return obj;
    }
    public void ClearPool()
    {
        foreach (var pool in poolDictionary.Values)
        {
            while (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                Destroy(obj);
            }
        }
        poolDictionary.Clear();
        prefabDictionary.Clear();
    }

}