using EventoRemember.Locais.Tests.Domain.Builders;

namespace EventoRemember.Locais.Tests.Domain.Tests
{
    public class FilaVoTests
    {
        [Fact(DisplayName = "Deve criar Fila com os critérios de aceite")]
        public void CriarFila()
        {
            //Arrange & Act
            var objeto = new FilaBuilder().ComCodigo("A").ComQtdDeCadeiras(20).Gerar();

            //Assert
            Assert.Equal("A", objeto.Value!.Codigo);
            Assert.Equal(20, objeto.Value!.QtdCadeiras);
        }

        [Theory(DisplayName = "Ao criar uma fila com código nulo deve lançar exceção")]
        [InlineData(null)]
        [InlineData("")]
        public void CriarFilaComCodigoNulo_DeveLancarExcecao(string nomeInvalido)
        {
            //Arrange & Act
            var filaRes = new FilaBuilder().ComCodigo(nomeInvalido).Gerar();

            //Assert
            Assert.Contains("Código da fila não pode ser vazio ou nulo", filaRes.Errors);
        }

        [Fact(DisplayName = "Ao criar uma fila com código inválido pelo tamanho deve lançar exceção")]
        public void CriarFilaComNomeInvalido_DeveLancarExcecao()
        {
            //Arrange & Act
            var filaRes = new FilaBuilder().ComCodigo("Aksah").Gerar();

            //Assert
            Assert.Contains("Código da fila precisa ter entre 1 a 3 caracteres.", filaRes.Errors);
        }

        [Theory(DisplayName = "Ao criar uma fila com quantidade de cadeiras menor do que 1 deve lançar exceção")]
        [InlineData(0)]
        [InlineData(201)]
        public void CriarFilaComQtdInvalida_DeveLancarExcecao(int quantidadeInvalida)
        {
            //Arrange & Act
            var filaRes = new FilaBuilder().ComQtdDeCadeiras(quantidadeInvalida).Gerar();

            //Assert
            Assert.Contains("Uma fila precisa conter ao menos uma cadeira e no máximo 200.", filaRes.Errors);
        }
    }
}
