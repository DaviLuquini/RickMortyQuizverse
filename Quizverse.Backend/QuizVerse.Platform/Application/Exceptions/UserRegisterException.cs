using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVerse.Platform.Application.Exceptions
{
    public class UserRegisterException(string code, string message) : Exception(message)
    {
        public string Code { get; } = code;
    }
}
