using UnityEngine;

public class PoolDebugUI : MonoBehaviour
{
    private GUIStyle labelStyle;
    void OnGUI()
    {
        if (ObjectPool.Instance == null) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 450, 300));

        labelStyle.fontSize = 40;  // 设置字体大小
        labelStyle.normal.textColor = Color.white; 
        
        // 使用自定义样式
        GUILayout.Label("=== 对象池状态 ===", labelStyle);
        
        // 获取当前激活的子弹数量
        int activeBullets = 0;
        foreach (Bullet bullet in FindObjectsByType<Bullet>(FindObjectsSortMode.None))
        {
            if (bullet.gameObject.activeSelf)
                activeBullets++;
        }
        
        GUILayout.Label($"激活的子弹: {activeBullets}", labelStyle);
        GUILayout.Label("操作说明：", labelStyle);
        GUILayout.Label("1. 按住鼠标左键：连续射击", labelStyle);
        GUILayout.Label("2. 按R键：回收所有子弹", labelStyle);
        
        GUILayout.EndArea();
    }
}
