// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Use with accompanying editor script ManageModularOutfitEditor.cs
// Assign current object and parent object in editor inspector,
// script will find all child skinned meshes (active or not),
// and provide a popup selection box.

public class ManageModularOutfit : MonoBehaviour
{
    public Transform currentHead;
    public Transform _headParent;
    [HideInInspector] public List<Transform> heads;

    public Transform currentEyebrows;
    public Transform _eyebrowParent;
    [HideInInspector] public List<Transform> eyebrows;

    public Transform currentTorso;
    public Transform _torsoParent;
    [HideInInspector] public List<Transform> torsos;

    public Transform currentUpperLeftArm;
    public Transform _upperLeftArmParent;
    [HideInInspector] public List<Transform> upperLeftArms;

    public Transform currentLowerLeftArm;
    public Transform _lowerLeftArmParent;
    [HideInInspector] public List<Transform> lowerLeftArms;

    public Transform currentLeftHand;
    public Transform _leftHandParent;
    [HideInInspector] public List<Transform> leftHands;

    public Transform currentUpperRightArm;
    public Transform _upperRightArmParent;
    [HideInInspector] public List<Transform> upperRightArms;

    public Transform currentLowerRightArm;
    public Transform _lowerRightArmParent;
    [HideInInspector] public List<Transform> lowerRightArms;

    public Transform currentRightHand;
    public Transform _rightHandParent;
    [HideInInspector] public List<Transform> rightHands;

    public Transform currentHips;
    public Transform _hipsParent;
    [HideInInspector] public List<Transform> hips;

    public Transform currentLeftLeg;
    public Transform _leftLegParent;
    [HideInInspector] public List<Transform> leftLegs;

    public Transform currentRightLeg;
    public Transform _rightLegParent;
    [HideInInspector] public List<Transform> rightLegs;


    private void Awake()
    {
        heads = GetAllSkinnedChildren(_headParent, heads);
        torsos = GetAllSkinnedChildren(_torsoParent, torsos);
        eyebrows = GetAllSkinnedChildren(_eyebrowParent, eyebrows);
        upperLeftArms = GetAllSkinnedChildren(_upperLeftArmParent, upperLeftArms);
        lowerLeftArms = GetAllSkinnedChildren(_lowerLeftArmParent, lowerLeftArms);
        upperRightArms = GetAllSkinnedChildren(_upperRightArmParent, upperRightArms);
        lowerRightArms = GetAllSkinnedChildren(_lowerRightArmParent, lowerRightArms);
        leftHands = GetAllSkinnedChildren(_leftHandParent, leftHands);
        rightHands = GetAllSkinnedChildren(_rightHandParent, rightHands);
        hips = GetAllSkinnedChildren(_hipsParent, hips);
        leftLegs = GetAllSkinnedChildren(_leftLegParent, leftLegs);
        rightLegs = GetAllSkinnedChildren(_rightLegParent, rightLegs);
    }



    List<Transform> GetAllSkinnedChildren(Transform parent, List<Transform> lt)
    {
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
