class NullSkill : Skill
{
    public void TakeEffect(Box b)
    {
    }

    public bool ShouldTakeEffect(Box b)
    {
        return false;
    }

    public float TimeToReady()
    {
        return 1.0f;
    }
}