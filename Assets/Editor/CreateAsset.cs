using DM.Building;
using TT.BuildSystem;
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

        foreach (Texture2D texture in _textures)
        {
            //count++;

            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.textureType = TextureImporterType.Sprite;

            textureImporter.filterMode = FilterMode.Trilinear;
            AssetDatabase.ImportAsset(path);

            material = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", path.Substring(0, path.Length - 7)));

            var modelRootGO = Resources.Load<GameObject>("ItemBase");//(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Resources/ItemBase.prefab");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path.Substring(0, path.Length - 7)));
            Transform variantRootChild = variantRoot.transform.GetChild(0);

            //PrefabUtility.SaveAsPrefabAssetAndConnect(Resources.Load<GameObject>("ItemBase"), path, InteractionMode.UserAction);

            //AssetDatabase.CreateAsset(material, string.Format("{0}.mat", path.Substring(0, path.Length - 7)));
            //material.SetTexture("_BaseMap", texture);

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            //return;
        }
    }
    [MenuItem("Assets/CreateMaterials_Wind")]
    static void CreateMaterials()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.filterMode = FilterMode.Trilinear;

            material = new Material(Resources.Load<Material>("Shader GP/wind"));

            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", filename));
            material.SetTexture("_MainTex", texture);
        }
    }
    [MenuItem("Assets/CreateMaterials_NoWind")]
    static void CreateMaterials_NoWind()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.filterMode = FilterMode.Trilinear;

            material = new Material(Resources.Load<Material>("Shader GP/wind_No"));

            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", filename));
            material.SetTexture("_MainTex", texture);
        }
    }
    [MenuItem("Assets/CreateAssets_Mine")]
    static void CreateAssets_Mine()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;// = new Material(Resources.Load<Material>("BaseMat"));
        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);

            var modelRootGO = Resources.Load<GameObject>("MineBase");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", filename));
            Transform variantRootChild = variantRoot.transform.Find("GameObject/Quad");

            material = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", filename));

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRoot.GetComponent<BoxCollider>().center = new Vector3(0, texture.height * 0.01f / 2 + 0.001f, 0);
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            variantRootChild.position = new Vector3(variantRoot.transform.position.x, texture.height * 0.01f / 2 + 0.001f, variantRoot.transform.position.z);
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
        }
    }
    [MenuItem("Assets/CreateAssets_Tree")]
    static void CreateAssets_Tree()
    {
        //int count = 0;
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;// = new Material(Resources.Load<Material>("BaseMat"));
        Material material2;// = new Material(Resources.Load<Material>("BaseMat"));
        Material material3;// = new Material(Resources.Load<Material>("BaseMat"));
        //Material basematerial = new Material(Resources.Load<Material>("Shader GP/Test"));// = new Material(Resources.Load<Material>("BaseMat"));
        //prefabUtility = Resources.Load<GameObject>("ItemBase");

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);//falltree_L_1 @ _v0.png 7자
            string upMatname = path.Substring(0, path.Length - 8) + "up_" + filename.Substring(filename.Length - 1, 1);
            string downMatname = path.Substring(0, path.Length - 8) + "down_" + filename.Substring(filename.Length - 1, 1);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.textureType = TextureImporterType.Sprite;

            textureImporter.filterMode = FilterMode.Trilinear;
            AssetDatabase.ImportAsset(path);


            //material = new Material(Resources.Load<Material>("Shader GP/wind"));
            material = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", filename));
            material2 = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", upMatname));//new Material(Resources.Load<Material>("MapObject/OutLine/Fall/tree/falltree_L_up_0"));//new Material(Resources.Load<Material>("MapObject/OutLine/Fall/tree/falltree_L_up_0"));
            material3 = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", downMatname));//new Material(Resources.Load<Material>("MapObject/OutLine/Fall/tree/falltree_L_up_0"));//new Material(Resources.Load<Material>("MapObject/OutLine/Fall/tree/falltree_L_up_0"));

            var modelRootGO = Resources.Load<GameObject>("TreeBase");//(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Resources/ItemBase.prefab");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);
            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", filename));
            Transform variantRootChild = variantRoot.transform.Find("GameObject/Quad");
            Transform variantRootChild2 = variantRoot.transform.Find("GameObject/Up");

            //AssetDatabase.CreateAsset(material, string.Format("{0}.mat", filename));
            //material.SetTexture("_MainTex", texture);

            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRootChild2.GetComponent<MeshRenderer>().material = material2;//string.Format("{0}.mat", upMatname));

            variantRoot.GetComponent<TreeObject>().SetDownMat(material3);
            variantRoot.GetComponent<BoxCollider>().center = new Vector3(0, texture.height * 0.01f / 2 + 0.001f, 0);
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRootChild2.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            variantRootChild.position = new Vector3(variantRoot.transform.position.x, texture.height * 0.01f / 2 + 0.001f, variantRoot.transform.position.z);
            variantRootChild2.position = new Vector3(variantRoot.transform.position.x, texture.height * 0.01f / 2, variantRoot.transform.position.z);

            //return;
        }
    }
    [MenuItem("Assets/CreateAssets_MagnifyObj")]
    static void CreateAssets_MagnifyObj()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;
        Item so;

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            AssetDatabase.ImportAsset(path);

            material = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", path.Substring(0, path.Length - 7)));

            var modelRootGO = Resources.Load<GameObject>("MagnifyObjBase");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);

            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path.Substring(0, path.Length - 7)));
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRoot.GetComponent<BoxCollider>().center = new Vector3(0, texture.height * 0.01f / 2 + 0.001f, 0);

            so = AssetDatabase.LoadAssetAtPath(string.Format(string.Format("{0}.asset", path.Substring(0, path.Length - 7))), typeof(Item)) as Item;
            variantRoot.GetComponent<MagnifyObject>().SetItem(so);

            Transform variantRootChild = variantRoot.transform.GetChild(0);
            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRootChild.position = new Vector3(variantRoot.transform.position.x, texture.height * 0.01f / 2 + 0.001f, variantRoot.transform.position.z);
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            AssetDatabase.Refresh();

        }
    }
    [MenuItem("Assets/CreateAssets_BuildObj")]
    static void CreateAssets_BuildObj()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material;
        Item so;

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            AssetDatabase.ImportAsset(path);

            material = AssetDatabase.LoadAssetAtPath<Material>(string.Format("{0}.mat", path.Substring(0, path.Length - 7)));

            var modelRootGO = Resources.Load<GameObject>("BuildItemObjBase");
            var instanceRoot = PrefabUtility.InstantiatePrefab(modelRootGO);

            GameObject variantRoot = PrefabUtility.SaveAsPrefabAsset((GameObject)instanceRoot, string.Format("{0}.prefab", path.Substring(0, path.Length - 7)));
            variantRoot.GetComponent<BoxCollider>().size = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);
            variantRoot.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);

            so = AssetDatabase.LoadAssetAtPath(string.Format(string.Format("{0}.asset", path.Substring(0, path.Length - 7))), typeof(Item)) as Item;
            variantRoot.GetComponent<BuildingItemObj>().SetItem(so);

            Transform variantRootChild = variantRoot.transform.GetChild(0);
            variantRootChild.GetComponent<MeshRenderer>().material = material;
            variantRootChild.localScale = new Vector3(texture.width * 0.01f, texture.height * 0.01f, 1);

            Debug.Log(AssetDatabase.GetAssetPath(material));
            AssetDatabase.Refresh();

        }
    }
    [MenuItem("Assets/CreateAssets_BuildObj_ScriptableObj")]
    static void CreateAssets_BuildObj_ScriptableObj()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Item so;

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);

            so = ScriptableObject.CreateInstance<Item>();
            Sprite tests = AssetDatabase.LoadAssetAtPath(string.Format(path), typeof(Sprite)) as Sprite;
            so.ItemSprite =tests;

            Material testm = AssetDatabase.LoadAssetAtPath(string.Format(string.Format("{0}.mat", path.Substring(0, path.Length - 7))), typeof(Material)) as Material;
            so.ItemMaterial= testm;

            GameObject testg = AssetDatabase.LoadAssetAtPath(string.Format(string.Format("{0}.prefab", path.Substring(0, path.Length - 7))), typeof(GameObject)) as GameObject;
            so.ItemPrefab = testg;

            so.MaximumStacks = 5;
            so.InItemType = InItemType.BuildNormal;
            so.recipe = new RecipeIteminfo[3];

            AssetDatabase.CreateAsset(so, string.Format("{0}.asset", filename));

            AssetDatabase.Refresh();
            //material.SetTexture("_MainTex", texture);
        }
    }

    [MenuItem("Assets/CreateAssets_BuildObj_ScriptableObj")]
    static void CreateAssets_TreeMats()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Tree so;
        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            string filename = path.Substring(0, path.Length - 7);

            so = ScriptableObject.CreateInstance<Tree>();
            Sprite tests = AssetDatabase.LoadAssetAtPath(string.Format(path), typeof(Sprite)) as Sprite;
            //so.TreeMat = tests;

            AssetDatabase.CreateAsset(so, string.Format("{0}.asset", filename));
        }
    }
    //[MenuItem("Assets/FixAssets_BuildObj_ScriptableObj")]
    //static void FixAssets_BuildObj_ScriptableObj()
    //{
    //    Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    //    Item so;

    //    foreach (Texture2D texture in _textures)
    //    {
    //        string path = AssetDatabase.GetAssetPath(texture);
    //        string filename = path.Substring(0, path.Length - 7);

    //        //so = ScriptableObject.CreateInstance<Item>();
    //        so = AssetDatabase.LoadAssetAtPath(string.Format(path), typeof(Item)) as Item;
    //        Debug.Log(so);
    //        //so.ItemSprite = tests;

    //        //Material testm = AssetDatabase.LoadAssetAtPath(string.Format(string.Format("{0}.mat", path.Substring(0, path.Length - 7))), typeof(Material)) as Material;
    //        //so.ItemMaterial = testm;

    //        //GameObject testg = AssetDatabase.LoadAssetAtPath(string.Format(string.Format("{0}.prefab", path.Substring(0, path.Length - 7))), typeof(GameObject)) as GameObject;
    //        //so.ItemPrefab = testg;

    //        so.MaximumStacks = 5;
    //        //so.InItemType = InItemType.BuildNormal;
    //        so.recipe = new RecipeIteminfo[3];

    //        AssetDatabase.(so, string.Format("{0}.asset", filename));

    //        AssetDatabase.Refresh();
    //        //material.SetTexture("_MainTex", texture);
    //    }
    //}
}
