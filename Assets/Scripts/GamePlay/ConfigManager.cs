using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for handling distributing config
/// used Singleton Pattern
/// </summary>
public static class ConfigManager
{
    private static string defaultConfigPath = "./config.json";
    private static Config config = init();

    public static Config Configuration => config;

    private static Config init()
    {
        Debug.Log("Config load");
        return Config.deserializeFrom(defaultConfigPath);
    }

    public static void save()
    {
        config.serializeTo(defaultConfigPath);
    }
}