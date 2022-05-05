// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Custom action task for Paradox Notion's Node Canvas.
// Agent will seek a location, and upon arrival rotate towards a target.
// Then set an animator paramater to true, wait a random duration, and
// set it back to false.
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using NavMeshAgent = UnityEngine.AI.NavMeshAgent;


namespace NodeCanvas.Tasks.Actions
{
    [Category("_Custom/Movement/Pathfinding")]
    [Description("Navmesh agent moves to location, rotates toward target, animates, and waits")]
    public class MoveLocationTargetAnimate : ActionTask<NavMeshAgent>
    {
        public BBParameter<GameObject> locationtarget;
        public BBParameter<GameObject> facetarget;
        public BBParameter<float> movementSpeed = 2f;
        public BBParameter<float> keepDistance = 0.1f;
        public BBParameter<Animator> animator;
        public BBParameter<string> idleAnimatorParameter = "isIdle";
        public BBParameter<float> maxWait = 5f;
        public BBParameter<float> minWait = 1f;
        public BBParameter<bool> useProp = false;
        public BBParameter<GameObject> prop;

        private bool arrived = false;
        private bool finished = false;
        private bool isFacing = false;
        private bool isAnimating = false;
        private float waitTime;
        private float timer = 0f;
        private Vector3 targetXZagentY { get { return new Vector3(facetarget.value.transform.position.x, agent.transform.position.y, facetarget.value.transform.position.z); } }
        private Vector3 targetDirection { get { return targetXZagentY - agent.transform.position; } }


        protected override void OnExecute()
        {
            waitTime = Random.Range(minWait.value, maxWait.value);

            if (Vector3.Distance(agent.transform.position, locationtarget.value.transform.position) <= agent.stoppingDistance + keepDistance.value)
            {
                arrived = true;
            }
            else
            {
                agent.speed = movementSpeed.value;
                agent.SetDestination(locationtarget.value.transform.position);
            }
        }


        protected override void OnUpdate()
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + keepDistance.value)
            {
                arrived = true;
            }

            if (arrived && !isFacing)
            {
                Quaternion rot = Quaternion.LookRotation(targetDirection);
                agent.transform.rotation = rot;
                isFacing = true;
            }

            else if (arrived && isFacing)
            {
                timer += Time.deltaTime;

                if (!isAnimating)
                {
                    agent.speed = 0;
                    animator.value.SetBool(idleAnimatorParameter.value, true);
                    if (useProp.value)
                    {
                        prop.value.SetActive(true);
                    }
                    isAnimating = true;
                }
            }

            if ((timer >= waitTime))
            {
                finished = true;
            }

            if (finished)
            {
                timer = 0f;
                if (useProp.value)
                {
                    prop.value.SetActive(false);
                }
                isFacing = false;
                arrived = false;
                finished = false;
                isAnimating = false;
                animator.value.SetBool(idleAnimatorParameter.value, false);
                EndAction(true);
            }
        }
    }
}

