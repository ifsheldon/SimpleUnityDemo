using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStat : MonoBehaviour
{
    public static int hit = 0;
    public static int score = 0;
    public static int destroyed = 0;

    // Start is called before the first frame update
    private Text t;

    void Start()
    {
        t = GetComponent<Text>();
    }

    void OnEnable()
    {
        hit = 0;
        score = 0;
        destroyed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t.text = $"Score: {score}\nMiss: {destroyed - hit}";
    }
}