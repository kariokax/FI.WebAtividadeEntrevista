$(document).ready(function () {

    function preencherTabelaBeneficiarios(beneficiarios) {

        $('#tabelaBeneficiarios').empty();

        beneficiarios.forEach(function (beneficiarios) {
            $('#tabelaBeneficiarios').append('<tr><td>' + beneficiarios.CPF + '</td><td>' + beneficiarios.Nome + '</td></tr>');
        });
    }
    preencherTabelaBeneficiarios(beneficiarios);

});