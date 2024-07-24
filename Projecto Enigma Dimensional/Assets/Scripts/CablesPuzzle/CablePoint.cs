using UnityEngine;

public enum CableColor
{
    Red, Blue, Green, Yellow
}

public class CablePoint : MonoBehaviour
{
    public CableColor color;
    [Range(0,1)]public int id;
}
