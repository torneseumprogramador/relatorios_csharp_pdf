using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Paginacao.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public static int Total()
        {
            string queryString = "select count(*) from clientes";
            var total = 0;
            using (SqlConnection connection = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Paginacao;Data Source=DIDOX-WINDOWS\SQLEXPRESS"))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                total = Convert.ToInt32(command.ExecuteScalar());
            }
            return total;
        }

        public static List<Cliente> Buscar(int porPagina, int paginaCorrete)
        {
            var clientes = new List<Cliente>();
            var offset = 1;
            if (paginaCorrete > 1)
            {
                offset = (porPagina * (paginaCorrete - 1)) + 1;
            }
            string queryString = "DECLARE @Limit INT = " + porPagina + ", @Offset INT = " + offset + "; WITH reaultado AS(SELECT *, ROW_NUMBER() OVER(ORDER BY ID) AS linha FROM clientes WHERE Id is not null) SELECT * FROM reaultado WHERE linha >= @Offset AND linha<@Offset +@Limit";

            using (SqlConnection connection = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Paginacao;Data Source=DIDOX-WINDOWS\SQLEXPRESS"))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente() { ID = Convert.ToInt32(reader["Id"]), Nome = reader["Nome"].ToString() });
                    }
                }
            }

            return clientes;
        }
    }
}