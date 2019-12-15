using System;

public abstract class Skill
{
    private SkillTimeConfig skillTimeConfig;
    private float timer = 0;
    private bool activated = false;
    private bool cooling = false;

    public Skill(SkillTimeConfig stc)
    {
        skillTimeConfig = stc;
    }

    public void Activate()
    {
        bool coolingNotUsable = (!activated) && cooling;
        if (!coolingNotUsable)
        {
            activated = true;
            cooling = true;
        }
    }


    public bool IsActivated
    {
        get => activated;
        set => activated = value;
    }

    public bool IsCooling => cooling;


    public float Timer => timer;

    public bool Expired => (!activated) && cooling;
    public abstract bool ShouldTakeEffect(Box b);

    public void TakeEffect(Box b)
    {
        if (Expired)
        {
            OnExpired(b);
        }
        else if (ShouldTakeEffect(b))
        {
            Effect(b);
        }
    }

    public abstract void Effect(Box b);

    public float TimeToReady()
    {
        return skillTimeConfig.CoolDownTime - timer;
    }


    public void AddTime(float elapsedTime)
    {
        if (cooling)
        {
            timer += elapsedTime;
            if (timer >= skillTimeConfig.LastDuration)
            {
                activated = false;
            }

            if (timer >= skillTimeConfig.CoolDownTime)
            {
                cooling = false;
            }
        }
    }

    public abstract void OnExpired(Box b);
}