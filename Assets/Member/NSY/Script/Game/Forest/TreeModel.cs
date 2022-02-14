using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TreeModel : MonoBehaviour//공통된 나무 속성을 관리하는 클래스
{
    [Header("공유된 너무들의 속성")]
    [SerializeField]
    protected Texture TreeTexture;
    [SerializeField]
    protected Texture ShadowTreeTexture;
    [SerializeField]
    protected Material TreeMesh;
    [SerializeField]
    protected Material ShadowMesh;


   
  
}
