using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tree", menuName = "Tree")]
public class Tree : ScriptableObject
{
    [SerializeField] Material treeMat;
    [SerializeField] Material treeUpMat;
    [SerializeField] Material treeDownMat;

    public Material TreeMat { get; set; }
    public Material TreeUpMat { get; set; }
    public Material TreeDownMat { get; set; }

}
