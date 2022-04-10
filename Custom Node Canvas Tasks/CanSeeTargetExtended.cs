// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Custom condition task for Paradox Notion's Node Canvas.
// Store multiple targets, check if they are in range, FOV, and agents view is un-obstructed
// by objects on targetLayers. Store a blackboard reference to the closest target,
// along with the position it was last seen at. 
// NOTE //
// To save the last seen position in the blackboard:
// > choose the blackboard option of the parameter
// > Graph > Create New > <enter var name>
// in the blackboard itself:
// > Settings cog > Change Type > Unity > Vector3
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions
{
    [Category("_Custom")]
    [Description("Tests if agent can see tagets. Chooses closest target")]
    public class CanSeeTargetExtended : ConditionTask<Transform>
    {

        [RequiredField]
        public BBParameter<List<GameObject>> targetObjects;
        public BBParameter<float> sightDistance = 50;
        public BBParameter<LayerMask> targetLayers = (LayerMask)(-1);
        public BBParameter<float> rayCastHeight = 1f;
        public BBParameter<string> targetTag1 = "Player";
        public BBParameter<string> targetTag2 = "AI";
        public BBParameter<Blackboard> saveTarget;
        public BBParameter<Blackboard> saveTargetPosition;


        private bool targetInFOV = false;
        private RaycastHit hit;
        private Vector3 fovDirection;
        private float distanceToTarget;
        private float shortestDistance;
        private int bestTargetIndex;

        private Vector3 rayCastOffSet { get { return new Vector3(0, rayCastHeight.value, 0); } }
        

        protected override bool OnCheck()
        {
            shortestDistance = sightDistance.value;

            for (int i = 0; i < targetObjects.value.Count; i++)
            {
                GameObject g = targetObjects.value[i];
                targetInFOV = false;
                distanceToTarget = Vector3.Distance(g.transform.position, agent.transform.position);

                if (distanceToTarget > shortestDistance)
                {
                    continue;
                }

                // get vector between object and target
                fovDirection = g.transform.position - agent.transform.position;
                // is target within vision cone of this object
                targetInFOV = Vector3.Dot(agent.transform.forward, fovDirection.normalized) > 0.5f;

                if (!targetInFOV)
                {
                    continue;
                }

                // raycast higher than the ground
                Vector3 origin = agent.transform.position + rayCastOffSet;
                // raycast for obstructions
                if (Physics.Raycast(origin, fovDirection, out hit, sightDistance.value, targetLayers.value))
                {
                    ////debug helpers
                    //Debug.DrawRay(agent.transform.position, fovDirection * hit.distance, Color.yellow);
                    //Debug.Log("Hit: " + hit.collider.gameObject.name);

                    // if raycast hit the target
                    if (hit.collider.gameObject.CompareTag(targetTag1.value) || hit.collider.gameObject.CompareTag(targetTag2.value))
                    {
                        // we know target is closest so far, in fov, and not obstructed
                        shortestDistance = distanceToTarget;
                        bestTargetIndex = i;
                    }
                }
            }

            // shortestDistance will only have changed on finding a valid target
            if (shortestDistance < sightDistance.value)
            {
                // save our target and its last position
                saveTarget.varRef.value = targetObjects.value[bestTargetIndex];
                // track target last position
                saveTargetPosition.varRef.value = targetObjects.value[bestTargetIndex].transform.position;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
