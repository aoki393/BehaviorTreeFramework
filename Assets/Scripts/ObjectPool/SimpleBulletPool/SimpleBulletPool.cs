using System.Collections.Generic;
using UnityEngine;

public class SimpleBulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;  // 子弹预制体
    public int poolSize = 5;        // 池大小
    
    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    
    void Start()
    {
        // 初始化对象池
        InitializePool();
    }
    
    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    
    public GameObject GetBullet()
    {
        // 如果池中有对象，直接取出
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        // 池空了，创建新的
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(true);
            Debug.LogWarning("对象池已空，创建新对象");
            return bullet;
        }
    }
    
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}