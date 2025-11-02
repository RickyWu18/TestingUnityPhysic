using UnityEngine;

public class Logger : MonoBehaviour
{
    public bool logPosition = true;
    private System.IO.StreamWriter fileWriter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!logPosition) return;

        // get file writer for logging
        string name = transform.name;
        string folderPath = Application.dataPath + "/Logs/";
        string filePath = "Positions_" + name + "_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        fileWriter = new System.IO.StreamWriter(folderPath + filePath, true);

        // log the names of all child transforms to the files
        fileWriter.WriteLine("Time," + name + "/x," + name + "/y," + name + "/z");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!logPosition) return;

        // log the position of the transform to the file
        fileWriter.WriteLine(Time.time + "," + transform.position.x + "," + transform.position.y + "," + transform.position.z);
    }

    void OnDestroy()
    {
        // close the file writer when the script is destroyed
        if (fileWriter != null)
        {
            fileWriter.Close();
        }
    }
}
