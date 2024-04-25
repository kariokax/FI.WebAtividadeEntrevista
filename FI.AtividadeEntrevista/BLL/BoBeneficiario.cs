using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public void InserirBeneficiario(DML.Beneficiario beneficiarios)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            beneficiario.IncluirBeneficiario(beneficiarios);
        }

        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            return beneficiario.consultarBeneficiario(id);
        }

        public void AlterarBeneficiario(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario daoBeneficiario = new DAL.DaoBeneficiario();
            daoBeneficiario.Alterar(beneficiario);
        }
        
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            beneficiario.Excluir(id);
        }

        public List<DML.Beneficiario> Listar(long id)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            return beneficiario.Listar(id);
        }

        public bool VerificarExistencia(string cpf, long id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.CpfExistente(cpf, id);
        }


    }
}
