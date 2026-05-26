using UnityEngine;
using UnityEngine.UI;

public class GemObjectiveManagerAlr : MonoBehaviour
{
    public static GemObjectiveManagerAlr Instance;

    [Header("=== GEM ICONS ===")]
    public Image iconRed;
    public Image iconGreen;
    public Image iconYellow;

    [Header("=== CHECK MARKS ===")]
    public Image checkRed;
    public Image checkGreen;
    public Image checkYellow;

    [Header("=== STATUS TEXT ===")]
    public Text statusRed;
    public Text statusGreen;
    public Text statusYellow;

    [Header("=== PROGRESS ===")]
    public Slider progressSlider;
    public Text   progressCountText;

    [Header("=== WIN PANEL ===")]
    public GameObject winPanel;

    // Internal state
    private bool redCollected    = false;
    private bool greenCollected  = false;
    private bool yellowCollected = false;

    // Colors for collected state
    private Color dimColor    = new Color(0.25f, 0.25f, 0.25f, 1f);   
    private Color brightColor = Color.white;                           

    void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Pastikan Win Panel tidak muncul di awal
        if (winPanel != null) winPanel.SetActive(false);

        // Set semua ikon gelap di awal (belum dikumpulkan)
        if (iconRed)    iconRed.color    = dimColor;
        if (iconGreen)  iconGreen.color  = dimColor;
        if (iconYellow) iconYellow.color = dimColor;

        // Sembunyikan semua checkmark di awal
        if (checkRed)    checkRed.enabled    = false;
        if (checkGreen)  checkGreen.enabled  = false;
        if (checkYellow) checkYellow.enabled = false;

        // Set progress awal
        UpdateProgress();
    }

    // ─── Dipanggil oleh GemPickup saat player menyentuh gem ───
    public void CollectGem(string gemColor)
    {
        switch (gemColor.ToLower())
        {
            case "red":
                if (redCollected) return;
                redCollected = true;
                if (iconRed)    iconRed.color    = brightColor;
                if (checkRed)   checkRed.enabled = true;
                if (statusRed)  statusRed.text   = "Red Gem collected!";
                break;

            case "green":
                if (greenCollected) return;
                greenCollected = true;
                if (iconGreen)   iconGreen.color    = brightColor;
                if (checkGreen)  checkGreen.enabled = true;
                if (statusGreen) statusGreen.text   = "Green Gem collected!";
                break;

            case "yellow":
                if (yellowCollected) return;
                yellowCollected = true;
                if (iconYellow)   iconYellow.color    = brightColor;
                if (checkYellow)  checkYellow.enabled = true;
                if (statusYellow) statusYellow.text   = "Yellow Gem collected!";
                break;
        }

        UpdateProgress();
    }

    private void UpdateProgress()
    {
        int count = 0;
        if (redCollected)    count++;
        if (greenCollected)  count++;
        if (yellowCollected) count++;

        // Update slider (0.0 sampai 1.0)
        if (progressSlider != null)
            progressSlider.value = count / 3f;

        // Update teks "0 / 3"
        if (progressCountText != null)
            progressCountText.text = count + " / 3";

        // Tampilkan Win Panel kalau semua sudah terkumpul
        if (count == 3 && winPanel != null)
            winPanel.SetActive(true);
    }
}
