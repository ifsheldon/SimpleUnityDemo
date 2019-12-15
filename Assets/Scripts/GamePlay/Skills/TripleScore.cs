class TripleScore : Skill
{
    public TripleScore(SkillTimeConfig stc) : base(stc)
    {
        
    }
    public override bool ShouldTakeEffect(Box b)
    {
        return IsActivated;
    }

    public override void Effect(Box b)
    {
        b.ScoreMultiplier = 3.0f;
    }

    public override void OnExpired(Box b)
    {
        b.ScoreMultiplier = 1.0f;
    }
}