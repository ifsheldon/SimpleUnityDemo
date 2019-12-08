using System;
using System.Net;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRandom = System.Random;

/// <summary>
/// Box Spawner with 4 tracks
/// </summary>
public class BoxSpawnerPlus : MonoBehaviour
{
    // constants from configuration
    private static int BOX_NUM_COEXIST = ConfigManager.Configuration.box_number_coexist;
    private static float MIN_SPAWN_DELAY = ConfigManager.Configuration.min_spawn_delay;
    private static float MAX_SPAWN_DELAY = ConfigManager.Configuration.max_spawn_delay;
    private static float MIN_SPEED = ConfigManager.Configuration.min_speed;
    private static float MAX_SPEED = ConfigManager.Configuration.max_speed;


    // SerializeFields which can be seen in Unity editor
    [SerializeField] private GameObject prefabBoxUp;
    [SerializeField] private GameObject prefabBoxDown;
    [SerializeField] private GameObject prefabBoxLeft;
    [SerializeField] private GameObject prefabBoxRight;


    // private fields
    private Timer spawnTimer;
    private BoxPolarity[] boxPolarities;
    private SysRandom random;
    private GameObject[] prefabBoxes = new GameObject[4];
    private float y_scaler = 1.5f;
    private bool action = true;
    private int sc_screenwidth;
    private int sc_box_x_width;
    private float wc_box_x_width;
    private int[] sc_box_x_positions = new int[4];

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
        UpdateSizes();
        spawnTimer = gameObject.AddComponent<Timer>();
        GameEventManager gameEventManager = EasyGetter.GetGameEventManager();
        gameEventManager.AddListener(GameEventType.EscHit, escHit);
        spawnTimer.Duration = Random.Range(MIN_SPAWN_DELAY, MAX_SPAWN_DELAY);
        spawnTimer.Run();
    }

    private bool flip = false;

    // Update is called once per frame
    void Update()
    {
        if (action) //changed
        {
            if (flip) // to avoid frequently get width, improve performance
            {
                flip = !flip;
                UpdateSizes();
            }

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

    void UpdateSizes()
    {
        sc_screenwidth = Screen.width;
        sc_box_x_width = sc_screenwidth / 4;
        int sc_x_base = sc_screenwidth / 8;
        Vector3 wc_temp = new Vector3(sc_screenwidth, 0, -Camera.main.transform.position.z);
        wc_temp = Camera.main.ScreenToWorldPoint(wc_temp);
        float wc_screenwidth = wc_temp.x;
        wc_box_x_width = wc_screenwidth / 4;
        for (int i = 0; i < sc_box_x_positions.Length; i++)
            sc_box_x_positions[i] = i * sc_box_x_width + sc_x_base;
    }


    void escHit()
    {
        action = !action;
    }

    public void SpawnBox()
    {
        Vector3 wcLocation, wcLocalScale;
        int kind = random.Next(0, boxPolarities.Length); //temp here
        GameObject prefabBox = prefabBoxes[kind];

        Vector3 scLocation = new Vector3(sc_box_x_positions[kind], Screen.height,
            -Camera.main.transform.position.z);
        wcLocation = Camera.main.ScreenToWorldPoint(scLocation);
        wcLocalScale = new Vector3(
            wc_box_x_width * 2.0f, //why *2.0???? but it works
            y_scaler * prefabBox.transform.localScale.y,
            prefabBox.transform.localScale.z);
        wcLocation.y = wcLocation.y + wcLocalScale.y;
        GameObject box = Instantiate(prefabBox);
        Box b = box.GetComponent<Box>();
        b.Polarity = boxPolarities[kind];
        b.FallingSpeed = Random.Range(MIN_SPEED, MAX_SPEED);
        box.transform.localScale = wcLocalScale;
        box.transform.position = wcLocation;
    }

}