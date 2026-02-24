using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private int _clickCount = 0;

    public Camera Camera;
    public Click clickScript;
    public Rotate rotateScript;
    private void Awake()
    {
        

        if(clickScript != null) clickScript.enabled = false;
        if(rotateScript != null) rotateScript.enabled = false;
    }

    private void Start()
    {
        _clickCount = 0;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cube1"))
                {
                    _clickCount++;
                    Debug.Log($"Clicked {_clickCount} Cube 1");
                }

                if (hit.collider.CompareTag("Cube2"))
                {
                    _clickCount++;
                    Debug.Log($"Clicked {_clickCount} Cube 2");
                }
            }
        }
        
        if(_clickCount == 1 && clickScript != null)
        {
            _clickCount = 0;
            clickScript.enabled = true;
            this.enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }
        else if(_clickCount == 2 && rotateScript != null)
        {
            rotateScript.enabled = true;
            this.enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }

        
    }
}
