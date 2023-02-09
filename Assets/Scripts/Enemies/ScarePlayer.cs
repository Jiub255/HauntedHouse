using UnityEngine;

public class ScarePlayer : DetectPlayerBase, IScarePlayer
{
    [SerializeField]
    private float fearPerSecond = 1f;
    private FearManager fearManager;

    private void Update()
    {
        if (justSawPlayer)
        {
            OnDetectPlayer();
        }

        if (justStoppedSeeingPlayer)
        {
            OnStopDetectingPlayer();
        }
    }

    public override void OnDetectPlayer()
    {
        Debug.Log("ScarePlayer just detected player");

        fearManager = playerTransform.gameObject.GetComponent<FearManager>();
        fearManager.fearPerSecond += fearPerSecond;
    }

    public override void OnStopDetectingPlayer()
    {
        Debug.Log("ScarePlayer just stopped detecting player");
        
        fearManager.fearPerSecond -= fearPerSecond;
    }
}