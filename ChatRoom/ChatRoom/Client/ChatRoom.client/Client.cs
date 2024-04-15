using System.Net;
using System.Net.Sockets;

namespace ChatRoom.Client;

public class Client
{
    public string Name { get; set; } = null;
    public IPAddress ClientIpAddress { get; set; }
    public int Port { get; set; }
    public Socket ClientSocket { get; set; }
}