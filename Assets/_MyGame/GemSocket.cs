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

                // Matikan physics agar tidak glitch / terlempar
                Rigidbody rb = gem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }

                // Snap objek gem tepat ke tengah plat altar
                gem.transform.position = transform.position;
                gem.transform.SetParent(transform);

                // Mencari manager menggunakan fungsi Unity modern (Unity 6+)
                GemObjectiveManager objectiveManager = Object.FindFirstObjectByType<GemObjectiveManager>();
                
                if (objectiveManager != null)
                {
                    objectiveManager.PlaceGem(socketColor);
                }
                else
                {
                    Debug.LogWarning("GemObjectiveManager tidak ditemukan di Scene!");
                }
            }
            else
            {
                Debug.Log("SALAH WARNA!");
            }
        }
    }
}