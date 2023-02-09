using UnityEngine;

public class GhostMovement : DetectPlayerBase
{
    private void OnEnable()
    {
        Hide.onChangeLayer += ChangeSortingLayer;
    }

    private void OnDisable()
    {
        Hide.onChangeLayer -= ChangeSortingLayer;
    }

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

        // Chase Player
        Vector3 movementVector = (playerTransform.position - transform.position).normalized;

        if (movementVector.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movementVector.x <= 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position += movementVector * moveSpeed * Time.deltaTime;
    }

    public override void PlayerFarAction()
    {
        base.PlayerFarAction();
    
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    public void ChangeSortingLayer()
    {
        if (canSeePlayer)
        {
            if (gameObject.GetComponent<Renderer>().sortingLayerName == "Default")
            {
                gameObject.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Hide");
                gameObject.layer = LayerMask.NameToLayer("Hide");
                Debug.Log("Set ghost layer to Hide");
            }
            else if (gameObject.GetComponent<Renderer>().sortingLayerName == "Hide")
            {
                gameObject.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Default");
                gameObject.layer = LayerMask.NameToLayer("Default");
                Debug.Log("Set ghost layer to Default");
            }
        }
    }
}