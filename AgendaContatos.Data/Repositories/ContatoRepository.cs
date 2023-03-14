using AgendaContatos.Data.Configurations;
using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Repositories
{
    public class ContatoRepository : IRepository<Contato>
    {
        public void Add(Contato entity)
        {
            var query = @"
            INSERT INTO CONTATO(IDCONTATO, NOME, TELEFONE, EMAIL, DATANASCIMENTO, TIPO, IDUSUARIO)
            VALUES(@IdContato, @Nome, @Telefone, @Email, @DataNascimento, @Tipo, @IdUsuario)
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Contato entity)
        {
            var query = @"
            UPDATE CONTATO SET
            NOME = @Nome, TELEFONE = @Telefone, EMAIL = @Email, DATANASCIMENTO = @DataNascimento
            WHERE IDCONTATO = @IdContato
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Contato entity)
        {
            var query = @"
            DELETE FROM CONTATO
            WHERE IDCONTATO = @IdContato
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {

                connection.Execute(query, entity);
            }

        }

        // retornando os contatos pelo nome
        public List<Contato> GetAll()
        {
            var query = @"
             SELECT * FROM CONTATO
                ORDER BY NOME
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {

                return connection.Query<Contato>(query).ToList();
            }
            
        }

        //consultando todos os contatos que pertencem a um determinado usuário
        public List<Contato>GetByUsuario(Guid idUsuario)
        {
            var query = @"
            SELECT * FROM CONTATO
            WHERE IDUSUARIO = @idUsuario
            ORDER BY NOME
            ";

            using( var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {

                return connection.Query<Contato>(query, new {idUsuario}).ToList();  
            }


        }

        public List<Contato> GetAllByUsuario(Guid idUsuario)
        {
            var query = @"
                SELECT * FROM CONTATO
                WHERE IDUSUARIO = @idUsuario
                ORDER BY NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Contato>(query, new { idUsuario }).ToList();
            }
        }


        public Contato GetById(Guid idContato)
        {
            var query = @"
             SELECT * FROM CONTATO
            WHERE IDCONTATO = @IdContato
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Contato>(query, new { idContato }).FirstOrDefault();
            }

        }
    }
}
