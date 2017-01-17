using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRentalApplication.Models.Migrations
{
    public partial class Turn_Off_Identity_On_ReservationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Reservations",
            //    table: "Reservations");

            //migrationBuilder.DropColumn(
            //    name: "ConfirmationNumber",
            //    table: "Reservations");

            //migrationBuilder.AddColumn<long>(
            //    name: "ConfirmationNumber",
            //    table: "Reservations",
            //    nullable: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Reservations",
            //    table: "Reservations",
            //    column: "ConfirmationNumber");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ConfirmationNumber = table.Column<long>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    PickupLocation = table.Column<string>(nullable: true),
                    ReservationContactId = table.Column<Guid>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    ReturnLocation = table.Column<string>(nullable: true),
                    Taxes = table.Column<double>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ConfirmationNumber);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationContacts_ReservationContactId",
                        column: x => x.ReservationContactId,
                        principalTable: "ReservationContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Reservations",
            //    table: "Reservations");

            //migrationBuilder.DropColumn(
            //    name: "ConfirmationNumber",
            //    table: "Reservations");

            //migrationBuilder.AddColumn<long>(
            //    name: "ConfirmationNumber",
            //    table: "Reservations",
            //    nullable: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Reservations",
            //    table: "Reservations",
            //    column: "ConfirmationNumber");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ConfirmationNumber = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    PickupLocation = table.Column<string>(nullable: true),
                    ReservationContactId = table.Column<Guid>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    ReturnLocation = table.Column<string>(nullable: true),
                    Taxes = table.Column<double>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ConfirmationNumber);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationContact_ReservationContactId",
                        column: x => x.ReservationContactId,
                        principalTable: "ReservationContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
