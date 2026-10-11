using System.Diagnostics.CodeAnalysis;

namespace Digitalroot.Valheim.EternalHeight
{
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public partial class Main
  {
    public const string Version = "0.0.1";
    public const string Name = "Eternal Height";
    public const string Guid = "digitalroot.mods.eternalheight";
    public const string Namespace = $"{nameof(Digitalroot)}.{nameof(Valheim)}.{nameof(EternalHeight)}";

    internal static class PluginConfigSection
    {
      internal static readonly string General = nameof(General);
    }
  }
}
