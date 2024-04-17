using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatRoom.Server;

public class ServerSocket
{
    private Server _server;

    public ServerSocket(Server server)
    {
        _server = server;
    }

    public void StartServer()
    {
        _server.HostEntry = Dns.GetHostEntry(Dns.GetHostName());

        _server.ServerIpAddress = _server.HostEntry.AddressList[0];

        _server.EndPoint = new IPEndPoint(_server.ServerIpAddress, 11000);
        
        // TODO: Fix connection interrupt
        // Unhandled exception. System.Net.Sockets.SocketException (10061): No connection could be made because the target machine actively refused it.

        _server.ServerSocket = new Socket(_server.ServerIpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        
        _server.ServerSocket.Connect(_server.EndPoint);

        if (_server.ServerSocket.Connected)
        {
            Console.WriteLine("Server socket connected to " + _server.ServerSocket.RemoteEndPoint);
        }
        else
        {
            StartServer();
        }
    }

    public void StopServer()
    {
        _server.ServerSocket.Shutdown(SocketShutdown.Both);
        _server.ServerSocket.Close();
    }

    public void BroadcastMessage(string message)
    {
        byte[] encodedMessage = Encoding.ASCII.GetBytes(message);

        _server.ServerSocket.Send(encodedMessage);
    }

    public string ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        
        int bytesReceived = _server.ServerSocket.Receive(buffer);

        return Encoding.ASCII.GetString(buffer, 0, bytesReceived);
    }
}