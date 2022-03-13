using UnityEngine;
using UnityEditor;

public class SetTexture
{

	[MenuItem("Assets/MySpriteSet")]
	static void MySpriteSet()
	{

		Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

		foreach (Texture2D texture in _textures)
		{
			string path = AssetDatabase.GetAssetPath(texture);

			TextureImporterSettings textureImporterSettings = new TextureImporterSettings();


			TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

			textureImporter.textureType = TextureImporterType.Sprite;

			textureImporter.spriteImportMode = SpriteImportMode.Single;

			textureImporter.ReadTextureSettings(textureImporterSettings);
			textureImporterSettings.spriteAlignment = (int)SpriteAlignment.BottomCenter;
			textureImporter.SetTextureSettings(textureImporterSettings);

			textureImporter.generateMipsInLinearSpace = false;

			textureImporter.filterMode = FilterMode.Trilinear;

			AssetDatabase.ImportAsset(path);

		}
	}
}
