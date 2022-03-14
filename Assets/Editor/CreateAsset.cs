using UnityEditor;
using UnityEngine;

public class CreateAsset : MonoBehaviour
{
    [MenuItem("Assets/CreateAssets")]
    static void CreateAssets()
    {
        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        Material material = new Material(Resources.Load<Material>("Material/BaseMat"));

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            //Material material = new Material(Shader.Find("Universal Rander Pipeline"));

            //Material material = new Material(Shader.Find("Unlit"));
            //string name = "DimuMat";
            AssetDatabase.CreateAsset(material, string.Format("{0}.mat", path));

            material.SetTexture("Base Map", texture);
            
            Debug.Log(AssetDatabase.GetAssetPath(material));
            //return;
        }
    }

}
