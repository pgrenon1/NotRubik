using UnityEngine;
public abstract class TileEffect
{
    /// <summary> 
    /// To be derived by all effects that can be applied to an actor when
    /// they are on a tile containing an effect


       
 //PERMANENT and LIFESPAN are there as ideas and future extensions 


    [SerializeField]
    protected TileEffectData tileEffectData;

    protected TileEffect(TileEffectData _tileEffectData)
    {
        tileEffectData = _tileEffectData;


        //Not sure how to make this work but I want to make it sure 
        //if we're missing one, we should catch it
        if (!tileEffectData)
            Debug.LogError(string.Format("{0} has no tileEffectData set. Please set it", this));
    }
    
    public TileEffectData ExamineTileEffectData()
    {
     //I don't think this is the right method but I needed it for debug and now it returns the value
     //I guess we can use it to return the tileeffectdata aoutisde of this class,
     //but rename it
        Debug.Log(tileEffectData.name + ": " + tileEffectData.tileEffectDescription);
        return tileEffectData;
    }


    public virtual bool Apply(Actor actor)
    {
        if (!actor.CanBeAffectedByEffects)
            return false;

        return true;
    }

}