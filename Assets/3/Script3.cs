using LiteNetLib.Utils;
using UnityEngine;

public class Script3 : MonoBehaviour
{
    [SerializeField] private Transform transformDataToSend;
    [SerializeField] private Transform playerTransform;

    private NetDataWriter dataWriter = new NetDataWriter();
    private NetDataReader dataReader = new NetDataReader();

    private void Update()
    {
        // First, we serialize transformDataToSend in the data writer.
        dataWriter.Reset();
        SerializeTransform(transformDataToSend, dataWriter);

        byte[] data = dataWriter.CopyData();
        Debug.Log($"Serialized data. Total bytes: {data.Length}");

        // Now, we deserialize the data back, and use it to set the playerTransform. They should match.
        dataReader.SetSource(data);
        DeserializeTransform(playerTransform, dataReader);
    }

    private void SerializeTransform(Transform transform, NetDataWriter dataWriter)
    {
        // Use 16-bit and 8-bit integer data types to represent the position and rotation values.
        dataWriter.Put((short) transform.position.x);
        dataWriter.Put((short) transform.position.y);
        dataWriter.Put((short) transform.position.z);

        dataWriter.Put((byte) transform.eulerAngles.x);
        dataWriter.Put((byte) transform.eulerAngles.y);
        dataWriter.Put((byte) transform.eulerAngles.z);
    }

    private void DeserializeTransform(Transform transform, NetDataReader dataReader)
    {
        // Read the position and rotation values as 16-bit and 8-bit integers.
        var x = dataReader.GetShort();
        var y = dataReader.GetShort();
        var z = dataReader.GetShort();

        var rx = dataReader.GetByte();
        var ry = dataReader.GetByte();
        var rz = dataReader.GetByte();

        // Set the position and rotation of the transform.
        transform.position = new Vector3(x, y, z);
        transform.eulerAngles = new Vector3(rx, ry, rz);
    }
}