using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///
/// The class handling actions on the Box
/// </summary>
public class Box : MonoBehaviour
{
    private static Vector2 FALLING_DIRECTION = new Vector2(0, -1);

    // Constants from configuration
    private static float PERFECT_BASELINE = ConfigManager.Configuration.perfect_baseline;
    private static float ALLOW_HIT_AREA = ConfigManager.Configuration.allow_hit_area;
    private static float PERFECT_HIT_AREA_RANGE = ConfigManager.Configuration.perfect_hit_area_range;
    private static int PERFECT_HIT_SCORE = ConfigManager.Configuration.perfect_hit_score;
    private static int NORMAL_HIT_SCORE = ConfigManager.Configuration.normal_hit_score;


    // fields
    private GameEventManager gameEventManager;
    private BoxPolarity boxPolarity;
    private int hitLifeTime = 1;
    private float fallingSpeed = 0.0f;
    private bool action = true;

    public float FallingSpeed
    {
        get => fallingSpeed;
        set => fallingSpeed = Math.Abs(value);
    }

    public int HitLifeTime
    {
        get => hitLifeTime;
        set => hitLifeTime = value;
    }

    public BoxPolarity Polarity
    {
        get => boxPolarity;
        set => boxPolarity = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameEventManager = EasyGetter.GetGameEventManager();
        gameEventManager.AddListener(GameEventType.DownArrowHit, HitDown);
        gameEventManager.AddListener(GameEventType.UpArrowHit, HitUp);
        gameEventManager.AddListener(GameEventType.LeftArrowHit, HitLeft);
        gameEventManager.AddListener(GameEventType.RightArrowHit, HitRight);
        gameEventManager.AddListener(GameEventType.EscHit, HitEsc);
    }

    void Update()
    {
        if (action)
            transform.Translate(fallingSpeed * FALLING_DIRECTION * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        if(action)
        {
            gameEventManager.RemoveListener(GameEventType.DownArrowHit, HitDown);
            gameEventManager.RemoveListener(GameEventType.UpArrowHit, HitUp);
            gameEventManager.RemoveListener(GameEventType.LeftArrowHit, HitLeft);
            gameEventManager.RemoveListener(GameEventType.RightArrowHit, HitRight);
            gameEventManager.RemoveListener(GameEventType.EscHit, HitEsc);
            ShowStat.destroyed++;
            Destroy(gameObject);
        }
    }

    private int Scorer(float wc_y_pos)
    {
        // screen space: bottom left (0.0) top right (pixelWidth,pixelHeight)
        int pixelHeight = Screen.height;
        int baseline_sc_y = (int) (PERFECT_BASELINE * pixelHeight);
        int range_sc_pixels = (int) (PERFECT_HIT_AREA_RANGE * pixelHeight);
        int upper_sc_y = baseline_sc_y + range_sc_pixels;
        int lower_sc_y = baseline_sc_y - range_sc_pixels;
        Vector3 sc_vec = new Vector3(0, 0, 0);
        sc_vec.y = upper_sc_y;
        Vector3 wc_vec = Camera.main.ScreenToWorldPoint(sc_vec);
        float upper_wc_y = wc_vec.y;
        sc_vec.y = lower_sc_y;
        wc_vec = Camera.main.ScreenToWorldPoint(sc_vec);
        float lower_wc_y = wc_vec.y;
        if (wc_y_pos > lower_wc_y && wc_y_pos < upper_wc_y)
        {
            Debug.Log("Perfect Hit");
            return PERFECT_HIT_SCORE;
        }
        return NORMAL_HIT_SCORE;
    }

    void HitEsc()
    {
        action = !action;
        gameObject.SetActive(action);
    }

    void HitBox()
    {
        float sc_allow_area = ALLOW_HIT_AREA * Screen.height;
        Vector3 sc_vec = new Vector3(0, sc_allow_area, 0);
        Vector3 wc_vec = Camera.main.ScreenToWorldPoint(sc_vec);
        float wc_allow_area = wc_vec.y;
        bool hitable = transform.position.y <= wc_allow_area;
        if (hitable)
        {
            hitLifeTime--;
            ShowStat.score += Scorer(transform.position.y);
            if (hitLifeTime <= 0) //预留长按lifetime = -1的情况
            {
                Destroy(gameObject);
                ShowStat.hit++;
            }
        }
    }

    void HitRight()
    {
        if (boxPolarity == BoxPolarity.Right)
        {
            HitBox();
        }
    }

    void HitLeft()
    {
        if (boxPolarity == BoxPolarity.Left)
        {
            HitBox();
        }
    }

    void HitUp()
    {
        if (boxPolarity == BoxPolarity.Up)
        {
            HitBox();
        }
    }

    void HitDown()
    {
        if (boxPolarity == BoxPolarity.Down)
        {
            HitBox();
        }
    }
}