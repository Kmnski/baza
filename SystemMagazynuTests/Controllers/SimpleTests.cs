using SystemMagazynu.DTOs;
using Xunit;

namespace SystemMagazynu.Tests
{
    public class SimpleTests
    {
        [Fact]
        public void DokumentCreateDto_WithEmptyType_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var dto = new DokumentCreateDto();

            // Assert
            Assert.Equal("oczekuj¹cy", dto.Status);
            Assert.Empty(dto.NumerDokumentu);
            Assert.Empty(dto.Pozycje);
        }

        [Fact]
        public void PozycjaCreateDto_Properties_ShouldWorkCorrectly()
        {
            // Arrange & Act
            var pozycja = new PozycjaDokumentuCreateDto
            {
                MaterialId = 1,
                Ilosc = 10.5m
            };

            // Assert
            Assert.Equal(1, pozycja.MaterialId);
            Assert.Equal(10.5m, pozycja.Ilosc);
        }

        [Fact]
        public void DokumentResponseDto_WithNullProperties_ShouldHandleNulls()
        {
            // Arrange & Act
            var dto = new DokumentResponseDto();

            // Assert
            Assert.Empty(dto.Typ);
            Assert.Empty(dto.Status);
            Assert.Empty(dto.DostawcaNazwa);
            Assert.Empty(dto.OdbiorcaNazwa);
            Assert.Empty(dto.Pozycje);
        }
    }
}