public class BonusScore : Skill
{
    public BonusScore(SkillTimeConfig stc) : base(stc)
    {

    }
    private int addScore = 30;

    public override bool ShouldTakeEffect(Box b)
    {
        return IsActivated;
    }

    public override void Effect(Box b)
    {
        ShowStat.score += addScore;
        IsActivated = false;
    }

    public override void OnExpired(Box b)
    {
    }
}