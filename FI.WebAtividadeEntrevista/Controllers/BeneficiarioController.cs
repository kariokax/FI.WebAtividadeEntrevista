using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Helpers;
using System.Reflection;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {

        [HttpPost]
        public ActionResult ExcluirBeneficiario(long id)
        {
            try
            {

                BoBeneficiario beneficiario = new BoBeneficiario();
                beneficiario.Excluir(id);

                return Json(new { success = true, message = "Beneficiário excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao excluir beneficiário: " + ex.Message });
            }
        }
        
        [HttpPost]
        public ActionResult AlterarBeneficiario(BeneficiarioModel beneficiarioModel)
        {
            try
            {

                BoBeneficiario boBeneficiario = new BoBeneficiario();
                Beneficiario beneficiario = new Beneficiario();

                if (!CpfHelper.CpfIsValid(beneficiarioModel.CPF))
                {
                    ModelState.AddModelError("CPF", "CPF inválido.");
                }

                else if (boBeneficiario.VerificarExistencia(beneficiarioModel.CPF, beneficiarioModel.Id))
                {
                    ModelState.AddModelError("CPF", "CPF do Beneficiário já está cadastrado.");
                }

                else
                {
                    beneficiario.Nome = beneficiarioModel.Nome;
                    beneficiario.CPF = Regex.Replace(beneficiarioModel.CPF, "[^0-9]+", "");
                    beneficiario.Id = beneficiarioModel.Id;

                    boBeneficiario.AlterarBeneficiario(beneficiario);

                }

                return Json(new { success = true, message = "Beneficiário excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao excluir beneficiário: " + ex.Message });
            }
        }
        
        [HttpPost]
        public ActionResult ConsultarBeneficiario(long id)
        {

                BoBeneficiario beneficiario = new BoBeneficiario();
                var beneficiarioRetorno = beneficiario.Consultar(id);

                
                return Json(beneficiarioRetorno);
        }
        
        [HttpPost]
        public ActionResult ConsultarBeneficiarios(long id)
        {

                BoBeneficiario beneficiario = new BoBeneficiario();
            List<Beneficiario> beneficiarios = beneficiario.Listar(id);


            return Json(beneficiarios);
        }
    }
}