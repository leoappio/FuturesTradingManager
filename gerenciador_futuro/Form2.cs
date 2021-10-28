using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gerenciador_futuro.classes;

namespace gerenciador_futuro
{
    public partial class Form2 : Form
    {
        public string Nome { get; set; }
        public Form2(string nome)
        {
            Nome = nome;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Olá, " + Nome;
            groupBox_periodo.Visible = false;
            dataGridView2.Visible = false;
            AtualizarGridview1();

            double total = OperacoesHoje.CalcularSaldoDoDia();
            double SaldoTotal = BancoDeDados.PegarSaldoAtual();
            
            label_total.Text = SaldoTotal.ToString("C2");
            label_saldohoje.Text = total.ToString("C2");
            button_salvar.Visible = false;

            DataTable infoMetas = BancoDeDados.PegarInfoMetas();
            double saldoAtual = BancoDeDados.PegarSaldoAtual();

            if (infoMetas.Rows.Count > 0)
            {
                double metaWin = double.Parse(infoMetas.Rows[0]["Ganho"].ToString());
                double metaPerda = double.Parse(infoMetas.Rows[0]["Perda"].ToString());
                int diasOperados = int.Parse(infoMetas.Rows[0]["DiasOperados"].ToString());

                textBox1.Text = metaWin.ToString();
                textBox6.Text = metaPerda.ToString();
                numericUpDown1.Value = diasOperados;
                textBox2.Text = saldoAtual.ToString("N2");
                OperacoesHoje.CalcularMetas();
                MostrarMetas();
                AtualizarMetas();

            }
            else
            {
                MessageBox.Show("Vá na aba configurações e adicione suas metas e saldo! Bons Lucros!", "Seja Bem-Vindo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            DataTable todasOperações = BancoDeDados.PegarTodasOperacoes();
            dataGridView3.DataSource = todasOperações;

        }

        private void AtualizarMetas()
        {
            DataTable dadosMetas = BancoDeDados.PegarDadosMetas();
            double totalDia = OperacoesHoje.CalcularSaldoDoDia();

            if (dadosMetas.Rows.Count > 0)
            {
                if (totalDia > 0)
                {
                    var metaWin = dadosMetas.Rows[0]["MetaWin"].ToString();
                    var maxLoss = dadosMetas.Rows[0]["MaxLoss"].ToString();

                    double metaWinDouble = double.Parse(metaWin);
                    double metaLossDouble = double.Parse(maxLoss);

                    label9.Text = "R$ " + totalDia.ToString("N2") + "/ R$" + metaWinDouble.ToString("N2");
                    label13.Text = "R$ 00,00 / R$" + metaLossDouble.ToString("N2");

                    int barraWin = Convert.ToInt32(totalDia / metaWinDouble * 100);

                    if (barraWin <= 100)
                    {
                        progressBar1.Value = barraWin;
                        progressBar2.Value = 0;
                    }
                    else
                    {

                        progressBar1.Value = 100;
                        progressBar2.Value = 0;
                        MessageBox.Show("Você bateu a meta do dia :) Parabéns!!!!! ", "Meta Atingida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var metaWin = dadosMetas.Rows[0]["MetaWin"].ToString();
                    var maxLoss = dadosMetas.Rows[0]["MaxLoss"].ToString();

                    double metaWinDouble = double.Parse(metaWin);
                    double metaLossDouble = double.Parse(maxLoss);

                    label9.Text = "R$ 00,00 /R$" + metaWinDouble.ToString("N2");
                    label13.Text = "R$  "+totalDia.ToString("N2")+ "/R$" + metaLossDouble.ToString("N2");

                    int barraLoss = Convert.ToInt32(totalDia / metaLossDouble * 100)*-1;

                    if (barraLoss <= 100)
                    {
                        progressBar2.Value = barraLoss;
                        progressBar1.Value = 0;
                    }
                    else
                    {

                        progressBar1.Value = 0;
                        progressBar2.Value = 100;
                        MessageBox.Show("Você atingiu seu stop loss diário :( ", "Stop Loss atingido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                OperacoesHoje.CalcularMetas();
                DataTable dadosMetasNovo = BancoDeDados.PegarDadosMetas();

                if (totalDia > 0)
                {
                    var metaWin = dadosMetasNovo.Rows[0]["MetaWin"].ToString();
                    var maxLoss = dadosMetasNovo.Rows[0]["MaxLoss"].ToString();

                    double metaWinDouble = double.Parse(metaWin);
                    double metaLossDouble = double.Parse(maxLoss);

                    label9.Text = "R$ " + totalDia.ToString("N2") + "/ R$" + metaWinDouble.ToString("N2");
                    label13.Text = "R$ 00,00 / R$" + metaLossDouble.ToString("N2");

                    int barraWin = Convert.ToInt32(totalDia / metaWinDouble * 100);

                    if (barraWin <= 100)
                    {
                        progressBar1.Value = barraWin;
                        progressBar2.Value = 0;
                    }
                    else
                    {

                        progressBar1.Value = 100;
                        progressBar2.Value = 0;
                        MessageBox.Show("Você bateu a meta do dia :) Parabéns!!!!! ", "Meta Atingida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var metaWin = dadosMetasNovo.Rows[0]["MetaWin"].ToString();
                    var maxLoss = dadosMetasNovo.Rows[0]["MaxLoss"].ToString();

                    double metaWinDouble = double.Parse(metaWin);
                    double metaLossDouble = double.Parse(maxLoss);

                    label9.Text = "R$ 00,00/ R$" + metaWinDouble.ToString("N2");
                    label13.Text = "R$  " + totalDia.ToString("N2") + "/R$" + metaLossDouble.ToString("N2");

                    int barraLoss = Convert.ToInt32(totalDia / metaLossDouble * 100) * -1;

                    if (barraLoss <= 100)
                    {
                        progressBar2.Value = barraLoss;
                        progressBar1.Value = 0;
                    }
                    else
                    {
                        progressBar1.Value = 0;
                        progressBar2.Value = 100;
                        MessageBox.Show("Você atingiu seu stop loss diário :( ", "Stop Loss atingido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button_salvar.Visible = true;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox_periodo.Visible = true;
            dataGridView2.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox_periodo.Visible = false;
            dataGridView2.Visible = true;

            DataTable dadosHoje = BancoDeDados.pegarTabelaOperacoesDeHoje();
            AtualizarGridView2(dadosHoje);
            string hoje = DateTime.Today.ToString("dd/MM/yyyy");
            double total = Relatorio.CalcularTotal(hoje, hoje);
            double Wins = Relatorio.CalcularWins(hoje, hoje);
            double Loss = Relatorio.CalcularLoss(hoje, hoje);
            int Contratos = Relatorio.TotalContratos(hoje, hoje);
            double Taxas = Relatorio.TotalTaxas(hoje, hoje);
            label10.Text = total.ToString("C2");
            label11.Text = Wins.ToString("C2");
            label12.Text = Loss.ToString("C2");
            label14.Text = Contratos.ToString();
            label15.Text = Taxas.ToString("C2");



        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox_periodo.Visible = false;
            dataGridView2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarMetas();

            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                if (double.Parse(textBox5.Text) >= 0.0)
                {
                    string ativo = comboBox1.Text;
                    int contratos = int.Parse(textBox3.Text);
                    double total = double.Parse(textBox4.Text);
                    double custos = double.Parse(textBox5.Text);
                    string diaAtual = DateTime.Today.ToString("dd/MM/yyyy");
                    BancoDeDados.ArmazenarOperacao(ativo, contratos, total, custos, diaAtual);
                    double valorLiquido = total - custos;
                    BancoDeDados.IncrementarSaldoTotal(valorLiquido);
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    double totalDoDia = OperacoesHoje.CalcularSaldoDoDia();
                    double SaldoTotal = BancoDeDados.PegarSaldoAtual();

                    label_total.Text = SaldoTotal.ToString("C2");
                    label_saldohoje.Text = totalDoDia.ToString("C2");
                    AtualizarMetas();
                    AtualizarGridview1();

                }
                else
                {
                    MessageBox.Show("O valor da corretagem não pode ser negativo", "Valor incorreto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            else
            {
                MessageBox.Show("Preencha todos os dados antes de adicionar uma entrada", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void AtualizarGridview1()
        {
            DataTable operacoes = BancoDeDados.pegarTabelaOperacoesDeHoje();
            dataGridView1.DataSource = operacoes;


        }

        private void AtualizarGridView2(DataTable dados)
        {
            dataGridView2.DataSource = dados;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button_salvar.Visible = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            button_salvar.Visible = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            button_salvar.Visible = true;
        }

        private void button_salvar_Click(object sender, EventArgs e)
        {

            if(textBox1.Text != "" && textBox6.Text!="" && textBox2.Text != "")
            {
                string ganhos =textBox1.Text;
                string perdas =textBox6.Text;
                int diasOperados = int.Parse(numericUpDown1.Value.ToString());
                double saldo = double.Parse(textBox2.Text);

                if (BancoDeDados.PegarInfoMetas().Rows.Count > 0)
                {
                    OperacoesHoje.CalcularMetas();
                    BancoDeDados.AtualizarInfoMetas(ganhos, perdas, diasOperados);
                    BancoDeDados.ArmazenarSaldoAtual(saldo);
                    MostrarMetas();
                    AtualizarMetas();

                }
                else
                {
                    BancoDeDados.ArmazenarInfosMetas(ganhos, perdas, diasOperados);
                    BancoDeDados.ArmazenarSaldoAtual(saldo);
                }
                MessageBox.Show("Dados Atualizados com Sucesso", "Entrada adicionada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Preencha todos os dados necessários", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void MostrarMetas()
        {
            DataTable metasinfos = BancoDeDados.PegarInfoMetas();

            if (metasinfos.Rows.Count > 0)
            {
                var metaWin = metasinfos.Rows[0]["Ganho"].ToString();
                var maxLoss = metasinfos.Rows[0]["Perda"].ToString();

                label_ganhos.Text = "Ganhos (" + metaWin + "%)";
                label_perdas.Text = "Perdas (" + maxLoss + "%)";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable todasOperações = BancoDeDados.PegarTodasOperacoes();
            dataGridView3.DataSource = todasOperações;
            DataTable metasinfos = BancoDeDados.PegarInfoMetas();
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            double totalDoDia = OperacoesHoje.CalcularSaldoDoDia();
            double SaldoTotal = BancoDeDados.PegarSaldoAtual();

            label_total.Text = SaldoTotal.ToString("C2");
            label_saldohoje.Text = totalDoDia.ToString("C2");
            AtualizarGridview1();

            double saldoAtual = BancoDeDados.PegarSaldoAtual();

            if (metasinfos.Rows.Count > 0)
            {
                double metaWin = double.Parse(metasinfos.Rows[0]["Ganho"].ToString());
                double metaPerda = double.Parse(metasinfos.Rows[0]["Perda"].ToString());
                int diasOperados = int.Parse(metasinfos.Rows[0]["DiasOperados"].ToString());

                textBox1.Text = metaWin.ToString();
                textBox6.Text = metaPerda.ToString();
                numericUpDown1.Value = diasOperados;
                textBox2.Text = saldoAtual.ToString("N2");

            }
            else
            {
                MessageBox.Show("Vá na aba configurações e adicione suas metas e saldo! Bons Lucros!", "Seja Bem-Vindo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            MostrarMetas();
            AtualizarMetas();


        }

        private void Gerenciamento_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;

            string dia1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string dia2 = dateTimePicker2.Value.ToString("dd/MM/yyyy");

            DataTable dados = BancoDeDados.PegarTabelaPeriodo(dia1, dia2);
            double total = Relatorio.CalcularTotal(dia1,dia2);
            double Wins = Relatorio.CalcularWins(dia1,dia2);
            double Loss = Relatorio.CalcularLoss(dia1, dia2);
            int Contratos = Relatorio.TotalContratos(dia1, dia2);
            double Taxas = Relatorio.TotalTaxas(dia1, dia2);
            label10.Text = total.ToString("C2");
            label11.Text = Wins.ToString("C2");
            label12.Text = Loss.ToString("C2");
            label14.Text = Contratos.ToString();
            label15.Text = Taxas.ToString("C2");
            AtualizarGridView2(dados);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(textBox7.Text);
            DataTable todasOperações = BancoDeDados.PegarTodasOperacoes();

            if (todasOperações.Rows.Count > 0)
            {
                BancoDeDados.ApagarOperacao(numero);
                DataTable todasOperaçõesNOVO = BancoDeDados.PegarTodasOperacoes();
                dataGridView3.DataSource = todasOperaçõesNOVO;
                AtualizarMetas();
                AtualizarGridview1();
            }
            else
            {
                MessageBox.Show("Nenhuma operação foi encontrada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
