using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public static class SkillManager
{
    private static Skill skill = SkillFactory.GetSkill(SkillEnum.NullSkill, Level.L1);

    public static void SetSkill(SkillEnum s, Level l)
    {
        skill = SkillFactory.GetSkill(s, l);
    }

    public static Skill GetSkill()
    {
        return skill;
    }
}