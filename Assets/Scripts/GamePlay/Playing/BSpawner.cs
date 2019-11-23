using System;
using System.Net;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRandom = System.Random;

/// <summary>
/// Box Spawner
/// </summary>
public class BSpawner : MonoBehaviour
{
    // constants from configuration
    private static int BOX_NUM_COEXIST = ConfigManager.Configuration.box_number_coexist;
    private static float MIN_SPAWN_DELAY = ConfigManager.Configuration.min_spawn_delay;
    private static float MAX_SPAWN_DELAY = ConfigManager.Configuration.max_spawn_delay;
    private static float MIN_FORCE = ConfigManager.Configuration.min_force;
    private static float MAX_FORCE = ConfigManager.Configuration.max_force;


    // SerializeFields which can be seen in Unity editor
    [SerializeField] private GameObject prefabBoxUp;
    [SerializeField] private GameObject prefabBoxDown;
    [SerializeField] private GameObject prefabBoxLeft;
    [SerializeField] private GameObject prefabBoxRight;


    // private fields
    private Timer spawnTimer;
    private Actioner actioner;
    private BoxPolarity[] boxPolarities;
    private SysRandom random;
    private GameObject[] prefabBoxes = new GameObject[4];
    private float y_scaler = 1.0f;

    public float Y_Scaler
    {
        get => y_scaler;
        set => y_scaler = value;
    }

    void Awake()
    {
        boxPolarities = Enum.GetValues(typeof(BoxPolarity)) as BoxPolarity[];
        prefabBoxes[0] = prefabBoxUp;
        prefabBoxes[1] = prefabBoxDown;
        prefabBoxes[2] = prefabBoxLeft;
        prefabBoxes[3] = prefabBoxRight;
        random = new SysRandom();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        actioner = GameObject.FindGameObjectWithTag("GameController").GetComponent<Actioner>();
        spawnTimer.Duration = Random.Range(MIN_SPAWN_DELAY, MAX_SPAWN_DELAY);
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (actioner.Action)
        {
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
            if (boxes.Length < BOX_NUM_COEXIST)
            {
                if (spawnTimer.Finished)
                {
                    SpawnBox();
                    spawnTimer.Duration = Random.Range(MIN_SPAWN_DELAY, MAX_SPAWN_DELAY);
                    spawnTimer.Run();
                }
            }
        }
    }


    public void SpawnBox()
    {
        Vector3 wcLocation, wcLocalScale;
        bool existPotentialCollision;
        int kind = random.Next(0, boxPolarities.Length);
        GameObject prefabBox = prefabBoxes[kind];
        do
        {
            Vector3 scLocation = new Vector3(Random.Range(0, Screen.width), Screen.height,
                -Camera.main.transform.position.z);
            wcLocation = Camera.main.ScreenToWorldPoint(scLocation);
            wcLocalScale = new Vector3(
                prefabBox.transform.localScale.x,
                y_scaler * prefabBox.transform.localScale.y,
                prefabBox.transform.localScale.z);
            wcLocation.y = wcLocation.y + wcLocalScale.y;
            var result = OutOfScreen(wcLocation, wcLocalScale);
            float boxWidth = result.Item3;
            bool outLeft = result.Item4;
            bool outRight = result.Item5;
            bool outOfBound = outLeft || outRight;
            if (outOfBound)
            {
                wcLocation.x = outLeft ? boxWidth : (result.Item2 - boxWidth);
            }

            Collider2D collider = Physics2D.OverlapArea(
                new Vector2(wcLocation.x - boxWidth, wcLocation.y - wcLocalScale.y / 2.0f),
                new Vector2(wcLocation.x + boxWidth, wcLocation.y + wcLocalScale.y / 2.0f));
            existPotentialCollision = collider != null;
            if (existPotentialCollision)
                Thread.Sleep(100);
        } while (existPotentialCollision);

        GameObject box = Instantiate(prefabBox);
        Rigidbody2D rigidbody = box.GetComponent<Rigidbody2D>();
        Box b = box.GetComponent<Box>();
        b.Polarity = boxPolarities[kind];
        rigidbody.AddForce(new Vector2(0, -Random.Range(MIN_FORCE, MAX_FORCE)), ForceMode2D.Impulse);
        box.transform.localScale = wcLocalScale;
        box.transform.position = wcLocation;
    }

    private (float, float, float, bool, bool) OutOfScreen(Vector3 wcLocation, Vector3 wcScale)
    {
        Vector3 cameraLeft = new Vector3(0, 0, 0);
        Vector3 cameraRight = new Vector3(Screen.width, 0, 0);
        Vector3 wcLeft = Camera.main.ScreenToWorldPoint(cameraLeft);
        Vector3 wcRight = Camera.main.ScreenToWorldPoint(cameraRight);
        float boxWidth = wcScale.x / 2.0f;
        float boxX = wcLocation.x;
        return (wcLeft.x, wcRight.x, boxWidth, boxX - boxWidth < wcLeft.x, boxX + boxWidth > wcRight.x);
    }
}