using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpScoket
{
    public partial class Form1 : Form
    {
        private Socket tcpSocket;
        private IPEndPoint ipEndPointReceive;
        private IPEndPoint ipEndPointDest;
        private EndPoint remoteEndPoint;

        public Form1()
        {
            InitializeComponent();
        }



        //boutton pour creer socket
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Création d'un socket UDP

                tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);

                // Création du point de terminaison local (réception)


                // Déclaration du point de terminaison de l'émetteur
                ipEndPointDest = new IPEndPoint(IPAddress.Parse(textBox2.Text), int.Parse(textBox4.Text));
                




                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    ipEndPointReceive = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox3.Text));
                    tcpSocket.Bind(ipEndPointReceive);
                }
                tcpSocket.Connect(ipEndPointDest);
                MessageBox.Show("Socket créé et lié avec succès !");
                textBox6.AppendText("Connecte au serveur. \r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }





        //boutton pour fermer 
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tcpSocket != null && tcpSocket.Connected)
                {
                    timer1.Stop();
                    tcpSocket.Shutdown(SocketShutdown.Both);
                    tcpSocket.Close();
                    tcpSocket = null;
                    MessageBox.Show("Socket fermé avec succès !");
                    textBox6.AppendText("Socket ferme.\r\n");
                }
                else
                {
                    MessageBox.Show("Aucun socket actif à fermer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }


        //boutton envoyer
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpSocket != null && tcpSocket.Connected)
                {
                    string message = textBox5.Text;
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    tcpSocket.Send(data);
                    textBox7.Text = "Message envoyé : " + message; // Mise à jour de la TextBox Recp.
                    MessageBox.Show("Message envoyé avec succès !");
                }
                else
                {
                    MessageBox.Show("Créez et liez un socket d'abord.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }


        //boutton pour recevoir 
        private void button4_Click(object sender, EventArgs e)
        {


            if (tcpSocket != null && tcpSocket.IsBound)
            {



                timer1.Start();
      


            }





        }


        //boutton CLS
        private void button5_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tcpSocket != null && tcpSocket.IsBound)
                {
                    //udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);


                    if (tcpSocket.Available != 0)
                    {

                        byte[] buffer = new byte[1024];
                        int receivedBytes = tcpSocket.ReceiveFrom(buffer, ref remoteEndPoint);
                        string receivedMessage = Encoding.ASCII.GetString(buffer, 0, receivedBytes);

                        textBox7.Text = "Message reçu : " + receivedMessage; // Mise à jour de la TextBox Recp.

                        MessageBox.Show("Message reçu avec succès !");
                    }


                }
                else
                {
                    MessageBox.Show("Créez et liez un socket d'abord.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
