using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RollsApi.Migrations
{
    /// <inheritdoc />
    public partial class name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banks",
                columns: table => new
                {
                    bank_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bank_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    record_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banks", x => x.bank_id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    record_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    state_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    state_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    state_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    record_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.state_id);
                    table.ForeignKey(
                        name: "FK_states_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    district_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    district_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    district_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    record_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp", nullable: false),
                    state_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.district_id);
                    table.ForeignKey(
                        name: "FK_districts_states_state_id",
                        column: x => x.state_id,
                        principalTable: "states",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "banks",
                columns: new[] { "bank_id", "bank_name", "record_status" },
                values: new object[,]
                {
                    { 1L, "STATE BANK OF INDIA", "ACTIVE" },
                    { 2L, "ICICI", "ACTIVE" },
                    { 3L, "HDFC BANK", "ACTIVE" },
                    { 4L, "AXIS BANK", "ACTIVE" },
                    { 5L, "UNION BANK OF INDIA", "ACTIVE" }
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "country_id", "country_code", "country_name", "record_status" },
                values: new object[] { 1L, "IND", "INDIA", "ACTIVE" });

            migrationBuilder.InsertData(
                table: "states",
                columns: new[] { "state_id", "country_id", "record_status", "state_code", "state_name" },
                values: new object[] { 1L, 1L, "ACTIVE", "KRL", "KERALA" });

            migrationBuilder.CreateIndex(
                name: "IX_banks_bank_name",
                table: "banks",
                column: "bank_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_country_code",
                table: "countries",
                column: "country_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_country_name",
                table: "countries",
                column: "country_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_districts_district_code",
                table: "districts",
                column: "district_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_districts_district_name",
                table: "districts",
                column: "district_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_districts_state_id",
                table: "districts",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_states_country_id",
                table: "states",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_states_state_code",
                table: "states",
                column: "state_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_states_state_name",
                table: "states",
                column: "state_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banks");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "states");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
