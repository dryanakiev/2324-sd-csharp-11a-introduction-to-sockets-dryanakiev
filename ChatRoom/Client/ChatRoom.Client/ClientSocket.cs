using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatRoom.Client;

public class ClientSocket
{
    private Client _client;

    public ClientSocket(Client client)
    {
        _client = client;
    }
    
    public void StartConnection()
    {
        try
        {
            IPHostEntry hosts = Dns.GetHostEntry(Dns.GetHostName());
            
            _client.ClientIpAddress = hosts.AddressList[0];
            
            IPEndPoint localEndPoint = new IPEndPoint(_client.ClientIpAddress, _client.Port);

            _client.ClientSocket = new Socket(_client.ClientIpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _client.ClientSocket.Connect(localEndPoint);

            if (_client.ClientSocket.Connected)
            {
                Console.WriteLine("Socket connected to " + _client.ClientSocket.RemoteEndPoint);
            }
            else
            {
                StartConnection();
            }
        }
        catch (Exception e)
        {
            // TODO: Add exception handle
        }
    }
    
    public void StopConnection()
    {
        _client.ClientSocket.Close();
    }

    public string SendMessage(string message)
    {
        byte[] byteMessage = Encoding.ASCII.GetBytes(message);
        _client.ClientSocket.Send(byteMessage);

        return ReceiveMessage();
    }

    public string ReceiveMessage()
    {
        byte[] messageBuffer = new byte[1024];

        int byteCount = _client.ClientSocket.Receive(messageBuffer);

        return Encoding.ASCII.GetString(messageBuffer, 0, byteCount);
    }
}