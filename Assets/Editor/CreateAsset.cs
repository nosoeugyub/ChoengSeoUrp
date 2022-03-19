using UnityEditor;
using UnityEngine;

public class CreateAsset : MonoBehaviour
{
    [MenuItem("Assets/CreateAssets")]
    static void CreateAssets()
    {
        //int count = 0;
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;// = new Material(Resources.Load<Material>("BaseMat"));
        //prefabUtility = Resources.Load<GameObject>("ItemBase");

        foreach (Texture2D texture in _textures)
        {
            //count++;
            string path = AssetDatabase.GetAssetPath(texture);

            material = new Material(Resources.Load<Material>("BaseMat"));

            var modelRootGO = Resources.Load<GameObject>("ItemBase");//(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Resources/ItemBase.prefab");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path));
            Transform variantRootChild = variantRoot.transform.GetChild(0);

            //PrefabUtility.SaveAsPrefabAssetAndConnect(Resources.Load<GameObject>("ItemBase"), path, InteractionMode.UserAction);

            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", path));
            material.SetTexture("_BaseMap", texture);

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRootChild.localScale = new Vector2(texture.width*0.01f, texture.height*0.01f);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            //return;
        }
    }

}
