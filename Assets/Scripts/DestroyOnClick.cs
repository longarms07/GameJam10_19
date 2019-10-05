using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() //Works on touch device as well.
    {
        BreakIntoPieces pieces = this.gameObject.GetComponent<BreakIntoPieces>();
        if (pieces != null) pieces.SpawnParts();
        Destroy(this.gameObject);
    }
}
