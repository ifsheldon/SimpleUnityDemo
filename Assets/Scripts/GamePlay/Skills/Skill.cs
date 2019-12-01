using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Skill
{
    void TakeEffect(Box b);
    bool ShouldTakeEffect(Box b);
    float TimeToReady();
}