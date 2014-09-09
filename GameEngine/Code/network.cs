using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace GameEngine
{
    // - - - - - - - - - - - - - //
    //		 TCP Synchronous     //
    // - - - - - - - - - - - - - //
    class NetworkTCP
    {
        List<Socket> socket = new List<Socket>();
        Socket listener;
        byte[] buffer;
        string tempStr;
        string convertedStr;
        List<string> messages = new List<string>();
        int bufferSize = 4096;

        public NetworkTCP()
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Blocking = false;
            buffer = new byte[bufferSize];
        }

        //sets the port that is listening for connections
        public void host(ushort port)
        {
            listener.Bind(new IPEndPoint(0, port));
        }

        //connects to a computer, returns true if a connection is established
        public bool connect(string ipAddress, ushort port)
        {
            try
            {
                socket.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                socket[socket.Count - 1].ReceiveBufferSize = bufferSize;
                socket[socket.Count - 1].SendBufferSize = bufferSize;
                socket[socket.Count - 1].Connect(ipAddress, port);
                socket[socket.Count - 1].Blocking = false;
                return true;
            }
            catch
            {
                socket.RemoveAt(socket.Count - 1);
                return false;
            }
        }

        //gets any incoming connections, and increments the listening port
        public bool listenForConnections()
        {
            try
            {
                listener.Listen(100);
                socket.Add(listener.Accept());
                socket[socket.Count - 1].Blocking = false;
                socket[socket.Count - 1].ReceiveBufferSize = bufferSize;
                socket[socket.Count - 1].SendBufferSize = bufferSize;
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        //disconnects from all computers
        public void disconnect()
        {
            for (int connected = 0; connected < socket.Count; connected++)
            {
                socket[connected].Close();
            }
            listener.Close();
        }
        
        //sends a message over a TCP stream to all connected users
        public void sendMessage(string message)
        {
            for (int connected = 0; connected < socket.Count; connected++)
            {
                socket[connected].Send(Encoding.UTF8.GetBytes(message + '|'));
            }
        }

        //sends a message over a TCP stream to one connected user
        public void sendMessage(string message, int index)
        {
            socket[index].Send(Encoding.UTF8.GetBytes(message + '|'));
        }

        //receives messages over a TCP stream from all connected users
        public void receiveMessages()
        {
            for (int connected = 0; connected < socket.Count; connected++)
            {
                while (true)
                {
                    if (socket[connected].Available == 0)
                    {
                        break;
                    }
                    Array.Clear(buffer, 0, buffer.Length);
                    socket[connected].Receive(buffer);
                    tempStr = string.Empty;
                    convertedStr = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Trim('\0');
                    for (int i = 0; i < convertedStr.Length; i++)
                    {
                        if (convertedStr[i] != '|')
                        {
                            tempStr += convertedStr[i];
                        }
                        if (convertedStr[i] == '|')
                        {
                            messages.Add(tempStr);
                            tempStr = string.Empty;
                        }
                    }
                }
            }
        }

        //receives messages over a TCP stream from one connected user
        public void receiveMessages(int index)
        {
            while (true)
            {
                if (socket[index].Available == 0)
                {
                    break;
                }
                Array.Clear(buffer, 0, buffer.Length);
                socket[index].Receive(buffer);
                tempStr = string.Empty;
                convertedStr = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Trim('\0');
                for (int i = 0; i < convertedStr.Length; i++)
                {
                    if (convertedStr[i] != '|')
                    {
                        tempStr += convertedStr[i];
                    }
                    if (convertedStr[i] == '|')
                    {
                        messages.Add(tempStr);
                        tempStr = string.Empty;
                    }
                }
            }
        }
        
        //sets a socket to blocking
        public void setBlocking(bool block, int connection)
        {
            socket[connection].Blocking = block;
        }

        //sets the listen socket to blocking
        public void setListenBlocking(bool block)
        {
            listener.Blocking = block;
        }

        //gets a message in que
        public string getMessage(int index)
        {
            if (socket.Count > 0)
            {
                return messages[index];
            }
            return "getMessage() error";
        }

        //gets the number of messages
        public int getNumberOfMessages()
        {
            return messages.Count;
        }
        
        //returns the number of computers connected to this one
        public int getNumberConnected()
        {
            return socket.Count;
        }

        //clears the message buffer
        public void clearMessages()
        {
            messages.Clear();
        }
    }




    // - - - - - - - - - - - - - //
    //		 UDP Synchronous     //
    // - - - - - - - - - - - - - //
    class NetworkUDP
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        byte[] buffer;
        int bufferSize = 4096;
        List<KeyValuePair<int, string>> messages = new List<KeyValuePair<int, string>>();
        int idNumber;
        string tempStr;
        string convertedStr;
        int tempIdentifier;

        public NetworkUDP()
        {
            Random random = new Random();
            idNumber = random.Next(10000, 99999);
            socket.Blocking = false;
            buffer = new byte[bufferSize];
            socket.EnableBroadcast = true;
        }

        //sends a message to a specific IP address on a specific port
        public void sendMessage(string address, ushort port, string message)
        {
            try
            {
                socket.SendTo(Encoding.UTF8.GetBytes(idNumber.ToString() + message), new IPEndPoint(IPAddress.Parse(address), port));
            }
            catch{}
        }

        //broadcasts message on a specific port over a local connection
        public void broadcastMessage(ushort port, string message)
        {
            try
            {
                socket.SendTo(Encoding.UTF8.GetBytes(idNumber.ToString() + message), new IPEndPoint(IPAddress.Broadcast, port));
            }
            catch{}
        }

        //receives a message from anywhere any port
        public void receiveMessages()
        {
            try
            {
                while (true)
                {
                    if (socket.Available == 0)
                    {
                        break;
                    }
                    tempStr = string.Empty;
                    Array.Clear(buffer, 0, buffer.Length);
                    socket.Receive(buffer);

                    convertedStr = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Trim('\0');
                    for (int i = 0; i < 5; i++)
                    {
                        tempStr += convertedStr[i];
                    }
                    tempIdentifier = Convert.ToInt32(tempStr);
                    tempStr = string.Empty;
                    for(int i = 5; i < convertedStr.Length; i++)
                    {
                        tempStr += convertedStr[i];
                    }
                    messages.Add(new KeyValuePair<int, string>(tempIdentifier, tempStr));
                }
            }
            catch{}
        }

        //returns a message from the message que
        public string getMessage(int index)
        {
            return messages[index].Value;
        }

        //returns the identification number for a message from the message que
        public int getIDNumber(int index)
        {
            return messages[index].Key;
        }

        //returns the number of messages in que
        public int getNumberOfMessages()
        {
	        return messages.Count;
        }

        //sets if the socket is blocking or not
        public void setBlocking(bool block)
        {
            socket.Blocking = block;
        }

        //returns your random identifier
        public int getMyIdentifier()
        {
	        return idNumber;
        }

        //clears the message que
        public void clearMessages()
        {
            messages.Clear();
        }

        //sets the port binding
        public void setBoundPort(ushort port)
        {
            socket.Bind(new IPEndPoint(0, port));
        }
    }
}