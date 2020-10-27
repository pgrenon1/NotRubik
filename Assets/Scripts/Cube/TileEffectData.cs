using UnityEngine.UI;
using UnityEngine;

public enum TileEffectType
{
    IMMEDIATE,
    NONE
    //PERMANENT
    //LIFESPAN
}

[CreateAssetMenu(fileName = "TileEffectData", menuName = "Assets/TileEffectData")]
public class TileEffectData : OdinSerializedScriptableObject
{
    /// <summary>
    /// Contains all the metadata required for TileEffect
    /// </summary>
   
    public string tileEffectName;
    public string tileEffectDescription;
    public TileEffectType type;
    public Sprite tileEffectIcon;



}