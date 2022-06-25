namespace WinFormsAppDesafioGaragem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Persistencia.lerArquivoEntrada(listaEntrada);
            Persistencia.lerArquivoSaida(listaSaida);
            preenchelstEntrada();
            preenchelstSaida();
        }

        List<Veiculo> listaEntrada = new List<Veiculo>();
        List<Veiculo> listaSaida = new List<Veiculo>();

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void preenchelstEntrada()
        {
            //limpa a lista de entrada
            lstEntrada.Items.Clear();

            //percorre a lista entrada e preenche o listbox
            foreach (var item in listaEntrada)
            {
                lstEntrada.Items.Add(item.Placa + " - " + item.DataEntrada + " - " + item.HoraEntrada);
            }
        }

        private void preenchelstSaida()
        {
            //limpa a lista de Saida
            //lstSaida.Items.Clear();

            //percorre a lista Saida e preenche o listbox
            foreach (var item in listaSaida)
            {
                lstSaida.Items.Add(item.Placa + " - " + item.TempoPermanecia + " minuto(s) - R$ " + item.ValorCobrado);
            }
        }
        private bool pesquisaVeiculo(string placa)
        {
            foreach (var veiculo in listaEntrada)
            {
                if (placa == veiculo.Placa)
                    return true;
            }
            return false;
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            Veiculo veiculo = new Veiculo(txtPlacaEntrada.Text);
            txtPlacaEntrada.Text = "";
            //gerarDataHora(string tipo)
            //pesquisa se a placa ja existe na entrada
            var existe = pesquisaVeiculo(veiculo.Placa);
            if (existe)
            {
                MessageBox.Show("A placa ja existe!");
                return;
            }
            veiculo.gerarDataHora("entrada");
            if (!Veiculo.temLugar(listaEntrada,50))
            {
                MessageBox.Show("Garagem cheia");
                return;
            }
            // adiciona o objeto veiculo na lista de veiculos
            listaEntrada.Add(veiculo);
           //preenche a lista de entrada
            preenchelstEntrada();

            Persistencia.gravarNoArquivoEntrada(listaEntrada);

        }

        private void btnSaida_Click(object sender, EventArgs e)
        {
            var posicao = Veiculo.localizado(txtPlacaSaida.Text, listaEntrada);
            txtPlacaSaida.Text = "";
            Veiculo veiculo = listaEntrada[posicao];

            veiculo.gerarDataHora("saida");

            veiculo.realizarCobranca(5.00);

            MessageBox.Show("Valor cobrado: "+veiculo.ValorCobrado);

            listaEntrada.Remove(veiculo);

            preenchelstEntrada();

            listaSaida.Add(veiculo);

            listaEntrada.Remove(veiculo);

            lstSaida.Items.Add(veiculo.Placa 
                + " - " + veiculo.TempoPermanecia
                + " minuto(s) - R$ " + veiculo.ValorCobrado);

            Persistencia.gravarNoArquivoSaida(listaSaida);

            Persistencia.gravarNoArquivoEntrada(listaEntrada);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}