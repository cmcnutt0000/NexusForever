namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectVendorPriceModifierData : ISpellEffectData
    {
        float BuyPriceMultiplier { get; }
        float SellPriceMultiplier { get; }
    }
}
