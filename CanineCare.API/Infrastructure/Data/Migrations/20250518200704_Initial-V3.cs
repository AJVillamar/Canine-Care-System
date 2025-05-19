using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pet_pet-extrainfo_PetExtraInfoId",
                table: "pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_person_PersonId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_pet_PetExtraInfoId",
                table: "pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "PetExtraInfoId",
                table: "pet");

            migrationBuilder.RenameTable(
                name: "Professionals",
                newName: "professional");

            migrationBuilder.RenameIndex(
                name: "IX_Professionals_PersonId",
                table: "professional",
                newName: "IX_professional_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_professional",
                table: "professional",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_pet-extrainfo_PetId",
                table: "pet-extrainfo",
                column: "PetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pet-extrainfo_pet_PetId",
                table: "pet-extrainfo",
                column: "PetId",
                principalTable: "pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_professional_person_PersonId",
                table: "professional",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pet-extrainfo_pet_PetId",
                table: "pet-extrainfo");

            migrationBuilder.DropForeignKey(
                name: "FK_professional_person_PersonId",
                table: "professional");

            migrationBuilder.DropIndex(
                name: "IX_pet-extrainfo_PetId",
                table: "pet-extrainfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_professional",
                table: "professional");

            migrationBuilder.RenameTable(
                name: "professional",
                newName: "Professionals");

            migrationBuilder.RenameIndex(
                name: "IX_professional_PersonId",
                table: "Professionals",
                newName: "IX_Professionals_PersonId");

            migrationBuilder.AddColumn<Guid>(
                name: "PetExtraInfoId",
                table: "pet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_pet_PetExtraInfoId",
                table: "pet",
                column: "PetExtraInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_pet_pet-extrainfo_PetExtraInfoId",
                table: "pet",
                column: "PetExtraInfoId",
                principalTable: "pet-extrainfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_person_PersonId",
                table: "Professionals",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
