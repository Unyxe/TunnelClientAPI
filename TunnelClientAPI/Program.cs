using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TunnelClientAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 8091);
            NetworkStream ns = client.GetStream();
            WriteBytesToNS(ns, Encoding.ASCII.GetBytes(@"<chatapp>\msg\?query-SGVsbG8gd29ybGQK&auth-fb1f92fce9539c1a723adabc3e9eb875"));
            byte[] response = GetBytesFromNS(ns);
            Console.WriteLine(Encoding.ASCII.GetString(response));
            Console.ReadLine();
        }

        static void WriteBytesToNS(NetworkStream stream, byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                stream.WriteByte(b);
            }
        }
        static byte[] GetBytesFromNS(NetworkStream stream)
        {
            List<byte> byte_list = new List<byte>();
            while (!stream.DataAvailable) { }
            while (stream.DataAvailable)
            {
                int next = stream.ReadByte();
                if (next == -1)
                {
                    break;
                }
                byte_list.Add((byte)next);
                Thread.Sleep(1);
            }
            return byte_list.ToArray();
        }
    }
}
