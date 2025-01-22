using UnityEngine;


public class TargetLockOn : MonoBehaviour
{
    public Camera scopeCamera;
    public RectTransform whiteAimRectangle;
    public RectTransform greenAimRectangle;
    public LayerMask targetLayer;

    private Transform lockedTarget;
    private Transform aimedTarget;
    private bool isLocked = false;
    public GameObject scopeOverlay;

    void Update()
    {
        if (isLocked && lockedTarget == null)
        {
            UnlockTarget(); 
        }

        if(scopeOverlay.activeInHierarchy){
            HandleTargetLock();
        } else {
            UnlockTarget();
            whiteAimRectangle.gameObject.SetActive(false);
        }
    }

    private void HandleTargetLock()
    {

        Ray ray = scopeCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer) && scopeOverlay.activeInHierarchy)
        {
            if (hit.collider.CompareTag("enemy"))
            {
                Bounds colliderBounds = hit.collider.bounds;
                Vector3 targetCenter = colliderBounds.center;

                aimedTarget = hit.transform;

                Vector3 screenPosition = scopeCamera.WorldToScreenPoint(targetCenter);
                whiteAimRectangle.position = screenPosition;
                whiteAimRectangle.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.L) && scopeOverlay.activeInHierarchy)
                {
                    if (isLocked)
                    {
                        UnlockTarget();
                    }
                    else
                    {
                        LockTarget(aimedTarget);
                    }
                }

            }
        }
        else
        {
            aimedTarget = null;
            whiteAimRectangle.gameObject.SetActive(false);
        }

        if (isLocked && lockedTarget != null)
        {
            if (lockedTarget.gameObject != null) 
            {
                Bounds colliderBounds = lockedTarget.GetComponent<Collider>().bounds;
                Vector3 targetCenter = colliderBounds.center;

                Vector3 lockedScreenPosition = scopeCamera.WorldToScreenPoint(targetCenter);
                greenAimRectangle.position = lockedScreenPosition;
                greenAimRectangle.gameObject.SetActive(true);
            }
        }
        else
        {
            greenAimRectangle.gameObject.SetActive(false);
        }
    }

    private void LockTarget(Transform target)
    {
        lockedTarget = target;
        isLocked = true;

        whiteAimRectangle.gameObject.SetActive(false);
        greenAimRectangle.gameObject.SetActive(true);
    }

    private void UnlockTarget()
    {
        lockedTarget = null;
        isLocked = false;
        greenAimRectangle.gameObject.SetActive(false);
    }
}
