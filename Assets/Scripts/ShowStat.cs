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
        //temp
//        Vector3 scTopRight = new Vector3(Screen.width / 2.2f, Screen.height / 2.2f, 0);
//        t.transform.localPosition = scTopRight;
    }

    // Update is called once per frame
    void Update()
    {
        t.text = $"Hit: {BoxBounce.hit}\nMiss: {BoxBounce.destroyed-BoxBounce.hit}";
    }
}
