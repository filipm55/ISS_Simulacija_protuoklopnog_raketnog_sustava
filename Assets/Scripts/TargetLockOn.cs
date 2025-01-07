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
        //provjera je li tenk unisten
        if (isLocked && lockedTarget == null)
        {
            UnlockTarget(); // Resetiraj aim
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
                //izracun gdje je centar tenka
                Bounds colliderBounds = hit.collider.bounds;
                Vector3 targetCenter = colliderBounds.center;

                //dohvati tenk koji je trenutno vidljiv
                aimedTarget = hit.transform;

                //postavi aim na poziciju tenka
                Vector3 screenPosition = scopeCamera.WorldToScreenPoint(targetCenter);
                whiteAimRectangle.position = screenPosition;
                whiteAimRectangle.gameObject.SetActive(true);

                //zakljucaj/otkljucaj tenk pritiskom na tipku L
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
            //ako nije aimano na nista
            aimedTarget = null;
            whiteAimRectangle.gameObject.SetActive(false);
        }

        if (isLocked && lockedTarget != null)
        {
            if (lockedTarget.gameObject != null) // provjeri je li ciljani tenk jos uvijek postoji
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
