using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public string gemColor = "red";

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // awal: diam, tidak jatuh
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // AKTIFKAN physics saat diambil
            rb.isKinematic = false;
            rb.useGravity = false;

            transform.SetParent(other.transform);
        }
    }
}