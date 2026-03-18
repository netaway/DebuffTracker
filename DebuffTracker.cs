using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;
using ImGuiNET;

namespace DebuffTracker;

public class DebuffTracker : BaseSettingsPlugin<DebuffTrackerSettings>
{
    private static readonly Vector4 ColorGold    = new(1.00f, 0.84f, 0.00f, 1f);
    private static readonly Vector4 ColorMissing = new(0.47f, 0.47f, 0.47f, 1f);
    private static readonly Vector4 ColorHeader  = new(1.00f, 1.00f, 1.00f, 1f);

    private static readonly Dictionary<DebuffCategory, string> CategoryLabels = new()
    {
        { DebuffCategory.Hex,           "Hexes"    },
        { DebuffCategory.Mark,          "Marks"    },
        { DebuffCategory.AilmentDmg,    "Ailments" },
        { DebuffCategory.AilmentNonDmg, "Ailments" },
        { DebuffCategory.Special,       "Special"  },
    };

    // Prevents the HUD window from being drawn more than once per frame
    private long _lastRenderedFrame = -1;

    // -------------------------------------------------------------------------
    // Settings UI — collapsible categories
    // -------------------------------------------------------------------------
    public override void DrawSettings()
    {
        var s = Settings;

        // General
        DrawToggle("Show Position Preview",           s.ShowPreview);
        DrawToggle("Uppercase Text (simulates bold)", s.ShowBold);
        DrawToggle("Hide Inactive Debuffs",           s.HideInactive);
        DrawToggle("Group by Category",               s.GroupByCategory);
        DrawSliderInt("Position X",                   s.HudX,       0, 3840);
        DrawSliderInt("Position Y",                   s.HudY,       0, 2160);
        DrawSliderInt("Window Opacity (0=invisible, 100=solid)", s.WindowAlpha, 0, 100);
        ImGui.Separator();

        // Monster Rarities
        if (ImGui.CollapsingHeader("Monster Rarities"))
        {
            ImGui.Indent();
            DrawToggle("Show Magic Monsters",  s.ShowMagic);
            DrawToggle("Show Rare Monsters",   s.ShowRare);
            DrawToggle("Show Unique Monsters", s.ShowUnique);
            ImGui.Unindent();
        }

        // Hexes
        if (ImGui.CollapsingHeader("Hexes"))
        {
            ImGui.Indent();
            DrawToggle("Conductivity",      s.TrackConductivity);
            DrawToggle("Vulnerability",     s.TrackVulnerability);
            DrawToggle("Flammability",      s.TrackFlammability);
            DrawToggle("Frostbite",         s.TrackFrostbite);
            DrawToggle("Enfeeble",          s.TrackEnfeeble);
            DrawToggle("Temporal Chains",   s.TrackTemporalChains);
            DrawToggle("Elemental Weakness",s.TrackElementalWeakness);
            DrawToggle("Despair",           s.TrackDespair);
            DrawToggle("Punishment",        s.TrackPunishment);
            ImGui.Unindent();
        }

        // Marks
        if (ImGui.CollapsingHeader("Marks"))
        {
            ImGui.Indent();
            DrawToggle("Assassin's Mark",  s.TrackAssassinsMark);
            DrawToggle("Poacher's Mark",   s.TrackPoachersMark);
            DrawToggle("Sniper's Mark",    s.TrackSnipersMark);
            DrawToggle("Warlord's Mark",   s.TrackWarlordsMark);
            DrawToggle("Alchemist's Mark", s.TrackAlchemistsMark);
            DrawToggle("Penance Mark",     s.TrackPenanceMark);
            ImGui.Unindent();
        }

        // Ailments (Damaging)
        if (ImGui.CollapsingHeader("Ailments (Damaging)"))
        {
            ImGui.Indent();
            DrawToggle("Ignite", s.TrackIgnite);
            DrawToggle("Bleed",  s.TrackBleed);
            DrawToggle("Poison", s.TrackPoison);
            ImGui.Unindent();
        }

        // Ailments (Non-Damaging)
        if (ImGui.CollapsingHeader("Ailments (Non-Damaging)"))
        {
            ImGui.Indent();
            DrawToggle("Chill",   s.TrackChill);
            DrawToggle("Freeze",  s.TrackFreeze);
            DrawToggle("Shock",   s.TrackShock);
            DrawToggle("Scorch",  s.TrackScorch);
            DrawToggle("Brittle", s.TrackBrittle);
            DrawToggle("Sap",     s.TrackSap);
            ImGui.Unindent();
        }

        // Special Debuffs
        if (ImGui.CollapsingHeader("Special Debuffs"))
        {
            ImGui.Indent();
            DrawToggle("Withered",   s.TrackWithered);
            DrawToggle("Impale",     s.TrackImpale);
            DrawToggle("Maim",       s.TrackMaim);
            DrawToggle("Hinder",     s.TrackHinder);
            DrawToggle("Blind",      s.TrackBlind);
            DrawToggle("Intimidate", s.TrackIntimidate);
            DrawToggle("Unnerve",    s.TrackUnnerve);
            ImGui.Unindent();
        }
    }

    // -------------------------------------------------------------------------
    // DrawSettings helpers
    // -------------------------------------------------------------------------
    private static void DrawToggle(string label, ExileCore.Shared.Nodes.ToggleNode node)
    {
        var value = node.Value;
        if (ImGui.Checkbox(label, ref value))
            node.Value = value;
    }

    private static void DrawSliderInt(string label, ExileCore.Shared.Nodes.RangeNode<int> node, int min, int max)
    {
        var value = node.Value;
        if (ImGui.SliderInt(label, ref value, min, max))
            node.Value = value;
    }

    // -------------------------------------------------------------------------
    // Build the active definition list from current settings
    // -------------------------------------------------------------------------
    private List<DebuffDefinition> GetActiveDefinitions()
    {
        var s = Settings;
        var active = new List<DebuffDefinition>();

        // Hexes
        if (s.TrackConductivity.Value)      active.Add(DebuffCatalog.Conductivity);
        if (s.TrackVulnerability.Value)     active.Add(DebuffCatalog.Vulnerability);
        if (s.TrackFlammability.Value)      active.Add(DebuffCatalog.Flammability);
        if (s.TrackFrostbite.Value)         active.Add(DebuffCatalog.Frostbite);
        if (s.TrackEnfeeble.Value)          active.Add(DebuffCatalog.Enfeeble);
        if (s.TrackTemporalChains.Value)    active.Add(DebuffCatalog.TemporalChains);
        if (s.TrackElementalWeakness.Value) active.Add(DebuffCatalog.ElementalWeakness);
        if (s.TrackDespair.Value)           active.Add(DebuffCatalog.Despair);
        if (s.TrackPunishment.Value)        active.Add(DebuffCatalog.Punishment);

        // Marks
        if (s.TrackAssassinsMark.Value)     active.Add(DebuffCatalog.AssassinsMark);
        if (s.TrackPoachersMark.Value)      active.Add(DebuffCatalog.PoachersMark);
        if (s.TrackSnipersMark.Value)       active.Add(DebuffCatalog.SnipersMark);
        if (s.TrackWarlordsMark.Value)      active.Add(DebuffCatalog.WarlordsMark);
        if (s.TrackAlchemistsMark.Value)    active.Add(DebuffCatalog.AlchemistsMark);
        if (s.TrackPenanceMark.Value)       active.Add(DebuffCatalog.PenanceMark);

        // Damaging ailments
        if (s.TrackIgnite.Value)            active.Add(DebuffCatalog.Ignite);
        if (s.TrackBleed.Value)             active.Add(DebuffCatalog.Bleed);
        if (s.TrackPoison.Value)            active.Add(DebuffCatalog.Poison);

        // Non-damaging ailments
        if (s.TrackChill.Value)             active.Add(DebuffCatalog.Chill);
        if (s.TrackFreeze.Value)            active.Add(DebuffCatalog.Freeze);
        if (s.TrackShock.Value)             active.Add(DebuffCatalog.Shock);
        if (s.TrackScorch.Value)            active.Add(DebuffCatalog.Scorch);
        if (s.TrackBrittle.Value)           active.Add(DebuffCatalog.Brittle);
        if (s.TrackSap.Value)               active.Add(DebuffCatalog.Sap);

        // Special debuffs
        if (s.TrackWithered.Value)          active.Add(DebuffCatalog.Withered);
        if (s.TrackImpale.Value)            active.Add(DebuffCatalog.Impale);
        if (s.TrackMaim.Value)              active.Add(DebuffCatalog.Maim);
        if (s.TrackHinder.Value)            active.Add(DebuffCatalog.Hinder);
        if (s.TrackBlind.Value)             active.Add(DebuffCatalog.Blind);
        if (s.TrackIntimidate.Value)        active.Add(DebuffCatalog.Intimidate);
        if (s.TrackUnnerve.Value)           active.Add(DebuffCatalog.Unnerve);

        return active;
    }

    // -------------------------------------------------------------------------
    // Render
    // -------------------------------------------------------------------------
    public override void Render()
    {
        if (!Settings.Enable) return;

        // Guard against Render() being called more than once per frame
        var currentFrame = GameController.IngameState.UpdateCount;
        if (currentFrame == _lastRenderedFrame) return;
        _lastRenderedFrame = currentFrame;

        var definitions = GetActiveDefinitions();

        if (Settings.ShowPreview.Value)
        {
            RenderPreview(definitions);
            return;
        }

        if (definitions.Count == 0) return;

        var monsters = GameController.EntityListWrapper
            .ValidEntitiesByType[EntityType.Monster]
            .Where(e => e.IsValid && e.IsAlive && e.IsHostile &&
                (e.Rarity == MonsterRarity.Unique && Settings.ShowUnique.Value ||
                 e.Rarity == MonsterRarity.Rare   && Settings.ShowRare.Value   ||
                 e.Rarity == MonsterRarity.Magic  && Settings.ShowMagic.Value))
            .GroupBy(e => e.Id)
            .Select(g => g.First())
            .ToList();

        if (monsters.Count == 0) return;

        var monsterData = new List<(string Name, List<DebuffState> States)>();
        foreach (var monster in monsters)
        {
            var name = monster.RenderName
                ?? monster.Metadata?.Split('/').LastOrDefault()
                ?? "Unknown";

            var states = DebuffReader.Read(monster, definitions);
            if (states.Count == 0) continue;

            if (Settings.HideInactive.Value && !states.Any(s => s.IsActive)) continue;

            monsterData.Add((name, states));
        }

        if (monsterData.Count == 0) return;

        RenderWindow(monsterData);
    }

    // -------------------------------------------------------------------------
    // Main window
    // -------------------------------------------------------------------------
    private void RenderWindow(List<(string Name, List<DebuffState> States)> data)
    {
        ImGui.SetNextWindowPos(new Vector2(Settings.HudX.Value, Settings.HudY.Value), ImGuiCond.Always);
        ImGui.SetNextWindowBgAlpha(Settings.WindowAlpha.Value / 100f);

        var flags = ImGuiWindowFlags.NoTitleBar
                  | ImGuiWindowFlags.NoResize
                  | ImGuiWindowFlags.NoScrollbar
                  | ImGuiWindowFlags.NoInputs
                  | ImGuiWindowFlags.NoCollapse
                  | ImGuiWindowFlags.NoNav
                  | ImGuiWindowFlags.NoMove
                  | ImGuiWindowFlags.AlwaysAutoResize;

        if (!ImGui.Begin("##DebuffTracker", flags)) { ImGui.End(); return; }

        DrawHeader("[ DEBUFF TRACKER ]");
        ImGui.Separator();

        foreach (var (name, states) in data)
        {
            DrawMonsterBlock(name, states);
            ImGui.Spacing();
        }

        ImGui.End();
    }

    // -------------------------------------------------------------------------
    // Preview window
    // -------------------------------------------------------------------------
    private void RenderPreview(List<DebuffDefinition> definitions)
    {
        ImGui.SetNextWindowPos(new Vector2(Settings.HudX.Value, Settings.HudY.Value), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(Settings.WindowAlpha.Value / 100f);

        var flags = ImGuiWindowFlags.NoTitleBar
                  | ImGuiWindowFlags.NoResize
                  | ImGuiWindowFlags.NoScrollbar
                  | ImGuiWindowFlags.NoCollapse
                  | ImGuiWindowFlags.NoNav
                  | ImGuiWindowFlags.AlwaysAutoResize;

        if (!ImGui.Begin("##DebuffTrackerPreview", flags)) { ImGui.End(); return; }

        DrawHeader("[ DEBUFF TRACKER - PREVIEW ] (drag to reposition)");
        ImGui.Separator();

        var fakeStates = definitions.Select((def, i) => new DebuffState
        {
            Definition = def,
            IsActive   = i % 2 == 0,
            Value      = def.ValueType == DebuffValueType.Stacks    ? def.MaxStacks * 0.7f :
                         def.ValueType == DebuffValueType.Magnitude ? 40f : 0f,
        }).ToList();

        DrawMonsterBlock("Example Unique Monster", fakeStates);

        var pos = ImGui.GetWindowPos();
        Settings.HudX.Value = (int)pos.X;
        Settings.HudY.Value = (int)pos.Y;

        ImGui.End();
    }

    // -------------------------------------------------------------------------
    // HUD drawing helpers
    // -------------------------------------------------------------------------
    private void DrawHeader(string text)
    {
        ImGui.TextColored(ColorHeader, Settings.ShowBold.Value ? text.ToUpper() : text);
    }

    private void DrawMonsterBlock(string monsterName, List<DebuffState> states)
    {
        ImGui.TextColored(ColorGold, Settings.ShowBold.Value ? monsterName.ToUpper() : monsterName);

        if (Settings.GroupByCategory.Value)
            DrawGrouped(states);
        else
            DrawFlat(states);
    }

    private void DrawGrouped(List<DebuffState> states)
    {
        var groups = states
            .GroupBy(s => s.Definition.Category)
            .OrderBy(g => (int)g.Key);

        foreach (var group in groups)
        {
            if (Settings.HideInactive.Value && !group.Any(s => s.IsActive)) continue;

            ImGui.TextColored(ColorMissing, $"  {CategoryLabels[group.Key]}:");
            foreach (var state in group)
                DrawDebuffLine(state);
        }
    }

    private void DrawFlat(List<DebuffState> states)
    {
        foreach (var state in states)
            DrawDebuffLine(state);
    }

    private void DrawDebuffLine(DebuffState state)
    {
        if (!state.IsActive && Settings.HideInactive.Value) return;

        if (state.IsActive)
            ImGui.TextColored(state.Definition.Color, $"  [v] {state.DisplayText}");
        else
            ImGui.TextColored(ColorMissing, $"  [x] {state.Definition.DisplayName}");
    }
}
