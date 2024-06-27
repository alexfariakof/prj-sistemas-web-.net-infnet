// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var backHistory = () => {
    window.history.back();
}

//form-validation.js
document.addEventListener('DOMContentLoaded', function () {
    // Seleciona todos os formulários na página
    var forms = document.querySelectorAll('form');

    forms.forEach(function (form) {
        // Adiciona um ouvinte para o evento submit no formulário
        form.addEventListener('submit', function (event) {
            // Verifica todos os campos de entrada dentro do formulário
            var inputs = form.querySelectorAll('input, select, textarea');
            inputs.forEach(function (input) {
                // Verifica se há erros de validação no campo
                if (!input.checkValidity()) {
                    // Adiciona a classe is-invalid se houver erro de validação
                    input.classList.add('is-invalid');
                } else {
                    // Remove a classe is-invalid se não houver erro de validação
                    input.classList.remove('is-invalid');
                }
            });

            // Verifica se o formulário é válido
            if (!form.checkValidity()) {
                // Impede o envio do formulário se não for válido
                event.preventDefault();
                event.stopPropagation();
            }
        });

        // Adiciona um ouvinte para o evento blur nos campos de entrada
        inputs.forEach(function (input) {
            input.addEventListener('blur', function () {
                if (!input.checkValidity()) {
                    input.classList.add('is-invalid');
                } else {
                    input.classList.remove('is-invalid');
                }
            });
        });
    });
});
