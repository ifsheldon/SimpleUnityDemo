using UnityEngine;
/// <summary>
///
/// The class handling actions on the Box
/// </summary>
public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        const float minForce = 2.0f;
        const float maxForce = 3.0f;
        int rand = Random.Range(-1, 2);
        Vector2 direction = new Vector2(0, rand);
        float magnitude = Random.Range(minForce, maxForce);
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Force);
    }

    void OnBecameInvisible()
    {
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

    void Update()
    {
        gameObject.transform.localRotation = Quaternion.identity;
    }
}