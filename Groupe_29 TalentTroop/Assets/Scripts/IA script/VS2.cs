using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VS2 : MonoBehaviour
{

    [SerializeField]
    private float wanderRadius = 10f; // Rayon de déplacement aléatoire

    [SerializeField]
    private float wanderTimer = 5f; // Temps avant de choisir une nouvelle destination

    [SerializeField]
    private int NumberOfRaycasts;

    [SerializeField]
    private int RaycastDistance;

    [SerializeField]
    private int FOV;

    [SerializeField]
    private int health;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private Transform spawn2;


    private Vector3 target; // Destination actuelle
    private NavMeshAgent agent;
    private float timer;

    private bool hasDestination = false;

    private int f;

    private Vector3 previousRotation;
    private Vector3 _currentPosition;
    private Vector3 _movementDirection;

    private static Transform _transform;
    private static Transform _player;

    [SerializeField]
    private int cadence;

    [SerializeField]
    private AudioSource shot2;

    [SerializeField]
    private AudioClip shot3;


    public PlayerIA player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        SetNewDestination();

        _transform = transform;
        f = RaycastDistance;
        previousRotation = transform.position;
    }

    private void Update()
    {
        hasDestination = false;
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            SetNewDestination();
            timer = 0f;
        }

        for (int i = 0; i < NumberOfRaycasts / 2; i++)
        {
            RaycastDistance = f;
            float angle = FOV / NumberOfRaycasts * i;
            Vector3 moveDirection = _transform.forward;
            Quaternion rotation = Quaternion.AngleAxis(angle, _transform.up);
            Vector3 rayDirection = rotation * moveDirection;

            RaycastHit hit;
            if (Physics.Raycast(_transform.position, rayDirection, out hit, RaycastDistance, mask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    hasDestination = true;
                }
                else
                {
                    Vector3 f = hit.point - _transform.position;
                    float q = f.magnitude;
                    RaycastDistance = (int)q;
                }
            }

            previousRotation = _currentPosition;
            Debug.DrawRay(_transform.position, rayDirection * RaycastDistance, Color.red);
        }

        for (int i = 0; i < NumberOfRaycasts / 2; i++)
        {
            RaycastDistance = f;
            float angle = -FOV / NumberOfRaycasts * i;
            Vector3 moveDirection = _transform.forward;
            Quaternion rotation = Quaternion.AngleAxis(angle, _transform.up);
            Vector3 rayDirection = rotation * moveDirection;

            RaycastHit hit;
            if (Physics.Raycast(_transform.position, rayDirection, out hit, RaycastDistance, mask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    hasDestination = true;
                }
                else
                {
                    Vector3 f = hit.point - _transform.position;
                    float q = f.magnitude;
                    RaycastDistance = (int)q;
                }
            }

            previousRotation = _currentPosition;
            Debug.DrawRay(_transform.position, rayDirection * RaycastDistance, Color.red);
        }
        if (hasDestination && cadence <= 0)
        {
            if (player != null)
            {
                player.Damage();
            }
            shot2.PlayOneShot(shot3);
            cadence = 10;
        }
        cadence -= 1;
    }

    private void SetNewDestination()
    {
        Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
        randomPoint += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, NavMesh.AllAreas))
        {
            target = hit.position;
            agent.SetDestination(target);
        }
    }

    public void Damage2()
    {
        
        health -= 10;

        if (health <= 0)
        {
            Debug.Log("IA mort");
            health = 100;
            transform.position = spawn2.position;
            transform.rotation = spawn2.rotation;
        }
        Debug.Log(health);
    }
}
