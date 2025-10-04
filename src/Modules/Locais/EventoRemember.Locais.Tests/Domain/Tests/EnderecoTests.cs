using EventoRemember.Locais.Tests.Domain.Builders;

namespace EventoRemember.Locais.Tests.Domain.Tests
{
    public class EnderecoTests
    {
        [Fact(DisplayName = "Deve criar um endereço conforme os critérios de aceite")]
        public void DeveCriarEndereco() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().Gerar();

            //Assert
            Assert.True(enderecoRes.IsSuccess);
        }

        [Fact(DisplayName = "Criar um endereço comn logradouro válido deve passar")]
        public void CriarEnderecoComLogradouroValido_DevePassar() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComLogradouro("Avenida Brasília").Gerar();

            //Assert
            Assert.True(enderecoRes.IsSuccess);
            Assert.Equal("Avenida Brasília", enderecoRes.Value!.Logradouro);
        }

        [Theory(DisplayName = "Criar um endereço com logradouro nulo ou vazio deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComLogradouroNuloVazio_DeveGerarResultFailure(string logradouroInvalido) 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComLogradouro(logradouroInvalido).Gerar();

            //Assert
            Assert.Contains("Logradouro não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com logradouro com tamanho inválido deve gerar lista de erros.")]
        public void CriarEnderecoComLogradouroTamanhoInvalido_DeveGerarResultFailure() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComLogradouro("As").Gerar();

            //Assert
            Assert.Contains("Logradouro deve possuir entre 5 e 50 de caracteres", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com número válido deve passar")]
        public void CriarEnderecoComNumeroValido_DevePassar() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComNumero("1001").Gerar();

            //Assert
            Assert.True(enderecoRes.IsSuccess);
            Assert.Equal("1001", enderecoRes.Value!.Numero);
        }

        [Theory(DisplayName = "Criar um endereço com número nulo ou vazio deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComNumeroNuloVazio_DeveGerarResultFailure(string numInvalido) 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComNumero(numInvalido).Gerar();

            //Assert
            Assert.Contains("Número do local não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com número usando não-digítos deve gerar lista de erros.")]
        public void CriarEnderecoComNumeroSemDigitos_DeveGerarResultFailure() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComNumero("Af1").Gerar();

            //Assert
            Assert.Contains("Número do local deve conter apenas números", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com número muito grande deve gerar lista de erros.")]
        public void CriarEnderecoComNumeroGrande_DeveGerarResultFailure() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComNumero("28823817427").Gerar();

            //Assert
            Assert.Contains("Número do local é muito grande", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com complemento válido deve passar")]
        public void CriarEnderecoComComplementoValido_DevePassar() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComComplemento("Bloco ABC 5").Gerar();

            //Assert
            Assert.Equal("Bloco ABC 5", enderecoRes.Value!.Complemento);
        }

        [Fact(DisplayName = "Criar um endereço com complemento com tamanho inválido deve gerar lista de erros.")]
        public void CriarEnderecoComComplementoComTamanhoInvalido_DeveGerarResultFailure() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComComplemento("Ansad wquhdwq uwhdquwhe weuqhiud qwds").Gerar();

            //Assert
            Assert.Contains("Complemento do local é muito grande", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um Endereço com bairro válido deve passar")]
        public void CriarEndereceoComBairroValido_DevePassar() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComBairro("Centro").Gerar();

            //Assert
            Assert.Equal("Centro", enderecoRes.Value!.Bairro);
        }

        [Theory(DisplayName = "Criar um Endereço com bairro nulo ou vazio deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComBairroNuloVazio_DeveGerarResultFailure(string bairroInvalido) 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComBairro(bairroInvalido).Gerar();

            //Assert
            Assert.Contains("Bairro não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um Endereço com bairr com tamanho inválido deve gerar lista de erros.")]
        public void CriarEnderecoComBairroComTamanhoInvalido_DeveGerarResultFailure() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComBairro("Ansad wquhdwq uwhdquwhe weuqhiud qwds").Gerar();

            //Assert
            Assert.Contains("Bairro deve possuir entre 3 e 25 de caracteres", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um Endereço com cidade válida deve passar")]
        public void CriarEnderecoComCidadeValida_DevePassar() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComCidade("Belo Horizonte").Gerar();

            //Assert
            Assert.Equal("Belo Horizonte", enderecoRes.Value!.Cidade);
        }

        [Theory(DisplayName = "Criar um Endereço com cidade nula ou vazia deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComCidadeNulaVazia_DeveGerarResultFailure(string cidadeInvalida) 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComCidade(cidadeInvalida).Gerar();
            
            //Assert
            Assert.Contains("Cidade não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com cidade com tamanho inválido deve gerar lista de erros.")]
        public void CriarEnderecoComCidadeComTamanhoInvalido_DeveGerarResultFailure() 
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComCidade("Snsdhadhduqwhd nwqjdnwndjnwqjd qwdnqwe").Gerar();

            //Assert
            Assert.Contains("Cidade deve possuir entre 3 e 30 de caracteres", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um Endereço com UF válida deve passar")]
        public void CriarEnderecoComUfValida_DevePassar()
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComUf("Minas Gerais").Gerar();

            //Assert
            Assert.Equal("Minas Gerais", enderecoRes.Value!.UF);
        }

        [Theory(DisplayName = "Criar um Endereço com UF nulo ou vazio deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComUFNuloVazio_DeveGerarResultFailure(string uFInvalida)
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComUf(uFInvalida).Gerar();

            //Assert
            Assert.Contains("Estado não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com UF inválido deve gerar lista de erros.")]
        public void CriarEnderecoComUfInvalido_DeveGerarResultFailure()
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComUf("Moon").Gerar();

            //Assert
            Assert.Contains("Estado inválido", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um Endereço com País válido deve passar")]
        public void CriarEnderecoComPaisValida_DevePassar()
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComPais("Brasil").Gerar();

            //Assert
            Assert.Equal("Brasil", enderecoRes.Value!.Pais);
        }

        [Theory(DisplayName = "Criar um Endereço com País nulo ou vazio deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComPaisNuloVazio_DeveGerarResultFailure(string paisInvalido)
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComPais(paisInvalido).Gerar();

            //Assert
            Assert.Contains("País não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um endereço com País inválido deve gerar lista de erros.")]
        public void CriarEnderecoComPaisInvalido_DeveGerarResultFailure()
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComPais("Mars").Gerar();

            //Assert
            Assert.Contains("Local precisa estar no Brasil", enderecoRes.Errors);
        }

        [Fact(DisplayName = "Criar um Endereço com Código Postal válido deve passar")]
        public void CriarEnderecoComCodigoPostalValida_DevePassar()
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComCodigoPostal("00111000").Gerar();

            //Assert
            Assert.Equal("00111000", enderecoRes.Value!.CodigoPostal);
        }

        [Theory(DisplayName = "Criar um Endereço com Código Postal nulo ou vazio deve gerar lista de erros.")]
        [InlineData("")]
        [InlineData(null)]
        public void CriarEnderecoComCodigoPostalNuloVazio_DeveGerarResultFailure(string paisInvalido)
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComPais(paisInvalido).Gerar();

            //Assert
            Assert.Contains("País não pode ser nulo ou vazio", enderecoRes.Errors);
        }

        [Theory(DisplayName = "Criar um endereço com Código Postal inválido deve gerar lista de erros.")]
        [InlineData("Abcdefgh")]
        [InlineData("001101000")]
        public void CriarEnderecoComCodigoPostalInvalido_DeveGerarResultFailure(string cpInvalido)
        {
            //Arrange & Act
            var enderecoRes = new EnderecoBuilder().ComCodigoPostal(cpInvalido).Gerar();

            //Assert
            Assert.Contains("CEP inválido", enderecoRes.Errors);
        }
    }
}
