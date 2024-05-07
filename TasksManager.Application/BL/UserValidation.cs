﻿using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Exceptions;
using TasksManager.Domain.Interfaces.Validations;

namespace TasksManager.Application.BL
{
    public class UserValidation : IUserValidation
    {
        private readonly IConfiguration _config;

        public UserValidation(IConfiguration config)
        {
            _config = config;
        }

        public bool IsPasswordCorrect(string pwdClient, string pwdHashed)
        {
            if (string.IsNullOrEmpty(pwdClient)) return false;
            return pwdClient.ValidateEncodedPassword(pwdHashed, _config);
        }

        public void ValidateEmail(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail)) throw new ClientException("No se proporcionó email");
            userEmail = userEmail.Trim();
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(userEmail, pattern)) throw new ClientException("No se proporcionó email válido");
        }

        public void ValidatePasswordFormat(string newPwd)
        {
            string pattern = @"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*()-_=+[\]{}|;:'"",.<>?/]).{8,}$";
            if (!Regex.IsMatch(newPwd, pattern)) throw new ClientException("Las contraseña debe tener al menos 8 caracteres, un número, una mayúscula, una minúscula y un carácter especial.");
        }

        public void ValidateRequiredFieldsUser(UserDto user)
        {
            if (string.IsNullOrEmpty(user.username))
            {
                throw new ClientException("No se envió nombre de usuario");
            }
            if (string.IsNullOrEmpty(user.password))
            {
                throw new ClientException("No se envió la contraseña del usuario");
            }

            if (string.IsNullOrEmpty(user.name))
            {
                throw new ClientException("No se envió el nombre de usuario");
            }
            if (string.IsNullOrEmpty(user.role))
            {
                throw new ClientException("No se envió el rol del usuario");
            }

            try
            {
                var num = Convert.ToInt64(user.phone);
            }
            catch (FormatException)
            {
                throw new ClientException("No se proporcionó un número de teléfono válido");
            }

        }

        public void ValidateUserUpdate(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}