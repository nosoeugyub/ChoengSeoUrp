using UnityEngine;
public class ClickManager : MonoBehaviour
{
    //RaycastHit hit;
    //Ray ray;
    //IInteractable interactable;
    public GameObject collisionUI;//띄울 UI

    //int layerMask;   // Player 레이어만 충돌 체크함
    //private void Awake()
    //{
    //    layerMask = 1 << LayerMask.NameToLayer("Interactable");
    //    //마우스 상호작용 오브젝트는 Interactable 이라는 레이어를 가지고 있어야 합니다.
    //}
    //private void Update()
    //{
    //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

    //    if (Physics.Raycast(ray, out hit, 10000, layerMask))
    //    {
    //        print(hit.collider.name);
    //        interactable = hit.collider.GetComponent<IInteractable>();
    //        if (interactable != null)
    //        {
    //            collisionUI.SetActive(true);
    //            interactable.CanInteract();
    //            Vector3 uiPos = new Vector3(interactable.ReturnTF().position.x, interactable.ReturnTF().position.y + 3, interactable.ReturnTF().position.z);
    //            collisionUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);
    //        }
    //    }
    //    else
    //        collisionUI.SetActive(false);


    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (Physics.Raycast(ray, out hit, 10000, layerMask))
    //        {
    //            print(hit.collider.name);
    //            interactable = hit.collider.GetComponent<IInteractable>();
    //            if (interactable != null)
    //            {
    //                interactable.Interact();
    //            }
    //        }
    //    }


    //}
}