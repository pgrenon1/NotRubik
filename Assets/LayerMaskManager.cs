using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskManager : OdinserializedSingletonBehaviour<LayerMaskManager>
{
    public LayerMask faceletMask;
    public LayerMask playerInputsLayerMask;
}
