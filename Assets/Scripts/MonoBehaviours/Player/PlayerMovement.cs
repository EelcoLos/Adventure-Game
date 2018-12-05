using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour 
{
	private WaitForSeconds inputHoldWait;
	private Vector3 destinationPosition;
	private Interactable currentInteractable;
	private bool handleInput = true;
	private const float stopDistanceProportion = 0.1f;
	private const float navMeshSampleDistance = 4f;
	private readonly int hashSpeedPara = Animator.StringToHash("Speed");
	private readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");
	
	public Animator animator;
	public NavMeshAgent agent;
	public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
	[Tooltip("Amount of time speed is change to its value")]
	public float speedDampTime = 0.1f;
    private float slowingSpeed = 0.175f;
    private float turnSmoothing = 15f;



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
	{
		agent.updateRotation = false;
		inputHoldWait = new WaitForSeconds(inputHoldDelay);

		destinationPosition = transform.position;
	}

	/// <summary>
	/// Callback for processing animation movements for modifying root motion.
	/// </summary>
	private void OnAnimatorMove()
	{
		agent.velocity = animator.deltaPosition / Time.deltaTime;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	private void Update()
	{
		if (agent.pathPending)
		{
			return;
		}

		float speed = agent.desiredVelocity.magnitude;
		if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
		{
			Stopping(out speed);
		}
		else if (agent.remainingDistance <= agent.stoppingDistance)
		{
			Slowing(out speed, agent.remainingDistance);
		}
		else if (speed > turnSpeedThreshold)
		{
			Moving();
		}

		animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
	}

	private void Stopping(out float speed)
	{
		agent.Stop();
		transform.position = destinationPosition;
		speed = 0f;

		// Determine if we're heading for an interactable
		if (currentInteractable)
		{
			transform.rotation = currentInteractable.interactionLocation.rotation;
			currentInteractable.Interact();
			currentInteractable = null;
			StartCoroutine(WaitForInteraction());
		}
	}

	private void Slowing(out float speed, float distanceToDestionation)
	{
		agent.Stop();
		transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);
		float proportionalDistance = 1f - distanceToDestionation/agent.stoppingDistance;
		speed = Mathf.Lerp(slowingSpeed, 0, proportionalDistance);

		Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
	}
	private IEnumerator WaitForInteraction()
	{
		handleInput = false;

		yield return inputHoldWait;

		while(animator.GetCurrentAnimatorStateInfo(0).tagHash != hashLocomotionTag)
		{
			yield return null;
		}

		handleInput = true;
	}

	/// <summary>
	/// Adjusting rotation of movement
	/// </summary>
	private void Moving()
	{
		Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
	}

	public void OnGroundClick(BaseEventData data)
	{
		if (!handleInput)
		{
			return;
		}
		currentInteractable = null;
		PointerEventData pdata = (PointerEventData)data;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(pdata.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
		{
			destinationPosition = hit.position;
		}
		else
		{
			destinationPosition = pdata.pointerCurrentRaycast.worldPosition;
		}
		agent.SetDestination(destinationPosition);
		agent.Resume();
	}

	public void OnInteracableClick(Interactable interactable)
	{
		if (!handleInput)
		{
			return;
		}
		currentInteractable = interactable;
		destinationPosition = currentInteractable.interactionLocation.position;
		agent.SetDestination(destinationPosition);
		agent.Resume();
	}

	
}