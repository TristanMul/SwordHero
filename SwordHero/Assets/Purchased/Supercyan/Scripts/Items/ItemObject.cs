#if UNITY_EDITOR

using UnityEngine;

[CreateAssetMenu()]
public class ItemObject : ScriptableObject
{
    [SerializeField] private ItemLogic m_itemPrefab = null;

    public ItemLogic Item { get { return m_itemPrefab; } }
}

#endif