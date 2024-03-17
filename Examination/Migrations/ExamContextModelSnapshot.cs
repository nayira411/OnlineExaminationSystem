﻿// <auto-generated />
using System;
using Examination.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examination.Migrations
{
    [DbContext(typeof(ExamContext))]
    partial class ExamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Examination.Models.Admin", b =>
                {
                    b.Property<int>("Aid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Aid"));

                    b.Property<string>("Aemail")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Aname")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Apassword")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Aid")
                        .HasName("PK__Admin__C69006288A1BE04D");

                    b.HasIndex(new[] { "Aemail" }, "UQ__Admin__221408751DA40015")
                        .IsUnique()
                        .HasFilter("[Aemail] IS NOT NULL");

                    b.HasIndex(new[] { "Apassword" }, "UQ__Admin__93C2B8ED72C5F4B9")
                        .IsUnique()
                        .HasFilter("[Apassword] IS NOT NULL");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("Examination.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"));

                    b.Property<string>("Answerbody")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("Qid")
                        .HasColumnType("int")
                        .HasColumnName("QId");

                    b.HasKey("AnswerId")
                        .HasName("PK__Answers__D4825004B2E6E4F4");

                    b.HasIndex("Qid");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Examination.Models.Course", b =>
                {
                    b.Property<int>("CrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrId"));

                    b.Property<string>("Cname")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Passgrade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(60);

                    b.HasKey("CrId")
                        .HasName("PK__Courses__AD761DA43839E327");

                    b.HasIndex(new[] { "Cname" }, "UQ__Courses__9F5E029982D921FF")
                        .IsUnique()
                        .HasFilter("[Cname] IS NOT NULL");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Examination.Models.Exam", b =>
                {
                    b.Property<int>("Eid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Eid"));

                    b.Property<int>("CrId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateOnly>("ExamDate")
                        .HasColumnType("date");

                    b.Property<int>("InsId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("Tid")
                        .HasColumnType("int")
                        .HasColumnName("TId");

                    b.HasKey("Eid")
                        .HasName("PK__Exams__C190176B4ADEEAEA");

                    b.HasIndex("CrId");

                    b.HasIndex("InsId");

                    b.HasIndex("Tid");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Examination.Models.Instructor", b =>
                {
                    b.Property<int>("InsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsId"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insemail")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Insgender")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Insname")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Inspassword")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<decimal>("Inssalary")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("InsId")
                        .HasName("PK__Instruct__9D104DEFD1D63C97");

                    b.HasIndex(new[] { "Inspassword" }, "UQ__Instruct__569305E9FC645637")
                        .IsUnique()
                        .HasFilter("[Inspassword] IS NOT NULL");

                    b.HasIndex(new[] { "Insemail" }, "UQ__Instruct__C14D26C13637B689")
                        .IsUnique()
                        .HasFilter("[Insemail] IS NOT NULL");

                    b.ToTable("Instructor", (string)null);
                });

            modelBuilder.Entity("Examination.Models.InstructorCourse", b =>
                {
                    b.Property<int>("InsId")
                        .HasColumnType("int");

                    b.Property<int>("CrId")
                        .HasColumnType("int");

                    b.Property<int?>("Tid")
                        .HasColumnType("int")
                        .HasColumnName("TId");

                    b.HasKey("InsId", "CrId");

                    b.ToTable("Instructor_Courses", (string)null);
                });

            modelBuilder.Entity("Examination.Models.Question", b =>
                {
                    b.Property<int>("Qid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("QId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Qid"));

                    b.Property<int>("CrId")
                        .HasColumnType("int");

                    b.Property<string>("Qbody")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Qmark")
                        .HasColumnType("int");

                    b.Property<string>("Qtype")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Qid")
                        .HasName("PK__Question__CAB1462B7330046F");

                    b.HasIndex("CrId");

                    b.HasIndex(new[] { "Qbody" }, "UQ__Question__ABA745AF30EBB3DE")
                        .IsUnique()
                        .HasFilter("[Qbody] IS NOT NULL");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Examination.Models.Student", b =>
                {
                    b.Property<int>("Sid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sid"));

                    b.Property<string>("Password")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("password");

                    b.Property<string>("Semail")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Sgender")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Sname")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Sid")
                        .HasName("PK__Students__CA195950F51D1EE6");

                    b.HasIndex("TrackId");

                    b.HasIndex(new[] { "Password" }, "UQ__Students__6E2DBEDEC5EC9E2E")
                        .IsUnique()
                        .HasFilter("[password] IS NOT NULL");

                    b.HasIndex(new[] { "Semail" }, "UQ__Students__9E5E9EA8C5FE6B07")
                        .IsUnique()
                        .HasFilter("[Semail] IS NOT NULL");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Examination.Models.StudentAnswer", b =>
                {
                    b.Property<int>("Sid")
                        .HasColumnType("int")
                        .HasColumnName("SId");

                    b.Property<int>("Eid")
                        .HasColumnType("int")
                        .HasColumnName("EId");

                    b.Property<int>("Qid")
                        .HasColumnType("int")
                        .HasColumnName("QId");

                    b.Property<string>("Sanswer")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("SAnswer");

                    b.HasKey("Sid", "Eid", "Qid");

                    b.HasIndex("Eid");

                    b.HasIndex("Qid");

                    b.ToTable("Student_Answers", (string)null);
                });

            modelBuilder.Entity("Examination.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"));

                    b.Property<int>("CrId")
                        .HasColumnType("int");

                    b.Property<string>("TopicDescription")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("TopicName")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("TopicId")
                        .HasName("PK__Topics__022E0F5D71BE2F12");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Examination.Models.Track", b =>
                {
                    b.Property<int>("Tid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Tid"));

                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<string>("Tname")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Tid")
                        .HasName("PK__Track__C456D74914119846");

                    b.HasIndex("SupervisorId");

                    b.HasIndex(new[] { "Tname" }, "UQ__Track__48581FED5097F7BA")
                        .IsUnique()
                        .HasFilter("[Tname] IS NOT NULL");

                    b.ToTable("Track", (string)null);
                });

            modelBuilder.Entity("Examination.Models.TrackCourse", b =>
                {
                    b.Property<int>("Tid")
                        .HasColumnType("int")
                        .HasColumnName("TId");

                    b.Property<int>("Crid")
                        .HasColumnType("int");

                    b.HasKey("Tid", "Crid");

                    b.HasIndex("Crid");

                    b.ToTable("Track_Courses", (string)null);
                });

            modelBuilder.Entity("ExamsQuestion", b =>
                {
                    b.Property<int>("Eid")
                        .HasColumnType("int")
                        .HasColumnName("EId");

                    b.Property<int>("Qid")
                        .HasColumnType("int")
                        .HasColumnName("QId");

                    b.HasKey("Eid", "Qid");

                    b.HasIndex("Qid");

                    b.ToTable("Exams_Questions", (string)null);
                });

            modelBuilder.Entity("StudentCourse", b =>
                {
                    b.Property<int>("Sid")
                        .HasColumnType("int")
                        .HasColumnName("SId");

                    b.Property<int>("CrId")
                        .HasColumnType("int");

                    b.HasKey("Sid", "CrId");

                    b.HasIndex("CrId");

                    b.ToTable("Student_Courses", (string)null);
                });

            modelBuilder.Entity("Examination.Models.Answer", b =>
                {
                    b.HasOne("Examination.Models.Question", "QidNavigation")
                        .WithMany("Answers")
                        .HasForeignKey("Qid")
                        .IsRequired()
                        .HasConstraintName("FK_Answers_Questions");

                    b.Navigation("QidNavigation");
                });

            modelBuilder.Entity("Examination.Models.Exam", b =>
                {
                    b.HasOne("Examination.Models.Course", "Cr")
                        .WithMany("Exams")
                        .HasForeignKey("CrId")
                        .IsRequired()
                        .HasConstraintName("FK_Exams_Courses");

                    b.HasOne("Examination.Models.Instructor", "Ins")
                        .WithMany("Exams")
                        .HasForeignKey("InsId")
                        .IsRequired()
                        .HasConstraintName("FK_Exams_Instructor");

                    b.HasOne("Examination.Models.Track", "TidNavigation")
                        .WithMany("Exams")
                        .HasForeignKey("Tid")
                        .HasConstraintName("FK_Exams_Track");

                    b.Navigation("Cr");

                    b.Navigation("Ins");

                    b.Navigation("TidNavigation");
                });

            modelBuilder.Entity("Examination.Models.Question", b =>
                {
                    b.HasOne("Examination.Models.Course", "Cr")
                        .WithMany("Questions")
                        .HasForeignKey("CrId")
                        .IsRequired()
                        .HasConstraintName("FK_Questions_Courses");

                    b.Navigation("Cr");
                });

            modelBuilder.Entity("Examination.Models.Student", b =>
                {
                    b.HasOne("Examination.Models.Track", "Track")
                        .WithMany("Students")
                        .HasForeignKey("TrackId")
                        .IsRequired()
                        .HasConstraintName("FK_Students_Track");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Examination.Models.StudentAnswer", b =>
                {
                    b.HasOne("Examination.Models.Exam", "EidNavigation")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("Eid")
                        .IsRequired()
                        .HasConstraintName("FK_Student_Answers_Exams");

                    b.HasOne("Examination.Models.Question", "QidNavigation")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("Qid")
                        .IsRequired()
                        .HasConstraintName("FK_Student_Answers_Questions");

                    b.HasOne("Examination.Models.Student", "SidNavigation")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("Sid")
                        .IsRequired()
                        .HasConstraintName("FK_Student_Answers_Students");

                    b.Navigation("EidNavigation");

                    b.Navigation("QidNavigation");

                    b.Navigation("SidNavigation");
                });

            modelBuilder.Entity("Examination.Models.Track", b =>
                {
                    b.HasOne("Examination.Models.Instructor", "Supervisor")
                        .WithMany("Tracks")
                        .HasForeignKey("SupervisorId")
                        .IsRequired()
                        .HasConstraintName("FK_Track_Instructor");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("Examination.Models.TrackCourse", b =>
                {
                    b.HasOne("Examination.Models.Course", "Cr")
                        .WithMany("TrackCourses")
                        .HasForeignKey("Crid")
                        .IsRequired()
                        .HasConstraintName("FK_Track_Courses_Courses");

                    b.Navigation("Cr");
                });

            modelBuilder.Entity("ExamsQuestion", b =>
                {
                    b.HasOne("Examination.Models.Exam", null)
                        .WithMany()
                        .HasForeignKey("Eid")
                        .IsRequired()
                        .HasConstraintName("FK_Exams_Questions_Exams");

                    b.HasOne("Examination.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("Qid")
                        .IsRequired()
                        .HasConstraintName("FK_Exams_Questions_Questions");
                });

            modelBuilder.Entity("StudentCourse", b =>
                {
                    b.HasOne("Examination.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CrId")
                        .IsRequired()
                        .HasConstraintName("FK_Student_Courses_Courses");

                    b.HasOne("Examination.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("Sid")
                        .IsRequired()
                        .HasConstraintName("FK_Student_Courses_Students");
                });

            modelBuilder.Entity("Examination.Models.Course", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Questions");

                    b.Navigation("TrackCourses");
                });

            modelBuilder.Entity("Examination.Models.Exam", b =>
                {
                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Examination.Models.Instructor", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Examination.Models.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Examination.Models.Student", b =>
                {
                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Examination.Models.Track", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
