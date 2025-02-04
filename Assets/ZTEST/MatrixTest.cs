using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MatrixTest : MonoBehaviour
{
    private float rotationSpeed = 45f;
    private Vector3 scaleFactor = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 translation = new Vector3(1f, 0f, 0f);

    private Matrix4x4 originalMatrix;

    public float slowFactor = 0.2f;
}
public partial class MatrixTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        originalMatrix = Matrix4x4.identity;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime * slowFactor;

        // rorate
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));

        // scale
        Matrix4x4 scaleMatrixl = Matrix4x4.Scale(Vector3.Lerp(Vector3.one, scaleFactor, slowFactor * Time.deltaTime));

        // movement
        Matrix4x4 translationMatrix = Matrix4x4.Translate(translation * Time.deltaTime * slowFactor);

        originalMatrix = translationMatrix * rotationMatrix * scaleMatrixl;

        ApplyMatrixTransform(originalMatrix);
    }
    public void ApplyMatrixTransform(Matrix4x4 matrix)
    {
        Vector3 position = matrix.GetColumn(3);
        Quaternion rotation = Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
        Vector3 scale = new Vector3(matrix.GetColumn(0).magnitude, matrix.GetColumn(1).magnitude, matrix.GetColumn(2).magnitude);

        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
    }
}
