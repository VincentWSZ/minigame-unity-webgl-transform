﻿using System;
using System.Collections;
using LuaInterface;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    IEnumerator Init2()
    {
        yield return AssetBundleLoad.Init();
    }

    void Init() { }

    void Awake()
    {
        StartCoroutine(Init2());

        LuaState lua = new LuaState();
        lua.Start();
        string hello =
            @"                
                print('hello tolua#')                                  
            ";

        lua.DoString(hello, "HelloWorld.cs");
        lua.CheckTop();
        lua.Dispose();
        lua = null;
    }
}
