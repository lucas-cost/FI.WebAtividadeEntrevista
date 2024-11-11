using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiário
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.ClienteId));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Verifica se o CPF do beneficiário já está cadastrado para o cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente</param>
        /// <param name="cpf">CPF do beneficiário</param>
        /// <returns>True se o CPF já estiver cadastrado, caso contrário, False</returns>
        internal bool CPFJaCadastrado(long idCliente, string cpf)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("IdCliente", idCliente),
                new System.Data.SqlClient.SqlParameter("CPF", cpf)
            };

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);
            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Lista todos os beneficiários de um cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente</param>
        /// <returns>Lista de beneficiários</returns>
        internal List<DML.Beneficiario> ListarBeneficiarios(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("IdCliente", idCliente)
            };

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarios", parametros);
            return Converter(ds);
        }

        /// <summary>
        /// Converte o DataSet em uma lista de objetos Beneficiario
        /// </summary>
        /// <param name="ds">DataSet com os dados dos beneficiários</param>
        /// <returns>Lista de objetos Beneficiario</returns>
        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario beneficiario = new DML.Beneficiario
                    {
                        Id = row.Field<long>("Id"),
                        CPF = row.Field<string>("CPF"),
                        Nome = row.Field<string>("Nome"),
                        ClienteId = row.Field<long>("IdCliente")
                    };
                    lista.Add(beneficiario);
                }
            }
            return lista;
        }

        /// <summary>
        /// Excluir Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.ClienteId));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }
    }
}
