using System.Net;
using System.Net.Sockets;

namespace ChatRoom.Server;

public class Server
{
    public string Name { get; set; }

    public IPHostEntry HostEntry { get; set; }

    public IPAddress ServerIpAddress { get; set; }

    public IPEndPoint EndPoint { get; set; }

    public Socket ServerSocket { get; set; }
}