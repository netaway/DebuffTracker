using System.Collections.Generic;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;

namespace DebuffTracker;

/// <summary>
/// Reads debuff states from a monster entity.
/// Keeps memory-reading logic isolated from rendering.
/// </summary>
public static class DebuffReader
{
    /// <summary>
    /// Reads all debuff states for the given definitions from a monster entity.
    /// </summary>
    /// <param name="monster">Target monster entity.</param>
    /// <param name="definitions">Subset of DebuffDefinitions to check (filtered by settings).</param>
    /// <returns>List of DebuffState, one per definition, in the same order.</returns>
    public static List<DebuffState> Read(Entity monster, IEnumerable<DebuffDefinition> definitions)
    {
        var result = new List<DebuffState>();
        var buffs = monster.GetComponent<Buffs>();

        if (buffs == null) return result;

        foreach (var def in definitions)
            result.Add(ReadSingle(buffs, def));

        return result;
    }

    private static DebuffState ReadSingle(Buffs buffs, DebuffDefinition def)
    {
        switch (def.ValueType)
        {
            case DebuffValueType.Binary:
            {
                return new DebuffState
                {
                    Definition = def,
                    IsActive   = buffs.HasBuff(def.BuffId),
                    Value      = 0,
                };
            }

            case DebuffValueType.Stacks:
            {
                // Count how many buffs with this id exist (each stack = a separate entry).
                // Fallback: use Charges if the game consolidates stacks into a single buff.
                int count = 0;
                bool found = false;

                foreach (var buff in buffs.BuffsList)
                {
                    if (buff.Name != def.BuffId) continue;
                    found = true;
                    count += buff.Charges > 0 ? buff.Charges : 1;
                }

                return new DebuffState { Definition = def, IsActive = found, Value = count };
            }

            case DebuffValueType.Magnitude:
            {
                // For ailments like Shock, the magnitude is stored in buff.Timer.
                // Note: buff.Timer may not be the real magnitude in all cases —
                // check poedb.tw for the exact field exposed by ExileApi per ailment.
                float magnitude = 0f;
                bool found = false;

                foreach (var buff in buffs.BuffsList)
                {
                    if (buff.Name != def.BuffId) continue;
                    found = true;
                    magnitude = buff.Timer * 100f; // heuristic: normalize to 0–100 range
                    break;
                }

                return new DebuffState { Definition = def, IsActive = found, Value = magnitude };
            }

            default:
                return new DebuffState { Definition = def, IsActive = false, Value = 0 };
        }
    }
}
