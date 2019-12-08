class DoubleScore : Skill
{
    public override bool ShouldTakeEffect(Box b)
    {
        return IsActivated;
    }

    public override void Effect(Box b)
    {
        b.ScoreMultiplier = 2.0f;
    }

    public override void OnExpired(Box b)
    {
        b.ScoreMultiplier = 1.0f;
    }
}