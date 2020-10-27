using System.Collections.Generic;

public class Tile
{

    List<TileEffect> tileEffects { get; set; } = new List<TileEffect>();
   

    public void ApplyEffectToTile(TileEffect effect)
    {
        tileEffects.Add(effect);
    }


}