namespace APICatalogo.Services
{
    public class MeuServico : IMeuServico
    {
        public string saudacao(string nome)
        {
            return $"Bem-Vindo, {nome} \n {DateTime.Now}";
        }
    }
}
