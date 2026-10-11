using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn.Utils;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DMF = Digitalroot.Modding.Framework;

namespace Digitalroot.Valheim.EternalHeight
{
  [BepInPlugin(Guid, Name, Version)]
  [BepInDependency(Jotunn.Main.ModGuid)]
  [NetworkCompatibility(CompatibilityLevel.VersionCheckOnly, VersionStrictness.Minor)]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public partial class Main : BaseUnityPlugin, DMF.Logging.ITraceableLogging
  {
    private Harmony _harmony;
    public static Main Instance;

    [UsedImplicitly]
    public static ConfigEntry<int> NexusId { get; private set; }
    public static ConfigEntry<int> hMulti { get; private set; }
    public static ConfigEntry<int> vMulti { get; private set; }

    public Main()
    {
      Instance = this;
      #if DEBUG
      EnableTrace = true;
      DMF.Logging.Log.RegisterSource(Instance); // Enable logging to a './BepInEx/logs' file.
      #else
      EnableTrace = false;
      #endif
      DMF.Logging.Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
    }

    [UsedImplicitly]
    private void Awake()
    {
      try
      {
        DMF.Logging.Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        NexusId = Config.Bind(PluginConfigSection.General, "NexusID", 4393, new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { Browsable = false, ReadOnly = true }));
        hMulti = Config.Bind(PluginConfigSection.General, "hMultiHorizontalLoss", 3, new ConfigDescription("How much to reduce horizontal loss to. 2 = half (1/2), 3 = a third (1/3), ..., 10 = a tenth (1/10)", new AcceptableValueRange<int>(1, 10), new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, IsAdminOnly = true}));
        vMulti = Config.Bind(PluginConfigSection.General, "vMultiVerticalLoss", 3, new ConfigDescription("How much to reduce vertical loss to. 2 = half (1/2), 3 = a third (1/3), ..., 10 = a tenth (1/10)", new AcceptableValueRange<int>(1, 10), new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, IsAdminOnly = true}));
        _harmony = Harmony.CreateAndPatchAll(typeof(Main).Assembly, Guid);
      }
      catch (Exception e)
      {
        DMF.Logging.Log.Error(Instance, e);
      }
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
      try
      {
        DMF.Logging.Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        _harmony?.UnpatchSelf();
      }
      catch (Exception e)
      {
        DMF.Logging.Log.Error(Instance, e);
      }
    }

    #region Implementation of ITraceableLogging

    /// <inheritdoc />
    public string Source => Namespace;

    /// <inheritdoc />
    public bool EnableTrace { get; }

    #endregion
  }
}
