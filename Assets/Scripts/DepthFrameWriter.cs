using System.IO;
using UnityEngine;

internal class DepthFrameWriter
{
    private int image_count;
    private string current_phrase;
    private int old_session_number;
    public void setCurrentPhrase(string p)
    {
        current_phrase = p;
    }

    public DepthFrameWriter()
    {
        image_count = 1;
        old_session_number = 0;
    }
    /*
    public async void ProcessWrite(BitmapFrame b)
    {
        string filename = "temp_" + image_count + ".png";
        image_count++;
        string filePath = @"C:\\Users\\ASLR\\Documents\\z-aslr-data\\"+filename;

        await WriteTextAsync(filePath, b);
    }

    private async Task WriteTextAsync(string filePath, BitmapFrame b)
    {
        //byte[] encodedText = Encoding.Unicode.GetBytes(text);

        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Append, FileAccess.Write, FileShare.None,
            bufferSize: 12000, useAsync: true))
        {
            //await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            BitmapEncoder encoder = new PngBitmapEncoder();
            //_rw.EnterReadLock();
            //encoder.Frames.Add(imageQueue.Dequeue());
            encoder.Frames.Add(b);
            //Thread.Sleep(100);
            //_rw.ExitReadLock();

            encoder.Save(sourceStream);
            await sourceStream.FlushAsync();
            sourceStream.Close();
        };
    }
    */

    public void ProcessWrite(byte[] b, int session_number, string dataWritePath)
    {
        if (session_number != old_session_number)
        {
            old_session_number = session_number;
            image_count = 1;
        }
        //Texture2D tex = new Texture2D(512, 424, TextureFormat.Alpha8, false);
        Texture2D tex = new Texture2D(512, 424, TextureFormat.RGBA32, false);
        tex.LoadRawTextureData(b);
        b = tex.EncodeToJPG(40);

        string filename = current_phrase + "_depth_" + image_count + ".jpg";
        image_count++;
        string filePath = dataWritePath + session_number + "\\depth\\" + filename;

        File.WriteAllBytes(filePath, b);
    }

    private void WriteText(string filePath, byte[] b)
    {
        //byte[] encodedText = Encoding.Unicode.GetBytes(text);

        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Append, FileAccess.Write, FileShare.None,
            bufferSize: 25000, useAsync: true))
        {
            sourceStream.Write(b, 0, b.Length);
            sourceStream.Close();

        };
    }
}