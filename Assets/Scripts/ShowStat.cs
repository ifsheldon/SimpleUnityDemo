using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStat : MonoBehaviour
{
    // Start is called before the first frame update
    private Text t;
    void Start()
    {
        t = GetComponent<Text>();
    }

    void OnEnable()
    {
        BoxBounce.hit = 0;
        BoxBounce.destroyed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        t.text = $"Hit: {BoxBounce.hit}\nMiss: {BoxBounce.destroyed-BoxBounce.hit}";
    }
}
