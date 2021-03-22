#if UNITY_EDITOR

using UnityEngine;

[CreateAssetMenu()]
public class AccessoryObject : ScriptableObject
{
    [SerializeField] private AccessoryLogic m_accessoryPrefab = null;

    public AccessoryLogic Accessory { get { return m_accessoryPrefab; } }
}

#endif