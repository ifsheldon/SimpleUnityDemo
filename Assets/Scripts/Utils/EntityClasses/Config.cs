using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// The entity class of configuration
/// </summary>
public class Config
{
    /// <summary>
    /// Game Progress
    /// </summary>
    public int finished_chapter = 0;

    /// <summary>
    /// Constants in BSpawner.cs
    /// </summary>
    public int box_number_coexist = 7;

    public float min_spawn_delay = 0.3f;
    public float max_spawn_delay = 0.8f;
    public float min_speed = 10.0f;
    public float max_speed = 13.0f;

    /// <summary>
    ///  Constants in Box.cs
    /// </summary>
    public float perfect_baseline = 0.25f;

    public float perfect_hit_area_range = 0.1f;
    public float allow_hit_area = 0.5f;
    public int perfect_hit_score = 10;
    public int normal_hit_score = 1;

    public Dictionary<SkillEnum, Dictionary<Level, SkillTimeConfig>> skillTimeDictionary;

    public Config()
    {
        if(skillTimeDictionary != null)
            return;

        skillTimeDictionary = new Dictionary<SkillEnum, Dictionary<Level, SkillTimeConfig>>();
        Dictionary<Level, SkillTimeConfig> dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(20, 10)},
            {Level.L2, new SkillTimeConfig(25, 15)},
            {Level.L3, new SkillTimeConfig(30, 20)}
        };
        skillTimeDictionary.Add(SkillEnum.DoubleScore,dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(20, 5)},
            {Level.L2, new SkillTimeConfig(25, 10)},
            {Level.L3, new SkillTimeConfig(30, 15)}
        };
        skillTimeDictionary.Add(SkillEnum.AbsIntonation, dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(30, 0)},
            {Level.L2, new SkillTimeConfig(20, 0)},
            {Level.L3, new SkillTimeConfig(30, 0)}
        };
        skillTimeDictionary.Add(SkillEnum.BonusScore, dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(60, 0)},
            {Level.L2, new SkillTimeConfig(45, 0)},
            {Level.L3, new SkillTimeConfig(30, 0)}
        };
        skillTimeDictionary.Add(SkillEnum.BlockReduction, dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(60, 10)},
            {Level.L2, new SkillTimeConfig(60, 20)},
            {Level.L3, new SkillTimeConfig(60, 30)}
        };
        skillTimeDictionary.Add(SkillEnum.LoserEatDust, dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(90, 10)},
            {Level.L2, new SkillTimeConfig(75, 15)},
            {Level.L3, new SkillTimeConfig(60, 20)}
        };
        skillTimeDictionary.Add(SkillEnum.Clear, dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(25, 7)},
            {Level.L2, new SkillTimeConfig(30, 10)},
            {Level.L3, new SkillTimeConfig(35, 15)}
        };
        skillTimeDictionary.Add(SkillEnum.TripleScore, dictionary);

        dictionary = new Dictionary<Level, SkillTimeConfig>
        {
            {Level.L1, new SkillTimeConfig(1, 0)},
            {Level.L2, new SkillTimeConfig(1, 0)},
            {Level.L3, new SkillTimeConfig(1, 0)}
        };
        skillTimeDictionary.Add(SkillEnum.NullSkill, dictionary);

    }

    public void serializeTo(string path)
    {
        Serializer.serailizeTo(this, path);
    }

    public static Config deserializeFrom(string path)
    {
        try
        {
            return Serializer.deserializeFrom<Config>(path);
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("Config not found, use defaults");
            Debug.Log(e);
            return new Config();
        }
    }
}