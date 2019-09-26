using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabBox;

    private const float MIN_SPAWN_DELAY = 0.3f;

    private const float MAX_SPAWN_DELAY = 0.8f;

    private const float MIN_SCALING_X = 1.0f;
    private const float MAX_SCALING_X = 1.5f;
    private const float MIN_SCALING_Y = 1.0f;
    private const float MAX_SCALING_Y = 1.5f;

    private Timer spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(MIN_SPAWN_DELAY, MAX_SPAWN_DELAY);
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        if (boxes.Length < 7)
        {
            if (spawnTimer.Finished)
            {
                SpawnBox();
                spawnTimer.Duration = Random.Range(MIN_SPAWN_DELAY, MAX_SPAWN_DELAY);
                spawnTimer.Run();
            }
        }
    }

    public void SpawnBox()
    {
        Vector3 location = new Vector3(Random.Range(0, Screen.width), Screen.height, -Camera.main.transform.position.z);
        Vector3 world_location = Camera.main.ScreenToWorldPoint(location);
        GameObject box = Instantiate(prefabBox) as GameObject;
        box.transform.localScale = new Vector3(Random.Range(MIN_SCALING_X, MAX_SCALING_X) * box.transform.localScale.x,
            Random.Range(MIN_SCALING_Y, MAX_SCALING_Y) * box.transform.localScale.y, box.transform.localScale.y);
//        print(box.transform.lossyScale);
        box.transform.position = world_location;
        box.transform.position = new Vector3(box.transform.position.x,
            box.transform.position.y + box.transform.lossyScale.y, box.transform.position.z);
//        print(box.transform.position);
        var result = OutOfScreen(box);
        bool outLeft = result.Item4;
        bool outRight = result.Item5;
        bool outOfBound = outLeft || outRight;
        float boxWidth = result.Item3;
        if (outOfBound)
        {
            box.transform.position = new Vector3(outLeft ? boxWidth : (result.Item2 - boxWidth),
                box.transform.position.y,
                box.transform.position.z);
        }
    }

    (float, float, float, bool, bool) OutOfScreen(GameObject box)
    {
        Vector3 cameraLeft = new Vector3(0, 0, 0);
        Vector3 cameraRight = new Vector3(Screen.width, 0, 0);
        Vector3 wcLeft = Camera.main.ScreenToWorldPoint(cameraLeft);
        Vector3 wcRight = Camera.main.ScreenToWorldPoint(cameraRight);
        float boxWidth = box.transform.lossyScale.x / 2.0f;
        float boxX = box.transform.position.x;
        return (wcLeft.x, wcRight.x, boxWidth, boxX - boxWidth < wcLeft.x, boxX + boxWidth > wcRight.x);
    }
}