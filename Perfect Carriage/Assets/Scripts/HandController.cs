using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject MovingObject;

    [SerializeField]
    private LayerMask back;

    [SerializeField]
    private LayerMask train;

    [SerializeField]
    private LayerMask human;

    private Vector2 offset;

    public bool DeleteOnUp;

    void Update()
    {

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero, 0.1f, human);

                if (hit.collider != null)
                {
                    MovingObject = hit.collider.gameObject;

                    offset = hit.point - (Vector2)MovingObject.transform.position;

                }
            }

            if (MovingObject != null)
            {
                RaycastHit2D hitBack = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero, 0.1f, back);

                MovingObject.transform.position = (Vector3)hitBack.point - (Vector3)offset + new Vector3(0, 0, -1);
            }


            if (DeleteOnUp)
            {
                MovingObject.GetComponent<Passanger>().ChangeCarriage(null);
                
                Destroy(MovingObject.gameObject);
                
                DeleteOnUp = false;

                return;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {


                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero, 0.1f, train);

                if (MovingObject != null && hit.collider.gameObject != null)
                {
                    MovingObject.GetComponent<Passanger>().ChangeCarriage(hit.collider.gameObject.GetComponent<CarriageControl>());

                    if (MovingObject.transform.position.y > -1.7f)
                    {
                        MovingObject.transform.position = new Vector3(MovingObject.transform.position.x, Random.Range(-2f, -4f), -1);
                    }

                    MovingObject = null;
                }
            }

        }

    }
}
