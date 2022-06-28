using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Range(0, 360)][SerializeField] private float viewAngle = 90f;
    [SerializeField] private float viewDistance = 15f;
    [SerializeField] private float detectionDistance = 3f;
    [SerializeField] private Transform enemyEye;
    
    private Transform target;
    private NavMeshAgent navAgent;
    private float rotationSpeed;
    private Transform navAgentTransform;

    private void OnEnable()
    {
        PlayerController.onAlarmActivated += MoveToTarget;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        rotationSpeed = navAgent.angularSpeed;
        navAgentTransform = navAgent.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnDisable()
    {
        PlayerController.onAlarmActivated -= MoveToTarget;
    }
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(target.transform.position, navAgent.transform.position);
        if (distanceToPlayer <= detectionDistance || IsInView())
        {
            RotateToTarget();
            MoveToTarget(target);
        }
        DrawViewState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
    private bool IsInView()
    {
        float realAngle = Vector3.Angle(enemyEye.forward, target.position - enemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyEye.transform.position, target.position - enemyEye.position, out hit, viewDistance))
        {
            if (realAngle < viewAngle / 2f && Vector3.Distance(enemyEye.position, target.position) <= viewDistance && hit.transform == target.transform)
            {
                return true;
            }
        }
        return false;
    }
    private void RotateToTarget() 
    {
        Vector3 lookVector = target.position - navAgentTransform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero) 
            return;
        navAgentTransform.rotation = Quaternion.RotateTowards
            (
                navAgentTransform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                rotationSpeed * Time.deltaTime
            );
    }
    private void MoveToTarget(Transform victim) 
    {
        navAgent.SetDestination(victim.position);
    }

    private void DrawViewState()
    {
        Vector3 left = enemyEye.position + Quaternion.Euler(new Vector3(0, viewAngle / 2f, 0)) * (enemyEye.forward * viewDistance);
        Vector3 right = enemyEye.position + Quaternion.Euler(-new Vector3(0, viewAngle / 2f, 0)) * (enemyEye.forward * viewDistance);
        Debug.DrawLine(enemyEye.position, left, Color.yellow);
        Debug.DrawLine(enemyEye.position, right, Color.yellow);
    }
}