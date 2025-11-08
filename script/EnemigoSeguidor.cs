using UnityEngine;
using UnityEngine.AI;

public class EnemigoSeguidor : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent agente;
    public float rangoParaQuitar = 2.0f;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    //void Update()
    //{
    //    if (jugador == null) return;

    //    agente.SetDestination(jugador.position);

    //    float distancia = Vector3.Distance(transform.position, jugador.position);
    //    if (distancia <= rangoParaQuitar)
    //    {
    //        Transform esfera = jugador.Find("Sphere");
    //        if (esfera != null)
    //        {
    //            Rigidbody rb = esfera.GetComponent<Rigidbody>();
    //            if (rb != null)
    //            {
    //                esfera.SetParent(null); // Desvincula la esfera del jugador
    //                rb.isKinematic = false; // Activa la física en la esfera
    //            }
    //        }
    //    }
    //}
    void Update()
    {
        if (jugador == null) return;

        agente.SetDestination(jugador.position);

        float distancia = Vector3.Distance(transform.position, jugador.position);
        if (distancia <= rangoParaQuitar)
        {
            Transform[] hijos = jugador.GetComponentsInChildren<Transform>();
            foreach (Transform hijo in hijos)
            {
                if (hijo.name.Contains("Sphere"))
                {
                    Rigidbody rb = hijo.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        hijo.SetParent(null); // Desvincula la esfera
                        rb.isKinematic = false; // Activa la física
                    }
                }
            }
        }
    }

}
