// C NELSON 2021

using System.Collections.Generic;
using UnityEngine;

// Use with accompanying editor script ManageModularOutfitEditor.cs
// Assign parent objects from character hierarchy to find all
// skinned child meshes (active or not) and make available for selection.
// Assign current object to deactivate it when selecting new object.


public class ManageModularOutfit : MonoBehaviour
{
    [System.Serializable]
    public class MMOModule
    {
        // using a class gives an easy collapsable section in the editor

        [Header("Hats")]
        public Transform _hatParent;
        public Transform currentHat;
        [HideInInspector] public List<Transform> hats;

        [Header("Hair")]
        public Transform _hairParent;
        public Transform currentHair;
        [HideInInspector] public List<Transform> hairs;

        [Header("Hat attachments")]
        public Transform _hatAttachmentParent;
        public Transform currentHatAttachment;
        [HideInInspector] public List<Transform> hatAttachments;

        [Header("Back attachments")]
        public Transform _backAttachmentParent;
        public Transform currentBackAttachment;
        [HideInInspector] public List<Transform> backAttachments;

        [Header("Right shoulder attachments")]
        public Transform _rightShoulderAttachmentParent;
        public Transform currentRightShoulderAttachment;
        [HideInInspector] public List<Transform> rightShoulderAttachments;

        [Header("Left shoulder attachments")]
        public Transform _leftShoulderAttachmentParent;
        public Transform currentLeftShoulderAttachment;
        [HideInInspector] public List<Transform> leftShoulderAttachments;

        [Header("Right elbow attachments")]
        public Transform _rightElbowAttachmentParent;
        public Transform currentRightElbowAttachment;
        [HideInInspector] public List<Transform> rightElbowAttachments;

        [Header("Left elbow attachments")]
        public Transform _leftElbowAttachmentParent;
        public Transform currentLeftElbowAttachment;
        [HideInInspector] public List<Transform> leftElbowAttachments;

        [Header("Hip attachments")]
        public Transform _hipAttachmentParent;
        public Transform currentHipAttachment;
        [HideInInspector] public List<Transform> hipAttachments;

        [Header("Right knee attachments")]
        public Transform _rightKneeAttachmentParent;
        public Transform currentRightKneeAttachment;
        [HideInInspector] public List<Transform> rightKneeAttachments;

        [Header("Left knee attachments")]
        public Transform _leftKneeAttachmentParent;
        public Transform currentLeftKneeAttachment;
        [HideInInspector] public List<Transform> leftKneeAttachments;

        [Header("Elf ears")]
        public Transform _elfEarsParent;
        public Transform currentElfEars;
        [HideInInspector] public List<Transform> elfEars;

        [Header("Head")]
        public Transform _headParent;
        public Transform currentHead;
        [HideInInspector] public List<Transform> heads;

        [Header("Eyebrows")]
        public Transform _eyebrowParent;
        public Transform currentEyebrows;
        [HideInInspector] public List<Transform> eyebrows;

        [Header("Torso")]
        public Transform _torsoParent;
        public Transform currentTorso;
        [HideInInspector] public List<Transform> torsos;

        [Header("Upper right arm")]
        public Transform _upperRightArmParent;
        public Transform currentUpperRightArm;
        [HideInInspector] public List<Transform> upperRightArms;

        [Header("Upper left arm")]
        public Transform _upperLeftArmParent;
        public Transform currentUpperLeftArm;
        [HideInInspector] public List<Transform> upperLeftArms;

        [Header("Lower right arm")]
        public Transform currentLowerRightArm;
        public Transform _lowerRightArmParent;
        [HideInInspector] public List<Transform> lowerRightArms;

        [Header("Lower left arm")]
        public Transform _lowerLeftArmParent;
        public Transform currentLowerLeftArm;
        [HideInInspector] public List<Transform> lowerLeftArms;

        [Header("Right hand")]
        public Transform _rightHandParent;
        public Transform currentRightHand;
        [HideInInspector] public List<Transform> rightHands;

        [Header("Left hand")]
        public Transform _leftHandParent;
        public Transform currentLeftHand;
        [HideInInspector] public List<Transform> leftHands;

        [Header("Hips")]
        public Transform _hipsParent;
        public Transform currentHips;
        [HideInInspector] public List<Transform> hips;

        [Header("Right leg")]
        public Transform _rightLegParent;
        public Transform currentRightLeg;
        [HideInInspector] public List<Transform> rightLegs;

        [Header("Left leg")]
        public Transform _leftLegParent;
        public Transform currentLeftLeg;
        [HideInInspector] public List<Transform> leftLegs;
    }

    public MMOModule mmomData = new MMOModule();



    private void Awake()
    {
        mmomData.hats = GetAllSkinnedChildren(mmomData._hatParent);
        mmomData.hatAttachments = GetAllSkinnedChildren(mmomData._hatAttachmentParent);
        mmomData.backAttachments = GetAllSkinnedChildren(mmomData._backAttachmentParent);
        mmomData.leftShoulderAttachments = GetAllSkinnedChildren(mmomData._leftShoulderAttachmentParent);
        mmomData.rightShoulderAttachments = GetAllSkinnedChildren(mmomData._rightShoulderAttachmentParent);
        mmomData.leftElbowAttachments = GetAllSkinnedChildren(mmomData._leftElbowAttachmentParent);
        mmomData.rightElbowAttachments = GetAllSkinnedChildren(mmomData._rightElbowAttachmentParent);
        mmomData.hipAttachments = GetAllSkinnedChildren(mmomData._hipAttachmentParent);
        mmomData.leftKneeAttachments = GetAllSkinnedChildren(mmomData._leftKneeAttachmentParent);
        mmomData.rightKneeAttachments = GetAllSkinnedChildren(mmomData._rightKneeAttachmentParent);
        mmomData.elfEars = GetAllSkinnedChildren(mmomData._elfEarsParent);
        mmomData.hairs = GetAllSkinnedChildren(mmomData._hairParent);
        mmomData.heads = GetAllSkinnedChildren(mmomData._headParent);
        mmomData.torsos = GetAllSkinnedChildren(mmomData._torsoParent);
        mmomData.eyebrows = GetAllSkinnedChildren(mmomData._eyebrowParent);
        mmomData.upperLeftArms = GetAllSkinnedChildren(mmomData._upperLeftArmParent);
        mmomData.lowerLeftArms = GetAllSkinnedChildren(mmomData._lowerLeftArmParent);
        mmomData.upperRightArms = GetAllSkinnedChildren(mmomData._upperRightArmParent);
        mmomData.lowerRightArms = GetAllSkinnedChildren(mmomData._lowerRightArmParent);
        mmomData.leftHands = GetAllSkinnedChildren(mmomData._leftHandParent);
        mmomData.rightHands = GetAllSkinnedChildren(mmomData._rightHandParent);
        mmomData.hips = GetAllSkinnedChildren(mmomData._hipsParent);
        mmomData.leftLegs = GetAllSkinnedChildren(mmomData._leftLegParent);
        mmomData.rightLegs = GetAllSkinnedChildren(mmomData._rightLegParent);
    }


    List<Transform> GetAllSkinnedChildren(Transform parent)
    {
        List<Transform> lt = new List<Transform>();
        
        if (parent == null)
        {
            return lt;
        }

        Transform[] all = parent.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in all)
        {
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                lt.Add(t);
            }
        }
        return lt;
    }
}
