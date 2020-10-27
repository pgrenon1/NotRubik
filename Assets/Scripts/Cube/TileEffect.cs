public abstract class TileEffect
{
    /// <summary> 
    /// To be derived by all effects that can be applied to an actor when they are on a tile containing an effect
    /// <remarks>
    /// Effects can be permanent or just last a turn.
    /// </remarks>


 //Not using anything but IMMEDIATE currently.
 //PERMANENT and LIFESPAN are there as ideas and future extensions 
   enum TileEffectType
    {
        IMMEDIATE,
        PERMANENT,
        LIFESPAN
    }

    public virtual bool Apply(Actor actor)
    {
        if (!actor.CanBeAffectedByEffects)
            return false;

        return true;
    }

}