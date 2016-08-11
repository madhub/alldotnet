using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultipartFileUpload
{
    public class FileUploadResult
    {
        public string LocalFilePath { get; set; }
        public string FileName { get; set; }
        public long FileLength { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {



        }

        public static void UploadFiles(String directoryName)
        {
            HttpClient httpClient = new HttpClient();
            // Read the files 
            foreach (String file in new[] {"","" })
            {
                var fileStream = File.Open(file, FileMode.Open);
                var fileInfo = new FileInfo(file);
                FileUploadResult uploadResult = null;
                bool _fileUploaded = false;

                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(fileStream), "\"file\"", string.Format("\"{0}\"", fileInfo.Name)
                );

                Task taskUpload = httpClient.PostAsync("", content).ContinueWith(task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                    {
                        var response = task.Result;

                        if (response.IsSuccessStatusCode)
                        {
                            uploadResult = response.Content.ReadAsAsync<FileUploadResult>().Result;
                            if (uploadResult != null)
                                _fileUploaded = true;

                            // Read other header values if you want..
                            foreach (var header in response.Content.Headers)
                            {
                                Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Status Code: {0} - {1}", response.StatusCode, response.ReasonPhrase);
                            Debug.WriteLine("Response Body: {0}", response.Content.ReadAsStringAsync().Result);
                        }
                    }

                    fileStream.Dispose();
                });

                taskUpload.Wait();
                if (_fileUploaded)
                    Debug.Write(uploadResult.FileName + " with length " + uploadResult.FileLength
                                    + " has been uploaded at " + uploadResult.LocalFilePath);
            }

            httpClient.Dispose();
        }
                
    }

    
}
