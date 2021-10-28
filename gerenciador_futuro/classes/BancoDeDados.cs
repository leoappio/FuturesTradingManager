using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace gerenciador_futuro.classes
{
    class BancoDeDados
    {
        public static string caminho = "Data Source = BancoDeDados.db";
        public SQLiteConnection conexao = new SQLiteConnection(caminho);
        private static BancoDeDados conexaoBanco = new BancoDeDados();


        public void Conectar()
        {
            conexao.Open();
        }
        public void Desconectar()
        {
            conexao.Close();
        }
        public static void ArmazenarDadosIniciais(string usuario, string nome, string senha)
        {
            
            conexaoBanco.Conectar();
            string sql = "INSERT INTO infoConta(usuario, nome, senha) VALUES ('"+usuario+"', '"+nome+"', '"+senha+"')";
            SQLiteCommand comando = new SQLiteCommand(sql, conexaoBanco.conexao);
            comando.ExecuteNonQuery();
            conexaoBanco.Desconectar();


        }
        public static void ArmazenarOperacao(string ativo,int contratos, double total, double custos, string diaAtual)
        {
            conexaoBanco.Conectar();
            string sql = "INSERT INTO operacoes(Dia, Ativo, Contratos, Total, Custos) VALUES ('"+diaAtual+"', '" + ativo + "', '" + contratos + "', '" + total + "', '"+custos+"')";
            SQLiteCommand comando = new SQLiteCommand(sql, conexaoBanco.conexao);
            comando.ExecuteNonQuery();
            conexaoBanco.Desconectar();

        }

        public static DataTable pegarTabelaOperacoesDeHoje()
        {
            string hoje = DateTime.Today.ToString("dd/MM/yyyy");
            conexaoBanco.Conectar();
            string sql = "SELECT * FROM operacoes WHERE Dia ='"+hoje+"'";
            SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
            DataTable operacoes = new DataTable();

            dados.Fill(operacoes);
            conexaoBanco.Desconectar();
            return operacoes;

        }

        public static void ArmazenarInfosMetas(string ganhos, string perdas, int diasOperados)
        {
            conexaoBanco.Conectar();
            string sql = "INSERT INTO infoMetas(Ganho, Perda, DiasOperados) VALUES ('" + ganhos + "', '" + perdas + "', '" + diasOperados +"')";
            SQLiteCommand comando = new SQLiteCommand(sql, conexaoBanco.conexao);
            comando.ExecuteNonQuery();
            conexaoBanco.Desconectar();
        }

        public static DataTable PegarInfoMetas()
        {
            conexaoBanco.Conectar();
            string sql = "SELECT * FROM infoMetas";
            SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
            DataTable infoMetas = new DataTable();

            dados.Fill(infoMetas);
            conexaoBanco.Desconectar();
            return infoMetas;
        }

        public static void ArmazenarSaldoAtual(double saldo)
        {

            conexaoBanco.Conectar();
            string sql = "UPDATE infoConta SET Saldo ='"+saldo+"'";
            SQLiteCommand comando = new SQLiteCommand(sql, conexaoBanco.conexao);
            comando.ExecuteNonQuery();
            conexaoBanco.Desconectar();
        }
        public static void AtualizarInfoMetas(string ganhos, string perdas, int diasOperados)
        {

            conexaoBanco.Conectar();
            string sql = "UPDATE infoMetas SET Ganho ='" + ganhos + "', Perda ='"+perdas+"', DiasOperados ='"+diasOperados+"'";
            SQLiteCommand comando = new SQLiteCommand(sql, conexaoBanco.conexao);
            comando.ExecuteNonQuery();
            conexaoBanco.Desconectar();
        }
        public static double PegarSaldoAtual()
        {
            conexaoBanco.Conectar();
            string sql = "SELECT saldo FROM infoConta";
            SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
            DataTable operacoes = new DataTable();
            dados.Fill(operacoes);
            double saldo = double.Parse(operacoes.Rows[0]["saldo"].ToString());
            conexaoBanco.Desconectar();
            return saldo;
        }

        public static void IncrementarSaldoTotal(double valorIncrementar)
        {
            double saldoAnterior = BancoDeDados.PegarSaldoAtual();

            double saldoNovo = saldoAnterior + valorIncrementar;

            BancoDeDados.ArmazenarSaldoAtual(saldoNovo);
        }

        public static void ArmazenarMetas(double metaWin, double MetaLoss)
        {
            string hoje = DateTime.Today.ToString("dd/MM/yyyy");

            DataTable dadosmetas = BancoDeDados.PegarDadosMetas();
            conexaoBanco.Conectar();
            string sql = "";

            if (dadosmetas.Rows.Count > 0)
            {

                sql = "UPDATE metasPorDia SET MetaWin ='" + metaWin + "', MaxLoss ='" + MetaLoss + "' WHERE Dia ='" + hoje + "'";

            }
            else
            {
                sql = "INSERT INTO metasPorDia(Dia, MetaWin, MaxLoss) VALUES ('" + hoje + "', '" + metaWin + "', '" + MetaLoss + "')";
            }

            SQLiteCommand comando = new SQLiteCommand(sql, conexaoBanco.conexao);
            comando.ExecuteNonQuery();
            conexaoBanco.Desconectar();

        }

        public static DataTable PegarDadosMetas()
        {
            string hoje = DateTime.Today.ToString("dd/MM/yyyy");

            conexaoBanco.Conectar();
            string sql = "SELECT * FROM metasPorDia WHERE Dia ='" + hoje + "'";
            SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
            DataTable dadosMetas = new DataTable();

            dados.Fill(dadosMetas);
            conexaoBanco.Desconectar();
            return dadosMetas;
        }

        public static DataTable PegarTabelaPeriodo(string dia1, string dia2)
        {
            conexaoBanco.Conectar();
            string sql = "SELECT * FROM operacoes WHERE Dia >='"+dia1+"' AND Dia<='"+dia2+"'";
            SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
            DataTable dadosOperacoes = new DataTable();
            dados.Fill(dadosOperacoes);

            return dadosOperacoes;
            
        }


        public static DataTable PegarTodasOperacoes()
        {
            conexaoBanco.Conectar();
            string sql = "SELECT * FROM operacoes";
            SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
            DataTable operacoes = new DataTable();

            dados.Fill(operacoes);
            conexaoBanco.Desconectar();
            return operacoes;
        }

        public static void ApagarOperacao(int numero)
        {
            conexaoBanco.Conectar();

            var cmd = new SQLiteCommand(conexaoBanco.conexao);
            cmd.CommandText = "DELETE FROM operacoes Where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", numero);
            cmd.ExecuteNonQuery();

            conexaoBanco.Desconectar();
        }
    }

   
}
