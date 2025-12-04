using UnityEngine;

// 生成子弹的脚本
public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;  // 子弹发射点
    public float fireRate = 0.1f; // 射击间隔
    
    private float nextFireTime = 0f;
    
    void Update()
    {
        // 按住鼠标左键连续射击
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        
        // 按R键重新开始（测试用）
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 重置所有子弹
            foreach (Bullet bullet in FindObjectsByType<Bullet>(FindObjectsSortMode.None))
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
        GameObject bullet = ObjectPool.Instance.GetObject("Bullet");
        
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
