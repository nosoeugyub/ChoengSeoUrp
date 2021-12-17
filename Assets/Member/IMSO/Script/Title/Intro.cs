using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro : MonoBehaviour
{
    public Image StartImage; //기존에 존재하는 이미지
    public Sprite NextImage1; //바뀌어질 이미지

    public void ChangeImage()
    {
        StartImage.sprite = NextImage1;
    }
}
