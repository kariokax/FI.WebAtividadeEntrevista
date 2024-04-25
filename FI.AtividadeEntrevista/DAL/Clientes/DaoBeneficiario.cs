using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal void IncluirBeneficiario(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();


                parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
                parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
                parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCLiente));

                DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
        }

        internal bool CpfExistente(string Cpf, long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            SqlParameter paramExiste = new SqlParameter("Existe", SqlDbType.Bit);
            paramExiste.Direction = ParameterDirection.Output;

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", Cpf));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", id));
            parametros.Add(paramExiste);

            DataSet ds = base.Consultar("FI_SP_VerificaCPFExistenteBeneficiario", parametros);
            
            bool cpfExiste = (bool)paramExiste.Value;

            return cpfExiste;
        }

        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }
        
        internal List<DML.Beneficiario> Listar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarios", parametros);

            List<DML.Beneficiario> beneficiarios = ConverterLista(ds);
            return beneficiarios;
        }
        
        internal DML.Beneficiario consultarBeneficiario(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            
            DML.Beneficiario beneficiarios = Converter(ds);
            return beneficiarios;
        }

        private List<DML.Beneficiario> ConverterLista(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario beneficiario = new DML.Beneficiario();
                    beneficiario.Id = row.Field<long>("Id");
                    beneficiario.IdCLiente = row.Field<long>("IdCliente");
                    beneficiario.CPF = row.Field<string>("Cpf");
                    beneficiario.Nome = row.Field<string>("Nome");
                    lista.Add(beneficiario);
                }
            }

            return lista;
        }
        
        private DML.Beneficiario Converter(DataSet ds)
        {
            DML.Beneficiario beneficiario = new DML.Beneficiario();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    beneficiario.Id = row.Field<long>("Id");
                    beneficiario.IdCLiente = row.Field<long>("IdCliente");
                    beneficiario.CPF = row.Field<string>("Cpf");
                    beneficiario.Nome = row.Field<string>("Nome");
                }
            }

            return beneficiario;
        }

        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }
    }
}
