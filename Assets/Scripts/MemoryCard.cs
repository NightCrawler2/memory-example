using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject _cardBack;
    [SerializeField] private SceneController _sceneController;

    public int Id { get; private set; }

    // Use this for initialization
    private void Start ()
    {
    }
	
	// Update is called once per frame
    private void Update () {
		
	}

    private void OnMouseDown()
    {
        if (!_cardBack.activeSelf || !_sceneController.CanReveal) return;
        _cardBack.SetActive(false);
        _sceneController.CardRevealed(this);
    }

    public void Unreveal()
    {
        _cardBack.SetActive(true);
    }

    public void SetCard(int id, Sprite image)
    {
        Id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
