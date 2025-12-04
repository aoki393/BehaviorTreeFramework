using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1.5f;
    private void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime); // 超过设置的lifeTime后返回对象池
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }
    void Update()
    {
        // 移动
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    
    public void ReturnToPool()
    {
        ObjectPool.Instance.ReturnObject(gameObject.name, gameObject);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }
}


