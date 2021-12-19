using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cam
{
    public class CamTransperant : MonoBehaviour
    {
        SpriteRenderer ObstacleRenderer;

        public GameObject Character;

      //  public Shader defaultShader;
      //  public Shader targetShader;

        public Material defaultMat;
        public Material TargetMat;

        // 가리는 오브젝트 리스트
        public List<GameObject> transparentObjs = new List<GameObject>();

        public void Awake()
        {
          //  targetShader = Shader.Find("Custom/Transperant");
        }


        IEnumerator returnObjs()
        {
            for (int i = 0; i < transparentObjs.Count; i++)
            {
                transparentObjs[i].GetComponentInChildren<SpriteRenderer>().material = defaultMat;
                Material Mat = transparentObjs[i].GetComponentInChildren<SpriteRenderer>().material;

               Color matColor= ObstacleRenderer.color;
                matColor.a = 1f;
                ObstacleRenderer.color = matColor;
            }

            transparentObjs.Clear();


            yield return null;
        }


        void Update()

        {
            float Distance = Vector3.Distance(transform.position, Character.transform.position);

            Vector3 Direction = (Character.transform.position - transform.position).normalized;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Direction, out hit, Distance))

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
                ObstacleRenderer = hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>();


                // 이미 반투명 상태라면 리턴
                if (ObstacleRenderer.material == TargetMat) return;


                if (ObstacleRenderer != null)

                {
                    ObstacleRenderer.material = TargetMat;
                    transparentObjs.Add(hit.transform.gameObject);
                    Material Mat = ObstacleRenderer.material;



                    Color matColor = Mat.color;
                       matColor.a = 0.5f;
                      Mat.color = matColor;
                }

            }
        }
    }
}

