using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gerenciador_futuro.classes
{
    class OperacoesHoje
    {
        public static double CalcularSaldoDoDia()
        {
            DataTable operacoes = BancoDeDados.pegarTabelaOperacoesDeHoje();

            double total = 0.0;

            for(int i = 0; i< operacoes.Rows.Count; i++)
            {
                total += double.Parse(operacoes.Rows[i]["Total"].ToString());
                total -= double.Parse(operacoes.Rows[i]["Custos"].ToString());
            }

            return total;

        }
        public static void CalcularMetas()
        {
            double saldo = BancoDeDados.PegarSaldoAtual();

            DataTable dadosInfo = BancoDeDados.PegarInfoMetas();
            if (dadosInfo.Rows.Count > 0)
            {
                var metaWinPorcentagem = dadosInfo.Rows[0]["Ganho"].ToString();

                double metaDecimal = double.Parse(metaWinPorcentagem) / 100;

                double valorMetaWin = saldo * metaDecimal;


                var maxPerdaPorcentagem = dadosInfo.Rows[0]["Perda"].ToString();

                double metaLossDecimal = double.Parse(maxPerdaPorcentagem) / 100;

                double valorMetaLoss = saldo * metaLossDecimal;

                BancoDeDados.ArmazenarMetas(valorMetaWin, valorMetaLoss);
            }
            

        }
      
    }
}
