using Digitalroot.Modding.Framework.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Digitalroot.Valheim.EternalHeight
{
  [UsedImplicitly]
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  public class Patch
  {
    [HarmonyPatch(typeof(WearNTear))]
    public class PatchWearNTear
    {
      [HarmonyPrefix, HarmonyPatch(typeof(WearNTear), nameof(WearNTear.GetMaterialProperties))]
      private static bool PrefixGetMaterialProperties(WearNTear __instance, ref float maxSupport, ref float minSupport, ref float horizontalLoss, ref float verticalLoss)
      {
        try
        {
          // Log.Trace(Main.Instance, $"{Main.Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
          CallOriginal(__instance, ref maxSupport, ref minSupport, ref horizontalLoss, ref verticalLoss);
          horizontalLoss /= Main.hMulti.Value;
          verticalLoss /= Main.vMulti.Value;
        }
        catch (Exception e)
        {
          Log.Error(Main.Instance, e);
        }

        return false;
      }

      [HarmonyReversePatch, HarmonyPatch(typeof(WearNTear), nameof(WearNTear.GetMaterialProperties))]
      public static void CallOriginal(WearNTear __instance, ref float maxSupport, ref float minSupport, ref float horizontalLoss, ref float verticalLoss)
      {
        // Harmony replaces this body with the original method's IL at patch time.
        throw new NotImplementedException("HarmonyReversePatch");
      }
    }
  }
}
