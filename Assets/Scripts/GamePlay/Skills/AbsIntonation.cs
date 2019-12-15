public class AbsIntonation : Skill
{
    public AbsIntonation(SkillTimeConfig stc) : base(stc)
    {

    }
    // the logic seems ok, have not been checked
    public override bool ShouldTakeEffect(Box b)
    {
        return IsActivated;
    }

    public override void Effect(Box b)
    {
        b.HitBox();
    }

    public override void OnExpired(Box b)
    {
        
    }
}