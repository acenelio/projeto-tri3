using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target = null;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        public float minY = -3f;

        // Use this for initialization
        private void Start() {
            transform.parent = null;
            if (target != null) {
                updateVariables();
            }
        }

        void updateVariables() {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
        }

        // Update is called once per frame
        private void Update()
        {
            if (target != null) {
                // only update lookahead pos if accelerating or changed direction
                float xMoveDelta = (target.position - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
                }

                Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                if (newPos.y < minY) {
                    newPos = new Vector3(newPos.x, minY, newPos.z);
                }

                transform.position = newPos;

                m_LastTargetPosition = target.position;
            }
            else {
                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                if (obj != null) {
                    target = obj.transform;
                    updateVariables();
                }
            }
        }
    }
}
