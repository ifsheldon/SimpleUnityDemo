﻿class NullSkill : Skill
{
    public override bool ShouldTakeEffect(Box b)
    {
        return false;
    }

    public override void Effect(Box b)
    {
    }

    public override void OnExpired(Box b)
    {
    }
}