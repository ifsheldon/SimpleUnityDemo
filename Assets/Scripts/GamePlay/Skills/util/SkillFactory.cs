using System.Collections.Generic;

public static class SkillFactory
{
    private static Dictionary<SkillEnum, Dictionary<Level, SkillTimeConfig>> skillTimeDictionary =
        ConfigManager.Configuration.skillTimeDictionary;

    public static Skill GetSkill(SkillEnum kind, Level level)
    {
        SkillTimeConfig stc = skillTimeDictionary[kind][level];
        // need to optimize
        switch (kind)
        {
            case SkillEnum.AbsIntonation:
                return new AbsIntonation(stc);
            case SkillEnum.BlockReduction:
                return new BlockReduction(stc);
            case SkillEnum.BonusScore:
                return new BonusScore(stc);
            case SkillEnum.Clear:
                return new Clear(stc);
            case SkillEnum.DoubleScore:
                return new DoubleScore(stc);
            case SkillEnum.LoserEatDust:
                return new LoserEatDust(stc);
            case SkillEnum.TripleScore:
                return new TripleScore(stc);
            case SkillEnum.NullSkill:
                return new NullSkill(stc);
            default:
                return null;
        }
    }
}