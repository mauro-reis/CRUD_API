using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUD_API.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OleDb;

namespace CRUD_API.Controllers
{
    public class PessoaFisicaController : ApiController
    {
        public PessoaFisica GetPessoaFisica(int id)
        {
            DataSet registroConsultado = ConsultaPorID(id);

            if (registroConsultado.Tables[0].Rows.Count > 0)
            {
                PessoaFisica item = new PessoaFisica
                {
                    Id = Convert.ToInt32(registroConsultado.Tables[0].Rows[0]["ID"]),
                    Nome = registroConsultado.Tables[0].Rows[0]["NOME"].ToString(),
                    DataNascimento = Convert.ToDateTime(registroConsultado.Tables[0].Rows[0]["DATA_NASCIMENTO"]),
                    ValorRenda = Convert.ToDouble(registroConsultado.Tables[0].Rows[0]["VALOR_RENDA"]),
                    CPF = registroConsultado.Tables[0].Rows[0]["CPF"].ToString()
                };

                return item;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PutPessoasFisicas(PessoaFisica pessoaFisica)
        {
            if (!IncluirRegistro(pessoaFisica))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PutPessoaFisica(int id, PessoaFisica PessoaFisica)
        {
            if (!AlterarRegistro(id, PessoaFisica))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DeletePessoaFisica(int id)
        {
            if(!ExcluirRegistro(id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #region DAL

        public DataSet ConsultaPorID(int pID)
        {
            string pQuery = "SELECT * FROM PESSOAS WHERE ID = " + pID + ";";

            MySqlConnection conexao = null;
            string strConexao = @"Server=localhost;Database=cadastropessoafisica;Uid=root;Pwd='123456';Connect Timeout=30;";

            try
            {
                conexao = new MySqlConnection(strConexao);
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(pQuery, conexao);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(comando);

                DataSet dados = new DataSet();

                dataAdapter.Fill(dados);

                return dados;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }

        public bool IncluirRegistro(PessoaFisica pPessoaFisica)
        {
            bool retorno = false;
            string pQuery = "INSERT INTO PESSOAS VALUES (@Id, @Nome, @DataNascimento, @ValorRenda, @CPF)";

            MySqlConnection conexao = null;
            string strConexao = @"Server=localhost;Database=cadastropessoafisica;Uid=root;Pwd='123456';Connect Timeout=30;";

            try
            {
                conexao = new MySqlConnection(strConexao);
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(pQuery, conexao);
                comando.Parameters.Add(new MySqlParameter("@Id", pPessoaFisica.Id));
                comando.Parameters.Add(new MySqlParameter("@Nome", pPessoaFisica.Nome));
                comando.Parameters.Add(new MySqlParameter("@DataNascimento", pPessoaFisica.DataNascimento));
                comando.Parameters.Add(new MySqlParameter("@ValorRenda", pPessoaFisica.ValorRenda));
                comando.Parameters.Add(new MySqlParameter("@CPF", pPessoaFisica.CPF));

                comando.ExecuteNonQuery();

                retorno = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

            return retorno;
        }

        public bool AlterarRegistro(int pID, PessoaFisica pPessoa)
        {
            bool retorno = false;
            string pQuery = "UPDATE PESSOAS SET NOME = @Nome, DATA_NASCIMENTO = @DataNascimento, VALOR_RENDA = @ValorRenda, CPF = @CPF WHERE ID = @Id";

            MySqlConnection conexao = null;
            string strConexao = @"Server=localhost;Database=cadastropessoafisica;Uid=root;Pwd='123456';Connect Timeout=30;";

            try
            {
                conexao = new MySqlConnection(strConexao);
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(pQuery, conexao);
                comando.Parameters.Add(new MySqlParameter("@Nome", pPessoa.Nome));
                comando.Parameters.Add(new MySqlParameter("@DataNascimento", pPessoa.DataNascimento));
                comando.Parameters.Add(new MySqlParameter("@ValorRenda", pPessoa.ValorRenda));
                comando.Parameters.Add(new MySqlParameter("@CPF", pPessoa.CPF));
                comando.Parameters.Add(new MySqlParameter("@Id", pPessoa.Id));

                comando.ExecuteNonQuery();

                retorno = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

            return retorno;
        }

        public bool ExcluirRegistro(int pID)
        {
            bool retorno = false;
            string pQuery = "DELETE FROM PESSOAS WHERE ID = @Id";

            MySqlConnection conexao = null;
            string strConexao = @"Server=localhost;Database=cadastropessoafisica;Uid=root;Pwd='123456';Connect Timeout=30;";

            try
            {
                conexao = new MySqlConnection(strConexao);
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(pQuery, conexao);
                comando.Parameters.Add(new MySqlParameter("@Id", pID));

                comando.ExecuteNonQuery();

                retorno = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

            return retorno;
        }
        #endregion
    }
}