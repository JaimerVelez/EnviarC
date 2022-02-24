using Dapper;
using EnviarC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviarC
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DP_Clientes modelo = new DP_Clientes();
            List<Clientes> contratos = modelo.BuscarContratosPorIdCliente();

            foreach (Clientes clientes in contratos)
            {

                string envio = clientes.Correo;
                SendEmail(envio);
            }

        }


        public static void SendEmail(string Mail)
        {
            try
            {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add("" + Mail + "");
                msg.Subject = ("Bienvenida Estimad@ ");
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;

                //Lleer el htrml

                string body = string.Empty;
                //using streamreader for reading my htmltemplate   

                // using (StreamReader reader = new StreamReader(Server.MapPath("~/Plantillas/Bienvenida.html")))

                Beneficios beneficiosAct = null;


                string Idbeneficio = "1";
                using (DP_Beneficios dP_Beneficios = new DP_Beneficios())
                {
                    beneficiosAct = dP_Beneficios.BuscarBeneficioId(Idbeneficio);
                }

                string Plantilla = beneficiosAct.Plantilla;

                //  string plnatilla = Beneficios.

                //string Mail = "jaimervelez@hotmail.com";

                string Pin = "11125";


                //string pathToFiles = Server.MapPath("/UploadedFiles");

                string appPath = Path.GetFullPath("~").Replace("~", "");

                body = File.ReadAllText(appPath + "/Plantilla/" + Plantilla);

                //using (StreamReader reader = new StreamReader(Server.MapPath("~/Plantillas/" + Plantilla + "")))
                //{
                //    body = reader.ReadToEnd();
                //}

                //  body = body.Replace("{@CLIENETE}", NombreCliente);
                body = body.Replace("{@CLAVEBENEFICIO}", Pin);
                // body = body.Replace("{@TITULO}", tirul);

                msg.Body = body;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.From = new System.Net.Mail.MailAddress("" + Mail + "");

                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                cliente.Credentials = new System.Net.NetworkCredential("jaimervelezlol@gmail.com", "Nunca lo sabras");

                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.Host = "smtp.gmail.com";
                cliente.Send(msg);

                //  string MsmTelefono = string.Empty;

            }
            catch (Exception ex)
            {
                string msgg = ex.Message;
            }
        }




        public class DP_Beneficios : IDisposable
        {
            public  List<Beneficios> Buscarbeneficios()
            {
                List<Beneficios> beneficios;

                using (SqlConnection con = new SqlConnection(Conexion.Cadena2()))
                {
                    string query = @"SELECT * FROM Archica_Productos_Especiales as ape " +
                                    "where " +
                                    "ape.Estado=1";
                    beneficios = con.Query<Beneficios>(query).ToList();
                }
                return beneficios;
            }
            public Beneficios BuscarBeneficioId(string IdBeneficio)
            {
                Beneficios beneficios;
                using (SqlConnection con = new SqlConnection(Conexion.Cadena2()))
                {
                    string query = @"SELECT * FROM Correo as ape " +
                                    "where " +
                                    "ape.Estado=1 and Id=" + IdBeneficio + "";
                    beneficios = con.Query<Beneficios>(query).FirstOrDefault();
                }
                return beneficios;
            }
            public void Dispose()
            {

            }
        }
    



    private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
