﻿using System.Collections;
using System.Collections.Generic;
using Unity.UNetWeaver;
using UnityEngine;
using UnityEngine.Events;

public class InputChecker : MonoBehaviour
{
    UnityEvent leftArrowHit = new LeftArrowHit();
    UnityEvent rightArrowHit = new RightArrowHit();
    UnityEvent upArrowHit = new UpArrowHit();
    UnityEvent downArrowHit = new DownArrowHit();
    UnityEvent escHit = new EscHit();

    private bool previousFrameChangedInput = false;

    private int hitCount = 0;

    // Config
    private int hitGap = 30;


    // Start is called before the first frame update
    void Start()
    {
        GameEventManager gameEventManager = EasyGetter.GetGameEventManager();
        gameEventManager.AddEvent(GameEventType.UpArrowHit, upArrowHit);
        gameEventManager.AddEvent(GameEventType.DownArrowHit, downArrowHit);
        gameEventManager.AddEvent(GameEventType.LeftArrowHit, leftArrowHit);
        gameEventManager.AddEvent(GameEventType.RightArrowHit, rightArrowHit);
        gameEventManager.AddEvent(GameEventType.EscHit, escHit);
        testAddListener(gameEventManager);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float escInput = Input.GetAxis("Cancel");
        bool hasInput = !(horizontalInput == 0 && verticalInput == 0 && escInput == 0);
        if (hasInput)
        {
            // only invoke events on first input frame
            if (!previousFrameChangedInput)
            {
                previousFrameChangedInput = true;
                bool right = horizontalInput > 0;
                bool left = horizontalInput < 0;
                bool up = verticalInput > 0;
                bool down = verticalInput < 0;
                bool esc = escInput != 0;
                if (esc)
                {
                    escHit.Invoke();
                }
                else
                {
                    if (right)
                        rightArrowHit.Invoke();
                    if (left)
                        leftArrowHit.Invoke();
                    if (up)
                        upArrowHit.Invoke();
                    if (down)
                        downArrowHit.Invoke();
                }
            }
            else
            {
                if (escInput == 0)
                {
                    hitCount++;
                    if (hitCount > hitGap)
                    {
                        previousFrameChangedInput = false;
                        hitCount = 0;
                    }
                }
            }
        }
        else
        {
            previousFrameChangedInput = false;
        }
    }

    void testAddListener(GameEventManager gameEventManager)
    {
        Debug.Log("Test Code");
        gameEventManager.AddListener(GameEventType.DownArrowHit, pressDown);
        gameEventManager.AddListener(GameEventType.UpArrowHit, pressUp);
        gameEventManager.AddListener(GameEventType.RightArrowHit, pressRight);
        gameEventManager.AddListener(GameEventType.LeftArrowHit, pressLeft);
        gameEventManager.AddListener(GameEventType.EscHit, pressEsc);
    }

    void pressEsc()
    {
        Debug.Log("ESC");
    }

    void pressRight()
    {
        Debug.Log("right");
    }

    void pressLeft()
    {
        Debug.Log("left");
    }

    void pressUp()
    {
        Debug.Log("up");
    }

    void pressDown()
    {
        Debug.Log("right");
    }
}