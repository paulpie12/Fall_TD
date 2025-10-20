using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask PlacementCheckMask;
    [SerializeField] private LayerMask PlacementCollideMask;
    [SerializeField] private Camera Playercamera;
    [SerializeField] private TowerLogic TowerLogic;

    private GameObject CurrentPlacingTower;

    void Update()
    {
        if (CurrentPlacingTower != null)
        {
            Ray camray = Playercamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;

            if (Physics.Raycast(camray, out HitInfo, 100f, PlacementCollideMask))
            {
                Vector3 placementPos = HitInfo.point;
                placementPos.y += 0.1f;
                CurrentPlacingTower.transform.position = placementPos;
            }

            // So HitInfo retains its value after a successful raycast.
            if (Input.GetMouseButtonDown(0))
            {

                // Only place if we have a valid hit
                if (HitInfo.collider != null && !HitInfo.collider.gameObject.CompareTag("CantPlace"))
                {
                    BoxCollider TowerCollider = CurrentPlacingTower.GetComponent<BoxCollider>();
                    TowerCollider.isTrigger = true;

                    Bounds bounds = TowerCollider.bounds;

                    if (!Physics.CheckBox(bounds.center, bounds.extents, Quaternion.identity, PlacementCheckMask, QueryTriggerInteraction.Ignore))
                    {
                        TowerCollider.isTrigger = false;
                        CurrentPlacingTower.GetComponent<TowerLogic>().Placed = true;
                        CurrentPlacingTower = null;
                    }
                }
            }
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        CurrentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
        CurrentPlacingTower.GetComponent<TowerLogic>().Placed = false;
    }
}
