// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Custom action task for Paradox Notion's Node Canvas.
// Agent waits a random duration while setting boolean isIdle animator parameter to true,
// then sets it to false, wanders a random distance, and ends action.
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

using NavMeshAgent = UnityEngine.AI.NavMeshAgent;
using NavMesh = UnityEngine.AI.NavMesh;
using NavMeshHit = UnityEngine.AI.NavMeshHit;


namespace NodeCanvas.Tasks.Actions
{

    [Category("_Custom/Movement/Pathfinding")]
    [Description("Navmesh agent waits for random duration, controls boolean animator parameter, and wanders random distance")]
    public class WanderRandomIdleWait : ActionTask<NavMeshAgent>
    {
        // wander vars
        public BBParameter<float> speed = 2f;
        public BBParameter<float> keepDistance = 0.2f;
        public BBParameter<float> minWanderDistance = 3f;
        public BBParameter<float> maxWanderDistance = 15f;
        private Vector3 wanderPos;
        private float wanderDistance { get {  return Random.Range(minWanderDistance.value, maxWanderDistance.value); } }
        
        // wait vars
        public BBParameter<float> waitTimeMin = 1f;
        public BBParameter<float> waitTimeMax = 4f;
        private float waitTime = 0f;

        // animation vars
        public BBParameter<string> idleAnimatorParameter = "isIdle";
        public Animator animator;

        private bool processingPath = false;
        

        protected override void OnExecute()
        {
            processingPath = false;
            agent.speed = speed.value;
            waitTime = Random.Range(waitTimeMin.value, waitTimeMax.value);
            animator.SetBool(idleAnimatorParameter.value, true);
        }


        protected override void OnUpdate()
        {
            if ((elapsedTime >= waitTime) && (processingPath == false))
            {
                processingPath = true;
                animator.SetBool(idleAnimatorParameter.value, false);
                DoWander();
            }

            if ((processingPath == true) && (!agent.pathPending) && (agent.remainingDistance <= agent.stoppingDistance + keepDistance.value))
            {
                EndAction();
            }
        }


        protected override void OnPause() { OnStop(); }

        protected override void OnStop()
        {
            if (agent.gameObject.activeSelf)
            {
                agent.ResetPath();
            }
        }


        private void DoWander()
        {
            wanderPos = RandomNavSphere(agent.transform.position, wanderDistance, NavMesh.AllAreas);
            agent.SetDestination(wanderPos);
        }


        private Vector3 RandomNavSphere(Vector3 playerOrigin, float distance, int layermask)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += playerOrigin;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
            return navHit.position;
        }
    }
}
