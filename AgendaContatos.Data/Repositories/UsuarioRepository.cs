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
    public class UsuarioRepository : IRepository<Usuario>
    {
        public void Add(Usuario entity)
        {
            var query = @"
            INSERT INTO USUARIO(IDUSUARIO, NOME, EMAIL, SENHA, DATACRIACAO)
            VALUES (@IdUsuario, @Nome, @Email, @Senha, @DataCriacao)
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {

                connection.Execute(query, entity);
            
            }
        }

        public void Update(Usuario entity)
        {
            var query = @"
            UPDATE USUARIO SET
            NOME = @Nome, EMAIL = @Email, SENHA = @Senha, DATACRIACAO = @DataCriacao
            WHERE IDUSUARIO = @IdUsuario
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Guid idUsuario, string novaSenha)
        {
            var query = @"
             UPDATE  USUARIO SET
             SENHA = @novaSenha
             WHERE IDUSUARIO = @idUsuario
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, new { idUsuario, novaSenha });
            }

        }

        public void Delete(Usuario entity)
        {
            var query = @"
             DELETE FROM USUARIO
             WHERE IDUSUARIO = @IdUsuario
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> GetAll()
        {
            var query = @"
            SELECT * FROM USUARIO
            ORDER BY NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario> (query).ToList();

            }
        }

        //retornando um usuário ou nenhum usuário baseado no email
        public Usuario? GetByEmail(string email)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { email }).FirstOrDefault();
            }
        }


        public Usuario? getByEmailAndSenha(string email, string senha)
        {
            var query = @"
            SELECT * FROM USUARIO
            WHERE EMAIL = @email AND SENHA = @senha
            ";

            using(var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            { 
                return connection.Query<Usuario>(query, new {email,senha}).FirstOrDefault();

            }

        }



        public Usuario? GetById(Guid idUsuario)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE IDUSUARIO = @idUsuario
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { idUsuario }).FirstOrDefault();
            }
        }
    }
}

    

