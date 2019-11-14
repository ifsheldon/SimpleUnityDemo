using UnityEngine;

/// <summary>
/// The class acts as a flag of whether the game is playing
/// </summary>
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

}