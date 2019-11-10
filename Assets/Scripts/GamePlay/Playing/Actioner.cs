using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actioner : MonoBehaviour
{
    private bool action = false;

    void Awake()
    {
        startAction();
    }
    public bool Action
    {
        get { return action; }
    }

    public void startAction()
    {
        action = true;
    }

    public void stopAction()
    {
        action = false;
    }

    public void setActivated(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void deactivate(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}