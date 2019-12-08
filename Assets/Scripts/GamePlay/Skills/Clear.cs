public class Clear : Skill
{
    private static int PERFECT_HIT_SCORE = ConfigManager.Configuration.perfect_hit_score;
    public override bool ShouldTakeEffect(Box b)
    {
        return IsActivated;
    }

    public override void Effect(Box b)
    {
        b.DestroySelf();
        ShowStat.score += PERFECT_HIT_SCORE;
        IsActivated = false;
    }

    public override void OnExpired(Box b)
    {
        
    }
}