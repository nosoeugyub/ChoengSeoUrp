using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Cam
{
    public class CamTransperant : MonoBehaviour
    {
        Renderer ObstacleRenderer;

        public GameObject Character;

        public Material defaultShader;
        public Material targetShader;


        // 가리는 오브젝트 리스트
        public List<GameObject> transparentObjs = new List<GameObject>();

        public void Awake()
        {
           // targetShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        }


        IEnumerator returnObjs()
        {
            for (int i = 0; i < transparentObjs.Count; i++)
            {
                transparentObjs[i].GetComponentInChildren<Renderer>().material = defaultShader;
                Material Mat = transparentObjs[i].GetComponentInChildren<Renderer>().material;

                Color matColor = Mat.color;
                matColor.a = 1f;
                Mat.color = matColor;
            }

            transparentObjs.Clear();


            yield return null;
        }


        void Update()

        {
            float Distance = Vector3.Distance(transform.position, Character.transform.position);

            Vector3 Direction = (Character.transform.position - transform.position).normalized;

            RaycastHit hit;
            int layerMask = ((1 << LayerMask.NameToLayer("CameraEvent"))); //| (1 << LayerMask.NameToLayer("GUN")));  // Everything에서 Player,GUN 레이어만 제외하고 충돌 체크함
            layerMask = ~layerMask;
            if (Physics.Raycast(transform.position, Direction, out hit, layerMask))

            {
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
                    ObstacleRenderer.material = targetShader;
                    transparentObjs.Add(hit.transform.gameObject);
                    Material Mat = ObstacleRenderer.material;

                    Color matColor = Mat.color;
                    matColor.a = 0.3f;
                    Mat.color = matColor;
                }

            }
        }
    }
    }

