using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitBase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Customers_CustomerId",
                table: "PromoCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Employees_EmployeeId",
                table: "PromoCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferenceId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Customers_CustomerId",
                table: "PromoCodes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Employees_EmployeeId",
                table: "PromoCodes",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Customers_CustomerId",
                table: "PromoCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Employees_EmployeeId",
                table: "PromoCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferenceId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Customers_CustomerId",
                table: "PromoCodes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Employees_EmployeeId",
                table: "PromoCodes",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
