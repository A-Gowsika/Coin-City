using UnityEngine;

public class RotationController : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed* Vector3.up * Time.deltaTime);
    }
}
