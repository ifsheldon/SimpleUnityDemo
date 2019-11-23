using System;
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
    public float min_force = 10.0f;
    public float max_force = 13.0f;

    /// <summary>
    ///  Constants in Box.cs
    /// </summary>
    public float perfect_baseline = 0.25f;
    public float perfect_hit_area_range = 0.1f;
    public float allow_hit_area = 0.5f;
    public int perfect_hit_score = 10;
    public int normal_hit_score = 1;


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