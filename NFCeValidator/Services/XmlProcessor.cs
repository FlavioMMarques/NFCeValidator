using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NFCeValidator.Models;

namespace NFCeValidator.Services
{
    public class XmlProcessor
    {
        public List<NFCeInfo> ProcessarPastaXml(string caminhoPasta)
        {
            List<NFCeInfo> listaNotas = new List<NFCeInfo>();

            if (!Directory.Exists(caminhoPasta))
            {
                throw new DirectoryNotFoundException("Pasta não encontrada: " + caminhoPasta);
            }

            string[] arquivosXml = Directory.GetFiles(caminhoPasta, "*.xml");

            foreach (string arquivo in arquivosXml)
            {
                try
                {
                    NFCeInfo nfce = LerXmlNFCe(arquivo);
                    if (nfce != null)
                    {
                        listaNotas.Add(nfce);
                    }
                }
                catch (Exception ex)
                {
                    // Log do erro mas continua processando outros arquivos
                    System.Diagnostics.Debug.WriteLine($"Erro ao processar {arquivo}: {ex.Message}");
                }
            }

            return listaNotas;
        }

        private NFCeInfo LerXmlNFCe(string caminhoArquivo)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(caminhoArquivo);

                NFCeInfo nfce = new NFCeInfo();
                nfce.CaminhoArquivo = caminhoArquivo;

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

                // Buscar chave de acesso
                XmlNode nodeChave = xmlDoc.SelectSingleNode("//nfe:infNFe", nsmgr);
                if (nodeChave != null && nodeChave.Attributes["Id"] != null)
                {
                    string idNFe = nodeChave.Attributes["Id"].Value;
                    nfce.ChaveAcesso = idNFe.Replace("NFe", "").Trim();
                }

                // Buscar série
                XmlNode nodeSerie = xmlDoc.SelectSingleNode("//nfe:ide/nfe:serie", nsmgr);
                if (nodeSerie != null)
                {
                    nfce.Serie = nodeSerie.InnerText;
                }

                // Buscar número da NFCe
                XmlNode nodeNumero = xmlDoc.SelectSingleNode("//nfe:ide/nfe:nNF", nsmgr);
                if (nodeNumero != null)
                {
                    nfce.NumeroNFCe = nodeNumero.InnerText;
                }

                // Buscar data de emissão
                XmlNode nodeData = xmlDoc.SelectSingleNode("//nfe:ide/nfe:dhEmi", nsmgr);
                if (nodeData != null)
                {
                    DateTime data;
                    if (DateTime.TryParse(nodeData.InnerText, out data))
                    {
                        nfce.DataEmissao = data;
                    }
                }

                // Buscar CFOP
                XmlNode nodeCFOP = xmlDoc.SelectSingleNode("//nfe:det/nfe:prod/nfe:CFOP", nsmgr);
                if (nodeCFOP != null)
                {
                    nfce.CFOP = nodeCFOP.InnerText;
                }

                // Buscar documento do destinatário (CNPJ ou CPF)
                XmlNode nodeCNPJ = xmlDoc.SelectSingleNode("//nfe:dest/nfe:CNPJ", nsmgr);
                XmlNode nodeCPF = xmlDoc.SelectSingleNode("//nfe:dest/nfe:CPF", nsmgr);
                
                if (nodeCNPJ != null)
                {
                    nfce.DocumentoDestinatario = nodeCNPJ.InnerText;
                    nfce.TipoDocumento = "CNPJ";
                }
                else if (nodeCPF != null)
                {
                    nfce.DocumentoDestinatario = nodeCPF.InnerText;
                    nfce.TipoDocumento = "CPF";
                }

                // Buscar valor total
                XmlNode nodeValorTotal = xmlDoc.SelectSingleNode("//nfe:total/nfe:ICMSTot/nfe:vNF", nsmgr);
                if (nodeValorTotal != null)
                {
                    decimal valor;
                    if (decimal.TryParse(nodeValorTotal.InnerText.Replace(".", ","), out valor))
                    {
                        nfce.ValorTotal = valor;
                    }
                }

                // Se não encontrou com namespace, tenta sem namespace (alguns XMLs podem não ter)
                if (string.IsNullOrEmpty(nfce.ChaveAcesso))
                {
                    nodeChave = xmlDoc.SelectSingleNode("//infNFe");
                    if (nodeChave != null && nodeChave.Attributes["Id"] != null)
                    {
                        string idNFe = nodeChave.Attributes["Id"].Value;
                        nfce.ChaveAcesso = idNFe.Replace("NFe", "").Trim();
                    }
                }

                if (string.IsNullOrEmpty(nfce.Serie))
                {
                    nodeSerie = xmlDoc.SelectSingleNode("//ide/serie");
                    if (nodeSerie != null)
                    {
                        nfce.Serie = nodeSerie.InnerText;
                    }
                }

                if (string.IsNullOrEmpty(nfce.NumeroNFCe))
                {
                    nodeNumero = xmlDoc.SelectSingleNode("//ide/nNF");
                    if (nodeNumero != null)
                    {
                        nfce.NumeroNFCe = nodeNumero.InnerText;
                    }
                }

                if (!nfce.DataEmissao.HasValue)
                {
                    nodeData = xmlDoc.SelectSingleNode("//ide/dhEmi");
                    if (nodeData != null)
                    {
                        DateTime data;
                        if (DateTime.TryParse(nodeData.InnerText, out data))
                        {
                            nfce.DataEmissao = data;
                        }
                    }
                }

                if (string.IsNullOrEmpty(nfce.CFOP))
                {
                    nodeCFOP = xmlDoc.SelectSingleNode("//det/prod/CFOP");
                    if (nodeCFOP != null)
                    {
                        nfce.CFOP = nodeCFOP.InnerText;
                    }
                }

                if (string.IsNullOrEmpty(nfce.DocumentoDestinatario))
                {
                    nodeCNPJ = xmlDoc.SelectSingleNode("//dest/CNPJ");
                    nodeCPF = xmlDoc.SelectSingleNode("//dest/CPF");
                    
                    if (nodeCNPJ != null)
                    {
                        nfce.DocumentoDestinatario = nodeCNPJ.InnerText;
                        nfce.TipoDocumento = "CNPJ";
                    }
                    else if (nodeCPF != null)
                    {
                        nfce.DocumentoDestinatario = nodeCPF.InnerText;
                        nfce.TipoDocumento = "CPF";
                    }
                }

                if (nfce.ValorTotal == 0)
                {
                    nodeValorTotal = xmlDoc.SelectSingleNode("//total/ICMSTot/vNF");
                    if (nodeValorTotal != null)
                    {
                        decimal valor;
                        if (decimal.TryParse(nodeValorTotal.InnerText.Replace(".", ","), out valor))
                        {
                            nfce.ValorTotal = valor;
                        }
                    }
                }

                return nfce;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ler XML: {ex.Message}");
            }
        }
    }
}
