using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44326/Home/Stream");
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                //var streamLength = (int)stream.Length;
                //1
                //using (FileStream file = new FileStream(@"c:\temp\Example1.txt", FileMode.Open, FileAccess.Write))
                //{
                //    var bytes = new byte[streamLength];

                //    stream.Read(bytes, 0, streamLength);
                //    file.Write(bytes, 0, streamLength);
                //}

                //2
                //using (var fs = new FileStream(@"c:\temp\Example1.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                //{
                //    while (stream.Position < stream.Length)
                //    {
                //        fs.WriteByte((byte)stream.ReadByte());
                //    }
                //}

                //3
                using (FileStream fs = new FileStream(@"c:\temp\Example1.txt", FileMode.CreateNew, FileAccess.Write))
                {
                    CopyStream(stream, fs);
                }

                stream.Close();
                stream.Flush();
            }
        }
       
        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        private static void CopyStream(Stream input, FileStream output)
        {
            var buffer = new byte[8 * 1024];
            int len;

            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
