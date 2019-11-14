using System;
using System.IO;
using UnityEngine;
/// <summary>
/// The entity class of configuration
/// </summary>
public class Config
{
    public int chapter = 0;
    public int boxNum = 7;
    public float minSpawnDelay = 0.3f;
    public float maxSpawnDelay = 0.8f;

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