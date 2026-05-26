using UnityEngine;

public class GemSocket : MonoBehaviour
{
    public string socketColor = "red";

    private void OnTriggerEnter(Collider other)
    {
        GemData gem = other.GetComponent<GemData>();

        if (gem != null)
        {
            if (gem.gemColor.ToLower() == socketColor.ToLower())
            {
                Debug.Log("BENAR - gem masuk!");

                // matikan physics biar gak glitch
                Rigidbody rb = gem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }

                // snap ke tengah plat
                gem.transform.position = transform.position;
                gem.transform.SetParent(transform);

                // update objective
                if (GemObjectiveManagerAlr.Instance != null)
                {
                    GemObjectiveManagerAlr.Instance.CollectGem(socketColor);
                }
            }
            else
            {
                Debug.Log("SALAH WARNA!");
            }
        }
    }
}