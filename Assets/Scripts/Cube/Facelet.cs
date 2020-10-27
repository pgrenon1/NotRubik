using UnityEngine;

public class Facelet : MonoBehaviour
{

    //Commenting to commit
    public GameObject highlight;

    public Cubelet Cubelet { get; set; }

    public void UpdateHighlightVisibility(bool isHighlighted)
    {
        highlight.SetActive(isHighlighted);
    }
}
