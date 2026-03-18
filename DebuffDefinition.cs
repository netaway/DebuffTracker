using System.Numerics;

namespace DebuffTracker;

/// <summary>
/// Debuff category — used for grouping in the HUD and filtering in settings.
/// </summary>
public enum DebuffCategory
{
    Hex,            // AoE curses: Flammability, Vulnerability, etc.
    Mark,           // Single-target curses: Assassin's Mark, Warlord's Mark, etc.
    AilmentDmg,     // Damaging ailments: Ignite, Bleed, Poison
    AilmentNonDmg,  // Non-damaging ailments: Chill, Freeze, Shock, Scorch, Brittle, Sap
    Special,        // Special debuffs: Withered, Impale, Maim, Blind, etc.
}

/// <summary>
/// How the debuff value should be read and displayed.
/// </summary>
public enum DebuffValueType
{
    /// <summary>Active or inactive only — no numeric value.</summary>
    Binary,

    /// <summary>
    /// Integer stacks (e.g. Withered up to 15, Poison can stack many times).
    /// Read via buff.Charges or by counting buffs with the same id.
    /// </summary>
    Stacks,

    /// <summary>
    /// Float magnitude (e.g. Shock has % increased damage taken, Ignite has DPS).
    /// Read via buff.Timer or a specific field when available.
    /// </summary>
    Magnitude,
}

/// <summary>
/// Defines a trackable debuff: metadata, color, how to read it, and how to display it.
/// </summary>
public record DebuffDefinition(
    /// <summary>Internal buff id used in the game (used with buffs.HasBuff / buffs.GetBuff).</summary>
    string BuffId,

    /// <summary>Human-readable name shown in the HUD.</summary>
    string DisplayName,

    /// <summary>Category for grouping and filtering.</summary>
    DebuffCategory Category,

    /// <summary>RGBA color used in the HUD when active.</summary>
    Vector4 Color,

    /// <summary>How the value should be read.</summary>
    DebuffValueType ValueType = DebuffValueType.Binary,

    /// <summary>Maximum stacks (used only when ValueType == Stacks).</summary>
    int MaxStacks = 1,

    /// <summary>
    /// Suffix displayed after the numeric value (e.g. "x", "%", " dps").
    /// Ignored when ValueType == Binary.
    /// </summary>
    string ValueSuffix = ""
);
