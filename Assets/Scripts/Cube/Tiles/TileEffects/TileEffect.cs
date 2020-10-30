using UnityEngine;
public abstract class TileEffect
{
    /// <summary> 
    /// To be derived by all effects that can be applied to an actor when
    /// they are on a tile containing an effect



    //PERMANENT and LIFESPAN are there as ideas and future extensions 




    //Unsure how to do this. I want to force initilization on inheriting classes, but I also want to expose it in editor.
    //AND I want us to be able to get the data from other classes
    public TileEffectData tileEffectData;

    //protected TileEffect(TileEffectData _tileEffectData)
    //{
    //    tileEffectData = _tileEffectData;

    //    //Not sure how to make this work but I want to make it sure 
    //    //if we're missing one, we should catch it
    //    if (!tileEffectData)
    //        Debug.LogError(string.Format("{0} has no tileEffectData set. Please set it", this));
    //}
    

    public TileEffectData GetTileEffectData()
    {

        return tileEffectData;
    }


    public virtual bool Apply(Actor actor)
    {
        if (!actor.CanBeAffectedByEffects)
            return false;

        return true;
    }

}