using System;
using Random = System.Random;


public class BlockReduction : Skill
{
    Random r = new Random();
    private static int PERFECT_HIT_SCORE = ConfigManager.Configuration.perfect_hit_score;

    public override bool ShouldTakeEffect(Box b)
    {
        return IsActivated;
    }

    public override void Effect(Box b)
    {
        double randomNum = r.NextDouble();
        if (randomNum > 0.5)
        {
            b.DestroySelf();
            ShowStat.score += PERFECT_HIT_SCORE;
        }

        IsActivated = false;
    }

    public override void OnExpired(Box b)
    {
    }
}