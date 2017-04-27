using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalApplication.Models.Migrations
{
    public partial class AddTotalVehicleCostColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalVehicleCost",
                table: "Reservations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "isActive",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVehicleCost",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Reservations");
        }
    }
}
