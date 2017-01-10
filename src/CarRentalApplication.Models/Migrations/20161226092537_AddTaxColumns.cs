using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalApplication.Models.Migrations
{
    public partial class AddTaxColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Taxes",
                table: "Reservations",
                newName: "StateTax");

            migrationBuilder.AddColumn<double>(
                name: "FederalTax",
                table: "Reservations",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FederalTax",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "StateTax",
                table: "Reservations",
                newName: "Taxes");
        }
    }
}
