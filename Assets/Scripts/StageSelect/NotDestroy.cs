using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); // このオブジェクトはシーンが変更されても破棄されない
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}