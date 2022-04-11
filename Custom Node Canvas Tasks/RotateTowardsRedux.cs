// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Custom action task for Paradox Notion's Node Canvas.
// Incrementally rotate agent on it's Y axis, towards target.
// Ignoring the targets Y axis prevents attempted modifications
// to agents Y axis (jittering on a constrained rigidbody)
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{
    [Category("_Custom/Movement/Direct")]
    [Description("Incrementally rotate the agent on it's Y axis towards target")]
    public class RotateTowardsRedux : ActionTask<Transform>
    {
        public BBParameter<GameObject> target;
        public BBParameter<float> rotationSpeed = 5f;

        private Vector3 newDirection;
        private Quaternion lookNewDirection;

        private Vector3 targetXZagentY { get { return new Vector3(target.value.transform.position.x, agent.transform.position.y, target.value.transform.position.z); } }
        private Vector3 targetDirection { get { return targetXZagentY - agent.transform.position; } }
        private float singleStep { get { return rotationSpeed.value * Time.deltaTime; } }

        protected override void OnUpdate()
        {
            // get the new incremental rotation
            newDirection = Vector3.RotateTowards(agent.transform.forward, targetDirection, singleStep, 0.0f);
            lookNewDirection = Quaternion.LookRotation(newDirection);

            if (agent.transform.rotation != lookNewDirection)
            {
                agent.transform.rotation = lookNewDirection;
            }

            EndAction();
        }
    }
}
