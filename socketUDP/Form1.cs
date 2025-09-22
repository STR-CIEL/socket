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

namespace socketUDP
{
    public partial class Form1 : Form
    {
        private Socket udpSocket;
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
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Création du point de terminaison local (réception)
                ipEndPointReceive = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox3.Text));
                udpSocket.Bind(ipEndPointReceive);

                // Déclaration du point de terminaison de l'émetteur
                ipEndPointDest = new IPEndPoint(IPAddress.Parse(textBox2.Text), int.Parse(textBox4.Text));
                remoteEndPoint = (EndPoint)ipEndPointDest;

                MessageBox.Show("Socket créé et lié avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }





        //boutton pour fermer 
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (udpSocket != null && udpSocket.Connected)
                {
                    udpSocket.Close();
                    udpSocket = null;
                    MessageBox.Show("Socket fermé avec succès !");
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
                if (udpSocket != null && udpSocket.IsBound)
                {
                    string message = textBox5.Text;
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    udpSocket.SendTo(data, ipEndPointDest);
                    textBox6.Text = "Message envoyé : " + message; // Mise à jour de la TextBox Recp.
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
            try
            {
                if (udpSocket != null && udpSocket.IsBound)
                {
                    byte[] buffer = new byte[1024];
                    int receivedBytes = udpSocket.ReceiveFrom(buffer, ref remoteEndPoint);
                    string receivedMessage = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
                    textBox6.Text = "Message reçu : " + receivedMessage; // Mise à jour de la TextBox Recp.
                    MessageBox.Show("Message reçu avec succès !");
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


        //boutton CLS
        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }
    }
}