using UnityEngine;

/// <summary>
///
/// The class handling actions on the Box
/// </summary>
public class Box : MonoBehaviour
{
    private GameEventManager gameEventManager;

    private BoxPolarity boxPolarity;

    public BoxPolarity Polarity
    {
        get => boxPolarity;
        set => boxPolarity = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        const float minForce = 2.0f;
        const float maxForce = 3.0f;
        int rand = Random.Range(-1, 2);
        Vector2 direction = new Vector2(0, rand);
        float magnitude = Random.Range(minForce, maxForce);
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Force);
        gameEventManager = EasyGetter.GetGameEventManager();
        gameEventManager.AddListener(GameEventType.DownArrowHit, HitDown);
        gameEventManager.AddListener(GameEventType.UpArrowHit, HitUp);
        gameEventManager.AddListener(GameEventType.LeftArrowHit, HitLeft);
        gameEventManager.AddListener(GameEventType.RightArrowHit, HitRight);
    }

    void OnBecameInvisible()
    {
        gameEventManager.RemoveListener(GameEventType.DownArrowHit, HitDown);
        gameEventManager.RemoveListener(GameEventType.UpArrowHit, HitUp);
        gameEventManager.RemoveListener(GameEventType.LeftArrowHit, HitLeft);
        gameEventManager.RemoveListener(GameEventType.RightArrowHit, HitRight);
        ShowStat.destroyed++;
        Destroy(gameObject);
    }


    void OnMouseDown()
    {
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
            ShowStat.hit++;
        }
    }

    void HitRight()
    {
        if (boxPolarity == BoxPolarity.Right)
        {
            OnMouseDown();
        }
    }

    void HitLeft()
    {
        if (boxPolarity == BoxPolarity.Left)
        {
            OnMouseDown();
        }
    }

    void HitUp()
    {
        if (boxPolarity == BoxPolarity.Up)
        {
            OnMouseDown();
        }
    }

    void HitDown()
    {
        if (boxPolarity == BoxPolarity.Down)
        {
            OnMouseDown();
        }
    }

//    void Update()
//    {
//        gameObject.transform.localRotation = Quaternion.identity;
//    }
}