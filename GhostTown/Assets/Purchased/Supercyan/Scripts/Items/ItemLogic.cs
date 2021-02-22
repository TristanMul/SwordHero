using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    public enum PreferredHand
    {
        Right,
        Left,
        Either
    }

    public bool m_useBothHands = false;

    public PreferredHand m_PreferredHand = PreferredHand.Right;

    public enum ItemType
    {
        AssaultRifle,
        SniperRifle,
        Pistol,

        Other,
    }

    [SerializeField] private ItemType m_itemTypeId = ItemType.Other;
    public int ItemTypeID { get { return (int)m_itemTypeId; } }

    [SerializeField] private Transform m_dummyPoint = null;
    public Transform DummyPoint { get { return m_dummyPoint; } }

    [SerializeField] private ItemAnimationsObject m_itemAnimations = null;
    public ItemAnimationsObject ItemAnimations { get { return m_itemAnimations; } }

    [SerializeField] private AccessoryLogic m_accessoryLogic = null;
    public AccessoryLogic AccessoryLogic { get { return m_accessoryLogic; } }

    public virtual void OnPickup() { }

    public virtual void OnDrop() { }
}
