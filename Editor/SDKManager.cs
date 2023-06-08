using System.Linq;
using UnityEditor;
using UnityEngine;

public enum Env
{
    Staging,
    Production
}

public class SDKManager
{
    private static Env currentEnvironment;

#if UNITY_EDITOR
    [MenuItem("Clanz SDK/Set Staging")]
    public static void SetStagingEnvironment()
    {
        currentEnvironment = Env.Staging;
        Debug.Log("Clanz SDK environment set to Staging.");

        SetEnvironmentSymbols();
    }

    [MenuItem("Clanz SDK/Set Production")]
    public static void SetProductionEnvironment()
    {
        currentEnvironment = Env.Production;
        Debug.Log("Clanz SDK environment set to Production.");

        SetEnvironmentSymbols();
    }

    public static Env GetCurrentEnvironment()
    {
        return currentEnvironment;
    }

    private static void SetEnvironmentSymbols()
    {
        string[] symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';');

        // Remove the existing symbols
        for (int i = 0; i < symbols.Length; i++)
        {
            if (symbols[i] == "STAGING" || symbols[i] == "PRODUCTION")
            {
                symbols[i] = string.Empty;
            }
        }

        // Add the current environment symbol
        if (currentEnvironment == Env.Staging)
        {
            symbols = symbols.Append("STAGING").ToArray();
        }
        else if (currentEnvironment == Env.Production)
        {
            symbols = symbols.Append("PRODUCTION").ToArray();
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", symbols));
    }
#endif
}