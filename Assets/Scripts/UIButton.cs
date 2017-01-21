using UnityEngine;

// ReSharper disable once InconsistentNaming
public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private string _targetMessage;
    public Color HighlightColor = Color.cyan;

	// Use this for initialization
    private void Start () {

	}
	
	// Update is called once per frame
    private void Update () {
		
	}

    private void OnMouseOver()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = HighlightColor;
        }
    }

    private void OnMouseExit()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (_targetObject != null)
        {
            _targetObject.SendMessage(_targetMessage);
        }
    }
}
