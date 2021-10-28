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
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace gerenciador_futuro
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "zNfZbw0KagXy61sCwHx904ZJtvDRxAQSO8eVMHs3",
            BasePath = "https://gerenciador-4ed2d.firebaseio.com/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();

        }

        private async void button_entrar_Click(object sender, EventArgs e)
        {
            button_entrar.Text = "Validando os dados...";

            if (textBox_user.Text != "" && textBox_senha.Text != "")
            {
                FirebaseResponse resultado = await client.GetTaskAsync("contas/" + textBox_user.Text);

                try
                {
                    Conta conta = resultado.ResultAs<Conta>();

                    bool senhaValida = conta.ValidarSenha(textBox_senha.Text);

                    if (senhaValida)
                    {
                        if(string.IsNullOrEmpty(conta.MacAdress) == false || string.IsNullOrWhiteSpace(conta.MacAdress) == false)
                        {
                            if (Conta.GetPrimeiroEnderecoMAC() == conta.MacAdress)
                            {
                                bool teste = conta.ValidarDiaTeste();
                                if (teste)
                                {
                                    if (!DadosJaArmazenados)
                                    {
                                        BancoDeDados.ArmazenarDadosIniciais(conta.Usuario, conta.Nome, conta.Senha);
                                    }
                                    button_entrar.Text = "entrando...";
                                    Hide();
                                    var form_novo = new Form2(conta.Nome);
                                    form_novo.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Seu período de teste acabou, para contratar esse serviço entre em contato em (48)99630-8551");
                                    button_entrar.Text = "Entrar";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Esta conta pertence a outro computador, adquira uma licença para utlizar este software");
                            }

                        }
                        else
                        {
                            conta.MacAdress = Conta.GetPrimeiroEnderecoMAC();
                            FirebaseResponse response = await client.UpdateTaskAsync("contas/"+textBox_user.Text, conta);
                            Conta result = response.ResultAs<Conta>();

                            bool teste = conta.ValidarDiaTeste();
                            if (teste)
                            {
                                if (!DadosJaArmazenados)
                                {
                                    BancoDeDados.ArmazenarDadosIniciais(conta.Usuario, conta.Nome, conta.Senha);
                                }

                                button_entrar.Text = "entrando...";
                                Hide();
                                var form_novo = new Form2(conta.Nome);
                                form_novo.Show();

                            }
                            else
                            {
                                MessageBox.Show("Seu período de teste acabou, para contratar esse serviço entre em contato em (48)99630-8551");
                                button_entrar.Text = "Entrar";
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Senha Inválida!");
                        button_entrar.Text = "Entrar";
                    }
                }
                catch
                {
                    button_entrar.Text = "Entrar";
                }
            }
            else
            {
                button_entrar.Text = "preencha todos os campos";
            }
        }
        public bool DadosJaArmazenados = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            BancoDeDados conexaoBanco = new BancoDeDados();

            try
            {
                conexaoBanco.Conectar();
                string sql = "SELECT * FROM infoConta";
                SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexaoBanco.conexao);
                DataTable infos = new DataTable();

                dados.Fill(infos);// passando os dados para o datatable infos

                if (infos.Rows.Count <= 0)
                {
                    DadosJaArmazenados = false;
                }
                else
                {
                    DadosJaArmazenados = true;
                    textBox_user.Text = infos.Rows[0]["usuario"].ToString();
                    textBox_senha.Text = infos.Rows[0]["senha"].ToString();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString(), "erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (client == null)
            {
                MessageBox.Show("Algo deu errado, cheque sua conexão com a internet");
            }
        }

    }
}
