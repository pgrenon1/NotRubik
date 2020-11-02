using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public abstract class TileEffect
{
    /// <summary> 
    /// To be derived by all effects that can be applied to an actor when
    /// they are on a tile containing an effect

    public enum TileEffectType
    {
        IMMEDIATE,
        NONE
        //PERMANENT
        //LIFESPAN
    }

    //PERMANENT and LIFESPAN are there as ideas and future extensions 


    //Unsure how to do this. I want to force initilization on inheriting classes, but I also want to expose it in editor.
    //AND I want us to be able to get the data from other classes
    
    [Required]
    public string tileEffectName;
    [Required]
    public string tileEffectDescription;
    [Required]
    public TileEffectType type;
    [Required]
    public Sprite tileEffectIcon;

    public virtual bool Apply(Actor actor)
    {
        
        if (!actor.CanBeAffectedByEffects)
            return false;

        return true;
    }

}