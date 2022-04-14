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
        Material basematerial = new Material(Resources.Load<Material>("BaseMat"));// = new Material(Resources.Load<Material>("BaseMat"));
        //prefabUtility = Resources.Load<GameObject>("ItemBase");

        foreach (Texture2D texture in _textures)
        {
            //count++;

            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.textureType = TextureImporterType.Sprite;

            textureImporter.filterMode = FilterMode.Trilinear;
            AssetDatabase.ImportAsset(path);


            material = new Material(Resources.Load<Material>("BaseMat")); ;

            var modelRootGO = Resources.Load<GameObject>("ItemBase");//(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Resources/ItemBase.prefab");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path.Substring(0, path.Length-7)));
            Transform variantRootChild = variantRoot.transform.GetChild(0);

            //PrefabUtility.SaveAsPrefabAssetAndConnect(Resources.Load<GameObject>("ItemBase"), path, InteractionMode.UserAction);

            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", path.Substring(0, path.Length - 7)));
            material.SetTexture("_BaseMap", texture);

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f,1);
            variantRootChild.localScale = new Vector3(texture.width*0.01f, texture.height*0.01f, 1);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            //return;
        }
    }

    [MenuItem("Assets/CreateAssets_Tree")]
    static void CreateAssets_Tree()
    {
        //int count = 0;
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;// = new Material(Resources.Load<Material>("BaseMat"));
        Material basematerial = new Material(Resources.Load<Material>("BaseMat"));// = new Material(Resources.Load<Material>("BaseMat"));
        //prefabUtility = Resources.Load<GameObject>("ItemBase");

        foreach (Texture2D texture in _textures)
        {
            //count++;

            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.textureType = TextureImporterType.Sprite;

            textureImporter.filterMode = FilterMode.Trilinear;
            AssetDatabase.ImportAsset(path);


            material = new Material(Resources.Load<Material>("BaseMat")); ;

            var modelRootGO = Resources.Load<GameObject>("TreeTest");//(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Resources/ItemBase.prefab");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path.Substring(0, path.Length - 7)));
            Transform variantRootChild = variantRoot.transform.Find("GameObject/Quad");

            //PrefabUtility.SaveAsPrefabAssetAndConnect(Resources.Load<GameObject>("ItemBase"), path, InteractionMode.UserAction);

            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", path.Substring(0, path.Length - 7)));
            material.SetTexture("_BaseMap", texture);

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            //return;
        }
    }
}
