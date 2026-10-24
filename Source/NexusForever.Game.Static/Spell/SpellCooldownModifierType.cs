namespace NexusForever.Game.Static.Spell
{
    /// <summary>
    /// Which cooldowns a ModifySpellCooldown spell effect changes, DataBits00 of the effect.
    /// </summary>
    /// <remarks>
    /// See 0x14046B920 in the client, the id in DataBits01 is compared with the cooling down spell's base spell, spell, spell groups or SpellCoolDown ids.
    /// </remarks>
    public enum SpellCooldownModifierType
    {
        BaseSpell  = 0,
        Spell      = 1,
        SpellGroup = 2,
        Cooldown   = 3
    }
}
