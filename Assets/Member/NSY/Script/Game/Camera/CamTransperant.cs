using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Cam
{
    public class CamTransperant : MonoBehaviour
    {
        Renderer ObstacleRenderer;

        public LayerMask layerMask;

        public GameObject Character;

        public Shader defaultShader;
        [SerializeField]
         Shader targetShader;


        // 가리는 오브젝트 리스트
        public List<GameObject> transparentObjs = new List<GameObject>();

        public void Awake()
        {
            targetShader = Shader.Find("Unlit/Transperants");
        }


        IEnumerator returnObjs()
        {
            for (int i = 0; i < transparentObjs.Count; i++)
            {
                transparentObjs[i].GetComponentInChildren<Renderer>().material.shader = defaultShader;
                Material Mat = transparentObjs[i].GetComponentInChildren<Renderer>().material;

               
            }

            transparentObjs.Clear();


            yield return null;
        }


        void Update()

        {
            float Distance = Vector3.Distance(transform.position, Character.transform.position);

            Vector3 Direction = (Character.transform.position - transform.position).normalized;

            RaycastHit hit;

            //   int layerMask = (1 << LayerMask.NameToLayer("CameraEvent")) + (1 << LayerMask.NameToLayer("Wall"));
            // int layerMask = (1 << LayerMask.NameToLayer("CameraEvent")) | (1 << LayerMask.NameToLayer("Wall")); // Everything에서 Player 레이어만 제외하고 충돌 체크함
            //layerMask = ~layerMask;

            Debug.DrawRay(transform.position, Direction * Distance, Color.green, 0.1f);

            if (Physics.Raycast(transform.position, Direction, out hit, Distance , layerMask.value))
            {

                if (hit.transform.gameObject.CompareTag("BuildingArea"))
                {
                    return;
                }

               // Debug.DrawRay(transform.position, (hit.point - transform.position).normalized * Vector3.Distance(hit.point, transform.position), Color.red, 0.1f);
                // 플레이어가 레이에 맞으면 (가려지는 오브젝트가 없으면)
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    // 시야를 가린 오브젝트가 존재하고 있다면 되돌리기 
                    if (transparentObjs.Count != 0)
                    {
                        StartCoroutine(returnObjs());
                    }

                    return;
                }


                // 레이에 맞았다면 오브젝트 가져오기
                ObstacleRenderer = hit.transform.gameObject.GetComponentInChildren<Renderer>();


                // 이미 반투명 상태라면 리턴
                if (ObstacleRenderer.material.shader == targetShader) return;


                if (ObstacleRenderer != null)

                {
                    ObstacleRenderer.material.shader = targetShader;
                    transparentObjs.Add(hit.transform.gameObject);
                   //  Material  = ObstacleRenderer.material;

                    
                }


            }
        }
    }
    }

