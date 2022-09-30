using System;
using System.Linq.Expressions;

namespace SuperPastel.Nucleo.Base
{
    public class Mensagens<T> where T : class
    {
        private readonly string _linguagem = string.Empty;

        public Mensagens()
        {
            _linguagem = "pt-BR";
        }

        public Mensagens(string linguagem)
        {
            _linguagem = linguagem;
        }

        public string Requerido(string campo) => ObterRequerido(campo);
        public string Requerido<TPropriedade>(Expression<Func<T, TPropriedade>> property) => ObterRequerido(property.Name);

        public string MaximoCaracterePermitido(string campo, int max) => ObterMaximoCaracterePermitido(campo, max);
        public string MaximoCaracterePermitido<TPropriedade>(Expression<Func<T, TPropriedade>> property, int max) => ObterMaximoCaracterePermitido(property.Name, max);

        public string MaximoDigitoPermitido(string campo, int max) => ObterMaximoDigitoPermitido(campo, max);
        public string MaximoDigitoPermitido<TPropriedade>(Expression<Func<T, TPropriedade>> property, int max) => ObterMaximoDigitoPermitido(property.Name, max);

        public string MaximoMinimoCaracterePermitido(string campo, int max, int min) => ObterMaximoMinimoCaracterePermitido(campo, max, min);
        public string MaximoMinimoCaracterePermitido<TPropriedade>(Expression<Func<T, TPropriedade>> property, int max, int min) => ObterMaximoMinimoCaracterePermitido(property.Name, max, min);

        public string MaximoMinimoDigitoPermitido(string campo, int max, int min) => ObterMaximoMinimoDigitoPermitido(campo, max, min);
        public string MaximoMinimoDigitoPermitido<TPropriedade>(Expression<Func<T, TPropriedade>> property, int max, int min) => ObterMaximoMinimoDigitoPermitido(property.Name, max, min);

        public string FixoCaracterePermitido(string campo, int length) => ObterFixoCaracterePermitido(campo, length);
        public string FixoCaracterePermitido<TPropriedade>(Expression<Func<T, TPropriedade>> property, int length) => ObterFixoCaracterePermitido(property.Name, length);

        public string FixoDigitoPermitido(string campo, int length) => ObterFixoDigitoPermitido(campo, length);
        public string FixoDigitoPermitido<TPropriedade>(Expression<Func<T, TPropriedade>> property, int length) => ObterFixoDigitoPermitido(property.Name, length);

        public string EmailInvalido() => ObterEmailInvalido();

        #region Private Methods

        private string ObterRequerido(string campo)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} é requerido.";
            }
            else if (_linguagem == "en")
            {
                message = $"The {campo} field is required.";
            }
            else if (_linguagem == "es")
            {
                message = $"Se requiere el campo {campo}.";
            }

            return message;
        }

        private string ObterMaximoCaracterePermitido(string campo, int max)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} ultrapassou o máximo permitido, {max} caracteres.";
            }
            else if (_linguagem == "en")
            {
                message = $"Field {campo} exceeded maximum allowed, {max} characters.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo {campo} superó el máximo permitido, {max} caracteres.";
            }

            return message;
        }

        private string ObterMaximoDigitoPermitido(string campo, int max)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} ultrapassou o máximo permitido, {max} dígitos.";
            }
            else if (_linguagem == "en")
            {
                message = $"Field {campo} exceeded maximum allowed, {max} digits.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo {campo} superó el máximo permitido, {max} dígitos.";
            }

            return message;
        }

        private string ObterMaximoMinimoCaracterePermitido(string campo, int max, int min)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} aceita entre {min} a {max} caracteres.";
            }
            else if (_linguagem == "en")
            {
                message = $"The {campo} field accepts between {min} and {max} characters.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo {campo} acepta entre {min} a {max} caracteres.";
            }

            return message;
        }

        private string ObterMaximoMinimoDigitoPermitido(string campo, int max, int min)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} aceita entre {min} a {max} dígitos.";
            }
            else if (_linguagem == "en")
            {
                message = $"The {campo} field accepts between {min} and {max} digits.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo {campo} acepta entre {min} a {max} dígitos.";
            }

            return message;
        }

        private string ObterFixoCaracterePermitido(string campo, int length)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} aceita apenas {length} caracteres.";
            }
            else if (_linguagem == "en")
            {
                message = $"The {campo} field accepts only {length} characters.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo {campo} sólo acepta caracteres {length}.";
            }

            return message;
        }

        private string ObterFixoDigitoPermitido(string campo, int tamanho)
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = $"O campo {campo} aceita apenas {tamanho} dígitos.";
            }
            else if (_linguagem == "en")
            {
                message = $"The {campo} field accepts only {tamanho} digits.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo {campo} sólo acepta dígitos {tamanho}.";
            }

            return message;
        }

        private string ObterEmailInvalido()
        {
            var message = string.Empty;

            if (_linguagem == "pt-BR")
            {
                message = "O campo Email está inválido.";
            }
            else if (_linguagem == "en")
            {
                message = "The Email field is invalid.";
            }
            else if (_linguagem == "es")
            {
                message = $"El campo Correo no es válido.";
            }

            return message;
        }

        #endregion
    }
}
