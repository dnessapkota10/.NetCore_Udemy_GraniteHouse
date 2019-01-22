using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class correctedFK_AppointmentId_Typo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedForAppointment_Appointment_AppointmnetId",
                table: "ProductSelectedForAppointment");

            migrationBuilder.DropIndex(
                name: "IX_ProductSelectedForAppointment_AppointmnetId",
                table: "ProductSelectedForAppointment");

            migrationBuilder.DropColumn(
                name: "AppointmnetId",
                table: "ProductSelectedForAppointment");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedForAppointment_AppointmentId",
                table: "ProductSelectedForAppointment",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedForAppointment_Appointment_AppointmentId",
                table: "ProductSelectedForAppointment",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedForAppointment_Appointment_AppointmentId",
                table: "ProductSelectedForAppointment");

            migrationBuilder.DropIndex(
                name: "IX_ProductSelectedForAppointment_AppointmentId",
                table: "ProductSelectedForAppointment");

            migrationBuilder.AddColumn<int>(
                name: "AppointmnetId",
                table: "ProductSelectedForAppointment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedForAppointment_AppointmnetId",
                table: "ProductSelectedForAppointment",
                column: "AppointmnetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedForAppointment_Appointment_AppointmnetId",
                table: "ProductSelectedForAppointment",
                column: "AppointmnetId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
