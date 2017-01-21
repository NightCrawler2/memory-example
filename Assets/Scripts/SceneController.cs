using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int GridRows = 2;
    public const int GridCols = 4;
    public const float OffsetX = 2f;
    public const float OffsetY = 2.5f;

    [SerializeField] private MemoryCard _originalCard;
    [SerializeField] private Sprite[] _images;
    [SerializeField] private TextMesh _scoreLabel;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    public bool CanReveal
    {
        get { return _secondRevealed == null; }
    }

    // Use this for initialization
    private void Start ()
    {
        var startPos = _originalCard.transform.position;

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};

        numbers = ShuffleArray(numbers);

        for (var i = 0; i < GridCols; i++)
        {
            for (var j = 0; j < GridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = _originalCard;
                }
                else
                {
                    card = Instantiate(_originalCard);
                }

                var index = j * GridCols + i;
                var id = numbers[index];
                card.SetCard(id, _images[id]);

                var posX = OffsetX * i + startPos.x;
                var posY = -(OffsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
	
	// Update is called once per frame
    private void Update () {
		
	}

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private static int[] ShuffleArray(int[] numbers)
    {
        var newArray = numbers.Clone() as int[];
        Debug.Assert(newArray != null, "newArray != null");
        for (var i = 0; i < newArray.Length; i++)
        {
            var tmp = newArray[i];
            var r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _score++;
            _scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene");
    }
}
