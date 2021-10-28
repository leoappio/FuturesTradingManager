using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace gerenciador_futuro.classes
{
    class Conta
    {
        public string Usuario {get; set;}
        public string Senha {get; set;}
        public string Nome {get; set;}
        public string MacAdress {get; set;}
        public DateTime DiaTeste {get; set;}
        public bool ValidarDiaTeste()
        {
            DateTime diaAtual = DateTime.Today;
            int dias = DiaTeste.CompareTo(diaAtual);
            if (dias <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidarSenha(string senhaDigitada)
        {
            if(senhaDigitada == Senha)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetPrimeiroEnderecoMAC()
        {

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics.Length == 0)
                return String.Empty;

            NetworkInterface adapter = nics[0];
            String enderecoMAC = adapter.GetPhysicalAddress().ToString();
            return enderecoMAC;

        }


        public static string GetEnderecoMAC1()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String enderecoMAC = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    // retorna endereço MAC do primeiro cartão
                    if (enderecoMAC == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        enderecoMAC = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return enderecoMAC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
