using UnityEngine;

public class BSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabBox;
    [SerializeField] private int boxNum = 7;

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
        if (boxes.Length < boxNum)
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
        // remember to use Physics2D.OverlapArea to check potential collision before spawning 
        Vector3 wcLocation, wcLocalScale;
        bool existPotentialCollision;
        do
        {
            Vector3 scLocation = new Vector3(Random.Range(0, Screen.width), Screen.height,
                -Camera.main.transform.position.z);
            wcLocation = Camera.main.ScreenToWorldPoint(scLocation);
            wcLocalScale = new Vector3(
                Random.Range(MIN_SCALING_X, MAX_SCALING_X) * prefabBox.transform.localScale.x,
                Random.Range(MIN_SCALING_Y, MAX_SCALING_Y) * prefabBox.transform.localScale.y,
                prefabBox.transform.localScale.y);
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

            Collider2D collider = Physics2D.OverlapArea(new Vector2(wcLocation.x - boxWidth, wcLocation.y - wcLocalScale.y / 2.0f),
                new Vector2(wcLocation.x + boxWidth, wcLocation.y + wcLocalScale.y / 2.0f));
            existPotentialCollision = collider != null;
        } while (existPotentialCollision);

        GameObject box = Instantiate(prefabBox);
        box.transform.localScale = wcLocalScale;
        box.transform.position = wcLocation;
    }

    (float, float, float, bool, bool) OutOfScreen(Vector3 wcLocation, Vector3 wcScale)
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