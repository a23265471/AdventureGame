using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovemont : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent agent;
    public float turnSpeedThreshold = 0.5f;
    public float speedDamptime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSmoothing = 15f;

    private Vector3 destinationPosition;
    private float curSpeed;
    private float stopDistinanceProportion = 0.1f;

    private void Start()
    {
        agent.updateRotation = false;
        destinationPosition = transform.position;
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;


    }

    private void Update()
    {
        if (agent.pathPending)
            return;
        curSpeed = agent.desiredVelocity.magnitude;
        if (agent.remainingDistance <= agent.stoppingDistance * stopDistinanceProportion)
        {
            Stopping();
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Slowing();
        }
        else if (curSpeed > turnSpeedThreshold)
        {
            Moving();
        }
        animator.SetFloat("Speed", curSpeed, speedDamptime, Time.deltaTime);
    }

    private void Stopping()
    {
        agent.isStopped = true;
        curSpeed = 0;
    }
    private void Slowing()
    {
        agent.isStopped = true;
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);
    }
    private void Moving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(agent.desiredVelocity.x, 0, agent.desiredVelocity.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        

    }
    public void OnGroundClick(BaseEventData data)
    {
        PointerEventData pData = data as PointerEventData;
        destinationPosition = pData.pointerCurrentRaycast.worldPosition;
        agent.SetDestination(destinationPosition);
        agent.isStopped = false; 
    }
}
