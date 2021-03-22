using UnityEngine;

public class Hand : MonoBehaviour
{
    public enum HandSide
    {
        Left,
        Right
    }

    [SerializeField] private HandSide m_side = HandSide.Left;

    public HandSide GetHandSide { get { return m_side; } }
}
