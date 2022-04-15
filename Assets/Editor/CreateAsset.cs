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
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path.Substring(0, path.Length - 7)));
            Transform variantRootChild = variantRoot.transform.GetChild(0);

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
    [MenuItem("Assets/CreateMaterials")]
    static void CreateMaterials()
    {

    }

    [MenuItem("Assets/CreateAssets_Tree")]
    static void CreateAssets_Tree()
    {
        //int count = 0;
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;// = new Material(Resources.Load<Material>("BaseMat"));
        Material material2;// = new Material(Resources.Load<Material>("BaseMat"));
        Material basematerial = new Material(Resources.Load<Material>("Shader GP/Test"));// = new Material(Resources.Load<Material>("BaseMat"));
        //prefabUtility = Resources.Load<GameObject>("ItemBase");

        foreach (Texture2D texture in _textures)
        {
            //count++;
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);//falltree_L_1 @ _v0.png 7자
            string upMatname = path.Substring(0, path.Length - 8) + "up_" + filename.Substring(filename.Length - 1, 1);
            Debug.Log(upMatname);
            Debug.Log(filename);

            // upMatname = upMatname + filename.Substring(path.Length-2, 1);//falltree_L_ @ 1_v0.png 11자  + up + (_1)  falltree_L_up_1_v0.png  path.Substring(path.Substring(0, path.Length - 9),)
            //Debug.Log(upMatname);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.textureType = TextureImporterType.Sprite;

            textureImporter.filterMode = FilterMode.Trilinear;
            AssetDatabase.ImportAsset(path);


            material = new Material(Resources.Load<Material>("Shader GP/Test"));
            Debug.Log(upMatname.Substring(17, upMatname.Length - 17));

            material2 = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", upMatname));//new Material(Resources.Load<Material>("MapObject/OutLine/Fall/tree/falltree_L_up_0"));//new Material(Resources.Load<Material>("MapObject/OutLine/Fall/tree/falltree_L_up_0"));

            var modelRootGO = Resources.Load<GameObject>("TreeTest");//(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Resources/ItemBase.prefab");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", filename));
            Transform variantRootChild = variantRoot.transform.Find("GameObject/Quad");
            Transform variantRootChild2 = variantRoot.transform.Find("GameObject/Up");

            //PrefabUtility.SaveAsPrefabAssetAndConnect(Resources.Load<GameObject>("ItemBase"), path, InteractionMode.UserAction);

            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", filename));
            material.SetTexture("_MainTex", texture);

            //AssetDatabase.CreateAsset(material2, string.Format("{0}.mat", upMatname));
            //material2.SetTexture("_BaseMap", Resources.Load<Texture2D>(upMatname));

            Debug.Log(material.name);
            Debug.Log(material2.name);

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRootChild2.GetComponent<MeshRenderer>().material = material2;//string.Format("{0}.mat", upMatname));

            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRootChild2.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            variantRootChild.position = new Vector3(variantRootChild.position.x, texture.height * 0.01f / 2, variantRootChild.position.z);
            variantRootChild2.position = new Vector3(variantRootChild2.position.x, texture.height * 0.01f / 2, variantRootChild2.position.z);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            Debug.Log(AssetDatabase.GetAssetPath(material2));
            //return;
        }
    }
}
