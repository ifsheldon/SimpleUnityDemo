using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingUIPanel : MonoBehaviour
{
    private bool activated = true;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager gameEventManager = EasyGetter.GetGameEventManager();
        gameEventManager.AddListener(GameEventType.EscHit, escHit);
    }

    void escHit()
    {
        activated = !activated;
        gameObject.SetActive(activated);
    }
}