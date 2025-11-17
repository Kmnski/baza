using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemMagazynu.Controllers;
using SystemMagazynu.Data;
using SystemMagazynu.Models;
using Xunit;

namespace SystemMagazynu.Tests.Controllers
{
	public class OdbiorcaControllerInMemoryTests
	{
		[Fact]
		public async Task GetOdbiorcy()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<MagazynDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb_GetOdbiorcy")
				.Options;

			using var context = new MagazynDbContext(options);

			// Dodajemy testowych odbiorców
			context.Odbiorcy.AddRange(
				new Odbiorca { Nazwa = "Aktywny 1", Email = "a1@test.pl", CzyAktywny = true },
				new Odbiorca { Nazwa = "Aktywny 2", Email = "a2@test.pl", CzyAktywny = true },
				new Odbiorca { Nazwa = "Nieaktywny", Email = "inactive@test.pl", CzyAktywny = false }
			);
			await context.SaveChangesAsync();

			var controller = new OdbiorcaController(context);

			// Act
			var result = await controller.GetOdbiorcy();

			// Assert
			var odbiorcy = Assert.IsType<List<Odbiorca>>(result.Value);
			Assert.Equal(2, odbiorcy.Count);
			Assert.All(odbiorcy, o => Assert.True(o.CzyAktywny));
		}


		[Fact]
		public async Task PostOdbiorca()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<MagazynDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb_PostOdbiorca")
				.Options;

			using var context = new MagazynDbContext(options);
			var controller = new OdbiorcaController(context);

			var odbiorca = new Odbiorca
			{
				Nazwa = "Testowy Odbiorca",
				Email = "test@test.pl",
				Telefon = "123-456-789",
				Adres = "Testowy adres"
			};

			// Act
			var result = await controller.PostOdbiorca(odbiorca);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
			var returnedOdbiorca = Assert.IsType<Odbiorca>(createdResult.Value);
			Assert.Equal("Testowy Odbiorca", returnedOdbiorca.Nazwa);
			Assert.True(returnedOdbiorca.CzyAktywny);
		}
		[Fact]
		public async Task NoPostOdbiorca()
		{
			//Arrange
			var options = new DbContextOptionsBuilder<MagazynDbContext>()
			   .UseInMemoryDatabase(databaseName: "TestDb_NullName")
				.Options;
			using var context = new MagazynDbContext(options);
			var controller = new OdbiorcaController(context);

			var odbiorca = new Odbiorca
			{
				Nazwa = "Email odbiorcy jest wymagany",
				Email = "",
				Telefon = "123-456-789",
				Adres = "Testowy adres"
			};
			// Act
			var result = await controller.PostOdbiorca(odbiorca);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
			Assert.Contains("Email odbiorcy jest wymagany", badRequestResult.Value?.ToString());
		}
	}
}