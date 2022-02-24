using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviarC.Data
{
    class DP_Beneficios
    {
        public class DP_Beneficios : IDisposable
        {
            public List<Beneficios> BuscarBeneficios()
            {
                List<Beneficios> beneficios;

                using (SqlConnection con = new SqlConnection(Conexion.Cadena2()))
                {
                    string query = @"SELECT * FROM Correo as ape " +
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
    }
}

