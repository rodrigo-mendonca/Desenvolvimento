using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PGM.SQL.Models;
using PGM.SQL.Repositories;
using PGM.Extensions.FoxPro;
using PGM.Sys;
using PGM.Interfaces;

namespace PGM.Extensions.Pgm
{
    public static class Pgm
    {
        /// <summary>
        /// Cor padrão para campos obrigatorios
        /// </summary>
        /// <returns>Retorna o objeto da cor</returns>
        public static Color Obrigatory(this Color tColor)
        {
            return Color.FromArgb(255,255,255,225);
        }

        /// <summary>
        /// Verifica se a Data é útil ou não,
        /// País padrão (BRA)
        /// </summary>
        /// <returns>Retorna True Se for útil e false para não útil</returns>
        public static bool VDiaUtil(this DateTime tDate)
        {
            return tDate.VDiaUtil("BRA");
        }

        /// <summary>
        /// Verifica se a Data é útil ou não,
        /// País padrão (BRA)
        /// </summary>
        /// <param name="Pais">Código do pais</param>
        /// <returns>Retorna True Se for útil e false para não útil</returns>
        public static bool VDiaUtil(this DateTime tDate, string Pais)
        {
            bool lUtil = false;

            // ignora finais de semana
            if (!(tDate.DayOfWeek == DayOfWeek.Saturday || tDate.DayOfWeek == DayOfWeek.Sunday))
            {
                SysRepository<Feriado> oRepo = (SysRepository<Feriado>)PgmInjector.GetInstance<IRepository<Feriado>>();
                // busca o feriado só com o dia e o mês
                IList<Feriado> oFer = oRepo.Where(
                        f => 
                            f.DtFeriado.Day   == tDate.Day 
                        && 
                            f.DtFeriado.Month == tDate.Month
                        &&
                            (f.DtFeriado.Year  == tDate.Year || f.Fixo == 2)
                        &&
                            f.FkPais          == Pais
                        );
                // se encontrar algum registro, então é feriado
                lUtil = oFer.Count == 0;
            }

            return lUtil;
        }

        /// <summary>
        /// Avança ou retorna n dias uteis apartir de uma data,
        /// País padrão (BRA)
        /// </summary>
        /// <param name="tDays">Número de dias para avançar ou retornar</param>
        /// <returns>Retorna a data encontrada</returns>
        public static DateTime ProxUtil(this DateTime tDate,int tDays)
        {
            return tDate.ProxUtil(tDays, "BRA");
        }

        /// <summary>
        /// Avança ou retorna n dias uteis apartir de uma data,
        /// País padrão (BRA)
        /// </summary>
        /// <param name="tDays">Número de dias para avançar ou retornar</param>
        /// <param name="Pais">Código do pais</param>
        /// <returns>Retorna a data encontrada</returns>
        public static DateTime ProxUtil(this DateTime tDate, int tDays,string Pais)
        {
            DateTime dUtil = tDate;
            int nCount = tDays;
            int nStep = tDays >= 0 ? 1 : -1; // verifica se precisa avançar ou retornar
            
            // Enquanto não zerar o numero de dias
            while (!(nCount==0))
            {
                dUtil = dUtil.AddDays(nStep);
                // para cada dia util encontrado, subtrair um do contador
                if (dUtil.VDiaUtil(Pais))
                    nCount -= nStep;            
            }

            return dUtil;
        }

        /// <summary>
        /// Conta quantos dias uteis existem até uma data,
        /// País padrão (BRA)
        /// </summary>
        /// <param name="tAte">Data futura para fazer a contagem</param>
        /// <returns>Retorna o numero de dias do periodo</returns>
        public static int ContaUteis(this DateTime tDate, DateTime tAte)
        {
            return tDate.ContaUteis(tAte, "BRA");
        }

        /// <summary>
        /// Conta quantos dias uteis existem até uma data,
        /// País padrão (BRA)
        /// </summary>
        /// <param name="tAte">Data futura para fazer a contagem</param>
        /// <param name="Pais">Código do pais</param>
        /// <returns>Retorna o numero de dias do periodo</returns>
        public static int ContaUteis(this DateTime tDate, DateTime tAte,string tPais)
        {
            int nUteis = 0;
            DateTime dData = tDate;
            // A data "Ate" não pode ser menor que a atual
            if (dData < tAte)
            {   
                // enquando não for igual
                while (dData!= tAte)
                {
                    // adiciona um dia e verifica se é util
                    dData = dData.AddDays(1);
                    if (dData.VDiaUtil(tPais))
                        nUteis++;
                }
            }

            return nUteis;
        }

        /// <summary>
        /// Completa o numero com zeros a esquerda
        /// </summary>
        /// <param name="tTam">Tamanho maximo da string</param>
        /// <returns>Retorna o numero como string</returns>
        public static string StrZero(this int tNum, int tTam)
        {
            return tNum.ToString("0".Replicate(tTam)); ;
        }
        /// <summary>
        /// Transforma o RGB em um numero unico
        /// </summary>
        /// <param name="tR">Vermelho</param>
        /// <param name="tG">Verde</param>
        /// <param name="tB">Azul</param>
        /// <returns>retorna a color como inteiro</returns>
        public static int ToRgb(this int tNum, int tR,int tG,int tB)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)tR;
            bytes[1] = (byte)tG;
            bytes[2] = (byte)tB;

            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Criptografa uma string de acordo com o usuario logado
        /// </summary>
        /// <param name="tStr">String que será criptografada</param>
        /// <param name="tSeed">Semente usada para a criptocrafia</param>
        /// <returns>Retorna a string criptografada</returns>
        public static string Encrypt(this string tStr)
        {
            return tStr.Encrypt(' ');
        }
        /// <summary>
        /// Criptografa uma string de acordo com o usuario logado
        /// </summary>
        /// <param name="tStr">String que será criptografada</param>
        /// <param name="tSeed">Semente usada para a criptocrafia</param>
        /// <returns>Retorna a string criptografada</returns>
        public static string Encrypt(this string tStr, char tSeed)
        {
            Random oRan = new Random();
            int nSeed = 0, nResto;

            // se não tiver passado uma semente, usa do usuario conectado
            if (tSeed == ' ')
                tSeed = Convert.ToChar(PgmGlobal.UserPassCurrent.Left(1));
            // se não tiver usuario logado, cria a semente
            if (tSeed == ' ')
            {
                nSeed = Convert.ToInt32(25 * oRan.NextDouble());
                tSeed = ((char)(64 + nSeed)) ;
            }
            nResto = ((int)tSeed % 10) + 1;

            string cEncrypt = tSeed.ToString();
            char cPart;
            int nLetter;

            for (int i = 0; i < 30; i++)
            {
                nLetter = 0;
                if (tStr.Length > i)
                    nLetter = (int)Convert.ToChar(tStr.Substring(i, 1));

                nLetter = nLetter + (i+1) + nResto;
                cPart = (char) ((nLetter % 220) + 14);

                if((int)cPart == 39)
                    cPart = (char)5;

                nResto = (nLetter % 32) + 1;

                cEncrypt+=cPart.ToString();
            }
            return cEncrypt;
        }
    }
}
