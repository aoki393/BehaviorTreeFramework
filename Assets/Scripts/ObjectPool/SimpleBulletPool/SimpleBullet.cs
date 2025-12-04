using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 1f;
    private SimpleBulletPool bulletPool;  // 直接引用
    
    void Start()
    {
        // 获取对象池引用（假设只有一个）
        bulletPool = FindObjectsByType<SimpleBulletPool>(FindObjectsSortMode.None)[0];
    }
    
    void OnEnable()
    {
        // 生命周期结束后自动回收
        Invoke("ReturnToPool", lifeTime);
    }
    
    void OnDisable()
    {
        CancelInvoke();
    }
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    
    public void ReturnToPool()
    {
        if (bulletPool != null)
        {
            bulletPool.ReturnBullet(gameObject);
        }
        else
        {
            // 如果找不到对象池，直接销毁
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }
}