using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace DebuffTracker;

// No [Menu] attributes needed here — the settings UI is rendered manually
// via DrawSettings() in DebuffTracker.cs using ImGui.CollapsingHeader.
public class DebuffTrackerSettings : ISettings
{
    // General
    public ToggleNode Enable       { get; set; } = new ToggleNode(true);
    public ToggleNode ShowPreview  { get; set; } = new ToggleNode(false);
    public ToggleNode ShowBold     { get; set; } = new ToggleNode(true);
    public ToggleNode HideInactive { get; set; } = new ToggleNode(false);
    public ToggleNode GroupByCategory { get; set; } = new ToggleNode(true);

    public RangeNode<int> HudX        { get; set; } = new RangeNode<int>(20,  0,    3840);
    public RangeNode<int> HudY        { get; set; } = new RangeNode<int>(200, 0,    2160);
    public RangeNode<int> WindowAlpha { get; set; } = new RangeNode<int>(80,  0,    100);

    // Monster Rarities
    public ToggleNode ShowMagic  { get; set; } = new ToggleNode(false);
    public ToggleNode ShowRare   { get; set; } = new ToggleNode(true);
    public ToggleNode ShowUnique { get; set; } = new ToggleNode(true);

    // Hexes
    public ToggleNode TrackConductivity      { get; set; } = new ToggleNode(true);
    public ToggleNode TrackVulnerability     { get; set; } = new ToggleNode(true);
    public ToggleNode TrackFlammability      { get; set; } = new ToggleNode(true);
    public ToggleNode TrackFrostbite         { get; set; } = new ToggleNode(true);
    public ToggleNode TrackEnfeeble          { get; set; } = new ToggleNode(true);
    public ToggleNode TrackTemporalChains    { get; set; } = new ToggleNode(true);
    public ToggleNode TrackElementalWeakness { get; set; } = new ToggleNode(false);
    public ToggleNode TrackDespair           { get; set; } = new ToggleNode(false);
    public ToggleNode TrackPunishment        { get; set; } = new ToggleNode(false);

    // Marks
    public ToggleNode TrackAssassinsMark  { get; set; } = new ToggleNode(false);
    public ToggleNode TrackPoachersMark   { get; set; } = new ToggleNode(false);
    public ToggleNode TrackSnipersMark    { get; set; } = new ToggleNode(false);
    public ToggleNode TrackWarlordsMark   { get; set; } = new ToggleNode(false);
    public ToggleNode TrackAlchemistsMark { get; set; } = new ToggleNode(false);
    public ToggleNode TrackPenanceMark    { get; set; } = new ToggleNode(false);

    // Ailments — Damaging
    public ToggleNode TrackIgnite { get; set; } = new ToggleNode(false);
    public ToggleNode TrackBleed  { get; set; } = new ToggleNode(false);
    public ToggleNode TrackPoison { get; set; } = new ToggleNode(false);

    // Ailments — Non-Damaging
    public ToggleNode TrackChill   { get; set; } = new ToggleNode(false);
    public ToggleNode TrackFreeze  { get; set; } = new ToggleNode(false);
    public ToggleNode TrackShock        { get; set; } = new ToggleNode(false);
    public ToggleNode TrackStackingShock { get; set; } = new ToggleNode(false);
    public ToggleNode TrackScorch  { get; set; } = new ToggleNode(false);
    public ToggleNode TrackBrittle { get; set; } = new ToggleNode(false);
    public ToggleNode TrackSap     { get; set; } = new ToggleNode(false);

    // Special Debuffs
    public ToggleNode TrackWithered   { get; set; } = new ToggleNode(false);
    public ToggleNode TrackImpale     { get; set; } = new ToggleNode(false);
    public ToggleNode TrackMaim       { get; set; } = new ToggleNode(false);
    public ToggleNode TrackHinder     { get; set; } = new ToggleNode(false);
    public ToggleNode TrackBlind      { get; set; } = new ToggleNode(false);
    public ToggleNode TrackIntimidate { get; set; } = new ToggleNode(false);
    public ToggleNode TrackUnnerve    { get; set; } = new ToggleNode(false);
}
