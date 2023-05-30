using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerIA : MonoBehaviour
{
    [SerializeField]
    private Camera camera1;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private int health;

    [SerializeField]
    private List<Transform> spawn1;

    public VS2 IA;

    private float raycastDistance = 100f;

    private Coroutine raycastCoroutine;

    private void Start()
    {

        if (camera1 == null)
        {
            Debug.LogError("pas caméra");
            this.enabled = false;
        }
        
        IA = gameObject.GetComponent<VS2>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera1.transform.position, camera1.transform.forward, out hit, raycastDistance, mask))
        {
            Debug.Log("Touché : " + hit.collider.name);

            if (hit.collider.CompareTag("IA"))
            {
                VS2 AI = hit.collider.GetComponent<VS2>();

                if (AI != null)
                {
                    AI.Damage2();
                }
            }
        }

        if (raycastCoroutine != null)
        {
            StopCoroutine(raycastCoroutine);
        }

        raycastCoroutine = StartCoroutine(ShowRaycast());
    }

    private IEnumerator ShowRaycast()
    {
        Debug.DrawRay(camera1.transform.position, camera1.transform.forward * raycastDistance, Color.red, 2f);

        yield return new WaitForSeconds(2f);

        Debug.DrawRay(camera1.transform.position, camera1.transform.forward * raycastDistance, Color.clear);
    }

    public void Damage()
    {
        if (Random.Range(0f, 1f) <= 0.3f)
        {
            health -= 10;

            if (health <= 0)
            {
                Debug.Log("Joueur mort");
                health = 100;
                int a = Random.Range(0, spawn1.Count);
                transform.position = spawn1[a].position;
                transform.rotation = spawn1[a].rotation;
                Debug.Log(health);
            }
        }
    }
}