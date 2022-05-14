using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tree", menuName = "Tree")]
public class Tree : Item
{
    [SerializeField] Material treeMat;
    [SerializeField] Material treeUpMat;
    [SerializeField] Material treeDownMat;

    public Material TreeMat { get { return treeMat; } set { treeMat = value; } }
    public Material TreeUpMat { get { return treeUpMat; } set { treeUpMat = value; } }
    public Material TreeDownMat { get { return treeDownMat; } set { treeDownMat = value; } }

}
