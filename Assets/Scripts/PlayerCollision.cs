
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("References")]
    private GameManager gm;

    [SerializeField] private Animator StumbleAnim;

    [Header("Strike Settings")]
    [SerializeField] private int maxStrikes = 3;

    private int currentStrikes = 0;

    private bool isStumbling;
    private bool hasEnteredStumble;

    public int CurrentStrikes => currentStrikes;

    private void Start()
    {
        gm = FindFirstObjectByType<GameManager>();

        if (gm == null)
            Debug.LogError("GameManager not found!");
    }

    private void Update()
    {
        CheckStumbleRecovery();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gm == null || !gm.IsGameRunning)
            return;

        if (collision.gameObject.CompareTag("Coin"))
        {
            gm.UpdateCoin(10);

            // Play coin collection sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayCoinCollect();

            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Fence"))
        {
            RegisterHit();
        }
    }

    private void RegisterHit()
    {
        if (isStumbling)
            return;

        currentStrikes++;

        Debug.Log("Strikes: " + currentStrikes + "/" + maxStrikes);

        if (currentStrikes >= maxStrikes)
        {
            gm.EndGame();
            return;
        }

        // Debug audio
        if (AudioManager.Instance != null)
        {
            Debug.Log("AudioManager found. Playing obstacle sound.");
            AudioManager.Instance.PlayObstacleHit();
        }
        else
        {
            Debug.LogError("AudioManager Instance is NULL!");
        }

        if (StumbleAnim != null)
        {
            isStumbling = true;
            hasEnteredStumble = false;

            StumbleAnim.ResetTrigger("Hit");
            StumbleAnim.SetTrigger("Hit");
        }
    }

    private void CheckStumbleRecovery()
    {
        if (!isStumbling || StumbleAnim == null)
            return;

        AnimatorStateInfo state = StumbleAnim.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("stumble"))
        {
            hasEnteredStumble = true;
        }
        else if (hasEnteredStumble && !StumbleAnim.IsInTransition(0))
        {
            isStumbling = false;
            hasEnteredStumble = false;

            Debug.Log("Player recovered!");
        }
    }
}
