using UnityEngine;

public class SimpleSingleton
{
    public static SimpleSingleton Instance { get; } = new SimpleSingleton();

    // C# 6.0 引入的只读自动属性语法糖，等价于以下代码：
    // private static readonly SimpleSingleton _instance = new SimpleSingleton();

    // public static SimpleSingleton Instance
    // {
    //     get { return _instance; }
    // }
    
    private SimpleSingleton() { }
}
