namespace DebuffTracker;

/// <summary>
/// Runtime snapshot of a debuff on a specific monster.
/// Produced by DebuffReader every frame.
/// </summary>
public readonly struct DebuffState
{
    /// <summary>Static definition of the debuff (metadata, color, type).</summary>
    public DebuffDefinition Definition { get; init; }

    /// <summary>True if the buff is currently active on the monster.</summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Numeric value of the debuff when active.
    /// - Binary    → always 0
    /// - Stacks    → number of stacks (e.g. 8 poisons, 12 withered)
    /// - Magnitude → rounded float value (e.g. 40 for a 40% Shock)
    /// </summary>
    public float Value { get; init; }

    /// <summary>
    /// Text ready to display in the HUD.
    /// Examples: "Withered  12/15", "Shock  40%", "Ignite  3x", "Freeze"
    /// </summary>
    public string DisplayText => Definition.ValueType switch
    {
        DebuffValueType.Binary    => Definition.DisplayName,
        DebuffValueType.Stacks    => $"{Definition.DisplayName}  {(int)Value}{Definition.ValueSuffix}",
        DebuffValueType.Magnitude => $"{Definition.DisplayName}  {Value:F0}{Definition.ValueSuffix}",
        _                         => Definition.DisplayName,
    };
}
