using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace DebuffTracker;

public class DebuffTrackerSettings : ISettings
{
    public ToggleNode Enable { get; set; } = new ToggleNode(true);

    [Menu("Show Position Preview")]
    public ToggleNode ShowPreview { get; set; } = new ToggleNode(false);

    [Menu("Position X")]
    public RangeNode<int> HudX { get; set; } = new RangeNode<int>(20, 0, 3840);

    [Menu("Position Y")]
    public RangeNode<int> HudY { get; set; } = new RangeNode<int>(200, 0, 2160);

    [Menu("Window Opacity (0=invisible, 100=solid)")]
    public RangeNode<int> WindowAlpha { get; set; } = new RangeNode<int>(80, 0, 100);

    [Menu("Uppercase Text (simulates bold)")]
    public ToggleNode ShowBold { get; set; } = new ToggleNode(true);

    // -------------------------------------------------------------------------
    // Monster Rarities
    // -------------------------------------------------------------------------
    [Menu("Show Magic Monsters")]
    public ToggleNode ShowMagic { get; set; } = new ToggleNode(false);

    [Menu("Show Rare Monsters")]
    public ToggleNode ShowRare { get; set; } = new ToggleNode(true);

    [Menu("Show Unique Monsters")]
    public ToggleNode ShowUnique { get; set; } = new ToggleNode(true);

    // -------------------------------------------------------------------------
    // Display
    // -------------------------------------------------------------------------
    [Menu("Hide Inactive Debuffs")]
    public ToggleNode HideInactive { get; set; } = new ToggleNode(false);

    [Menu("Group by Category")]
    public ToggleNode GroupByCategory { get; set; } = new ToggleNode(true);

    // -------------------------------------------------------------------------
    // HEXES
    // -------------------------------------------------------------------------
    [Menu("--- Hexes ---")]
    public ToggleNode TrackConductivity { get; set; } = new ToggleNode(true);

    [Menu("Track Vulnerability")]
    public ToggleNode TrackVulnerability { get; set; } = new ToggleNode(true);

    [Menu("Track Flammability")]
    public ToggleNode TrackFlammability { get; set; } = new ToggleNode(true);

    [Menu("Track Frostbite")]
    public ToggleNode TrackFrostbite { get; set; } = new ToggleNode(true);

    [Menu("Track Enfeeble")]
    public ToggleNode TrackEnfeeble { get; set; } = new ToggleNode(true);

    [Menu("Track Temporal Chains")]
    public ToggleNode TrackTemporalChains { get; set; } = new ToggleNode(true);

    [Menu("Track Elemental Weakness")]
    public ToggleNode TrackElementalWeakness { get; set; } = new ToggleNode(false);

    [Menu("Track Despair")]
    public ToggleNode TrackDespair { get; set; } = new ToggleNode(false);

    [Menu("Track Punishment")]
    public ToggleNode TrackPunishment { get; set; } = new ToggleNode(false);

    // -------------------------------------------------------------------------
    // MARKS
    // -------------------------------------------------------------------------
    [Menu("--- Marks ---")]
    public ToggleNode TrackAssassinsMark { get; set; } = new ToggleNode(false);

    [Menu("Track Poacher's Mark")]
    public ToggleNode TrackPoachersMark { get; set; } = new ToggleNode(false);

    [Menu("Track Sniper's Mark")]
    public ToggleNode TrackSnipersMark { get; set; } = new ToggleNode(false);

    [Menu("Track Warlord's Mark")]
    public ToggleNode TrackWarlordsMark { get; set; } = new ToggleNode(false);

    [Menu("Track Alchemist's Mark")]
    public ToggleNode TrackAlchemistsMark { get; set; } = new ToggleNode(false);

    [Menu("Track Penance Mark")]
    public ToggleNode TrackPenanceMark { get; set; } = new ToggleNode(false);

    // -------------------------------------------------------------------------
    // AILMENTS — Damaging
    // -------------------------------------------------------------------------
    [Menu("--- Ailments (Damaging) ---")]
    public ToggleNode TrackIgnite { get; set; } = new ToggleNode(false);

    [Menu("Track Bleed")]
    public ToggleNode TrackBleed { get; set; } = new ToggleNode(false);

    [Menu("Track Poison")]
    public ToggleNode TrackPoison { get; set; } = new ToggleNode(false);

    // -------------------------------------------------------------------------
    // AILMENTS — Non-Damaging
    // -------------------------------------------------------------------------
    [Menu("--- Ailments (Non-Damaging) ---")]
    public ToggleNode TrackChill { get; set; } = new ToggleNode(false);

    [Menu("Track Freeze")]
    public ToggleNode TrackFreeze { get; set; } = new ToggleNode(false);

    [Menu("Track Shock")]
    public ToggleNode TrackShock { get; set; } = new ToggleNode(false);

    [Menu("Track Scorch")]
    public ToggleNode TrackScorch { get; set; } = new ToggleNode(false);

    [Menu("Track Brittle")]
    public ToggleNode TrackBrittle { get; set; } = new ToggleNode(false);

    [Menu("Track Sap")]
    public ToggleNode TrackSap { get; set; } = new ToggleNode(false);

    // -------------------------------------------------------------------------
    // SPECIAL DEBUFFS
    // -------------------------------------------------------------------------
    [Menu("--- Special Debuffs ---")]
    public ToggleNode TrackWithered { get; set; } = new ToggleNode(false);

    [Menu("Track Impale")]
    public ToggleNode TrackImpale { get; set; } = new ToggleNode(false);

    [Menu("Track Maim")]
    public ToggleNode TrackMaim { get; set; } = new ToggleNode(false);

    [Menu("Track Hinder")]
    public ToggleNode TrackHinder { get; set; } = new ToggleNode(false);

    [Menu("Track Blind")]
    public ToggleNode TrackBlind { get; set; } = new ToggleNode(false);

    [Menu("Track Intimidate")]
    public ToggleNode TrackIntimidate { get; set; } = new ToggleNode(false);

    [Menu("Track Unnerve")]
    public ToggleNode TrackUnnerve { get; set; } = new ToggleNode(false);
}
