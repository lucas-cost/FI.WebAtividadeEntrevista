var arrBeneficiarios = [];
var lastId = 0;
console.log('VINDOS DO BANCO', arrBeneficiarios);

$(document).ready(function () {
    $('#formCadastro #CPF').mask('000.000.000-00');
    $('#beneficiarioForm #BeneficiarioCPF').mask('000.000.000-00');
    $('#formCadastro #Telefone').mask('(00) 00000-0000');
    $('#formCadastro #CEP').mask('00000-000');

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#CPF").val(),
                "Beneficiarios": arrBeneficiarios
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r, urlRetorno)
                }
        });
    })

    $('#addBeneficiarioBtn').click(function () {
        $('#beneficiarioModal').modal('show');
    });

    $('#beneficiarioForm').submit(function (event) {
        event.preventDefault();

        var formData = {
            CPF: $('#BeneficiarioCPF').val(),
            Nome: $('#BeneficiarioNome').val()
        };

        var cpfDuplicado = arrBeneficiarios.some(function (beneficiario) {
            return beneficiario.CPF == formData.CPF && beneficiario.Id != $('#BeneficiarioAlterando').val();
        });

        if (cpfDuplicado) {
            $('#alertMessage').text('Já existe um beneficiário com esse CPF para este cliente.');
            return;
        }

        var index = arrBeneficiarios.findIndex(function (beneficiario) {
            return beneficiario.Id == $('#BeneficiarioAlterando').val();
        });

        if (index !== -1) {
            var beneficiarioParaEditar = arrBeneficiarios[index];

            $("#btnAction").html('Incluir');
            $("#btnAction").removeClass('btn-warning').addClass('btn-success');

            beneficiarioParaEditar.CPF = formData.CPF;
            beneficiarioParaEditar.Nome = formData.Nome;

            $('#tabelaBeneficiarios #' + beneficiarioParaEditar.Id).find('td:nth-child(1)').text(beneficiarioParaEditar.CPF);
            $('#tabelaBeneficiarios #' + beneficiarioParaEditar.Id).find('td:nth-child(2)').text(beneficiarioParaEditar.Nome);

            console.log('ALTERADO', arrBeneficiarios);
        } else {
            $("#btnAction").html('Incluir');
            $("#btnAction").removeClass('btn-warning').addClass('btn-success');

            formData.Id = getNewId();
            arrBeneficiarios.push(formData);

            var newRow = '<tr id="' + formData.Id + '">' +
                '             <td>' + formData.CPF + '</td>' +
                '             <td>' + formData.Nome + '</td>' +
                '             <td>' +
                '                  <div style="display: flex; gap: 10px; justify-content: right;">' +
                '                       <button type="button" class="btn btn-sm btn-primary edit-beneficiario-btn" value="' + formData.Id + '">Alterar</button>' +
                '                       <button type="button" class="btn btn-sm btn-primary del-beneficiario-btn" value="' + formData.Id + '">Excluir</button>' +
                '                  </div>' +
                '             </td>' +
                '         </tr>';

            $('#tabelaBeneficiarios tbody').append(newRow);

            console.log('CADASTRADO', arrBeneficiarios);
        }

        $('#BeneficiarioCPF').val('');
        $('#BeneficiarioNome').val('');
        $('#alertMessage').text('');
        $('#BeneficiarioAlterando').val('');
    });
});

function ModalDialog(titulo, texto, urlRetorno) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">' +
        '        <div class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"' + (urlRetorno ? ' onclick="window.location.href=\'' + urlRetorno + '\'"' : '') + '>×</button>' +
        '                    <h4 class="modal-title">' + titulo + '</h4>' +
        '                </div>' +
        '                <div class="modal-body">' +
        '                    <p>' + texto + '</p>' +
        '                </div>' +
        '                <div class="modal-footer">' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal"' + (urlRetorno ? ' onclick="window.location.href=\'' + urlRetorno + '\'"' : '') + '>Fechar</button>' +
        '                </div>' +
        '            </div>' +
        '  </div>' +
        '</div>';

    $('body').append(texto);
    $('#' + random).modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#' + random).modal('show');
}

function AlterarBeneficiario(id) {
    var beneficiarioClicado = arrBeneficiarios.find(x => x.Id == id);

    $('#BeneficiarioCPF').val(beneficiarioClicado.CPF);
    $('#BeneficiarioNome').val(beneficiarioClicado.Nome);
    $('#BeneficiarioAlterando').val(beneficiarioClicado.Id);

    $("#btnAction").html('Alterar');
    $("#btnAction").removeClass('btn-success').addClass('btn-warning');
}

function DeletarBeneficiario(id) {
    $('#tabelaBeneficiarios #' + id).remove();

    var index = arrBeneficiarios.findIndex(function (beneficiario) {
        return beneficiario.Id == id;
    });

    if (index !== -1) {
        arrBeneficiarios.splice(index, 1);
    }

    console.log('REMOÇÃO', arrBeneficiarios);
}

function getNewId() {
    return ++lastId;
}
