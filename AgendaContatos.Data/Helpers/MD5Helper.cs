using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;

namespace AgendaContatos.Data.Helpers
{
    public class MD5Helper
    {
        public static string Encrypt(string value)//pegando o valor em string
        {
            var md5 = MD5.Create();
            //converto o valor string em Byte e o método computehash criptografa e devolve um resultado para variavel hash
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;
            foreach(var item in hash) 
            {
                result += item.ToString("X2");// X2 hexadecimal
            
            }

            return result;  
        }
    }
}
