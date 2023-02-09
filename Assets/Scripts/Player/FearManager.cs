using UnityEngine;
using UnityEngine.SceneManagement;

public class FearManager : MonoBehaviour
{
    public float maxFear { get; private set; } = 100f;
    public float currentFear { get; private set; } = 0f;
   
    //public int numberOfScarers = 0;
    public float fearPerSecond = 0f;
    private float scareTimer;
    [SerializeField]
    private float scareTimerLength = 0.1f;

    private float unscareTimer;
    [SerializeField]
    private float unscareTimerLength = 0.1f;
    [SerializeField]
    private float unscareFearPerSecond = 10f;

    private void Update()
    {
        if (fearPerSecond <= 0)
        {
            unscareTimer -= Time.deltaTime;

            if (unscareTimer <= 0)
            {
                GetUnscared(unscareFearPerSecond * unscareTimerLength);
            }
        }
        else
        {
            scareTimer -= Time.deltaTime;

            if (scareTimer <= 0)
            {
                GetScared(fearPerSecond * scareTimerLength);
            }
        }
    }

    public void GetScared(float amount)
    {
        currentFear += amount;

        scareTimer = scareTimerLength;

        if (currentFear > maxFear)
        {
            currentFear = maxFear;
            Faint();
        }
    }

    public void GetUnscared(float amount)
    {
        currentFear -= amount;

        unscareTimer = unscareTimerLength;

        if (currentFear < 0)
        {
            currentFear = 0;
        }
    }

    private void Faint()
    {
        currentFear = 0f;

        Debug.Log("You Fainted.");

        // Faint animation

        // Ogre coming to get you cutscene

        // Restart from beginning (or checkpoint)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}