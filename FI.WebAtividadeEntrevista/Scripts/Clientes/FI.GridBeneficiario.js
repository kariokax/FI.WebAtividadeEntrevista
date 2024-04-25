$(document).ready(function () {

    function obterBeneficiarios(idCliente) {
        $.ajax({
            url: urlConsultaList,
            type: 'POST',
            data: { id: idCliente },
            success: function (data) {
                beneficiarios = data;
                console.log(beneficiarios);
            },
            error: function () {
                console.error('Erro ao obter beneficiários.');
            }
        });
    }

    function adicionarBeneficiarioNaTabela(nome, cpf) {
        $('#tabelaBeneficiarios').append('<tr><td>' + cpf + '</td><td>' + nome + '</td><td><button class="btn btn-primary btn-alterar mr-2" data-id="' + beneficiarios.Id + '">Alterar</button>  <button class="btn btn-primary btn-excluir" data-id="' + beneficiarios.Id + '">Excluir</button> <td></tr>');
    }
    function preencherBeneficiarioExistente() {
        obterBeneficiarios(idCliente);
        beneficiarios?.forEach(function (beneficiario) {
            $('#tabelaBeneficiarios').append('<tr><td>' + beneficiario.CPF + '</td><td>' + beneficiario.Nome + '</td><td><button class="btn btn-primary btn-alterar mr-2" data-id="' + beneficiario.Id + " " + beneficiario.IdCLiente + '">Alterar</button>  <button class="btn btn-primary btn-excluir" data-id="' + beneficiario.Id + " " + beneficiario.IdCLiente+'">Excluir</button> <td></tr>');
        });
    }

    preencherBeneficiarioExistente();

    function adicionarBeneficiarioNaLista(nome, cpf) {
        beneficiarios.push({ nome: nome, cpf: cpf });
    }

    function obterBeneficiarios(idCliente) {
        $.ajax({
            url: urlConsultaList,
            type: 'POST',
            data: { id: idCliente },
            success: function (data) {
                beneficiarios = data;
            },
            error: function () {
                console.error('Erro ao obter beneficiários.');
            }
        });
    }

    $(document).on('click', '#salvarBeneficiario', function () {

        console.log("beneficiario salvar " + idBeneficiario);

        if (idBeneficiario == 0) {

            $('#formCadastroBeneficiario').submit(function (event) {
                event.preventDefault();

                var nome = $('#NomeBeneficiario').val();
                var cpf = $('#CpfBeneficiario').val();

                adicionarBeneficiarioNaLista(nome, cpf);

                adicionarBeneficiarioNaTabela(nome, cpf);

                $('#NomeBeneficiario').val('');
                $('#CpfBeneficiario').val('');

            });
        }
        else {
            console.log("Chegou aqui");
            $.ajax({
                url: urlAlterar,
                type: 'POST',
                data: {
                    "Nome": $('#NomeBeneficiario').val(),
                    "CPF": $('#CpfBeneficiario').val(),
                    "Id": idBeneficiario
                },
                success: function (response) {

                    idBeneficiario = 0
                    obterBeneficiarios(idCliente);
                    preencherBeneficiarioExistente();
                },
                error: function (xhr, status, error) {

                    console.error('Erro ao carregar detalhes do beneficiário: ' + error);
                }
            });
        }
    });

        $(document).on('click', '.btn-excluir', function () {
            var ids = $(this).data('id').split(' ');
            idBeneficiario = ids[0];
            idCliente = ids[1];
            var confirmacao = confirm('Tem certeza de que deseja excluir o beneficiário com ID ' + idBeneficiario + '?');

            if (confirmacao) {

                $.ajax({
                    url: urlExclusao,
                    type: 'POST',
                    data: { id: idBeneficiario },
                    success: function (response) {
                        idBeneficiario = 0;
                        preencherBeneficiarioExistente();
                    },
                    error: function (xhr, status, error) {

                        console.error('Erro ao excluir beneficiário: ' + error);
                    }
                });
            }
        });

        $(document).on('click', '.btn-alterar', function () {
            var ids = $(this).data('id').split(' ');
            console.log(ids);
            idBeneficiario = ids[0];
            idCliente = ids[1];

            $.ajax({
                url: urlConsulta,
                type: 'POST',
                data: { id: idBeneficiario },
                success: function (response) {

                    $('#CpfBeneficiario').val(response.CPF);
                    $('#NomeBeneficiario').val(response.Nome);
                },
                error: function (xhr, status, error) {
                    console.error('Erro ao carregar detalhes do beneficiário: ' + error);
                }
            });
        });

        preencherTabelaBeneficiarios();

});

