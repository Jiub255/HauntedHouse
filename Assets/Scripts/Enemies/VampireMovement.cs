public class VampireMovement : DetectPlayerBase
{
    private void Update()
    {
        if (canSeePlayer)
        {
            PlayerNearAction();
        }
        else
        {
            PlayerFarAction();
        }
    }

    public override void PlayerNearAction()
    {
        base.PlayerNearAction();
    }

    public override void PlayerFarAction()
    {
        base.PlayerFarAction();
    }
}