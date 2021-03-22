using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoldLogic : MonoBehaviour, IInitializable
{
    public void Initialize(GameObject character)
    {
        if (m_croucher == null) { m_croucher = character.GetComponent<Croucher>(); }
        if (m_aimScript == null) { m_aimScript = character.GetComponent<CharacterWeaponAnimator>(); }
        if (m_itemScriptR == null || m_itemScriptL == null)
        {
            CharacterItemAnimator[] animators = character.GetComponents<CharacterItemAnimator>();
            foreach (CharacterItemAnimator a in animators)
            {
                if (a.ThisHand == CharacterItemAnimator.Hand.Right) { m_itemScriptR = a; }
                else if (a.ThisHand == CharacterItemAnimator.Hand.Left) { m_itemScriptL = a; }
            }
        }

        Hand[] hands = character.GetComponentsInChildren<Hand>();

        for (int j = 0; j < hands.Length; j++)
        {
            switch (hands[j].GetHandSide)
            {
                case Hand.HandSide.Left:
                    m_handBoneL = hands[j].transform;
                    break;

                case Hand.HandSide.Right:
                    m_handBoneR = hands[j].transform;
                    break;

                default:
                    break;
            }
        }

        Animator animator = GetComponent<Animator>();
        m_animator = new AnimatorOverrideController(animator.runtimeAnimatorController);
        m_animator.name = animator.runtimeAnimatorController.name;
        animator.runtimeAnimatorController = m_animator;

        m_animatorOverridesApplier = new AnimatorOverridesApplier();
    }

    private void Awake()
    {
        Initialize(gameObject);
    }

    [SerializeField] private Croucher m_croucher;

    [SerializeField] private CharacterWeaponAnimator m_aimScript;
    [SerializeField] private CharacterItemAnimator m_itemScriptL;
    [SerializeField] private CharacterItemAnimator m_itemScriptR;

    [SerializeField] private Transform m_handBoneL;
    [SerializeField] private Transform m_handBoneR;

    private AnimatorOverrideController m_animator;
    private AnimatorOverridesApplier m_animatorOverridesApplier;

    //TODO: this is most likely not needed unless a weapon is suddenly added
    public CharacterWeaponAnimator AimScript { set { m_aimScript = value; } }
    
    //TODO: set is not used
    public Transform HandBoneR
    {
        get { return m_handBoneR; }
        set { m_handBoneR = value; }
    }
    //TODO: set is not used
    public Transform HandBoneL
    {
        get { return m_handBoneL; }
        set { m_handBoneL = value; }
    }

    //TODO: Most likely possible to use accessors
    public ItemLogic m_itemInHandL;
    public ItemLogic m_itemInHandR;

    private bool m_itemUsesBothHands = false;
    private bool m_isHoldingWeapon = false;

    private void Start()
    {
        if (!m_handBoneR || !m_handBoneL)
        {
            Debug.LogError("Handbones not set. Can't hold items.");
            return;
        }

        if (m_itemInHandR == null && m_itemInHandL == null)
        {
            if (m_aimScript) { m_aimScript.SetGunInHand(false, -1); }
        }

        CheckHands();
        if (m_itemInHandR) { AttachItem(m_itemInHandR, CharacterItemAnimator.Hand.Right); }
        if (m_itemInHandL) { AttachItem(m_itemInHandL, CharacterItemAnimator.Hand.Left); }
    }

    private void CheckHands()
    {
        ItemLogic right = m_itemInHandR;
        ItemLogic left = m_itemInHandL;
        ItemLogic either = null;

        if (right)
        {
            if (right.m_useBothHands && m_itemInHandL)
            {
                Drop(CharacterItemAnimator.Hand.Left);
                left = null;
            }

            if (right.m_PreferredHand == ItemLogic.PreferredHand.Right) { m_itemInHandR = right; }
            else if (right.m_PreferredHand == ItemLogic.PreferredHand.Left)
            {
                m_itemInHandL = right;
                m_itemInHandR = null;
            }
            else if (right.m_PreferredHand == ItemLogic.PreferredHand.Either)
            {
                m_itemInHandR = right;
                either = right;
            }
        }

        if (left)
        {
            if (left.m_useBothHands)
            {
                if (m_itemInHandR) { Drop(CharacterItemAnimator.Hand.Right); }
                if (m_itemInHandL) { Drop(CharacterItemAnimator.Hand.Left); }
                either = null;
            }

            if (left.m_PreferredHand == ItemLogic.PreferredHand.Left) { m_itemInHandL = left; }
            else if (left.m_PreferredHand == ItemLogic.PreferredHand.Right)
            {
                if (m_itemInHandR == null) { m_itemInHandR = left; }
                else { Drop(CharacterItemAnimator.Hand.Left); }

                if (either != null)
                {
                    m_itemInHandR = left;
                    m_itemInHandL = either;
                }
            }
            else if (left.m_PreferredHand == ItemLogic.PreferredHand.Either)
            {
                if (m_itemInHandL != null && m_itemInHandL != left) { m_itemInHandR = left; }
                else { m_itemInHandL = left; }
            }
        }
    }

    public void AttachItem(ItemLogic item, CharacterItemAnimator.Hand handToAttach)
    {
        if (item.m_PreferredHand == ItemLogic.PreferredHand.Left) { handToAttach = CharacterItemAnimator.Hand.Left; }
        else if (item.m_PreferredHand == ItemLogic.PreferredHand.Right) { handToAttach = CharacterItemAnimator.Hand.Right; }

        if (m_itemUsesBothHands || item.m_useBothHands)
        {
            if (m_itemInHandL && m_itemInHandL != item) { Drop(CharacterItemAnimator.Hand.Left); }
            if (m_itemInHandR && m_itemInHandR != item) { Drop(CharacterItemAnimator.Hand.Right); }
        }

        if (item == m_itemInHandL && handToAttach == CharacterItemAnimator.Hand.Left) { m_itemInHandL = item; }
        else if (item == m_itemInHandR && handToAttach == CharacterItemAnimator.Hand.Right) { m_itemInHandR = item; }
        else if (m_itemInHandL == null && handToAttach == CharacterItemAnimator.Hand.Left) { m_itemInHandL = item; }
        else if (m_itemInHandR == null && handToAttach == CharacterItemAnimator.Hand.Right) { m_itemInHandR = item; }
        else if (item.m_PreferredHand == ItemLogic.PreferredHand.Right)
        {
            if (m_itemInHandR) { Drop(CharacterItemAnimator.Hand.Right); }
            m_itemInHandR = item;
        }
        else if (item.m_PreferredHand == ItemLogic.PreferredHand.Left ||
                 item.m_PreferredHand == ItemLogic.PreferredHand.Either)
        {
            if (m_itemInHandL) { Drop(CharacterItemAnimator.Hand.Left); }
            m_itemInHandL = item;
        }
        CheckHands();

        Transform dummyPoint = null;
        if (item.DummyPoint != null) { dummyPoint = item.DummyPoint; }

        bool isWeapon = false;
        if (item.ItemAnimations) { isWeapon = item.ItemAnimations.IsWeapon; }

        m_itemUsesBothHands = item.m_useBothHands;
        if (m_itemScriptR != null && item == m_itemInHandR) { m_itemScriptR.UseBothHands = m_itemUsesBothHands; }
        else if (m_itemScriptL != null && item == m_itemInHandL) { m_itemScriptL.UseBothHands = m_itemUsesBothHands; }

        if (item == m_itemInHandR) { Attach(item, m_handBoneR, dummyPoint); }
        else if (item == m_itemInHandL) { Attach(item, m_handBoneL, dummyPoint); }

        if (m_aimScript)
        {
            if (item.ItemTypeID <= 2) { m_aimScript.SetGunInHand(true, item.ItemTypeID); }
            else { m_aimScript.SetGunInHand(false, -1); }
        }

        if (m_itemScriptL || m_itemScriptR) { m_animatorOverridesApplier.SetCorrectAnimations(m_animator,m_itemInHandR, m_itemInHandL, m_itemScriptR, m_itemScriptL); }
    }

    private static Vector3 FixNegativeScale(Vector3 scale)
    {
        return new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
    }

    private void AttachToHand(ItemLogic item, CharacterItemAnimator itemScript, ref ItemLogic itemInHand)
    {
        if(itemScript != null)
        {
            if(item.ItemAnimations) { m_isHoldingWeapon = item.ItemAnimations.IsWeapon; }
            itemScript.SetHolding(true, m_isHoldingWeapon);
            itemInHand = item;
        }
    }

    private void Attach(ItemLogic item, Transform hand, Transform dummyPoint)
    {
        Transform itemTransform = item.transform;

        itemTransform.parent = hand;

        itemTransform.localScale = FixNegativeScale(item.transform.localScale);
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;

        if(hand == HandBoneL)
        {
            AttachToHand(item, m_itemScriptL, ref m_itemInHandL);
        }
        else if(hand == HandBoneR)
        {
            AttachToHand(item, m_itemScriptR, ref m_itemInHandR);
        }

        if (dummyPoint != null)
        {
            itemTransform.position = dummyPoint.position;
            itemTransform.localRotation *= dummyPoint.localRotation;
        }

        item.OnPickup();
    }

    public void Drop(CharacterItemAnimator.Hand hand)
    {
        //TODO: potential bug - if a weapon is dropped m_isHoldingWeapon gets stuck to being true

        if (hand == CharacterItemAnimator.Hand.Right)
        {
            ItemLogic itemLogic = m_itemInHandR.GetComponent<ItemLogic>();
            m_itemInHandR.transform.parent = null;
            m_itemInHandR = null;
            if (itemLogic) { itemLogic.OnDrop(); }
        }
        else if (hand == CharacterItemAnimator.Hand.Left)
        {
            ItemLogic itemLogic = m_itemInHandL.GetComponent<ItemLogic>();
            m_itemInHandL.transform.parent = null;
            m_itemInHandL = null;
            if (itemLogic) { itemLogic.OnDrop(); }
        }
    }

    public void Toggle(CharacterItemAnimator.Hand hand)
    {
        if (hand == CharacterItemAnimator.Hand.Right)
        {
            if (m_itemInHandR.gameObject.activeSelf) { m_itemInHandR.gameObject.SetActive(false); }
            else
            {
                ItemLogic item = m_itemInHandR;
                item.gameObject.SetActive(true);
                m_itemInHandR = null;
                AttachItem(item, hand);
            }
        }
        else if (hand == CharacterItemAnimator.Hand.Left)
        {
            if (m_itemInHandL.gameObject.activeSelf) { m_itemInHandL.gameObject.SetActive(false); }
            else
            {
                ItemLogic item = m_itemInHandL;
                item.gameObject.SetActive(true);
                m_itemInHandL = null;
                AttachItem(item, hand);
            }
        }
    }


}
