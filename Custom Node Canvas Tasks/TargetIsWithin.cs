// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Custom condition task for Paradox Notion's Node Canvas.
// Test if target is within distance plus or minus offset.
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions
{
    [Category("_Custom")]
    [Description("Test if target is within distance plus or minus offset")]
    public class TargetIsWithin : ConditionTask<Transform>
    {
        [RequiredField]
        public BBParameter<GameObject> checkTarget;
        public BBParameter<float> distance = 10f;
        public BBParameter<float> offset = 1f;

        private float measuredDistance;

        protected override bool OnCheck()
        {
            measuredDistance = Vector3.Distance(agent.transform.position, checkTarget.value.transform.position);

            if (measuredDistance > (distance.value - offset.value) && measuredDistance < (distance.value + offset.value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}