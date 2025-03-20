using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitBase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Employees_PartnerManagerId",
                table: "PromoCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes");

            migrationBuilder.DropIndex(
                name: "IX_PromoCodes_PartnerManagerId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "PartnerManagerId",
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

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_EmployeeId",
                table: "PromoCodes",
                column: "EmployeeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Employees_EmployeeId",
                table: "PromoCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes");

            migrationBuilder.DropIndex(
                name: "IX_PromoCodes_EmployeeId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PromoCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferenceId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerManagerId",
                table: "PromoCodes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_PartnerManagerId",
                table: "PromoCodes",
                column: "PartnerManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Employees_PartnerManagerId",
                table: "PromoCodes",
                column: "PartnerManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id");
        }
    }
}
