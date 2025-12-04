using UnityEngine;

public class SimplePlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public SimpleBulletPool bulletPool;
    
    void Start()
    {
        // bulletPool = FindObjectsByType<SimpleBulletPool>(FindObjectsSortMode.None)[0];
        
        if (firePoint == null)
        {
            firePoint = transform;  // 如果没有指定发射点，用自身位置
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 回收所有激活的子弹
            foreach (SimpleBullet bullet in FindObjectsByType<SimpleBullet>(FindObjectsSortMode.None))
            {
                if (bullet.gameObject.activeSelf)
                {
                    bullet.ReturnToPool();
                }
            }
        }
    }
    
    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("请设置FirePoint!");
            return;
        }
        
        // 从对象池获取子弹
        GameObject bullet = bulletPool.GetBullet();
        
        if (bullet != null)
        {
            // 设置子弹的位置和旋转
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
        }
        else
        {
            Debug.LogWarning("对象池返回了空对象");
        }
    }
}