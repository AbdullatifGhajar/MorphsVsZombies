using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panningSpeed = 30f;
    public float panningBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    void Update()
    {

        if (GameManager.GameIsOver)
        {
            enabled = false;
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panningBorderThickness)
            transform.Translate(Vector3.forward * panningSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("s") || Input.mousePosition.y <= panningBorderThickness)
            transform.Translate(Vector3.back * panningSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panningBorderThickness)
            transform.Translate(Vector3.right * panningSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("a") || Input.mousePosition.x <= panningBorderThickness)
            transform.Translate(Vector3.left * panningSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

    }
}
