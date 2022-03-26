using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdaTest : MonoBehaviour
{

    int result;

    delegate void TestDeleage<T>(T a , T b);//T는 형식 매개변수  T 타입을 갖는 변수 a와 b
    TestDeleage<int> testDeleage; //델리게이트를 변수화, 위의 형식매개변수 T의 타입을 여기서 결정해줍니다.

    private void Start()
    {

        testDeleage += (int a, int b) => print(a+b);

        testDeleage(5, 5);
    }


}
