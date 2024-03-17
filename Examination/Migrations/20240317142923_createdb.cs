using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination.Migrations
{
    /// <inheritdoc />
    public partial class createdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Aemail = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Apassword = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admin__C69006288A1BE04D", x => x.AId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Passgrade = table.Column<int>(type: "int", nullable: false, defaultValue: 60)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__AD761DA43839E327", x => x.CrId);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Insname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Insemail = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Inspassword = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Insgender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Inssalary = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Instruct__9D104DEFD1D63C97", x => x.InsId);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_Courses",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    CrId = table.Column<int>(type: "int", nullable: false),
                    TId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_Courses", x => new { x.InsId, x.CrId });
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TopicDescription = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Topics__022E0F5D71BE2F12", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qbody = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Qtype = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Qmark = table.Column<int>(type: "int", nullable: false),
                    CrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__CAB1462B7330046F", x => x.QId);
                    table.ForeignKey(
                        name: "FK_Questions_Courses",
                        column: x => x.CrId,
                        principalTable: "Courses",
                        principalColumn: "CrId");
                });

            migrationBuilder.CreateTable(
                name: "Track_Courses",
                columns: table => new
                {
                    TId = table.Column<int>(type: "int", nullable: false),
                    Crid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track_Courses", x => new { x.TId, x.Crid });
                    table.ForeignKey(
                        name: "FK_Track_Courses_Courses",
                        column: x => x.Crid,
                        principalTable: "Courses",
                        principalColumn: "CrId");
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    TId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Track__C456D74914119846", x => x.TId);
                    table.ForeignKey(
                        name: "FK_Track_Instructor",
                        column: x => x.SupervisorId,
                        principalTable: "Instructor",
                        principalColumn: "InsId");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answerbody = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    QId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Answers__D4825004B2E6E4F4", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions",
                        column: x => x.QId,
                        principalTable: "Questions",
                        principalColumn: "QId");
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    EId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CrId = table.Column<int>(type: "int", nullable: false),
                    InsId = table.Column<int>(type: "int", nullable: false),
                    TId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exams__C190176B4ADEEAEA", x => x.EId);
                    table.ForeignKey(
                        name: "FK_Exams_Courses",
                        column: x => x.CrId,
                        principalTable: "Courses",
                        principalColumn: "CrId");
                    table.ForeignKey(
                        name: "FK_Exams_Instructor",
                        column: x => x.InsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId");
                    table.ForeignKey(
                        name: "FK_Exams_Track",
                        column: x => x.TId,
                        principalTable: "Track",
                        principalColumn: "TId");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Semail = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    password = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Sgender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__CA195950F51D1EE6", x => x.SId);
                    table.ForeignKey(
                        name: "FK_Students_Track",
                        column: x => x.TrackId,
                        principalTable: "Track",
                        principalColumn: "TId");
                });

            migrationBuilder.CreateTable(
                name: "Exams_Questions",
                columns: table => new
                {
                    EId = table.Column<int>(type: "int", nullable: false),
                    QId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams_Questions", x => new { x.EId, x.QId });
                    table.ForeignKey(
                        name: "FK_Exams_Questions_Exams",
                        column: x => x.EId,
                        principalTable: "Exams",
                        principalColumn: "EId");
                    table.ForeignKey(
                        name: "FK_Exams_Questions_Questions",
                        column: x => x.QId,
                        principalTable: "Questions",
                        principalColumn: "QId");
                });

            migrationBuilder.CreateTable(
                name: "Student_Answers",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false),
                    EId = table.Column<int>(type: "int", nullable: false),
                    QId = table.Column<int>(type: "int", nullable: false),
                    SAnswer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Answers", x => new { x.SId, x.EId, x.QId });
                    table.ForeignKey(
                        name: "FK_Student_Answers_Exams",
                        column: x => x.EId,
                        principalTable: "Exams",
                        principalColumn: "EId");
                    table.ForeignKey(
                        name: "FK_Student_Answers_Questions",
                        column: x => x.QId,
                        principalTable: "Questions",
                        principalColumn: "QId");
                    table.ForeignKey(
                        name: "FK_Student_Answers_Students",
                        column: x => x.SId,
                        principalTable: "Students",
                        principalColumn: "SId");
                });

            migrationBuilder.CreateTable(
                name: "Student_Courses",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false),
                    CrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Courses", x => new { x.SId, x.CrId });
                    table.ForeignKey(
                        name: "FK_Student_Courses_Courses",
                        column: x => x.CrId,
                        principalTable: "Courses",
                        principalColumn: "CrId");
                    table.ForeignKey(
                        name: "FK_Student_Courses_Students",
                        column: x => x.SId,
                        principalTable: "Students",
                        principalColumn: "SId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Admin__221408751DA40015",
                table: "Admin",
                column: "Aemail",
                unique: true,
                filter: "[Aemail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Admin__93C2B8ED72C5F4B9",
                table: "Admin",
                column: "Apassword",
                unique: true,
                filter: "[Apassword] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QId",
                table: "Answers",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "UQ__Courses__9F5E029982D921FF",
                table: "Courses",
                column: "Cname",
                unique: true,
                filter: "[Cname] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CrId",
                table: "Exams",
                column: "CrId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_InsId",
                table: "Exams",
                column: "InsId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_TId",
                table: "Exams",
                column: "TId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_Questions_QId",
                table: "Exams_Questions",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__569305E9FC645637",
                table: "Instructor",
                column: "Inspassword",
                unique: true,
                filter: "[Inspassword] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__C14D26C13637B689",
                table: "Instructor",
                column: "Insemail",
                unique: true,
                filter: "[Insemail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CrId",
                table: "Questions",
                column: "CrId");

            migrationBuilder.CreateIndex(
                name: "UQ__Question__ABA745AF30EBB3DE",
                table: "Questions",
                column: "Qbody",
                unique: true,
                filter: "[Qbody] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Answers_EId",
                table: "Student_Answers",
                column: "EId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Answers_QId",
                table: "Student_Answers",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Courses_CrId",
                table: "Student_Courses",
                column: "CrId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TrackId",
                table: "Students",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "UQ__Students__6E2DBEDEC5EC9E2E",
                table: "Students",
                column: "password",
                unique: true,
                filter: "[password] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Students__9E5E9EA8C5FE6B07",
                table: "Students",
                column: "Semail",
                unique: true,
                filter: "[Semail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Track_SupervisorId",
                table: "Track",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Track__48581FED5097F7BA",
                table: "Track",
                column: "Tname",
                unique: true,
                filter: "[Tname] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Track_Courses_Crid",
                table: "Track_Courses",
                column: "Crid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Exams_Questions");

            migrationBuilder.DropTable(
                name: "Instructor_Courses");

            migrationBuilder.DropTable(
                name: "Student_Answers");

            migrationBuilder.DropTable(
                name: "Student_Courses");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Track_Courses");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "Instructor");
        }
    }
}
