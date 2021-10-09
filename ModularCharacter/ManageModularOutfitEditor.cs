// C NELSON 2021

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Custom editor goes in \Editor.
// Accompanies ManageModularOutfit.
// Provides inspector interface for switching body parts.


[System.Serializable]
[CustomEditor(typeof(ManageModularOutfit))]
public class ManageModularOutfitEditor : Editor
{
    string[] _hatChoices;
    int _hatChoiceIndex = 0;

    string[] _hatAttachmentChoices;
    int _hatAttachmentChoiceIndex = 0;

    string[] _backAttachmentChoices;
    int _backAttachmentChoiceIndex = 0;

    string[] _leftShoulderAttachmentChoices;
    int _leftShoulderAttachmentChoiceIndex = 0;

    string[] _rightShoulderAttachmentChoices;
    int _rightShoulderAttachmentChoiceIndex = 0;

    string[] _leftElbowAttachmentChoices;
    int _leftElbowAttachmentChoiceIndex = 0;

    string[] _rightElbowAttachmentChoices;
    int _rightElbowAttachmentChoiceIndex = 0;

    string[] _hipAttachmentChoices;
    int _hipAttachmentChoiceIndex = 0;

    string[] _leftKneeAttachmentChoices;
    int _leftKneeAttachmentChoiceIndex = 0;

    string[] _rightKneeAttachmentChoices;
    int _rightKneeAttachmentChoiceIndex = 0;

    string[] _elfEarsChoices;
    int _elfEarsChoiceIndex = 0;

    string[] _hairChoices;
    int _hairChoiceIndex = 0;

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

        // create lists of options from parent transforms provided by user
        _hatChoices = TransformToString(_hatChoices, mmo.mmomData.hats);
        _hatAttachmentChoices = TransformToString(_hatAttachmentChoices, mmo.mmomData.hatAttachments);
        _backAttachmentChoices = TransformToString(_backAttachmentChoices, mmo.mmomData.backAttachments);
        _leftShoulderAttachmentChoices = TransformToString(_leftShoulderAttachmentChoices, mmo.mmomData.leftShoulderAttachments);
        _rightShoulderAttachmentChoices = TransformToString(_rightShoulderAttachmentChoices, mmo.mmomData.rightShoulderAttachments);
        _leftElbowAttachmentChoices = TransformToString(_leftElbowAttachmentChoices, mmo.mmomData.leftElbowAttachments);
        _rightElbowAttachmentChoices = TransformToString(_rightElbowAttachmentChoices, mmo.mmomData.rightElbowAttachments);
        _hipAttachmentChoices = TransformToString(_hipAttachmentChoices, mmo.mmomData.hipAttachments);
        _leftKneeAttachmentChoices = TransformToString(_leftKneeAttachmentChoices, mmo.mmomData.leftKneeAttachments);
        _rightKneeAttachmentChoices = TransformToString(_rightKneeAttachmentChoices, mmo.mmomData.rightKneeAttachments);
        _elfEarsChoices = TransformToString(_elfEarsChoices, mmo.mmomData.elfEars);
        _hairChoices = TransformToString(_hairChoices, mmo.mmomData.hairs);
        _headChoices = TransformToString(_headChoices, mmo.mmomData.heads);
        _eyebrowChoices = TransformToString(_eyebrowChoices, mmo.mmomData.eyebrows);
        _torsoChoices = TransformToString(_torsoChoices, mmo.mmomData.torsos);
        _upperLeftArmChoices = TransformToString(_upperLeftArmChoices, mmo.mmomData.upperLeftArms);
        _lowerLeftArmChoices = TransformToString(_lowerLeftArmChoices, mmo.mmomData.lowerLeftArms);
        _upperRightArmChoices = TransformToString(_upperRightArmChoices, mmo.mmomData.upperRightArms);
        _lowerRightArmChoices = TransformToString(_lowerRightArmChoices, mmo.mmomData.lowerRightArms);
        _leftHandChoices = TransformToString(_leftHandChoices, mmo.mmomData.leftHands);
        _rightHandChoices = TransformToString(_rightHandChoices, mmo.mmomData.rightHands);
        _hipsChoices = TransformToString(_hipsChoices, mmo.mmomData.hips);
        _leftLegChoices = TransformToString(_leftLegChoices, mmo.mmomData.leftLegs);
        _rightLegChoices = TransformToString(_rightLegChoices, mmo.mmomData.rightLegs);
    }


    public override void OnInspectorGUI()
    {

        GUILayout.Label("\nSETUP:");

        GUILayout.BeginVertical();
        DrawDefaultInspector();
        GUILayout.EndVertical();
        

        GUILayout.Label("\nCUSTOMISE:\n");

        GUILayout.Label("\t\tBODY");

        GUILayout.Label("Hair:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _hairChoiceIndex, _hairChoices, ref mmo.mmomData.currentHair, mmo.mmomData.hairs);
        GUILayout.EndHorizontal();

        GUILayout.Label("Head:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _headChoiceIndex, _headChoices, ref mmo.mmomData.currentHead, mmo.mmomData.heads);
        GUILayout.EndHorizontal();

        GUILayout.Label("Eyebrows:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _eyebrowChoiceIndex, _eyebrowChoices, ref mmo.mmomData.currentEyebrows, mmo.mmomData.eyebrows);
        GUILayout.EndHorizontal();

        GUILayout.Label("Elf ears:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _elfEarsChoiceIndex, _elfEarsChoices, ref mmo.mmomData.currentElfEars, mmo.mmomData.elfEars);
        GUILayout.EndHorizontal();

        GUILayout.Label("Torso:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _torsoChoiceIndex, _torsoChoices, ref mmo.mmomData.currentTorso, mmo.mmomData.torsos);
        GUILayout.EndHorizontal();

        GUILayout.Label("Upper right arm:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _upperRightArmChoiceIndex, _upperRightArmChoices, ref mmo.mmomData.currentUpperRightArm, mmo.mmomData.upperRightArms);
        GUILayout.EndHorizontal();

        GUILayout.Label("Upper left arm:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _upperLeftArmChoiceIndex, _upperLeftArmChoices, ref mmo.mmomData.currentUpperLeftArm, mmo.mmomData.upperLeftArms);
        GUILayout.EndHorizontal();

        GUILayout.Label("Lower right arm:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _lowerRightArmChoiceIndex, _lowerRightArmChoices, ref mmo.mmomData.currentLowerRightArm, mmo.mmomData.lowerRightArms);
        GUILayout.EndHorizontal();

        GUILayout.Label("Lower left arm:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _lowerLeftArmChoiceIndex, _lowerLeftArmChoices, ref mmo.mmomData.currentLowerLeftArm, mmo.mmomData.lowerLeftArms);
        GUILayout.EndHorizontal();

        GUILayout.Label("Right hand:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _rightHandChoiceIndex, _rightHandChoices, ref mmo.mmomData.currentRightHand, mmo.mmomData.rightHands);
        GUILayout.EndHorizontal();

        GUILayout.Label("Left hand:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _leftHandChoiceIndex, _leftHandChoices, ref mmo.mmomData.currentLeftHand, mmo.mmomData.leftHands);
        GUILayout.EndHorizontal();

        GUILayout.Label("Hips:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _hipsChoiceIndex, _hipsChoices, ref mmo.mmomData.currentHips, mmo.mmomData.hips);
        GUILayout.EndHorizontal();

        GUILayout.Label("Right leg:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _rightLegChoiceIndex, _rightLegChoices, ref mmo.mmomData.currentRightLeg, mmo.mmomData.rightLegs);
        GUILayout.EndHorizontal();

        GUILayout.Label("Left leg:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _leftLegChoiceIndex, _leftLegChoices, ref mmo.mmomData.currentLeftLeg, mmo.mmomData.leftLegs);
        GUILayout.EndHorizontal();

        GUILayout.Label("\t\tACCESSORIES");

        GUILayout.Label("Hat:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _hatChoiceIndex, _hatChoices, ref mmo.mmomData.currentHat, mmo.mmomData.hats);
        GUILayout.EndHorizontal();

        GUILayout.Label("Hat attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _hatAttachmentChoiceIndex, _hatAttachmentChoices, ref mmo.mmomData.currentHatAttachment, mmo.mmomData.hatAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Back attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _backAttachmentChoiceIndex, _backAttachmentChoices, ref mmo.mmomData.currentBackAttachment, mmo.mmomData.backAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Right shoulder attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _rightShoulderAttachmentChoiceIndex, _rightShoulderAttachmentChoices, ref mmo.mmomData.currentRightShoulderAttachment, mmo.mmomData.rightShoulderAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Left shoulder attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _leftShoulderAttachmentChoiceIndex, _leftShoulderAttachmentChoices, ref mmo.mmomData.currentLeftShoulderAttachment, mmo.mmomData.leftShoulderAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Right elbow attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _rightElbowAttachmentChoiceIndex, _rightElbowAttachmentChoices, ref mmo.mmomData.currentRightElbowAttachment, mmo.mmomData.rightElbowAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Left elbow attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _leftElbowAttachmentChoiceIndex, _leftElbowAttachmentChoices, ref mmo.mmomData.currentLeftElbowAttachment, mmo.mmomData.leftElbowAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Hip attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _hipAttachmentChoiceIndex, _hipAttachmentChoices, ref mmo.mmomData.currentHipAttachment, mmo.mmomData.hipAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Right knee attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _rightKneeAttachmentChoiceIndex, _rightKneeAttachmentChoices, ref mmo.mmomData.currentRightKneeAttachment, mmo.mmomData.rightKneeAttachments);
        GUILayout.EndHorizontal();

        GUILayout.Label("Left knee attachments:");
        GUILayout.BeginHorizontal();
        DoGUI(ref _leftKneeAttachmentChoiceIndex, _leftKneeAttachmentChoices, ref mmo.mmomData.currentLeftKneeAttachment, mmo.mmomData.leftKneeAttachments);
        GUILayout.EndHorizontal();

    }

    // deactive current object, swicth to object indexed in list, active new object
    void ToggleObject(ref Transform tTo, List<Transform> tlFrom, int i)
    {
        if (tTo != null)
        {
            tTo.gameObject.SetActive(false);
        }
        tTo = tlFrom[i];
        tTo.gameObject.SetActive(true);
    }

    // generate a list of strings from a list of transforms to reference as options in UI
    string[] TransformToString(string[] s, List<Transform> lt)
    {
        s = new string[lt.Count];

        for (int i = 0; i < lt.Count; i++)
        {
            s[i] = lt[i].name.ToString();
        }
        return s;
    }

    // present user with popup list, save the selection as an index, use index to 
    // toggle objects on button press
    void DoGUI(ref int choiceIndex, string[] choices, ref Transform current, List<Transform> all)
    {
        choiceIndex = EditorGUILayout.Popup(choiceIndex, choices);
        if (GUILayout.Button("Change"))
        {
            ToggleObject(ref current, all, choiceIndex);
        }
        if (GUILayout.Button("Clear"))
        {
            if (current != null)
            {
                current.gameObject.SetActive(false);
            }
            
        }
    }
}
