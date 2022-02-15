using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TreeModel : MonoBehaviour//공통된 나무 속성을 관리하는 클래스
{
    [Header("공유된 너무들의 속성")]
   
    protected Vector3 FruitPosition;//열매 생성위치

    [SerializeField]
    protected GameObject Fruit;//나무의 열매



}
