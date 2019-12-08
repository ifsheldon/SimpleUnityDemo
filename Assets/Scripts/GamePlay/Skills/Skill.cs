using System;

public abstract class Skill
{
    private float coolDownTime = 100;
    private float timer = 0;
    private bool activated = false;
    private float lastDuration = 10f;
    private bool cooling = false;

    public void Activate()
    {
        bool coolingNotUsable = (!activated) && cooling;
        if (!coolingNotUsable)
        {
            activated = true;
            cooling = true;
        }
    }

    public float CoolDownTime
    {
        get => coolDownTime;
        set => Math.Abs(value);
    }

    public bool IsActivated
    {
        get => activated;
        set => activated = value;
    }

    public bool IsCooling => cooling;

    public float LastDuration
    {
        get => lastDuration;
        set => Math.Abs(value);
    }

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
        return coolDownTime - timer;
    }


    public void AddTime(float elapsedTime)
    {
        if (cooling)
        {
            timer += elapsedTime;
            if (timer >= lastDuration)
            {
                activated = false;
            }

            if (timer >= coolDownTime)
            {
                cooling = false;
            }
        }
    }

    public abstract void OnExpired(Box b);
}