using System.Collections.Generic;
using System.Numerics;

namespace DebuffTracker;

/// <summary>
/// Central catalog of all trackable debuffs.
/// To add a new debuff: add an entry here and a toggle in DebuffTrackerSettings.
/// No other class needs to be changed.
/// </summary>
public static class DebuffCatalog
{
    private static Vector4 RGB(float r, float g, float b) => new(r / 255f, g / 255f, b / 255f, 1f);

    // -------------------------------------------------------------------------
    // HEXES
    // -------------------------------------------------------------------------
    public static readonly DebuffDefinition Conductivity = new(
        BuffId:      "curse_lightning_weakness",
        DisplayName: "Conductivity",
        Category:    DebuffCategory.Hex,
        Color:       RGB(100, 181, 255)
    );

    public static readonly DebuffDefinition Vulnerability = new(
        BuffId:      "curse_vulnerability",
        DisplayName: "Vulnerability",
        Category:    DebuffCategory.Hex,
        Color:       RGB(220, 79, 79)
    );

    public static readonly DebuffDefinition Flammability = new(
        BuffId:      "curse_fire_weakness",
        DisplayName: "Flammability",
        Category:    DebuffCategory.Hex,
        Color:       RGB(255, 140, 40)
    );

    public static readonly DebuffDefinition Frostbite = new(
        BuffId:      "curse_cold_weakness",
        DisplayName: "Frostbite",
        Category:    DebuffCategory.Hex,
        Color:       RGB(79, 199, 255)
    );

    public static readonly DebuffDefinition Enfeeble = new(
        BuffId:      "curse_enfeeble",
        DisplayName: "Enfeeble",
        Category:    DebuffCategory.Hex,
        Color:       RGB(181, 100, 255)
    );

    public static readonly DebuffDefinition TemporalChains = new(
        BuffId:      "curse_temporal_chains",
        DisplayName: "Temporal Chains",
        Category:    DebuffCategory.Hex,
        Color:       RGB(199, 199, 61)
    );

    public static readonly DebuffDefinition ElementalWeakness = new(
        BuffId:      "curse_elemental_weakness",
        DisplayName: "Elemental Weakness",
        Category:    DebuffCategory.Hex,
        Color:       RGB(199, 255, 100)
    );

    public static readonly DebuffDefinition Despair = new(
        BuffId:      "curse_chaos_weakness",
        DisplayName: "Despair",
        Category:    DebuffCategory.Hex,
        Color:       RGB(130, 61, 181)
    );

    public static readonly DebuffDefinition Punishment = new(
        BuffId:      "curse_newpunishment",
        DisplayName: "Punishment",
        Category:    DebuffCategory.Hex,
        Color:       RGB(255, 61, 61)
    );

    // -------------------------------------------------------------------------
    // MARKS
    // Source: https://poedb.tw/us/Mark
    // All 5 active mark skills in PoE1 are listed here.
    // Buff ids confirmed via poedb / community sources — verify with ExileApi if needed.
    // -------------------------------------------------------------------------
    public static readonly DebuffDefinition AssassinsMark = new(
        BuffId:      "mark_assassin",
        DisplayName: "Assassin's Mark",
        Category:    DebuffCategory.Mark,
        Color:       RGB(255, 220, 60)
    );

    public static readonly DebuffDefinition PoachersMark = new(
        BuffId:      "mark_poacher",
        DisplayName: "Poacher's Mark",
        Category:    DebuffCategory.Mark,
        Color:       RGB(100, 220, 100)
    );

    public static readonly DebuffDefinition SnipersMark = new(
        BuffId:      "mark_sniper",
        DisplayName: "Sniper's Mark",
        Category:    DebuffCategory.Mark,
        Color:       RGB(180, 230, 255)
    );

    public static readonly DebuffDefinition WarlordsMark = new(
        BuffId:      "mark_warlord",
        DisplayName: "Warlord's Mark",
        Category:    DebuffCategory.Mark,
        Color:       RGB(210, 120, 60)   // warm orange — endurance charge theme
    );

    public static readonly DebuffDefinition AlchemistsMark = new(
        BuffId:      "mark_alchemist",
        DisplayName: "Alchemist's Mark",
        Category:    DebuffCategory.Mark,
        Color:       RGB(180, 255, 120)  // yellow-green — flask / alchemy theme
    );

    public static readonly DebuffDefinition PenanceMark = new(
        BuffId:      "mark_penance",
        DisplayName: "Penance Mark",
        Category:    DebuffCategory.Mark,
        Color:       RGB(220, 180, 255)  // pale purple — phantasm theme
    );

    // -------------------------------------------------------------------------
    // AILMENTS — Damaging
    // -------------------------------------------------------------------------
    public static readonly DebuffDefinition Ignite = new(
        BuffId:      "ignited",
        DisplayName: "Ignite",
        Category:    DebuffCategory.AilmentDmg,
        Color:       RGB(255, 120, 30),
        ValueType:   DebuffValueType.Stacks,
        MaxStacks:   99,
        ValueSuffix: "x"
    );

    public static readonly DebuffDefinition Bleed = new(
        BuffId:      "bleeding",
        DisplayName: "Bleed",
        Category:    DebuffCategory.AilmentDmg,
        Color:       RGB(210, 30, 30),
        ValueType:   DebuffValueType.Stacks,
        MaxStacks:   99,
        ValueSuffix: "x"
    );

    public static readonly DebuffDefinition Poison = new(
        BuffId:      "poison",
        DisplayName: "Poison",
        Category:    DebuffCategory.AilmentDmg,
        Color:       RGB(100, 210, 80),
        ValueType:   DebuffValueType.Stacks,
        MaxStacks:   999,
        ValueSuffix: "x"
    );

    // -------------------------------------------------------------------------
    // AILMENTS — Non-Damaging
    // -------------------------------------------------------------------------
    public static readonly DebuffDefinition Chill = new(
        BuffId:      "chilled",
        DisplayName: "Chill",
        Category:    DebuffCategory.AilmentNonDmg,
        Color:       RGB(140, 220, 255)
    );

    public static readonly DebuffDefinition Freeze = new(
        BuffId:      "frozen",
        DisplayName: "Freeze",
        Category:    DebuffCategory.AilmentNonDmg,
        Color:       RGB(200, 240, 255)
    );

    public static readonly DebuffDefinition Shock = new(
        BuffId:      "shocked",
        DisplayName: "Shock",
        Category:    DebuffCategory.AilmentNonDmg,
        Color:       RGB(255, 255, 80),
        ValueType:   DebuffValueType.Magnitude,
        ValueSuffix: "%"
    );

    public static readonly DebuffDefinition Scorch = new(
        BuffId:      "scorched",
        DisplayName: "Scorch",
        Category:    DebuffCategory.AilmentNonDmg,
        Color:       RGB(255, 160, 60),
        ValueType:   DebuffValueType.Magnitude,
        ValueSuffix: "% -res"
    );

    public static readonly DebuffDefinition Brittle = new(
        BuffId:      "brittle",
        DisplayName: "Brittle",
        Category:    DebuffCategory.AilmentNonDmg,
        Color:       RGB(180, 230, 255),
        ValueType:   DebuffValueType.Magnitude,
        ValueSuffix: "% crit"
    );

    public static readonly DebuffDefinition Sap = new(
        BuffId:      "sapped",
        DisplayName: "Sap",
        Category:    DebuffCategory.AilmentNonDmg,
        Color:       RGB(200, 200, 100),
        ValueType:   DebuffValueType.Magnitude,
        ValueSuffix: "% -dmg"
    );

    // -------------------------------------------------------------------------
    // SPECIAL DEBUFFS
    // -------------------------------------------------------------------------
    public static readonly DebuffDefinition Withered = new(
        BuffId:      "withered",
        DisplayName: "Withered",
        Category:    DebuffCategory.Special,
        Color:       RGB(160, 80, 210),
        ValueType:   DebuffValueType.Stacks,
        MaxStacks:   15,
        ValueSuffix: "/15"
    );

    public static readonly DebuffDefinition Impale = new(
        BuffId:      "impaled",
        DisplayName: "Impale",
        Category:    DebuffCategory.Special,
        Color:       RGB(200, 200, 200),
        ValueType:   DebuffValueType.Stacks,
        MaxStacks:   5,
        ValueSuffix: "/5"
    );

    public static readonly DebuffDefinition Maim = new(
        BuffId:      "maimed",
        DisplayName: "Maim",
        Category:    DebuffCategory.Special,
        Color:       RGB(210, 140, 60)
    );

    public static readonly DebuffDefinition Hinder = new(
        BuffId:      "hindered",
        DisplayName: "Hinder",
        Category:    DebuffCategory.Special,
        Color:       RGB(180, 140, 80)
    );

    public static readonly DebuffDefinition Blind = new(
        BuffId:      "blinded",
        DisplayName: "Blind",
        Category:    DebuffCategory.Special,
        Color:       RGB(80, 80, 80)
    );

    public static readonly DebuffDefinition Intimidate = new(
        BuffId:      "intimidated",
        DisplayName: "Intimidate",
        Category:    DebuffCategory.Special,
        Color:       RGB(255, 100, 50)
    );

    public static readonly DebuffDefinition Unnerve = new(
        BuffId:      "unnerved",
        DisplayName: "Unnerve",
        Category:    DebuffCategory.Special,
        Color:       RGB(200, 80, 200)
    );

    // -------------------------------------------------------------------------
    // MASTER LIST — single source of truth used by the plugin.
    // Order here = display order per category in the HUD.
    // -------------------------------------------------------------------------
    public static readonly IReadOnlyList<DebuffDefinition> All = new List<DebuffDefinition>
    {
        // Hexes
        Conductivity, Vulnerability, Flammability, Frostbite,
        Enfeeble, TemporalChains, ElementalWeakness, Despair, Punishment,

        // Marks (all active mark skills in PoE1)
        AssassinsMark, PoachersMark, SnipersMark, WarlordsMark, AlchemistsMark, PenanceMark,

        // Damaging ailments
        Ignite, Bleed, Poison,

        // Non-damaging ailments
        Chill, Freeze, Shock, Scorch, Brittle, Sap,

        // Special debuffs
        Withered, Impale, Maim, Hinder, Blind, Intimidate, Unnerve,
    };
}
