using UnityEngine;

// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

public class Bounce : MonoBehaviour
{
    [SerializeField] float jumpForce = 8f;
    public Material[] padColorMaterials;

    private int _currentColor = 0;
        
    private void OnTriggerEnter(Collider other)
    {
        ChangeMaterial();
        JumpyJumpy(other);
    }

    private void ChangeMaterial()
    {
        Debug.Log($"i: {_currentColor}");
        GetComponent<Renderer>().material = padColorMaterials[_currentColor];

        _currentColor++;
        
        if (_currentColor == padColorMaterials.Length)
            Destroy(gameObject);
    }

    private void JumpyJumpy(Component other)
    {
        var rigidBody = other.GetComponent<Rigidbody>();
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}