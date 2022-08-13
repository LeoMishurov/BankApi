using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Migrations
{
    public partial class ppp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sum",
                table: "Transaction",
                newName: "receipts");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Transaction",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SumPay",
                table: "Transaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SumPay",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "receipts",
                table: "Transaction",
                newName: "Sum");
        }
    }
}
