using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Globalization;
using Dominio.Seguranca;
using BusinessLogicLayer;
using System.ServiceProcess;
using BusinessLogicLayer.Seguranca;
using Microsoft.AspNetCore.Hosting;

namespace WebUI
{
    public class KitandaConfig
    { 

        public AcessoDTO pSessionInfo;
        public string FilePath, tokeLogginID = "KitandaSoft_Logged";
        public GenericRN _genericClass;
        public static Dictionary<string, AcessoDTO> sessionDetails;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KitandaConfig()
        {
            _genericClass = new GenericRN();
        }

        public KitandaConfig(IWebHostEnvironment webHostEnvironment)
        {
            _genericClass = new GenericRN();
            _webHostEnvironment = webHostEnvironment; 

            FilePath = Path.Combine(Directory.GetCurrentDirectory(), _genericClass.Decrypt(_genericClass.LicFilePath));
            sessionDetails = sessionDetails ?? new Dictionary<string, AcessoDTO>();
        } 
         

        public void SetAppSettings(AcessoDTO pAcesso)
        {

            if (!string.IsNullOrEmpty(pAcesso.Utilizador))
            {
                ConfiguracaoRN.GetInstance().ExecuteBackup(pAcesso.Settings);
                //Session["SessionInfo"] = pAcesso; 

                //webConfig.SetCookies(Encrypt(pAcesso.Utilizador), tokeLogginID);

                if (sessionDetails.ContainsKey(pAcesso.UserID.ToString()))
                    sessionDetails.Remove(pAcesso.UserID.ToString());

                sessionDetails.Add(pAcesso.UserID.ToString(), pAcesso);
            } 
        }

        public string ShowForm(int pFormID, bool IsAccording)
        {

            var acesso = _genericClass.ShowForm(pSessionInfo, pFormID);

            if (IsAccording && acesso != "")
            {
                acesso = "hidden";
            }

            return acesso;
        }


        public string AllowedModulo(int pModuleID, bool IsAccording)
        {  
            if (pSessionInfo != null)
            {
                var acesso = _genericClass.CanAccessModulo(pSessionInfo, pModuleID);

                if (IsAccording && acesso != "")
                {
                    acesso = "hidden";
                }

                return acesso;
            }
            else
            {
                return "hidden";
            }
        }



        public String NumeroProcesso(int codigo, string ano)
        {
            String matricula = "";

            if (codigo < 10)
            {
                matricula = "00000";
            }
            else if (codigo < 100)
            {
                matricula = "0000";
            }
            else if (codigo < 1000)
            {
                matricula = "000";
            }
            else if (codigo < 10000)
            {
                matricula = "00";
            }
            else if (codigo < 100000)
            {
                matricula = "0";
            }

            return matricula + codigo.ToString() + "/" + ano;

        }







        
        public String FormatarPreco(decimal preco)
        {
            string precoFormatado = String.Format(new CultureInfo("pt-PT"), "{0:N2}", preco) + " ";// + Session["CurrencySymbol"].ToString();
            return precoFormatado;
        }


        public String FormatarNumero(decimal valor)
        {
            string precoFormatado = String.Format(new CultureInfo("pt-PT"),  "{0:N2}", valor);
            return precoFormatado;
        }

        public decimal ValorDecimal(String valor)
        {
            valor = valor == string.Empty ? "0" : valor;
            var decimalValue = valor.Replace(".", "");
            int numero;
            string campo = "";
            decimal retorno = 0;
            if (decimal.Parse(decimalValue) < 10000)
            {


                var valueStr = valor.Replace(".", ",");

                string[] valueArray = valueStr.Split(',');
                if (valueArray.Length <= 2)
                    retorno = decimal.Parse(valueStr);
                else
                    retorno = decimal.Parse(valor);
            }
            else
            {
                foreach (char caracter in decimalValue)
                {
                    bool res = int.TryParse(caracter.ToString(), out numero);

                    if (res.Equals(true) || caracter.Equals(',') || caracter.Equals('-'))
                    {
                        campo += caracter;
                    }

                }

                if (campo != null && !campo.Equals(""))
                {
                    retorno = Convert.ToDecimal(campo);
                }
            }
            return retorno;
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        
        
        internal string GotoLogin()
        {
            return "window.location.href='../Seguranca/Login'";
        } 
        

        
    }
}