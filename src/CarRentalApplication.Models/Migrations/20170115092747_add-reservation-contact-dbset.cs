using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalApplication.Models.Migrations
{
    public partial class addreservationcontactdbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationContact_ReservationContactId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationContact",
                table: "ReservationContact");

            migrationBuilder.RenameTable(
                name: "ReservationContact",
                newName: "ReservationContacts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationContacts",
                table: "ReservationContacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationContacts_ReservationContactId",
                table: "Reservations",
                column: "ReservationContactId",
                principalTable: "ReservationContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationContacts_ReservationContactId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationContacts",
                table: "ReservationContacts");

            migrationBuilder.RenameTable(
                name: "ReservationContacts",
                newName: "ReservationContact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationContact",
                table: "ReservationContact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationContact_ReservationContactId",
                table: "Reservations",
                column: "ReservationContactId",
                principalTable: "ReservationContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
