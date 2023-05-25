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
            while (true)
            {
                if (!ns.DataAvailable) continue;
                byte[] response = GetBytesFromNS(ns);
                string response_str = Encoding.ASCII.GetString(response);
                foreach(byte b in response)
                {
                    Console.WriteLine(b);
                }
                Console.WriteLine(response_str);
            }
        }

        static void WriteBytesToNS(NetworkStream stream, byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                stream.WriteByte(b);
            }
            stream.WriteByte(126);
        }
        static byte[] GetBytesFromNS(NetworkStream stream)
        {
            List<byte> byte_list = new List<byte>();
            while (true)
            {
                int next = stream.ReadByte();
                if (next == -1 || next == 126)
                {
                    break;
                }
                byte_list.Add((byte)next);
            }
            return byte_list.ToArray();
        }
    }
}
