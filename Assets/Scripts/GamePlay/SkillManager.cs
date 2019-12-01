using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public static class SkillManager
{
    private static Skill skill = new NullSkill();

    public static void SetSkill(Skill s)
    {
        Assert.IsNotNull(skill);
        skill = s;
    }

    public static Skill GetSkill()
    {
        return skill;
    }
}