using DataAccessLayer.Seguranca;
using Dominio.Seguranca;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Seguranca
{
    public class LicenseRN
    {

        private static LicenseRN _instancia; 
        private LicencaDAO dao;

        public LicenseRN()
        {
            dao = new LicencaDAO();
        }

        public static LicenseRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LicenseRN();
            }

            return _instancia;
        }


        public void AddLic(LicencaDTO dto)
        {
            dao.Inserir(dto);
        }

        public Tuple<bool, string> GetSystemValidLicense(LicencaDTO dto)
        {
            dto.HostName = Encrypt(dto.HostName);
            var licenseInfo = dao.ObterPorLicencaValida(dto);

            if (dto.Sucesso)
            {
                
            } 

            return new Tuple<bool, string>(licenseInfo.Sucesso, licenseInfo.MensagemErro);


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

        public bool ExistLicFile(string pLicFile)
        {
            if (!File.Exists(pLicFile))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

         

        internal LicencaDTO GenerateLicense(LicencaDTO dto, string pFilePath)
        {

            try
            {
                dto.HostMacAddress = getMacByIp(dto.HostMacAddress);
                dto.LicenseID = Encrypt(dto.Filial + "_" + dto.HostMacAddress + "_" + dto.HostName + "_" + DateTime.Today.Date);
                dto.FiscalYear = Encrypt(DateTime.Today.Year.ToString());
                dto.ValidateDate = Encrypt(new DateTime(DateTime.Today.Year, 12, 1).ToString());
                dto.HostMacAddress = Encrypt(dto.HostMacAddress);
                dto.HostName = Encrypt(dto.HostName);
                dto.GenereatedDate = Encrypt(DateTime.Now.ToString());
                dto.Status = 1;
                dto = dao.Inserir(dto);

                if (!string.IsNullOrEmpty(dto.HostMacAddress))
                {
                    using (FileStream fs = File.Create(pFilePath))
                    {
                        

                        using (StreamWriter sw = new StreamWriter(pFilePath))
                        {
                           
                            // Add some information to the file.
                            sw.Write(Encrypt(new JavaScriptSerializer().Serialize(dto).ToString()));
                            
                        }
                    } 

                }
                else
                {
                    dto.MensagemErro = "Erro Durante a verificação da Licença: Endereço Físico não carregado ";
                }
            }
            catch(Exception ex)
            {
                dto.MensagemErro = "Erro Durante a verificação da Licença: "+ex.Message.Replace("'", "");
            }

            return dto;

        }

        public string getMacByIp(string ip)
        {
            var macIpPairs = GetAllMacAddressesAndIppairs();
            int index = macIpPairs.FindIndex(x => x.IpAddress == ip);
            if (macIpPairs.Count > 0)
            {
                return macIpPairs[0].MacAddress.ToUpper();
            }
            else
            {
                return null;
            }
        }

        public List<MacIpPair> GetAllMacAddressesAndIppairs()
        {
            List<MacIpPair> mip = new List<MacIpPair>();
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a ";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string cmdOutput = pProcess.StandardOutput.ReadToEnd();
            string pattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            foreach (Match m in Regex.Matches(cmdOutput, pattern, RegexOptions.IgnoreCase))
            {
                mip.Add(new MacIpPair()
                {
                    MacAddress = m.Groups["mac"].Value,
                    IpAddress = m.Groups["ip"].Value
                });
            }

            return mip;
        }
        public struct MacIpPair
        {
            public string MacAddress;
            public string IpAddress;
        }

        public List<LicencaDTO> LicenseTypeList()
        {
            var licenses = new List<LicencaDTO>();

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("PS"),
                LicDesignation = Encrypt("POS SIMPLES")
            });

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("PC"),
                LicDesignation = Encrypt("POS COMPLETO")
            });

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("RT"),
                LicDesignation = Encrypt("POS RESTAURACAO")
            });

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("GL"),
                LicDesignation = Encrypt("GESTÃO COMERCIAL LIVRE")
            });

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("GF"),
                LicDesignation = Encrypt("GESTÃO COMERCIAL FACTURAÇÃO")
            });

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("GP"),
                LicDesignation = Encrypt("GESTÃO COMERCIAL PADRÃO")
            });

            licenses.Add(new LicencaDTO
            {
                LicType = Encrypt("GC"),
                LicDesignation = Encrypt("GESTÃO COMERCIAL COMPLETO")
            });

            return licenses;
        }



    }
}
