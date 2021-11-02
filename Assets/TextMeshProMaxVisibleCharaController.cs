using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshPro))]
public class TextMeshProMaxVisibleCharaController : MonoBehaviour
{
	public int maxVisibleCharacters;
	[SerializeField]
	private TextMeshPro text;

	private void Update()
	{

		//text.maxVisibleCharacters = maxVisibleCharacters;
	}
}