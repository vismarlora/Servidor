using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using Servidor.Models;
using Servidor.BLL;

namespace Servidor
{
    class Servidor
    {
        /*        
            TcpListener--------> Espera la conexion del Cliente.        
            TcpClient----------> Proporciona la Conexion entre el Servidor y el Cliente.        
            NetworkStream------> Se encarga de enviar mensajes a traves de los sockets.        
        */
        //  IPHostEntry host = Dns.GetHostEntry("localhost");
        // IPAddress ipAddress = host.AddressList[0];

        //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11200);

        private TcpListener Server;
        private IPEndPoint Puerto = new IPEndPoint(IPAddress.Parse("192.168.8.185"), 5000);
        private TcpClient Cliente = new TcpClient();
        private List<Connection> ListaClientes = new List<Connection>();

        Connection Conexion;

        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamEscritor;
            public StreamReader streamLector;
        }

        public Servidor()
        {
            Inicio();
        }

        public void Inicio()
        {

            Console.WriteLine("El Servidor esta encendido!");
            Server = new TcpListener(Puerto);
            Server.Start();

            while (true)
            {
                Cliente = Server.AcceptTcpClient();

                EstablecerConexion();

                ListaClientes.Add(Conexion);
                Console.WriteLine("Un cliente se ha conectado...");

                Thread hilo = new Thread(Escuchar_conexion);

                hilo.Start();
            }

        }

        void EstablecerConexion()
        {
            Conexion = new Connection();
            Conexion.stream = Cliente.GetStream();
            Conexion.streamLector = new StreamReader(Conexion.stream);
            Conexion.streamEscritor = new StreamWriter(Conexion.stream);
        }

        void Escuchar_conexion()
        {
            Connection hcon = Conexion;
            string[] agregar ;
            do
            {
                try
                {
                    string tmp = hcon.streamLector.ReadLine();

                    if (tmp.Contains("@citasA@"))
                    {
                        agregar = tmp.Split(',');
                        AgregarCita(hcon, agregar);
                    }
                    else if (tmp.Contains("@citasV@"))
                        LeerCitas(hcon);
                }
                catch
                {
                    ListaClientes.Remove(hcon);
                    Console.WriteLine("Un cliente se ha desconectado...");
                    break;
                }
            } while (true);
        }

        private void AgregarCita(Connection hcon, string[] agregar)
        {
            Mesa mesa = new Mesa()
            {
                IdMesa = 0,
                Capacidad = 0,
                Forma = "",
                Precio = 0,
                //Fecha = DateTime.Parse(agregar[2]),
                Disponibilidad = true,

            };

            try
            {
                bool guardo = CitasBLL.Guardar(mesa);
                if (guardo)
                    Console.WriteLine("La Reservacion de " + mesa.IdMesa + " se ha guardado!");
                else
                    Console.WriteLine("Error!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("No se pudo guardar la Reservacion!");
            }

        }

        private void LeerCitas(Connection hcon)
        {
            List<Mesa> lista = CitasBLL.GetCita();
            foreach (Mesa x in lista)
            {
                hcon.streamEscritor.WriteLine("Id: " + x.IdMesa);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Capacidad: " + x.Capacidad);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Forma: " + x.Forma);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Precio: " + x.Precio);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Disponibilidad: " + x.Disponibilidad);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.Flush();

            }
        }
    }
}
