using System.Net;
using System.Threading;
using UnityEngine;
/// <summary>
/// Box Spawner
/// </summary>
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
    private Actioner actioner;

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
    }


    public void SpawnBox()
    {
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
        rigidbody.AddForce(new Vector2(0, -Random.Range(10.0f, 13.0f)), ForceMode2D.Impulse);
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