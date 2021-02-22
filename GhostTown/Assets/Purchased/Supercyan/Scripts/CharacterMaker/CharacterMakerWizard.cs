#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class CharacterMakerWizard : ScriptableWizard
{
    public CharacterAppearanceObject m_appearanceObject;
    public CharacterBehaviorObject m_behaviorObject;

    public ItemObject m_itemLeftHand;
    public ItemObject m_itemRightHand;

    public AccessoryObject[] m_accessories;

    [MenuItem("Supercyan/Character Maker")]
    private static void CreateWizard()
    {
        DisplayWizard<CharacterMakerWizard>("Character Maker");
    }

    private void OnWizardCreate()
    {
        GameObject character = Instantiate(m_appearanceObject.Model);

        CapsuleCollider collider = character.AddComponent<CapsuleCollider>();
        collider.radius = 0.1f;
        collider.direction = 1;
        collider.center = new Vector3(0.0f, 0.5f, 0.0f);

        Rigidbody rigidbody = character.AddComponent<Rigidbody>();
        rigidbody.angularDrag = 5.0f;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        m_behaviorObject.InitializeBehaviour(character);

        character.transform.position = Vector3.zero;

        ItemHoldLogic itemHoldLogic = character.GetComponent<ItemHoldLogic>();
        AccessoryWearLogic accessoryWearLogic = character.GetComponent<AccessoryWearLogic>();
        bool hasItems = m_itemLeftHand || m_itemRightHand;

        if ((itemHoldLogic == null) &&
            (m_itemLeftHand || m_itemRightHand))
        {
            itemHoldLogic = character.AddComponent<ItemHoldLogic>();
            hasItems = true;
        }
        if (accessoryWearLogic == null)
        {
            if (m_itemLeftHand != null && m_itemLeftHand.Item.AccessoryLogic != null) { accessoryWearLogic = character.AddComponent<AccessoryWearLogic>(); }
            else if (m_itemRightHand != null && m_itemRightHand.Item.AccessoryLogic != null) { accessoryWearLogic = character.AddComponent<AccessoryWearLogic>(); }
            else if (m_accessories.Length > 0) { accessoryWearLogic = character.AddComponent<AccessoryWearLogic>(); }
        }

        // Attach items
        if (hasItems)
        {
            CharacterItemAnimator itemAnimator;
            itemAnimator = itemHoldLogic.gameObject.AddComponent<CharacterItemAnimator>();
            itemAnimator.ThisHand = CharacterItemAnimator.Hand.Left;

            itemAnimator = itemHoldLogic.gameObject.AddComponent<CharacterItemAnimator>();
            itemAnimator.ThisHand = CharacterItemAnimator.Hand.Right;
        }

        if (m_itemLeftHand)
        {
            ItemLogic item = Instantiate(m_itemLeftHand.Item);
            itemHoldLogic.m_itemInHandL = item;

            if (m_itemLeftHand.Item.AccessoryLogic)
            {
                item.transform.parent = character.transform;
                accessoryWearLogic.Attach(item.AccessoryLogic);
            }
            else
            {
                item.transform.parent = itemHoldLogic.HandBoneL;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
            }
        }

        if (m_itemRightHand)
        {
            ItemLogic item = Instantiate(m_itemRightHand.Item);
            itemHoldLogic.m_itemInHandR = item;

            if (m_itemRightHand.Item.AccessoryLogic)
            {
                item.transform.parent = character.transform;
                accessoryWearLogic.Attach(item.AccessoryLogic);
            }
            else
            {
                item.transform.parent = itemHoldLogic.HandBoneR;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
            }
        }

        // Attach accessories
        foreach (AccessoryObject a in m_accessories)
        {
            AccessoryLogic accessory = Instantiate(a.Accessory);
            accessory.transform.parent = character.transform;
            accessoryWearLogic.Attach(accessory);
        }
    }
}
#endif