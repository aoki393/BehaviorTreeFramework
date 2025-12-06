using System;
using UnityEngine;

public class CSharpSingleton
{
    private static readonly Lazy<CSharpSingleton> _instance = 
        new Lazy<CSharpSingleton>(() => new CSharpSingleton());
    
    public static CSharpSingleton Instance => _instance.Value;
    
    private CSharpSingleton() { }
}
