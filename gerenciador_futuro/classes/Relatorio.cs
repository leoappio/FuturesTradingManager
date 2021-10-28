using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gerenciador_futuro.classes
{
    class Relatorio
    {
        public static double CalcularTotal(string dia1, string dia2)
        {
            DataTable dados = BancoDeDados.PegarTabelaPeriodo(dia1, dia2);
            double total = 0.0;
            for (int i = 0; i < dados.Rows.Count; i++)
            {

                total += double.Parse(dados.Rows[i]["Total"].ToString());
                total -= double.Parse(dados.Rows[i]["Custos"].ToString());
            }
            return total;

        }

        public static double CalcularWins(string dia1, string dia2)
        {
            DataTable dados = BancoDeDados.PegarTabelaPeriodo(dia1, dia2);
            double total = 0.0;
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                if (double.Parse(dados.Rows[i]["Total"].ToString()) >= 0)
                {
                    total += double.Parse(dados.Rows[i]["Total"].ToString());
                }

            }
            return total;
        }


        public static double CalcularLoss(string dia1, string dia2)
        {
            DataTable dados = BancoDeDados.PegarTabelaPeriodo(dia1, dia2);
            double total = 0.0;
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                if (double.Parse(dados.Rows[i]["Total"].ToString()) <= 0)
                {
                    total += double.Parse(dados.Rows[i]["Total"].ToString());
                }

            }
            return total;
        }


        public static int TotalContratos(string dia1, string dia2)
        {
            DataTable dados = BancoDeDados.PegarTabelaPeriodo(dia1, dia2);
            int total = 0;
            for (int i = 0; i < dados.Rows.Count; i++)
            {

                total += int.Parse(dados.Rows[i]["Contratos"].ToString());

            }
            return total;

        }

        public static double TotalTaxas(string dia1, string dia2)
        {
            DataTable dados = BancoDeDados.PegarTabelaPeriodo(dia1, dia2);
            double total = 0.0;
            for (int i = 0; i < dados.Rows.Count; i++)
            {

                total += double.Parse(dados.Rows[i]["Custos"].ToString());

            }
            return total;

        }
    }
}
