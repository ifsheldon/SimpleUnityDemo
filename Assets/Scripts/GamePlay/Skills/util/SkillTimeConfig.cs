using System;

public class SkillTimeConfig
{
    private float coolDownTime;
    private float lastDuration;

    public SkillTimeConfig(float coolDownTime, float lastDuration)
    {
        this.coolDownTime = Math.Abs(coolDownTime);
        this.lastDuration = Math.Abs(lastDuration);
    }

    public float CoolDownTime
    {
        get => coolDownTime;
        set => Math.Abs(value);
    }

    public float LastDuration
    {
        get => lastDuration;
        set => Math.Abs(value);
    }
}