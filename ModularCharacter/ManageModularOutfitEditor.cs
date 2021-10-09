// C NELSON 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Custom editor goes in \Editor.
// Accompanies ManageModularOutfit.
// Provides inspector interface for switching body parts.

[CustomEditor(typeof(ManageModularOutfit))]
public class ManageModularOutfitEditor : Editor
{

    string[] _headChoices;
    int _headChoiceIndex = 0;

    string[] _eyebrowChoices;
    int _eyebrowChoiceIndex = 0;

    string[] _torsoChoices;
    int _torsoChoiceIndex = 0;

    string[] _upperLeftArmChoices;
    int _upperLeftArmChoiceIndex = 0;

    string[] _lowerLeftArmChoices;
    int _lowerLeftArmChoiceIndex = 0;

    string[] _leftHandChoices;
    int _leftHandChoiceIndex = 0;

    string[] _upperRightArmChoices;
    int _upperRightArmChoiceIndex = 0;

    string[] _lowerRightArmChoices;
    int _lowerRightArmChoiceIndex = 0;

    string[] _rightHandChoices;
    int _rightHandChoiceIndex = 0;

    string[] _hipsChoices;
    int _hipsChoiceIndex = 0;

    string[] _leftLegChoices;
    int _leftLegChoiceIndex = 0;

    string[] _rightLegChoices;
    int _rightLegChoiceIndex = 0;


    ManageModularOutfit mmo;

    void OnEnable()
    {
        mmo = (ManageModularOutfit)target;

        _headChoices = TranformToString(_headChoices, mmo.heads);
        _eyebrowChoices = TranformToString(_eyebrowChoices, mmo.eyebrows);
        _torsoChoices = TranformToString(_torsoChoices, mmo.torsos);
        _upperLeftArmChoices = TranformToString(_upperLeftArmChoices, mmo.upperLeftArms);
        _lowerLeftArmChoices = TranformToString(_lowerLeftArmChoices, mmo.lowerLeftArms);
        _upperRightArmChoices = TranformToString(_upperRightArmChoices, mmo.upperRightArms);
        _lowerRightArmChoices = TranformToString(_lowerRightArmChoices, mmo.lowerRightArms);
        _leftHandChoices = TranformToString(_leftHandChoices, mmo.leftHands);
        _rightHandChoices = TranformToString(_rightHandChoices, mmo.rightHands);
        _hipsChoices = TranformToString(_hipsChoices, mmo.hips);
        _leftLegChoices = TranformToString(_leftLegChoices, mmo.leftLegs);
        _rightLegChoices = TranformToString(_rightLegChoices, mmo.rightLegs);
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _headChoiceIndex = EditorGUILayout.Popup(_headChoiceIndex, _headChoices);
        if (GUILayout.Button("Change head"))
        {
            ToggleObject(ref mmo.currentHead, mmo.heads, _headChoiceIndex);
        }

        _eyebrowChoiceIndex = EditorGUILayout.Popup(_eyebrowChoiceIndex, _eyebrowChoices);
        if (GUILayout.Button("Change eyebrow"))
        {
            ToggleObject(ref mmo.currentEyebrows, mmo.eyebrows, _eyebrowChoiceIndex);
        }

        _torsoChoiceIndex = EditorGUILayout.Popup(_torsoChoiceIndex, _torsoChoices);
        if (GUILayout.Button("Change torso"))
        {
            ToggleObject(ref mmo.currentTorso, mmo.torsos, _torsoChoiceIndex);
        }

        _upperLeftArmChoiceIndex = EditorGUILayout.Popup(_upperLeftArmChoiceIndex, _upperLeftArmChoices);
        if (GUILayout.Button("Change upper left arm"))
        {
            ToggleObject(ref mmo.currentUpperLeftArm, mmo.upperLeftArms, _upperLeftArmChoiceIndex);
        }

        _lowerLeftArmChoiceIndex = EditorGUILayout.Popup(_lowerLeftArmChoiceIndex, _lowerLeftArmChoices);
        if (GUILayout.Button("Change lower left arm"))
        {
            ToggleObject(ref mmo.currentLowerLeftArm, mmo.lowerLeftArms, _lowerLeftArmChoiceIndex);
        }

        _upperRightArmChoiceIndex = EditorGUILayout.Popup(_upperRightArmChoiceIndex, _upperRightArmChoices);
        if (GUILayout.Button("Change upper right arm"))
        {
            ToggleObject(ref mmo.currentUpperRightArm, mmo.upperRightArms, _upperRightArmChoiceIndex);
        }

        _lowerRightArmChoiceIndex = EditorGUILayout.Popup(_lowerRightArmChoiceIndex, _lowerRightArmChoices);
        if (GUILayout.Button("Change lower right arm"))
        {
            ToggleObject(ref mmo.currentLowerRightArm, mmo.lowerRightArms, _lowerRightArmChoiceIndex);
        }

        _leftHandChoiceIndex = EditorGUILayout.Popup(_leftHandChoiceIndex, _leftHandChoices);
        if (GUILayout.Button("Change left hand"))
        {
            ToggleObject(ref mmo.currentLeftHand, mmo.leftHands, _leftHandChoiceIndex);
        }

        _rightHandChoiceIndex = EditorGUILayout.Popup(_rightHandChoiceIndex, _rightHandChoices);
        if (GUILayout.Button("Change right hand"))
        {
            ToggleObject(ref mmo.currentRightHand, mmo.rightHands, _rightHandChoiceIndex);
        }

        _hipsChoiceIndex = EditorGUILayout.Popup(_hipsChoiceIndex, _hipsChoices);
        if (GUILayout.Button("Change hips"))
        {
            ToggleObject(ref mmo.currentHips, mmo.hips, _hipsChoiceIndex);
        }

        _leftLegChoiceIndex = EditorGUILayout.Popup(_leftLegChoiceIndex, _leftLegChoices);
        if (GUILayout.Button("Change left leg"))
        {
            ToggleObject(ref mmo.currentLeftLeg, mmo.leftLegs, _leftLegChoiceIndex);
        }

        _rightLegChoiceIndex = EditorGUILayout.Popup(_rightLegChoiceIndex, _rightLegChoices);
        if (GUILayout.Button("Change right leg"))
        {
            ToggleObject(ref mmo.currentRightLeg, mmo.rightLegs, _rightLegChoiceIndex);
        }

    }


    void ToggleObject(ref Transform tTo, List<Transform> tlFrom, int i)
    {
        if (tTo != null)
        {
            tTo.gameObject.SetActive(false);
        }
        tTo = tlFrom[i];
        tTo.gameObject.SetActive(true);
    }

    string[] TranformToString(string[] s, List<Transform> lt)
    {
        s = new string[lt.Count];

        for (int i = 0; i < lt.Count; i++)
        {
            s[i] = lt[i].name.ToString();
        }
        return s;
    }
}
