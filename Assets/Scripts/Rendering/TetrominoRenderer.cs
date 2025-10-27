using UnityEngine;
using UnityEngine.UI;

public class TetrominoRenderer : MonoBehaviour
{
	[SerializeField]
	private RawImage image;

	[SerializeField]
	private Material material;

	private bool useBlink;

	void Start()
	{
		UpdateMaterial();
	}

	public void UpdateMaterial(bool condition = false)
	{
		useBlink = condition;
		image.material = Instantiate(material);
		image.SetMaterialDirty();
		image.material.SetFloat("_UseBlink", useBlink ? 1f : 0f);
	}
}
