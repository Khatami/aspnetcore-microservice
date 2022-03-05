using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
	public partial class OrderTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "ZipCode",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "State",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "Expiration",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "Country",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "CardNumber",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "CardName",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "CVV",
				table: "Orders",
				type: "nvarchar(4)",
				maxLength: 4,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(4)",
				oldMaxLength: 4);

			migrationBuilder.AlterColumn<string>(
				name: "AddressLine",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "ZipCode",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "State",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Expiration",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Country",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "CardNumber",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "CardName",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "CVV",
				table: "Orders",
				type: "nvarchar(4)",
				maxLength: 4,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(4)",
				oldMaxLength: 4,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "AddressLine",
				table: "Orders",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);
		}
	}
}
