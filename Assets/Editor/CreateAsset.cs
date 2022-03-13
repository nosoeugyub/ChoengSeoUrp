using UnityEngine;
using UnityEditor;

public class CreateAsset : MonoBehaviour
{
    [MenuItem("Assets/CreateAssets")]
    static void CreateAssets()
    {
        Object[] _textures = Selection.GetFiltered(typeof(PrefabUtility), SelectionMode.DeepAssets);


        Material material = new Material(Shader.Find("Specular"));
        AssetDatabase.CreateAsset(material, "Assets/MyMaterial.mat");

        Debug.Log(AssetDatabase.GetAssetPath(material));
    }

}
