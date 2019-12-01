using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(activated);
        GameEventManager gameEventManager = EasyGetter.GetGameEventManager();
        gameEventManager.AddListener(GameEventType.EscHit, escHit);
    }

    void escHit()
    {
        activated = !activated;
        gameObject.SetActive(activated);
    }
}